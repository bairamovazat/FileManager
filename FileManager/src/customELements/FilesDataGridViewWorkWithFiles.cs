using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileManager
{
    public partial class FilesDataGridView : CustomDataGridView
    {

        public static void RunProgram(string path, string programName)
        {
            ProcessStartInfo infoStartProcess = new ProcessStartInfo();

            infoStartProcess.WorkingDirectory = path;
            infoStartProcess.FileName = path + programName;

            Process.Start(infoStartProcess);
        }

        //----START delete-----//
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

        //----START move-----//
        private static void MoveDirectory(string sourceDirName, string destDirName)
        {
            if (destDirName.Contains(sourceDirName))
            {
                throw new ArgumentException("Конечная папка, в которую следует поместить файлы, является дочерней для папки, в которой она находится.");
            }
            Directory.Move(sourceDirName, destDirName);
        }

        private static void MoveFile(string sourceFile, string destinationFile)
        {
            File.Move(sourceFile, destinationFile);
        }
        //----END move-----//

        //----START copy-----//
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
        //----END copy-----//

        private static string GetFileSize(FileInfo file)
        {
            return (file.Length / 1024).ToString() + " KB";
        }
    }
}
