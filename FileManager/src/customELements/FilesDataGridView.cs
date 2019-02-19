using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileManager
{
    class FilesDataGridView : CustomDataGridView
    {
        private List<string> drivers = new List<string>();
        private string currentDriver = null;
        private string currentDirectory = null;
        private DirectoryInfo directoryInfo;
        private event Action<List<string>> updateDriversHandler;

        public FilesDataGridView()
        {
            this.MouseDoubleClick += CustomSelectElement;
            this.updateDriversHandler += (drivers) => this.drivers = drivers;
            this.UpdateDrivers();
            this.currentDriver = this.drivers[0];
            this.currentDirectory = this.currentDriver;
            this.directoryInfo = new DirectoryInfo(currentDriver);
            this.UpdateDirectoryView();
        }

        public void UpdateDrivers()
        {
            List<string> drivers = new List<string>(Environment.GetLogicalDrives());
            this.updateDriversHandler(drivers);
        }

        private void CustomSelectElement(object sender, MouseEventArgs e)
        {
            int index = this.HitTest(e.X, e.Y).RowIndex;
            if (index != -1)
            {
                DataGridViewRow row = this.Rows[index];
                currentDirectory += row.Cells[0].Value + @"\";
                this.UpdateDirectoryView();
            }
        }

        public void UpdateDirectoryView()
        {

            if (directoryInfo == null || !directoryInfo.FullName.Equals(currentDirectory))
            {
                directoryInfo = new DirectoryInfo(currentDirectory);
            }
            Console.WriteLine(directoryInfo.FullName);

            List<List<string>> data = new List<List<string>>();
            if (currentDirectory != currentDriver)
            {
                List<string> goBackRow = new List<string>();
                goBackRow.Add("..");
            }

            directoryInfo.GetDirectories().ToList().ForEach(file =>
            {
                List<string> currentRow = new List<string>();
                currentRow.Add(file.Name);
                data.Add(currentRow);
            });

            directoryInfo.GetDirectories().ToList().ForEach(directory =>
            {
                List<string> currentRow = new List<string>();
                currentRow.Add(directory.Name);
                data.Add(currentRow);
            });

            this.SetData(data);
        }
    }
}
