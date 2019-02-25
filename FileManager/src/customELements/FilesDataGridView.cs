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

        private FileSystemWatcher fileWatcher = new FileSystemWatcher();
        private delegate void UpdateDirectory();

        public FilesDataGridView()
        {
            this.MouseDoubleClick += CustomSelectElement;
            this.UpdateDrivesHandler += (drives) => this.driveList = drives;
            UpdateDirectory updateDirectory = UpdateDirectoryView;
            fileWatcher.Changed += (sender, e) => this.Invoke(updateDirectory);
            fileWatcher.Created += (sender, e) => this.Invoke(updateDirectory);
            fileWatcher.Deleted += (sender, e) => this.Invoke(updateDirectory);
            fileWatcher.Renamed += (sender, e) => this.Invoke(updateDirectory);
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
            }
            Console.WriteLine(directoryInfo.FullName);

            List<List<string>> data = new List<List<string>>();
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
                    MoveDirectory(this.GetCurrentPath() + name, destinationDir + @"/" + name);
                }
                else if (type.ToLower().Equals("файл"))
                {
                    MoveFile(this.GetCurrentPath() + name, destinationDir + @"/" + name);
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
                    CopyDirectory(this.GetCurrentPath() + name, destinationDir + @"/" + name);
                }
                else if (type.ToLower().Equals("файл"))
                {
                    CopyFile(this.GetCurrentPath() + name, destinationDir + @"/" + name);
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
