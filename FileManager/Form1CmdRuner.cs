using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager
{
    public partial class MainForm
    {
        private string lastSelectedFile = null;
        private string lastSelectedPath = null;

        private void BindCmdRuner()
        {
            this.dataGridViewOne.UpdateSelectedElement += UpdateSelectedElement;
            this.dataGridViewTwo.UpdateSelectedElement += UpdateSelectedElement;
        }

        private void UpdateSelectedElement(string file, string path)
        {
            this.lastSelectedPath = path;
            this.labelCmdRun.Text = path;

            if (!file.Equals(".."))
            {
                this.lastSelectedFile = file;
                this.textBoxCmdRun.Text = file;
            }
        }

        private void RunCmdFromTextBoxCmdRun()
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.FileName = "CMD.exe";
            startInfo.Arguments = "/k " + this.textBoxCmdRun.Text + " & exit";
            startInfo.WorkingDirectory = this.lastSelectedPath;
            process.StartInfo = startInfo;
            process.Start();
        }

    }
}
