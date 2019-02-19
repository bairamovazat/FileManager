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
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            string[,] data = {
                { "00", "01", "02", "03" },
                { "10", "11", "12", "13" },
                { "20", "21", "22", "23" },
                { "30", "31", "32", "33" }

            };
            dataGridViewOne.DataSource = GetDataTable(data);

            //this.dataGridViewOne.CellValueNeeded += (sender, e) => { Console.WriteLine("CellValueNeeded"); Console.WriteLine(sender.ToString() + e.ToString()); };
            //this.dataGridViewOne.CellValuePushed += (sender, e) => { Console.WriteLine("CellValuePushed"); Console.WriteLine(sender.ToString() + e.ToString()); };
            //this.dataGridViewOne.NewRowNeeded += (sender, e) => { Console.WriteLine("NewRowNeeded"); Console.WriteLine(sender.ToString() + e.ToString()); };
            //this.dataGridViewOne.RowValidated += (sender, e) => { Console.WriteLine("RowValidated"); Console.WriteLine(sender.ToString() + e.ToString()); };
            //this.dataGridViewOne.RowDirtyStateNeeded += (sender, e) => { Console.WriteLine("RowDirtyStateNeeded"); Console.WriteLine(sender.ToString() + e.ToString()); };
            //this.dataGridViewOne.CancelRowEdit += (sender, e) => { Console.WriteLine("CancelRowEdit"); Console.WriteLine(sender.ToString() + e.ToString()); };
            //this.dataGridViewOne.UserDeletingRow += (sender, e) => { Console.WriteLine("UserDeletingRow"); Console.WriteLine(sender.ToString() + e.ToString()); };

            dataGridViewTwo.DataSource = GetDataTable(data);

        }


        private DataTable GetDataTable(string[,] data)
        {
            DataTable table = new DataTable();

            for (int i = 0; i < data.GetLength(1); i++)
            {
                table.Columns.Add();
            }
            for (int i = 0; i < data.GetLength(0); i++)
            {
                table.Rows.Add(table.NewRow());
                for (int j = 0; j < data.GetLength(1); j++)
                {
                    table.Rows[i][j] = data[i, j];
                }
            }

            return table;
        }
        private void panelOneData_Paint(object sender, PaintEventArgs e)
        {

        }
        private bool enableDragAndDrop = false;
        private string testFilePath = @"C:/Users/Azat/Documents/test.txt";

        private void dataGridViewOne_MouseMove(object sender, MouseEventArgs e)
        {

            if (enableDragAndDrop)
            {
                string[] files = new String[1];

                files[0] = testFilePath;

                //create a dataobject holding this array as a filedrop

                DataObject data = new DataObject(DataFormats.FileDrop, files);

                //also add the selection as textdata

                data.SetData(DataFormats.StringFormat, files[0]);

                //do the dragdrop
                DoDragDrop(data, DragDropEffects.Copy);
            }

        }

        private void dataGridViewOne_MouseDown(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                enableDragAndDrop = true;
            }
        }

        private void dataGridViewOne_MouseUp(object sender, MouseEventArgs e)
        {
            enableDragAndDrop = false;
        }
    }
}
