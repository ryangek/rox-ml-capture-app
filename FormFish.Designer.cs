
namespace Farming
{
    partial class FormFish
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
            this.buttonRefresh = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.RichTextBox();
            this.buttonPlay = new System.Windows.Forms.Button();
            this.buttonForResult = new System.Windows.Forms.Button();
            this.listBoxProcesses = new System.Windows.Forms.ListBox();
            this.listBoxEmulators = new System.Windows.Forms.ListBox();
            this.numericCount = new System.Windows.Forms.NumericUpDown();
            //((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericCount)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonRefresh
            // 
            this.buttonRefresh.BackColor = System.Drawing.Color.Transparent;
            this.buttonRefresh.Cursor = System.Windows.Forms.Cursors.No;
            this.buttonRefresh.Enabled = false;
            this.buttonRefresh.FlatAppearance.BorderSize = 0;
            this.buttonRefresh.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
            this.buttonRefresh.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(30)))), ((int)(((byte)(45)))));
            this.buttonRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonRefresh.Image = global::Farming.Properties.Resources.refresh_16_gray;
            this.buttonRefresh.Location = new System.Drawing.Point(216, 17);
            this.buttonRefresh.Name = "buttonRefresh";
            this.buttonRefresh.Size = new System.Drawing.Size(36, 37);
            this.buttonRefresh.TabIndex = 1;
            this.buttonRefresh.UseVisualStyleBackColor = false;
            this.buttonRefresh.Click += new System.EventHandler(this.buttonRefresh_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.pictureBox1.Location = new System.Drawing.Point(216, 17);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(369, 374);
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.ForeColor = System.Drawing.Color.Cyan;
            // 
            // buttonPlay
            // 
            this.buttonPlay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonPlay.BackColor = System.Drawing.Color.Transparent;
            this.buttonPlay.Cursor = System.Windows.Forms.Cursors.No;
            this.buttonPlay.Enabled = false;
            this.buttonPlay.FlatAppearance.BorderSize = 0;
            this.buttonPlay.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
            this.buttonPlay.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(30)))), ((int)(((byte)(45)))));
            this.buttonPlay.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonPlay.Image = global::Farming.Properties.Resources.play_16_gray;
            this.buttonPlay.Location = new System.Drawing.Point(548, 18);
            this.buttonPlay.Name = "buttonPlay";
            this.buttonPlay.Size = new System.Drawing.Size(37, 36);
            this.buttonPlay.TabIndex = 1;
            this.buttonPlay.UseVisualStyleBackColor = false;
            this.buttonPlay.Click += new System.EventHandler(this.buttonPlay_Click);
            // 
            // buttonForResult
            // 
            this.buttonForResult.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonForResult.BackColor = System.Drawing.Color.Transparent;
            this.buttonForResult.Enabled = false;
            this.buttonForResult.FlatAppearance.BorderSize = 0;
            this.buttonForResult.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.buttonForResult.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.buttonForResult.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonForResult.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.buttonForResult.ForeColor = System.Drawing.Color.White;
            this.buttonForResult.Location = new System.Drawing.Point(216, 17);
            this.buttonForResult.Name = "buttonForResult";
            this.buttonForResult.Size = new System.Drawing.Size(369, 37);
            this.buttonForResult.TabIndex = 3;
            this.buttonForResult.Text = "Pending";
            this.buttonForResult.UseVisualStyleBackColor = false;
            // 
            // listBoxProcesses
            // 
            this.listBoxProcesses.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.listBoxProcesses.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.listBoxProcesses.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listBoxProcesses.Cursor = System.Windows.Forms.Cursors.No;
            this.listBoxProcesses.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.listBoxProcesses.ForeColor = System.Drawing.Color.White;
            this.listBoxProcesses.FormattingEnabled = true;
            this.listBoxProcesses.ItemHeight = 17;
            this.listBoxProcesses.Location = new System.Drawing.Point(12, 18);
            this.listBoxProcesses.Name = "listBoxProcesses";
            this.listBoxProcesses.Size = new System.Drawing.Size(188, 272);
            this.listBoxProcesses.TabIndex = 0;
            this.listBoxProcesses.SelectedIndexChanged += new System.EventHandler(this.listBoxProcesses_SelectedIndexChanged);
            // 
            // listBoxEmulators
            // 
            this.listBoxEmulators.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.listBoxEmulators.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.listBoxEmulators.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listBoxEmulators.Cursor = System.Windows.Forms.Cursors.No;
            this.listBoxEmulators.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.listBoxEmulators.ForeColor = System.Drawing.Color.White;
            this.listBoxEmulators.FormattingEnabled = true;
            this.listBoxEmulators.ItemHeight = 17;
            this.listBoxEmulators.Location = new System.Drawing.Point(12, 306);
            this.listBoxEmulators.Name = "listBoxEmulators";
            this.listBoxEmulators.Size = new System.Drawing.Size(188, 85);
            this.listBoxEmulators.TabIndex = 0;
            this.listBoxEmulators.SelectedIndexChanged += new System.EventHandler(this.listBoxEmulators_SelectedIndexChanged);
            // 
            // numericCount
            // 
            this.numericCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.numericCount.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(30)))), ((int)(((byte)(45)))));
            this.numericCount.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.numericCount.Font = new System.Drawing.Font("Segoe UI", 11.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.numericCount.ForeColor = System.Drawing.Color.White;
            this.numericCount.Location = new System.Drawing.Point(516, 367);
            this.numericCount.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numericCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericCount.Name = "numericCount";
            this.numericCount.Size = new System.Drawing.Size(69, 24);
            this.numericCount.TabIndex = 5;
            this.numericCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numericCount.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericCount.ValueChanged += new System.EventHandler(this.numericCount_ValueChanged);
            // 
            // FormFish
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(30)))), ((int)(((byte)(45)))));
            this.ClientSize = new System.Drawing.Size(597, 403);
            this.Controls.Add(this.numericCount);
            this.Controls.Add(this.listBoxProcesses);
            this.Controls.Add(this.listBoxEmulators);
            this.Controls.Add(this.buttonPlay);
            this.Controls.Add(this.buttonRefresh);
            this.Controls.Add(this.buttonForResult);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormFish";
            this.Text = "FormFish";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormFish_FormClosing);
            this.Load += new System.EventHandler(this.FormFish_Load);
            //((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericCount)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button buttonRefresh;
        private System.Windows.Forms.RichTextBox pictureBox1;
        private System.Windows.Forms.Button buttonPlay;
        private System.Windows.Forms.Button buttonForResult;
        private System.Windows.Forms.ListBox listBoxProcesses;
        private System.Windows.Forms.ListBox listBoxEmulators;
        private System.Windows.Forms.NumericUpDown numericCount;
    }
}