namespace AudioWorkshop.Recorder
{
    partial class Form1
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
            this.txtOutput = new System.Windows.Forms.TextBox();
            this.btnRecord = new System.Windows.Forms.Button();
            this.chkPlayback = new System.Windows.Forms.CheckBox();
            this.lblLength = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpRecording = new System.Windows.Forms.TabPage();
            this.tpFiles = new System.Windows.Forms.TabPage();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnLabel = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnLength = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnWhenCreated = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnTag = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabControl1.SuspendLayout();
            this.tpRecording.SuspendLayout();
            this.tpFiles.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtOutput
            // 
            this.txtOutput.Location = new System.Drawing.Point(6, 311);
            this.txtOutput.Multiline = true;
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.ReadOnly = true;
            this.txtOutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtOutput.Size = new System.Drawing.Size(783, 107);
            this.txtOutput.TabIndex = 0;
            // 
            // btnRecord
            // 
            this.btnRecord.Location = new System.Drawing.Point(6, 6);
            this.btnRecord.Name = "btnRecord";
            this.btnRecord.Size = new System.Drawing.Size(75, 23);
            this.btnRecord.TabIndex = 1;
            this.btnRecord.Text = "Record";
            this.btnRecord.UseVisualStyleBackColor = true;
            this.btnRecord.Click += new System.EventHandler(this.btnRecord_Click);
            // 
            // chkPlayback
            // 
            this.chkPlayback.AutoSize = true;
            this.chkPlayback.Checked = true;
            this.chkPlayback.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkPlayback.Location = new System.Drawing.Point(6, 48);
            this.chkPlayback.Name = "chkPlayback";
            this.chkPlayback.Size = new System.Drawing.Size(70, 17);
            this.chkPlayback.TabIndex = 2;
            this.chkPlayback.Text = "Playback";
            this.chkPlayback.UseVisualStyleBackColor = true;
            // 
            // lblLength
            // 
            this.lblLength.AutoSize = true;
            this.lblLength.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLength.Location = new System.Drawing.Point(116, 28);
            this.lblLength.Name = "lblLength";
            this.lblLength.Size = new System.Drawing.Size(143, 37);
            this.lblLength.TabIndex = 3;
            this.lblLength.Text = "00:00:00";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tpRecording);
            this.tabControl1.Controls.Add(this.tpFiles);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(800, 450);
            this.tabControl1.TabIndex = 4;
            // 
            // tpRecording
            // 
            this.tpRecording.Controls.Add(this.btnRecord);
            this.tpRecording.Controls.Add(this.txtOutput);
            this.tpRecording.Controls.Add(this.lblLength);
            this.tpRecording.Controls.Add(this.chkPlayback);
            this.tpRecording.Location = new System.Drawing.Point(4, 22);
            this.tpRecording.Name = "tpRecording";
            this.tpRecording.Padding = new System.Windows.Forms.Padding(3);
            this.tpRecording.Size = new System.Drawing.Size(792, 424);
            this.tpRecording.TabIndex = 0;
            this.tpRecording.Text = "Recording";
            this.tpRecording.UseVisualStyleBackColor = true;
            // 
            // tpFiles
            // 
            this.tpFiles.Controls.Add(this.listView1);
            this.tpFiles.Location = new System.Drawing.Point(4, 22);
            this.tpFiles.Name = "tpFiles";
            this.tpFiles.Padding = new System.Windows.Forms.Padding(3);
            this.tpFiles.Size = new System.Drawing.Size(792, 424);
            this.tpFiles.TabIndex = 1;
            this.tpFiles.Text = "Files";
            this.tpFiles.UseVisualStyleBackColor = true;
            // 
            // listView1
            // 
            this.listView1.CheckBoxes = true;
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnLabel,
            this.columnLength,
            this.columnWhenCreated,
            this.columnTag});
            this.listView1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(3, 6);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(684, 410);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // columnLabel
            // 
            this.columnLabel.Text = "File Name";
            this.columnLabel.Width = 180;
            // 
            // columnLength
            // 
            this.columnLength.Text = "Length";
            this.columnLength.Width = 120;
            // 
            // columnWhenCreated
            // 
            this.columnWhenCreated.Text = "Start At";
            this.columnWhenCreated.Width = 120;
            // 
            // columnTag
            // 
            this.columnTag.Text = "Tag";
            this.columnTag.Width = 200;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.tpRecording.ResumeLayout(false);
            this.tpRecording.PerformLayout();
            this.tpFiles.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtOutput;
        private System.Windows.Forms.Button btnRecord;
        private System.Windows.Forms.CheckBox chkPlayback;
        private System.Windows.Forms.Label lblLength;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tpRecording;
        private System.Windows.Forms.TabPage tpFiles;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnLabel;
        private System.Windows.Forms.ColumnHeader columnLength;
        private System.Windows.Forms.ColumnHeader columnWhenCreated;
        private System.Windows.Forms.ColumnHeader columnTag;
    }
}

