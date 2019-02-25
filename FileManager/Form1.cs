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
        private FilesDataGridView lastFocusedFileDataGridView = null;

        public MainForm()
        {
            InitializeComponent();
            dataGridViewOne.GotFocus += (sender, e) => lastFocusedFileDataGridView = dataGridViewOne;
            dataGridViewTwo.GotFocus += (sender, e) => lastFocusedFileDataGridView = dataGridViewTwo;

            dataGridViewOne.UpdateView += (dataGrid) => labelOne.Text = dataGrid.GetCurrentPath();
            dataGridViewTwo.UpdateView += (dataGrid) => labelTwo.Text = dataGrid.GetCurrentPath();

            dataGridViewOne.GlobalErrorHandler += (ex) => MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            dataGridViewTwo.GlobalErrorHandler += (ex) => MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);

            Bind(dataGridViewOne, comboBoxOne);
            Bind(dataGridViewTwo, comboBoxTwo);

            dataGridViewOne.UpdateDrives();
            dataGridViewTwo.UpdateDrives();


            dataGridViewOne.Focus();

        }

        public void Bind(FilesDataGridView filesDataGridView, ComboBox comboBox)
        {
            filesDataGridView.UpdateDrivesHandler += (driverInfoList) =>
            {
                comboBox.Items.Clear();
                driverInfoList.ForEach(info => comboBox.Items.Add(info.Name));
                if (comboBox.SelectedIndex == -1)
                {
                    comboBox.SelectedIndex = 0;
                };
            };

            comboBox.SelectedIndexChanged += (sender, e) =>
            {
                string drive = (string)comboBox.SelectedItem;
                filesDataGridView.SelectDriveByName(drive);
            };
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            Keys key = (keyData & Keys.KeyCode);

            if (key == Keys.Tab)
            {
                GetFocusedDataGridView().Focus();
                return true;
            }
            return base.ProcessDialogKey(keyData);
        }

        public FilesDataGridView GetFocusedDataGridView()
        {
            if (dataGridViewOne.Focused)
            {
                return dataGridViewOne;
            }
            else
            {
                return dataGridViewTwo;
            }
        }

        private void buttonDelet_Click(object sender, EventArgs e)
        {
            FilesDataGridView focused = lastFocusedFileDataGridView;

            new Questions("Вы действительно хотите удалить все выделенные элементы ?",
                (answer) =>
                {
                    if (answer)
                    {
                        foreach (DataGridViewRow row in focused.SelectedRows)
                        {
                            focused.DeleteRow(row);
                        }
                        focused.Focus();
                    }
                })
            {
                Owner = this,
                StartPosition = FormStartPosition.CenterParent
            }.ShowDialog();
        }

        private void buttonNewFolder_Click(object sender, EventArgs e)
        {
            FilesDataGridView focused = lastFocusedFileDataGridView;
            new InputQuestion("Введите название нового каталога:",
               (answer, newDirName) =>
               {
                   if (answer)
                   {

                       focused.CreateSubDirectory(newDirName);
                       focused.Focus();
                   }
               })
            {
                Owner = this,
                StartPosition = FormStartPosition.CenterParent
            }.ShowDialog();
        }

        private void buttonMove_Click(object sender, EventArgs e)
        {
            FilesDataGridView focused = lastFocusedFileDataGridView;
            if (focused.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите хотябы 1 элемент!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            new PathQuestions("Выберите каталог для копирования:",
               (answer, newDirName) =>
               {
                   if (answer)
                   {
                       foreach (DataGridViewRow row in focused.SelectedRows)
                       {
                           focused.MoveRow(row, newDirName);
                       }
                       focused.Focus();
                   }
               })
            {
                Owner = this,
                StartPosition = FormStartPosition.CenterParent
            }.ShowDialog();
        }

        public void ShowError(Exception ex)
        {
            MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
