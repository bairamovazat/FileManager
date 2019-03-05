using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FileManager
{
    public partial class FilesDataGridView : CustomDataGridView
    {
        private List<DriveInfo> driveList = new List<DriveInfo>();
        private DriveInfo currentDrive = null;
        private List<string> currentDirectoryList = new List<string>();
        private DirectoryInfo directoryInfo = null;
        public event Action<List<DriveInfo>> UpdateDrivesHandler;
        public event Action<FilesDataGridView> UpdateView;
        public event Action<Exception> GlobalErrorHandler;
        public event Action<string, string> UpdateSelectedElement;

        private ISettingCache cache = new SettingCacheFakeImpl();
        private FileSystemWatcher fileWatcher = new FileSystemWatcher();

        public ISettingCache Cache { get => cache; set => cache = value; }
        public DriveInfo CurrentDrive { get => currentDrive; set => currentDrive = value; }

        private delegate void UpdateDirectory();
        private bool firstUpdateDrivers = true;

        public FilesDataGridView()
        {
            Console.WriteLine(Properties.Settings.Default);
            this.MouseDoubleClick += CustomSelectElement;

            this.UpdateDrivesHandler += (drives) =>
            {
                List<DriveInfo> filteredDriveInfo = new List<DriveInfo>();
                drives.ForEach(drive =>
                {
                    if (drive.IsReady) {
                        filteredDriveInfo.Add(drive);
                    }
                });
                this.driveList = filteredDriveInfo;
                if (firstUpdateDrivers)
                {
                    UpdateDriveAndDirectoryListFromCache();
                    /**TODO Пофиксить
                    * Тут большой костыль из-за того, что после загрузкт Drive и Path из кеша 
                    * происходит обновление комбобокса Form1.Bind.UpdateDrivesHandler на указанный в this.currentDrive
                    * на это событие срабатывает Form1.Bind.SelectedIndexChanged и устанвливает новый драйвер,
                    * который удаляет текущий Path = new List(), если не высталять условие driveInfo != currentDrive.
                    * Но при этом условии при Form1.Bind.UpdateDrivesHandler comboBox.SelectedIndex = i
                    * не происходит обновление UpdateDirectoryView, как это предполагалось изначально
                    **/
                    UpdateDirectoryView();
                    firstUpdateDrivers = false;
                }
            };

            UpdateDirectory updateDirectory = UpdateDirectoryView;

            fileWatcher.Changed += (sender, e) => this.Invoke(updateDirectory);
            fileWatcher.Created += (sender, e) => this.Invoke(updateDirectory);
            fileWatcher.Deleted += (sender, e) => this.Invoke(updateDirectory);
            fileWatcher.Renamed += (sender, e) => this.Invoke(updateDirectory);

            this.SelectionChanged += (sender, e) =>
            {
                UpdateSelectedElement?.Invoke(this.CurrentRow.Cells[0].Value.ToString(), this.GetCurrentPath());
            };
        }

        public void UpdateDriveAndDirectoryListFromCache()
        {
            string previousDriveName = Cache.LastDriverName();
            DriveInfo previousDrive = this.driveList.Find(d => d.Name.Equals(previousDriveName));
            if (previousDrive == null)
            {
                currentDrive = driveList[0];
                return;
            }
            else
            {
                currentDrive = previousDrive;
            }

            List<string> directoryList = Cache.LastPath();
            string previousDirectory = previousDriveName + String.Join("", directoryList);
            DirectoryInfo directoryInfo = new DirectoryInfo(previousDirectory);

            if (directoryInfo.Exists)
            {
                currentDirectoryList = directoryList;
            }
        }

        public void UpdateWatcher()
        {
            fileWatcher.Path = GetCurrentPath();
            fileWatcher.EnableRaisingEvents = true;
        }

        public void UpdateDrives()
        {
            List<DriveInfo> drives = new List<DriveInfo>();
            DriveInfo.GetDrives().ToList().ForEach(e =>
            {
                drives.Add(e);
            });
            this.UpdateDrivesHandler(drives);
        }

        //Тут может быть NullPointer
        public void SelectDriveByName(string name)
        {
            DriveInfo driveInfo = this.driveList.Find(e => e.Name.Equals(name));
            if (driveInfo == null)
            {
                throw new ArgumentNullException("Не найден drive");
            }
            else if (currentDrive != null && driveInfo == currentDrive)
            {
                return;
            }

            SetCurrentDrive(driveInfo);
        }

        public override List<string> CurrentSelectedFiles()
        {
            string currentPath = GetCurrentPath();
            List<string> selectedFiles = new List<string>();
            foreach (DataGridViewRow row in this.SelectedRows)
            {
                selectedFiles.Add(currentPath + row.Cells[0].Value.ToString());
            }
            return selectedFiles;
        }

        public override void DragDropFiles(List<string> files)
        {
            ProcessException(() =>
            {
                foreach (string filePath in files)
                {
                    FileAttributes attr = File.GetAttributes(filePath);

                    if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
                    {
                        DirectoryInfo directoryInfo = new DirectoryInfo(filePath);
                        MoveDirectory(filePath, GetCurrentPath() + directoryInfo.Name);
                    }
                    else
                    {
                        FileInfo fileInfo = new FileInfo(filePath);
                        MoveFile(filePath, GetCurrentPath() + fileInfo.Name);
                    }
                }
            });

        }

        private void UpdateDirectoryView()
        {
            if (directoryInfo == null || !directoryInfo.FullName.Equals(GetCurrentPath()))
            {
                directoryInfo = new DirectoryInfo(GetCurrentPath());
                UpdateWatcher();
                UpdateCache();
            }

            Console.WriteLine(directoryInfo.FullName);

            List<List<string>> data = new List<List<string>>();
            List<string> header = new List<string>();
            header.Add("Название");
            header.Add("Тип");
            header.Add("Размер");
            data.Add(header);

            if (currentDirectoryList.Count != 0)
            {
                List<string> goBackRow = new List<string>();
                goBackRow.Add("..");
                goBackRow.Add("Папка");
                goBackRow.Add("");
                data.Add(goBackRow);
            }

            directoryInfo.GetDirectories().ToList().ForEach(directory =>
            {
                if (!directory.Attributes.HasFlag(FileAttributes.ReparsePoint)
                && !directory.Attributes.HasFlag(FileAttributes.System))
                {
                    List<string> currentRow = new List<string>();
                    currentRow.Add(directory.Name);
                    currentRow.Add("Папка");
                    currentRow.Add("-");
                    data.Add(currentRow);
                }
            });

            directoryInfo.GetFiles().ToList().ForEach(file =>
            {
                if (!file.Attributes.HasFlag(FileAttributes.ReparsePoint)
                && !file.Attributes.HasFlag(FileAttributes.System))
                {
                    List<string> currentRow = new List<string>();
                    currentRow.Add(file.Name);
                    currentRow.Add("Файл");
                    currentRow.Add(GetFileSize(file));
                    data.Add(currentRow);
                }
            });

            this.SetData(data);
            this.UpdateView?.Invoke(this);
        }

        private void SetCurrentDrive(DriveInfo driveInfo)
        {
            this.currentDrive = driveInfo;
            this.currentDirectoryList.Clear();
            this.UpdateDirectoryView();
        }

        private void UpdateCache()
        {
            this.Cache.SaveLastDriverName(this.currentDrive.Name);
            this.Cache.SaveLastPath(this.currentDirectoryList);
        }

        //----START Action control-----//
        protected override bool ProcessDialogKey(Keys keyData)
        {
            Keys key = (keyData & Keys.KeyCode);

            if (key == Keys.Enter)
            {
                if (this.SelectedRows.Count == 1)
                {
                    this.GoToDirectoryOrRunProgram(this.SelectedRows[0]);
                }
                return true;
            }
            return base.ProcessDialogKey(keyData);
        }

        protected override bool ProcessDataGridViewKey(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (this.SelectedRows.Count == 1)
                {
                    this.GoToDirectoryOrRunProgram(this.SelectedRows[0]);
                }
                return true;
            }
            return base.ProcessDataGridViewKey(e);
        }

        private void CustomSelectElement(object sender, MouseEventArgs e)
        {
            int index = this.HitTest(e.X, e.Y).RowIndex;
            if (index != -1)
            {
                DataGridViewRow row = this.Rows[index];
                this.GoToDirectoryOrRunProgram(row);
            }

        }
        //----END Action control-----//

        //----START Navigation-----//
        public void GoBack()
        {
            if (currentDirectoryList.Count > 0)
            {
                currentDirectoryList.RemoveAt(currentDirectoryList.Count - 1);
                this.UpdateDirectoryView();
            }
        }

        public void GoToDirectoryOrRunProgram(DataGridViewRow row)
        {
            string name = row.Cells[0].Value.ToString();
            string type = row.Cells[1].Value.ToString();
            ProcessException(() =>
            {
                if (type.ToLower().Equals("папка"))
                {
                    GoToDirectory(name);
                }
                else if (type.ToLower().Equals("файл"))
                {
                    RunProgram(GetCurrentPath(), name);
                }
                else
                {
                    throw new ArgumentNullException("Тип файла не опознан");
                }
            });
        }

        public void GoToDirectory(string directory)
        {
            if (directory.Equals(".."))
            {
                GoBack();
            }
            else
            {
                currentDirectoryList.Add(directory + @"\");
                this.UpdateDirectoryView();
            }
        }

        public string GetCurrentPath()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(currentDrive.Name);
            currentDirectoryList.ForEach(dir =>
            {
                stringBuilder.Append(dir);
            });

            return stringBuilder.ToString();
        }
        //----END Navigation-----//

        //----START File/Directory work-----//
        public void DeleteRow(DataGridViewRow row)
        {
            string name = row.Cells[0].Value.ToString();
            string type = row.Cells[1].Value.ToString();
            ProcessException(() =>
            {
                if (type.ToLower().Equals("папка"))
                    DeleteFolder(this.GetCurrentPath() + name);

                else if (type.ToLower().Equals("файл"))
                    DeleteFile(this.GetCurrentPath() + name);

                else
                    throw new ArgumentNullException("Тип файла не опознан");
            });
        }

        public void CreateSubDirectory(string name)
        {
            ProcessException(() => directoryInfo.CreateSubdirectory(name));
        }

        public void MoveRow(DataGridViewRow row, string destinationDir)
        {
            string name = row.Cells[0].Value.ToString();
            string type = row.Cells[1].Value.ToString();
            ProcessException(() =>
            {
                if (type.ToLower().Equals("папка"))
                {
                    MoveDirectory(this.GetCurrentPath() + name, destinationDir + @"\" + name);
                }
                else if (type.ToLower().Equals("файл"))
                {
                    MoveFile(this.GetCurrentPath() + name, destinationDir + @"\" + name);
                }
                else
                {
                    throw new ArgumentNullException("Тип файла не опознан");
                }
            });
        }

        public void CopyRow(DataGridViewRow row, string destinationDir)
        {
            string name = row.Cells[0].Value.ToString();
            string type = row.Cells[1].Value.ToString();
            ProcessException(() =>
            {
                if (type.ToLower().Equals("папка"))
                {
                    CopyDirectory(this.GetCurrentPath() + name, destinationDir + @"\" + name);
                }
                else if (type.ToLower().Equals("файл"))
                {
                    CopyFile(this.GetCurrentPath() + name, destinationDir + @"\" + name);
                }
                else
                {
                    throw new ArgumentNullException("Тип файла не опознан");
                }
            });
        }

        public void RanameRow(DataGridViewRow row, string newName)
        {
            string name = row.Cells[0].Value.ToString();
            string type = row.Cells[1].Value.ToString();
            ProcessException(() =>
            {
                if (type.ToLower().Equals("папка"))
                {
                    RenameDirectory(this.GetCurrentPath() + name, this.GetCurrentPath() + newName);
                }
                else if (type.ToLower().Equals("файл"))
                {
                    RenameFile(this.GetCurrentPath() + name, this.GetCurrentPath() + newName);
                }
                else
                {
                    throw new ArgumentNullException("Тип файла не опознан");
                }
            });
        }


        //----END File/Directory work-----//

        private void ProcessException(Action action)
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        private void ProcessException(Exception ex)
        {
            GlobalErrorHandler?.Invoke(ex);
        }
    }
}
