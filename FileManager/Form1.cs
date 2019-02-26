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


<<<<<<< HEAD
            //TODO сделать систему коммитов, чтобы каждый пиксель не записывать
            this.SizeChanged += (sender, e) =>
            {
                Console.WriteLine("SizeChange");
                settingsCache.Width = this.Width;
                settingsCache.Height = this.Height;
            };

            this.LocationChanged += (sender, e) =>
            {
                settingsCache.XPosition = this.Location.X;
                settingsCache.YPosition = this.Location.Y;
            };
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData.Equals(Keys.F4)) {
                this.buttonCopy.PerformClick();
            }
            else if (keyData.Equals(Keys.F5))
            {
                this.buttonEditFile.PerformClick();
            }
            else if (keyData.Equals(Keys.F6))
            {
                this.buttonMove.PerformClick();
            }
            else if (keyData.Equals(Keys.F7))
            {
                this.buttonNewFolder.PerformClick();
            }
            else if (keyData.Equals(Keys.F8))
            {
                this.buttonDelete.PerformClick();
            }
            else if (keyData.Equals(Keys.F9))
            {
                this.buttonMove.PerformClick();
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void LoadSizeFromCache()
        {
            int width = settingsCache.Width;
            int height = settingsCache.Height;
            if (width != 0)
            {
                this.Width = width;
            }

            if (height != 0)
            {
                this.Height = height;
            }
        }

        private void LoadPositionFromCache() {
            int xPosition = settingsCache.XPosition;
            int yPosition = settingsCache.YPosition;
=======
            dataGridViewOne.Focus();
>>>>>>> parent of ea1ca53... First release

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
