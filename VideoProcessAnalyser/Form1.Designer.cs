namespace VideoProcessAnalyser
{
    partial class MainForm
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
            this.FiletextBox = new System.Windows.Forms.TextBox();
            this.Filelabel = new System.Windows.Forms.Label();
            this.SelectFilebutton = new System.Windows.Forms.Button();
            this.Analysebutton = new System.Windows.Forms.Button();
            this.ImagepictureBox = new System.Windows.Forms.PictureBox();
            this.SecondstextBox = new System.Windows.Forms.TextBox();
            this.Secondslabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.AddButton = new System.Windows.Forms.Button();
            this.DeleteButton = new System.Windows.Forms.Button();
            this.VideopropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.VideotrackBar = new System.Windows.Forms.TrackBar();
            this.Imagepanel = new System.Windows.Forms.Panel();
            this.ShowErrorscheckBox = new System.Windows.Forms.CheckBox();
            this.CorrectErrorscheckBox = new System.Windows.Forms.CheckBox();
            this.DeleteFilescheckBox = new System.Windows.Forms.CheckBox();
            this.ShowSecondscheckBox = new System.Windows.Forms.CheckBox();
            this.CurrentSecondstextBox = new System.Windows.Forms.TextBox();
            this.PercentcheckBox = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.ImagepictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.VideotrackBar)).BeginInit();
            this.Imagepanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // FiletextBox
            // 
            this.FiletextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.FiletextBox.Location = new System.Drawing.Point(67, 6);
            this.FiletextBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.FiletextBox.Name = "FiletextBox";
            this.FiletextBox.ReadOnly = true;
            this.FiletextBox.Size = new System.Drawing.Size(405, 22);
            this.FiletextBox.TabIndex = 0;
            // 
            // Filelabel
            // 
            this.Filelabel.AutoSize = true;
            this.Filelabel.Location = new System.Drawing.Point(12, 9);
            this.Filelabel.Name = "Filelabel";
            this.Filelabel.Size = new System.Drawing.Size(34, 17);
            this.Filelabel.TabIndex = 12;
            this.Filelabel.Text = "File:";
            // 
            // SelectFilebutton
            // 
            this.SelectFilebutton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SelectFilebutton.Location = new System.Drawing.Point(479, 6);
            this.SelectFilebutton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.SelectFilebutton.Name = "SelectFilebutton";
            this.SelectFilebutton.Size = new System.Drawing.Size(176, 23);
            this.SelectFilebutton.TabIndex = 1;
            this.SelectFilebutton.Text = "Select";
            this.SelectFilebutton.UseVisualStyleBackColor = true;
            this.SelectFilebutton.Click += new System.EventHandler(this.SelectFilebutton_Click);
            // 
            // Analysebutton
            // 
            this.Analysebutton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Analysebutton.Location = new System.Drawing.Point(663, 6);
            this.Analysebutton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Analysebutton.Name = "Analysebutton";
            this.Analysebutton.Size = new System.Drawing.Size(127, 23);
            this.Analysebutton.TabIndex = 11;
            this.Analysebutton.Text = "Analyse";
            this.Analysebutton.UseVisualStyleBackColor = true;
            this.Analysebutton.Click += new System.EventHandler(this.Analysebutton_Click);
            // 
            // ImagepictureBox
            // 
            this.ImagepictureBox.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.ImagepictureBox.Location = new System.Drawing.Point(3, 2);
            this.ImagepictureBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ImagepictureBox.Name = "ImagepictureBox";
            this.ImagepictureBox.Size = new System.Drawing.Size(500, 500);
            this.ImagepictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.ImagepictureBox.TabIndex = 5;
            this.ImagepictureBox.TabStop = false;
            this.ImagepictureBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ImagepictureBox_MouseMove_1);
            this.ImagepictureBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ImagepictureBox_MouseClick);
            this.ImagepictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ImagepictureBox_MouseDown);
            this.ImagepictureBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ImagepictureBox_MouseUp);
            // 
            // SecondstextBox
            // 
            this.SecondstextBox.Location = new System.Drawing.Point(552, 34);
            this.SecondstextBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.SecondstextBox.Name = "SecondstextBox";
            this.SecondstextBox.Size = new System.Drawing.Size(40, 22);
            this.SecondstextBox.TabIndex = 8;
            this.SecondstextBox.Text = "5";
            // 
            // Secondslabel
            // 
            this.Secondslabel.AutoSize = true;
            this.Secondslabel.Location = new System.Drawing.Point(439, 37);
            this.Secondslabel.Name = "Secondslabel";
            this.Secondslabel.Size = new System.Drawing.Size(107, 17);
            this.Secondslabel.TabIndex = 14;
            this.Secondslabel.Text = "Make pic every ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(599, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 17);
            this.label1.TabIndex = 15;
            this.label1.Text = "seconds";
            // 
            // AddButton
            // 
            this.AddButton.Location = new System.Drawing.Point(15, 126);
            this.AddButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.AddButton.Name = "AddButton";
            this.AddButton.Size = new System.Drawing.Size(139, 39);
            this.AddButton.TabIndex = 5;
            this.AddButton.Text = "Add";
            this.AddButton.UseVisualStyleBackColor = true;
            this.AddButton.Click += new System.EventHandler(this.AddButton_Click);
            // 
            // DeleteButton
            // 
            this.DeleteButton.Location = new System.Drawing.Point(164, 126);
            this.DeleteButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.DeleteButton.Name = "DeleteButton";
            this.DeleteButton.Size = new System.Drawing.Size(139, 39);
            this.DeleteButton.TabIndex = 6;
            this.DeleteButton.Text = "Remove";
            this.DeleteButton.UseVisualStyleBackColor = true;
            this.DeleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
            // 
            // VideopropertyGrid
            // 
            this.VideopropertyGrid.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.VideopropertyGrid.Location = new System.Drawing.Point(15, 172);
            this.VideopropertyGrid.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.VideopropertyGrid.Name = "VideopropertyGrid";
            this.VideopropertyGrid.Size = new System.Drawing.Size(288, 434);
            this.VideopropertyGrid.TabIndex = 7;
            this.VideopropertyGrid.ToolbarVisible = false;
            this.VideopropertyGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.VideopropertyGrid_PropertyValueChanged);
            // 
            // VideotrackBar
            // 
            this.VideotrackBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.VideotrackBar.Enabled = false;
            this.VideotrackBar.Location = new System.Drawing.Point(419, 64);
            this.VideotrackBar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.VideotrackBar.Name = "VideotrackBar";
            this.VideotrackBar.Size = new System.Drawing.Size(371, 56);
            this.VideotrackBar.TabIndex = 10;
            this.VideotrackBar.Scroll += new System.EventHandler(this.VideotrackBar_Scroll);
            // 
            // Imagepanel
            // 
            this.Imagepanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.Imagepanel.AutoScroll = true;
            this.Imagepanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Imagepanel.Controls.Add(this.ImagepictureBox);
            this.Imagepanel.Location = new System.Drawing.Point(309, 126);
            this.Imagepanel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Imagepanel.Name = "Imagepanel";
            this.Imagepanel.Size = new System.Drawing.Size(469, 480);
            this.Imagepanel.TabIndex = 9;
            // 
            // ShowErrorscheckBox
            // 
            this.ShowErrorscheckBox.AutoSize = true;
            this.ShowErrorscheckBox.Location = new System.Drawing.Point(15, 37);
            this.ShowErrorscheckBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ShowErrorscheckBox.Name = "ShowErrorscheckBox";
            this.ShowErrorscheckBox.Size = new System.Drawing.Size(106, 21);
            this.ShowErrorscheckBox.TabIndex = 2;
            this.ShowErrorscheckBox.Text = "Show errors";
            this.ShowErrorscheckBox.UseVisualStyleBackColor = true;
            this.ShowErrorscheckBox.CheckedChanged += new System.EventHandler(this.ShowErrorscheckBox_CheckedChanged);
            // 
            // CorrectErrorscheckBox
            // 
            this.CorrectErrorscheckBox.AutoSize = true;
            this.CorrectErrorscheckBox.Enabled = false;
            this.CorrectErrorscheckBox.Location = new System.Drawing.Point(15, 64);
            this.CorrectErrorscheckBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.CorrectErrorscheckBox.Name = "CorrectErrorscheckBox";
            this.CorrectErrorscheckBox.Size = new System.Drawing.Size(118, 21);
            this.CorrectErrorscheckBox.TabIndex = 3;
            this.CorrectErrorscheckBox.Text = "Correct errors";
            this.CorrectErrorscheckBox.UseVisualStyleBackColor = true;
            this.CorrectErrorscheckBox.CheckedChanged += new System.EventHandler(this.CorrectErrorscheckBox_CheckedChanged);
            // 
            // DeleteFilescheckBox
            // 
            this.DeleteFilescheckBox.AutoSize = true;
            this.DeleteFilescheckBox.Location = new System.Drawing.Point(15, 91);
            this.DeleteFilescheckBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.DeleteFilescheckBox.Name = "DeleteFilescheckBox";
            this.DeleteFilescheckBox.Size = new System.Drawing.Size(135, 21);
            this.DeleteFilescheckBox.TabIndex = 4;
            this.DeleteFilescheckBox.Text = "Delete temp files";
            this.DeleteFilescheckBox.UseVisualStyleBackColor = true;
            this.DeleteFilescheckBox.CheckedChanged += new System.EventHandler(this.DeleteTemFilescheckBox_CheckedChanged);
            // 
            // ShowSecondscheckBox
            // 
            this.ShowSecondscheckBox.AutoSize = true;
            this.ShowSecondscheckBox.Enabled = false;
            this.ShowSecondscheckBox.Location = new System.Drawing.Point(216, 63);
            this.ShowSecondscheckBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ShowSecondscheckBox.Name = "ShowSecondscheckBox";
            this.ShowSecondscheckBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ShowSecondscheckBox.Size = new System.Drawing.Size(85, 21);
            this.ShowSecondscheckBox.TabIndex = 4;
            this.ShowSecondscheckBox.Text = "Seconds";
            this.ShowSecondscheckBox.UseVisualStyleBackColor = true;
            this.ShowSecondscheckBox.CheckedChanged += new System.EventHandler(this.ShowSecondscheckBox_CheckedChanged);
            // 
            // CurrentSecondstextBox
            // 
            this.CurrentSecondstextBox.Enabled = false;
            this.CurrentSecondstextBox.Location = new System.Drawing.Point(313, 64);
            this.CurrentSecondstextBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.CurrentSecondstextBox.Name = "CurrentSecondstextBox";
            this.CurrentSecondstextBox.Size = new System.Drawing.Size(100, 22);
            this.CurrentSecondstextBox.TabIndex = 16;
            this.CurrentSecondstextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CurrentSecondstextBox_KeyDown);
            this.CurrentSecondstextBox.Leave += new System.EventHandler(this.CurrentSecondstextBox_Leave);
            // 
            // PercentcheckBox
            // 
            this.PercentcheckBox.AutoSize = true;
            this.PercentcheckBox.Location = new System.Drawing.Point(216, 34);
            this.PercentcheckBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.PercentcheckBox.Name = "PercentcheckBox";
            this.PercentcheckBox.Size = new System.Drawing.Size(86, 21);
            this.PercentcheckBox.TabIndex = 2;
            this.PercentcheckBox.Text = "Percents";
            this.PercentcheckBox.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(663, 32);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(127, 23);
            this.button1.TabIndex = 11;
            this.button1.Text = "Only analyse";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(801, 617);
            this.Controls.Add(this.CurrentSecondstextBox);
            this.Controls.Add(this.ShowSecondscheckBox);
            this.Controls.Add(this.DeleteFilescheckBox);
            this.Controls.Add(this.CorrectErrorscheckBox);
            this.Controls.Add(this.PercentcheckBox);
            this.Controls.Add(this.ShowErrorscheckBox);
            this.Controls.Add(this.Imagepanel);
            this.Controls.Add(this.VideotrackBar);
            this.Controls.Add(this.VideopropertyGrid);
            this.Controls.Add(this.DeleteButton);
            this.Controls.Add(this.AddButton);
            this.Controls.Add(this.SecondstextBox);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.Analysebutton);
            this.Controls.Add(this.SelectFilebutton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Secondslabel);
            this.Controls.Add(this.Filelabel);
            this.Controls.Add(this.FiletextBox);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "MainForm";
            this.Text = "Video Analyser";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.ImagepictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.VideotrackBar)).EndInit();
            this.Imagepanel.ResumeLayout(false);
            this.Imagepanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox FiletextBox;
        private System.Windows.Forms.Label Filelabel;
        private System.Windows.Forms.Button SelectFilebutton;
        private System.Windows.Forms.Button Analysebutton;
        private System.Windows.Forms.PictureBox ImagepictureBox;
        private System.Windows.Forms.TextBox SecondstextBox;
        private System.Windows.Forms.Label Secondslabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button AddButton;
        private System.Windows.Forms.Button DeleteButton;
        private System.Windows.Forms.PropertyGrid VideopropertyGrid;
        private System.Windows.Forms.TrackBar VideotrackBar;
        private System.Windows.Forms.Panel Imagepanel;
        private System.Windows.Forms.CheckBox ShowErrorscheckBox;
        private System.Windows.Forms.CheckBox CorrectErrorscheckBox;
        private System.Windows.Forms.CheckBox DeleteFilescheckBox;
        private System.Windows.Forms.CheckBox ShowSecondscheckBox;
        private System.Windows.Forms.TextBox CurrentSecondstextBox;
        private System.Windows.Forms.CheckBox PercentcheckBox;
        private System.Windows.Forms.Button button1;
    }
}

