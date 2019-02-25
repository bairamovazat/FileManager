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
    public class FilesDataGridView : CustomDataGridView
    {
        private List<DriveInfo> driveList = new List<DriveInfo>();
        private DriveInfo currentDrive = null;
        private List<string> currentDirectoryList = new List<string>();

        private DirectoryInfo directoryInfo = null;
        public event Action<List<DriveInfo>> UpdateDrivesHandler;
        private FileSystemWatcher fileWatcher = new FileSystemWatcher();
        private delegate void UpdateDirectory();

        //Если не выбирать drive, то список файлов всё равно обновляется
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


        public void GoBack()
        {
            if (currentDirectoryList.Count > 0)
            {
                currentDirectoryList.RemoveAt(currentDirectoryList.Count - 1);
                this.UpdateDirectoryView();
            }
        }

        public void GoToDirectoryOrRunProgram(DataGridViewRow row) {
            string name = row.Cells[0].Value.ToString();
            string type = row.Cells[1].Value.ToString();

            if (type.ToLower().Equals("папка"))
            {
                GoToDirectory(name);
            }
            else if (type.ToLower().Equals("файл"))
            {
                RunProgram(name);
            }
            else
            {
                throw new ArgumentNullException("Тип файла не опознан");
            }
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
                stringBuilder.Append(dir + @"\");
            });

            return stringBuilder.ToString();
        }

        public void RunProgram(string programName) {
            ProcessStartInfo infoStartProcess = new ProcessStartInfo();

            infoStartProcess.WorkingDirectory = GetCurrentPath();
            infoStartProcess.FileName = GetCurrentPath() + programName;

            Process.Start(infoStartProcess);

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

        //----START delete-----//
        public void DeleteRow(DataGridViewRow row)
        {
            string name = row.Cells[0].Value.ToString();
            string type = row.Cells[1].Value.ToString();
            if (type.ToLower().Equals("папка"))
            {
                DeleteFolder(this.GetCurrentPath() + name);

            }
            else if (type.ToLower().Equals("файл"))
            {
                DeleteFile(this.GetCurrentPath() + name);
            }
            else
            {
                throw new ArgumentNullException("Тип файла не опознан");
            }
        }

        private static void DeleteFolder(string path)
        {
            Directory.Delete(path, true);
        }

        private static void DeleteFile(string path)
        {
            FileInfo file = new FileInfo(path);
            if (file.Exists)
            {
                file.Delete();
            }
            else
            {
                throw new NullReferenceException("Файл не найден");
            }
        }
        //----END delete-----//

        //----START new folder-----//
        public void CreateSubDirectory(string name)
        {
            directoryInfo.CreateSubdirectory(name);
        }
        //----END new folder-----//

        //----START move-----//
        public void MoveRow(DataGridViewRow row, string destinationDir)
        {
            string name = row.Cells[0].Value.ToString();
            string type = row.Cells[1].Value.ToString();

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
        }

        private static void MoveDirectory(string sourceDirName, string destDirName)
        {
            Directory.Move(sourceDirName, destDirName);
        }

        private static void MoveFile(string sourceFile, string destinationFile)
        {
            File.Move(sourceFile, destinationFile);
        }
        //----END move-----//

        //----START move-----//
        public void CopyRow(DataGridViewRow row, string destinationDir)
        {
            string name = row.Cells[0].Value.ToString();
            string type = row.Cells[1].Value.ToString();

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
        }

        private static void CopyDirectory(string sourceDirName, string destDirName)
        {
            CopyFolder(sourceDirName, destDirName);
        }

        private static void CopyFolder(string sourceFolder, string destFolder)
        {
            if (!Directory.Exists(destFolder))
                Directory.CreateDirectory(destFolder);
            string[] files = Directory.GetFiles(sourceFolder);
            foreach (string file in files)
            {
                string name = Path.GetFileName(file);
                string dest = Path.Combine(destFolder, name);
                File.Copy(file, dest);
            }
            string[] folders = Directory.GetDirectories(sourceFolder);
            foreach (string folder in folders)
            {
                string name = Path.GetFileName(folder);
                string dest = Path.Combine(destFolder, name);
                CopyFolder(folder, dest);
            }
        }

        private static void CopyFile(string sourceFile, string destinationFile)
        {
            File.Copy(sourceFile, destinationFile);
        }
        //----END move-----//

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
                goBackRow.Add("");
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
        }

        private void SetCurrentDrive(DriveInfo driveInfo)
        {
            this.currentDrive = driveInfo;
            this.currentDirectoryList.Clear();
            this.UpdateDirectoryView();
        }

        private static string GetFileSize(FileInfo file)
        {
            return (file.Length / 1024).ToString() + " KB";
        }
    }
}
