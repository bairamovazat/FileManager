using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileManager
{
    class CustomDataGridView : DataGridView
    {
        private bool enableDragAndDrop = false;
        private string testFilePath = @"C:/Users/Azat/Documents/test.txt";

        public CustomDataGridView()
        {
            this.MouseMove += CustomMouseMove;
            this.MouseUp += CustomMouseUp;
            this.MouseUp += CustomSelectElement;
            this.MouseLeave += CustomMouseLeave;
            this.ClearSelection();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            //base.OnMouseDown(e);
        }

        private void CustomMouseMove(object sender, MouseEventArgs e)
        {

            if (!enableDragAndDrop && (e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                Console.WriteLine("Enable drag and drop");
                enableDragAndDrop = true;
                string[] files = new String[1];
                files[0] = testFilePath;
                DataObject data = new DataObject(DataFormats.FileDrop, files);
                data.SetData(DataFormats.StringFormat, files[0]);
                DoDragDrop(data, DragDropEffects.Copy);
            }

        }

        private void CustomSelectElement(object sender, MouseEventArgs e)
        {
            int index = this.HitTest(e.X, e.Y).RowIndex;
            if (index != -1)
            {
                if (Control.ModifierKeys != Keys.Control)
                {
                    this.ClearSelection();
                }
                DataGridViewRow row = this.Rows[index];
                row.Selected = !row.Selected;
            }
        }

        private void CustomMouseUp(object sender, MouseEventArgs e)
        {
            enableDragAndDrop = false;
        }

        private void CustomMouseLeave(object sender, EventArgs e)
        {
            enableDragAndDrop = false;
        }

        public void SetData(string[,] data)
        {
            this.DataSource = GetDataTable(data);
        }

        public void SetData(List<List<string>> data)
        {
            this.DataSource = GetDataTable(data);
        }

        public static DataTable GetDataTable(string[,] data)
        {
            if (data.GetLength(0) == 0)
            {
                throw new ArgumentException("data is empty");
            }
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

        public static DataTable GetDataTable(List<List<string>> data)
        {
            if (data.Count == 0)
            {
                throw new ArgumentException("data is empty");
            }
            DataTable table = new DataTable();

            data.First().ForEach(element => table.Columns.Add());

            for (int i = 0; i < data.Count; i++)
            {
                table.Rows.Add(table.NewRow());
                for (int j = 0; j < data.First().Count; j++)
                {
                    table.Rows[i][j] = data[i][j];
                }
            }
            return table;
        }

    }
}
