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
    public partial class InputQuestion : Form
    {
        private Action<bool, string> resultFunction;

        public InputQuestion(string text, string inputValue, Action<bool, string> resultFunction)
        {
            this.resultFunction = resultFunction;
            InitializeComponent();
            if (inputValue != null) {
                textBox.Text = inputValue;
            }
            this.labelText.Text = text;
        }

        private void buttonYes_Click(object sender, EventArgs e)
        {
            resultFunction(true, textBox.Text);
            Close();
        }

        private void buttonNo_Click(object sender, EventArgs e)
        {
            resultFunction(false, textBox.Text);
            Close();
        }

    }
}
