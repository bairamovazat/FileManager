using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileManager
{
    public partial class Editor : Form
    {
        private Action<bool, string> resultFunction;

        public Editor(string fileName, string text, Action<bool, string> resultFunction)
        {
            this.resultFunction = resultFunction;
            InitializeComponent();
            this.labelFIleName.Text = fileName;
            this.textBoxEditor.Text = text;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            resultFunction(true, textBoxEditor.Text);
            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            resultFunction(false, textBoxEditor.Text);
            Close();
        }
    }
}
