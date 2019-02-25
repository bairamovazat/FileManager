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
    public partial class Questions : Form
    {
        private Action<bool> resultFunction;

        public Questions(string text, Action<bool> resultFunction)
        {
            this.resultFunction = resultFunction;
            InitializeComponent();
            this.labelText.Text = text;

        }

        private void buttonYes_Click(object sender, EventArgs e)
        {
            resultFunction(true);
            Close();
        }

        private void buttonNo_Click(object sender, EventArgs e)
        {
            resultFunction(false);
            Close();
        }
    }
}
