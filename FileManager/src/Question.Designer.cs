namespace FileManager
{
    partial class Questions
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.labelText = new System.Windows.Forms.Label();
            this.buttonYes = new System.Windows.Forms.Button();
            this.buttonNo = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelText
            // 
            this.labelText.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelText.Location = new System.Drawing.Point(0, 0);
            this.labelText.Name = "labelText";
            this.labelText.Size = new System.Drawing.Size(344, 77);
            this.labelText.TabIndex = 0;
            this.labelText.Text = "Сообщение, которое нужно переопределить";
            this.labelText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonYes
            // 
            this.buttonYes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonYes.Location = new System.Drawing.Point(12, 96);
            this.buttonYes.Name = "buttonYes";
            this.buttonYes.Size = new System.Drawing.Size(75, 23);
            this.buttonYes.TabIndex = 1;
            this.buttonYes.Text = "Да";
            this.buttonYes.UseVisualStyleBackColor = true;
            this.buttonYes.Click += new System.EventHandler(this.buttonYes_Click);
            // 
            // buttonNo
            // 
            this.buttonNo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonNo.Location = new System.Drawing.Point(257, 96);
            this.buttonNo.Name = "buttonNo";
            this.buttonNo.Size = new System.Drawing.Size(75, 23);
            this.buttonNo.TabIndex = 2;
            this.buttonNo.Text = "Нет";
            this.buttonNo.UseVisualStyleBackColor = true;
            this.buttonNo.Click += new System.EventHandler(this.buttonNo_Click);
            // 
            // Questions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(344, 131);
            this.Controls.Add(this.buttonNo);
            this.Controls.Add(this.buttonYes);
            this.Controls.Add(this.labelText);
            this.Name = "Questions";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelText;
        private System.Windows.Forms.Button buttonYes;
        private System.Windows.Forms.Button buttonNo;
    }
}