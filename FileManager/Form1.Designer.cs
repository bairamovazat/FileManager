namespace FileManager
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            FileManager.SettingCacheFakeImpl settingCacheFakeImpl1 = new FileManager.SettingCacheFakeImpl();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            FileManager.SettingCacheFakeImpl settingCacheFakeImpl2 = new FileManager.SettingCacheFakeImpl();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panelOneData = new System.Windows.Forms.Panel();
            this.panelOneChoseDisk = new System.Windows.Forms.Panel();
            this.panelForLabelOne = new System.Windows.Forms.Panel();
            this.labelOne = new System.Windows.Forms.Label();
            this.comboBoxOne = new System.Windows.Forms.ComboBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panelTwoChoseDisk = new System.Windows.Forms.Panel();
            this.panelForLabelTwo = new System.Windows.Forms.Panel();
            this.labelTwo = new System.Windows.Forms.Label();
            this.comboBoxTwo = new System.Windows.Forms.ComboBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.testToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.buttonRename = new System.Windows.Forms.Button();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.buttonNewFolder = new System.Windows.Forms.Button();
            this.buttonMove = new System.Windows.Forms.Button();
            this.buttonEditFile = new System.Windows.Forms.Button();
            this.buttonCopy = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.labelCmdRun = new System.Windows.Forms.Label();
            this.textBoxCmdRun = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.dataGridViewOne = new FileManager.FilesDataGridView();
            this.dataGridViewTwo = new FileManager.FilesDataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panelOneData.SuspendLayout();
            this.panelOneChoseDisk.SuspendLayout();
            this.panelForLabelOne.SuspendLayout();
            this.panelTwoChoseDisk.SuspendLayout();
            this.panelForLabelTwo.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewOne)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTwo)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.AllowDrop = true;
            resources.ApplyResources(this.splitContainer1, "splitContainer1");
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.AllowDrop = true;
            this.splitContainer1.Panel1.Controls.Add(this.panelOneData);
            this.splitContainer1.Panel1.Controls.Add(this.panelOneChoseDisk);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dataGridViewTwo);
            this.splitContainer1.Panel2.Controls.Add(this.panel2);
            this.splitContainer1.Panel2.Controls.Add(this.panelTwoChoseDisk);
            this.splitContainer1.TabStop = false;
            // 
            // panelOneData
            // 
            this.panelOneData.AllowDrop = true;
            this.panelOneData.Controls.Add(this.dataGridViewOne);
            resources.ApplyResources(this.panelOneData, "panelOneData");
            this.panelOneData.Name = "panelOneData";
            // 
            // panelOneChoseDisk
            // 
            this.panelOneChoseDisk.Controls.Add(this.panelForLabelOne);
            this.panelOneChoseDisk.Controls.Add(this.comboBoxOne);
            resources.ApplyResources(this.panelOneChoseDisk, "panelOneChoseDisk");
            this.panelOneChoseDisk.Name = "panelOneChoseDisk";
            // 
            // panelForLabelOne
            // 
            resources.ApplyResources(this.panelForLabelOne, "panelForLabelOne");
            this.panelForLabelOne.Controls.Add(this.labelOne);
            this.panelForLabelOne.Name = "panelForLabelOne";
            // 
            // labelOne
            // 
            resources.ApplyResources(this.labelOne, "labelOne");
            this.labelOne.Name = "labelOne";
            this.labelOne.UseCompatibleTextRendering = true;
            // 
            // comboBoxOne
            // 
            this.comboBoxOne.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxOne.DropDownWidth = 80;
            this.comboBoxOne.FormattingEnabled = true;
            resources.ApplyResources(this.comboBoxOne, "comboBoxOne");
            this.comboBoxOne.Name = "comboBoxOne";
            this.comboBoxOne.TabStop = false;
            // 
            // panel2
            // 
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Name = "panel2";
            // 
            // panelTwoChoseDisk
            // 
            resources.ApplyResources(this.panelTwoChoseDisk, "panelTwoChoseDisk");
            this.panelTwoChoseDisk.BackColor = System.Drawing.SystemColors.Control;
            this.panelTwoChoseDisk.Controls.Add(this.panelForLabelTwo);
            this.panelTwoChoseDisk.Controls.Add(this.comboBoxTwo);
            this.panelTwoChoseDisk.Name = "panelTwoChoseDisk";
            // 
            // panelForLabelTwo
            // 
            resources.ApplyResources(this.panelForLabelTwo, "panelForLabelTwo");
            this.panelForLabelTwo.Controls.Add(this.labelTwo);
            this.panelForLabelTwo.Name = "panelForLabelTwo";
            // 
            // labelTwo
            // 
            resources.ApplyResources(this.labelTwo, "labelTwo");
            this.labelTwo.Name = "labelTwo";
            this.labelTwo.UseCompatibleTextRendering = true;
            // 
            // comboBoxTwo
            // 
            this.comboBoxTwo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxTwo.DropDownWidth = 80;
            this.comboBoxTwo.FormattingEnabled = true;
            resources.ApplyResources(this.comboBoxTwo, "comboBoxTwo");
            this.comboBoxTwo.Name = "comboBoxTwo";
            this.comboBoxTwo.TabStop = false;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.testToolStripMenuItem});
            resources.ApplyResources(this.menuStrip1, "menuStrip1");
            this.menuStrip1.Name = "menuStrip1";
            // 
            // testToolStripMenuItem
            // 
            this.testToolStripMenuItem.Name = "testToolStripMenuItem";
            resources.ApplyResources(this.testToolStripMenuItem, "testToolStripMenuItem");
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tableLayoutPanel1);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.buttonRename, 5, 0);
            this.tableLayoutPanel1.Controls.Add(this.buttonDelete, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.buttonNewFolder, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.buttonMove, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.buttonEditFile, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.buttonCopy, 0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // buttonRename
            // 
            resources.ApplyResources(this.buttonRename, "buttonRename");
            this.buttonRename.Name = "buttonRename";
            this.buttonRename.TabStop = false;
            this.buttonRename.UseVisualStyleBackColor = true;
            this.buttonRename.Click += new System.EventHandler(this.buttonRename_Click);
            // 
            // buttonDelete
            // 
            resources.ApplyResources(this.buttonDelete, "buttonDelete");
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.TabStop = false;
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelet_Click);
            // 
            // buttonNewFolder
            // 
            resources.ApplyResources(this.buttonNewFolder, "buttonNewFolder");
            this.buttonNewFolder.Name = "buttonNewFolder";
            this.buttonNewFolder.TabStop = false;
            this.buttonNewFolder.UseVisualStyleBackColor = true;
            this.buttonNewFolder.Click += new System.EventHandler(this.buttonNewFolder_Click);
            // 
            // buttonMove
            // 
            resources.ApplyResources(this.buttonMove, "buttonMove");
            this.buttonMove.Name = "buttonMove";
            this.buttonMove.TabStop = false;
            this.buttonMove.UseVisualStyleBackColor = true;
            this.buttonMove.Click += new System.EventHandler(this.buttonMove_Click);
            // 
            // buttonEditFile
            // 
            resources.ApplyResources(this.buttonEditFile, "buttonEditFile");
            this.buttonEditFile.Name = "buttonEditFile";
            this.buttonEditFile.TabStop = false;
            this.buttonEditFile.UseVisualStyleBackColor = true;
            this.buttonEditFile.Click += new System.EventHandler(this.buttonChangeFolderName_Click);
            // 
            // buttonCopy
            // 
            resources.ApplyResources(this.buttonCopy, "buttonCopy");
            this.buttonCopy.Name = "buttonCopy";
            this.buttonCopy.TabStop = false;
            this.buttonCopy.UseVisualStyleBackColor = true;
            this.buttonCopy.Click += new System.EventHandler(this.buttonCopy_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.labelCmdRun);
            resources.ApplyResources(this.panel3, "panel3");
            this.panel3.Name = "panel3";
            // 
            // labelCmdRun
            // 
            resources.ApplyResources(this.labelCmdRun, "labelCmdRun");
            this.labelCmdRun.Name = "labelCmdRun";
            this.labelCmdRun.UseCompatibleTextRendering = true;
            // 
            // textBoxCmdRun
            // 
            resources.ApplyResources(this.textBoxCmdRun, "textBoxCmdRun");
            this.textBoxCmdRun.Name = "textBoxCmdRun";
            this.textBoxCmdRun.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxCmdRun_KeyPress);
            // 
            // tableLayoutPanel2
            // 
            resources.ApplyResources(this.tableLayoutPanel2, "tableLayoutPanel2");
            this.tableLayoutPanel2.Controls.Add(this.panel3, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.textBoxCmdRun, 1, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            // 
            // dataGridViewOne
            // 
            this.dataGridViewOne.AllowDrop = true;
            this.dataGridViewOne.AllowUserToAddRows = false;
            this.dataGridViewOne.AllowUserToDeleteRows = false;
            this.dataGridViewOne.AllowUserToResizeRows = false;
            resources.ApplyResources(this.dataGridViewOne, "dataGridViewOne");
            this.dataGridViewOne.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewOne.BackgroundColor = System.Drawing.Color.White;
            settingCacheFakeImpl1.LastDriverName = null;
            settingCacheFakeImpl1.LastPath = null;
            settingCacheFakeImpl1.SaveLastDriverName = null;
            settingCacheFakeImpl1.SaveLastPath = null;
            this.dataGridViewOne.Cache = settingCacheFakeImpl1;
            this.dataGridViewOne.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dataGridViewOne.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.dataGridViewOne.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewOne.CurrentDrive = null;
            this.dataGridViewOne.Cursor = System.Windows.Forms.Cursors.Default;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Red;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewOne.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewOne.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridViewOne.GridColor = System.Drawing.Color.Black;
            this.dataGridViewOne.Name = "dataGridViewOne";
            this.dataGridViewOne.ReadOnly = true;
            this.dataGridViewOne.RowHeadersVisible = false;
            this.dataGridViewOne.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewOne.StandardTab = true;
            this.dataGridViewOne.VirtualMode = true;
            // 
            // dataGridViewTwo
            // 
            this.dataGridViewTwo.AllowDrop = true;
            this.dataGridViewTwo.AllowUserToAddRows = false;
            this.dataGridViewTwo.AllowUserToDeleteRows = false;
            this.dataGridViewTwo.AllowUserToResizeRows = false;
            resources.ApplyResources(this.dataGridViewTwo, "dataGridViewTwo");
            this.dataGridViewTwo.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewTwo.BackgroundColor = System.Drawing.Color.White;
            settingCacheFakeImpl2.LastDriverName = null;
            settingCacheFakeImpl2.LastPath = null;
            settingCacheFakeImpl2.SaveLastDriverName = null;
            settingCacheFakeImpl2.SaveLastPath = null;
            this.dataGridViewTwo.Cache = settingCacheFakeImpl2;
            this.dataGridViewTwo.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dataGridViewTwo.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.dataGridViewTwo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewTwo.CurrentDrive = null;
            this.dataGridViewTwo.Cursor = System.Windows.Forms.Cursors.Default;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Red;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTwo.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewTwo.GridColor = System.Drawing.Color.Black;
            this.dataGridViewTwo.Name = "dataGridViewTwo";
            this.dataGridViewTwo.ReadOnly = true;
            this.dataGridViewTwo.RowHeadersVisible = false;
            this.dataGridViewTwo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewTwo.StandardTab = true;
            this.dataGridViewTwo.VirtualMode = true;
            // 
            // MainForm
            // 
            this.AllowDrop = true;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.ShowIcon = false;
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panelOneData.ResumeLayout(false);
            this.panelOneChoseDisk.ResumeLayout(false);
            this.panelForLabelOne.ResumeLayout(false);
            this.panelForLabelOne.PerformLayout();
            this.panelTwoChoseDisk.ResumeLayout(false);
            this.panelForLabelTwo.ResumeLayout(false);
            this.panelForLabelTwo.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewOne)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTwo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem testToolStripMenuItem;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panelOneChoseDisk;
        private System.Windows.Forms.Panel panelTwoChoseDisk;
        private System.Windows.Forms.Panel panelOneData;
        private System.Windows.Forms.Panel panel2;
        private FilesDataGridView dataGridViewOne;
        private System.Windows.Forms.ComboBox comboBoxOne;
        private FilesDataGridView dataGridViewTwo;
        private System.Windows.Forms.ComboBox comboBoxTwo;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button buttonRename;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.Button buttonNewFolder;
        private System.Windows.Forms.Button buttonMove;
        private System.Windows.Forms.Button buttonEditFile;
        private System.Windows.Forms.Button buttonCopy;
        private System.Windows.Forms.Panel panelForLabelOne;
        private System.Windows.Forms.Label labelOne;
        private System.Windows.Forms.Panel panelForLabelTwo;
        private System.Windows.Forms.Label labelTwo;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label labelCmdRun;
        private System.Windows.Forms.TextBox textBoxCmdRun;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
    }
}

