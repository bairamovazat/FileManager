using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileManager
{
    public partial class MainForm : Form
    {
        private FilesDataGridView lastFocusedFileDataGridView = null;

        private ISettingsCache settingsCache;

        public MainForm()
        {
            InitializeComponent();
            settingsCache = new SettingsCacheIniImpl();
            LoadSizeFromCache();
            LoadPositionFromCache();
            dataGridViewOne.Cache = GetSettingCacheOne(settingsCache);
            dataGridViewTwo.Cache = GetSettingCacheTwo(settingsCache);

            dataGridViewOne.GotFocus += (sender, e) => lastFocusedFileDataGridView = dataGridViewOne;
            dataGridViewTwo.GotFocus += (sender, e) => lastFocusedFileDataGridView = dataGridViewTwo;

            dataGridViewOne.UpdateView += (dataGrid) => labelOne.Text = dataGrid.GetCurrentPath();
            dataGridViewTwo.UpdateView += (dataGrid) => labelTwo.Text = dataGrid.GetCurrentPath();

            dataGridViewOne.GlobalErrorHandler += (ex) => MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            dataGridViewTwo.GlobalErrorHandler += (ex) => MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);

            Bind(dataGridViewOne, comboBoxOne);
            Bind(dataGridViewTwo, comboBoxTwo);

            BindFocus(dataGridViewOne, panelOneChoseDisk);
            BindFocus(dataGridViewTwo, panelTwoChoseDisk);

            dataGridViewOne.UpdateDrives();
            dataGridViewTwo.UpdateDrives();

            dataGridViewOne.Focus();

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

            if (xPosition != 0 && yPosition != 0)
            {
                this.StartPosition = FormStartPosition.Manual;
                this.Location = new Point(xPosition, yPosition);
            }
        }

        public void Bind(FilesDataGridView filesDataGridView, ComboBox comboBox)
        {
            filesDataGridView.UpdateDrivesHandler += (driverInfoList) =>
            {
                comboBox.Items.Clear();
                driverInfoList.ForEach(info => comboBox.Items.Add(info.Name));

                if (comboBox.SelectedIndex == -1)
                {
                    for (int i = 0; i < comboBox.Items.Count; i++)
                    {
                        string value = comboBox.GetItemText(comboBox.Items[i]);
                        if (value.Equals(filesDataGridView.CurrentDrive.Name))
                        {
                            comboBox.SelectedIndex = i;
                        }
                    }
                };
            };

            comboBox.SelectedIndexChanged += (sender, e) =>
            {
                string drive = (string)comboBox.SelectedItem;
                filesDataGridView.SelectDriveByName(drive);
            };
        }

        public void BindFocus(FilesDataGridView dataGrid, Panel panel)
        {
            dataGrid.GotFocus += (source, e) => {
                panel.BackColor = SystemColors.ActiveCaption;
            };
            dataGrid.LostFocus += (source, e) => {
                panel.BackColor = SystemColors.Control;
            };
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData.Equals(Keys.F4))
            {
                this.buttonCopy.PerformClick();
            }
            else if (keyData.Equals(Keys.F5))
            {
                this.buttonDelete.PerformClick();
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

        protected override bool ProcessDialogKey(Keys keyData)
        {
            //Keys key = (keyData & Keys.KeyCode);

            //if (key == Keys.Tab)
            //{
            //    GetFocusedDataGridView().Focus();
            //    return true;
            //}
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
                    }
                    focused.Focus();
                })
            {
                Owner = this,
                StartPosition = FormStartPosition.CenterParent
            }.ShowDialog();
        }

        private void buttonNewFolder_Click(object sender, EventArgs e)
        {
            FilesDataGridView focused = lastFocusedFileDataGridView;
            new InputQuestion("Введите название нового каталога:", null,
               (answer, newDirName) =>
               {
                   if (answer)
                   {

                       focused.CreateSubDirectory(newDirName);
                   }
                   focused.Focus();
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

            new PathQuestions("Выберите каталог для перемещения, выбрано " + focused.SelectedRows.Count + " элементов:",
               (answer, newDirName) =>
               {
                   if (answer)
                   {
                       foreach (DataGridViewRow row in focused.SelectedRows)
                       {
                           focused.MoveRow(row, newDirName);
                       }
                   }
                   focused.Focus();
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

        private void buttonCopy_Click(object sender, EventArgs e)
        {
            FilesDataGridView focused = lastFocusedFileDataGridView;

            new PathQuestions("Выберите каталог для копирования, выбрано " + focused.SelectedRows.Count + " элементов:",
              (answer, newDirName) =>
              {
                  if (answer)
                  {
                      foreach (DataGridViewRow row in focused.SelectedRows)
                      {
                          focused.CopyRow(row, newDirName);
                      }
                  }
                  focused.Focus();
              })
            {
                Owner = this,
                StartPosition = FormStartPosition.CenterParent
            }.ShowDialog();
        }

        private void buttonChangeFolderName_Click(object sender, EventArgs e)
        {
            FilesDataGridView focused = lastFocusedFileDataGridView;

            if (focused.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите хотябы 1 элемент!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!focused.SelectedRows[0].Cells[1].Value.ToString().ToLower().Equals("файл"))
            {
                MessageBox.Show("Выберите хотябы 1 файл!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string fileName = focused.SelectedRows[0].Cells[0].Value.ToString();
            string filePath = focused.GetCurrentPath() + fileName;

            FileInfo fileInfo = new FileInfo(filePath);
            if (!fileInfo.Extension.Equals(".txt"))
            {
                MessageBox.Show("Файл должен быть формата txt", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string data = File.ReadAllText(filePath);

            new Editor(fileName, data,
              (answer, changedData) =>
              {
                  if (answer)
                  {
                      File.WriteAllText(filePath, changedData);
                  }
                  focused.Focus();
              })
            {
                Owner = this,
                StartPosition = FormStartPosition.CenterScreen
            }.Show();
        }

        private void buttonRename_Click(object sender, EventArgs e)
        {
            FilesDataGridView focused = lastFocusedFileDataGridView;

            if (focused.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите хотябы 1 элемент!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            else if (focused.SelectedRows.Count > 1)
            {
                MessageBox.Show("Выберите только 1 элемент!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string fileName = focused.SelectedRows[0].Cells[0].Value.ToString();

            new InputQuestion("Выберите новое название", fileName,
               (answer, newName) =>
               {
                   if (answer)
                   {
                       focused.RanameRow(focused.SelectedRows[0], newName);
                   }
                   focused.Focus();
               })
            {
                Owner = this,
                StartPosition = FormStartPosition.CenterParent
            }.ShowDialog();
        }

        private ISettingCache GetSettingCacheOne(ISettingsCache settingsCache)
        {
            return new SettingCacheImpl()
            {
                LastDriverName = new Func<string>(() => settingsCache.LastDriverNameOne()),

                LastPath = new Func<List<string>>(() => settingsCache.LastPathOne()),

                SaveLastDriverName = new Action<string>((name) => settingsCache.SaveLastDriverNameOne(name)),

                SaveLastPath = new Action<List<string>>((list) => settingsCache.SaveLastPathOne(list))
            };
        }

        private ISettingCache GetSettingCacheTwo(ISettingsCache settingsCache)
        {
            return new SettingCacheImpl()
            {
                LastDriverName = new Func<string>(() => settingsCache.LastDriverNameTwo()),

                LastPath = new Func<List<string>>(() => settingsCache.LastPathTwo()),

                SaveLastDriverName = new Action<string>((name) => settingsCache.SaveLastDriverNameTwo(name)),

                SaveLastPath = new Action<List<string>>((list) => settingsCache.SaveLastPathTwo(list))
            };
        }
    }
}
