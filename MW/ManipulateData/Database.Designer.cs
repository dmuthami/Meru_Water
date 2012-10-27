namespace MW.ManipulateData
{
    partial class Database
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
            this.btnListButton = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.clbDatasets = new System.Windows.Forms.CheckedListBox();
            this.btnLoadData = new System.Windows.Forms.Button();
            this.rtbDatasetsToLoad = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // btnListButton
            // 
            this.btnListButton.Location = new System.Drawing.Point(170, 405);
            this.btnListButton.Name = "btnListButton";
            this.btnListButton.Size = new System.Drawing.Size(75, 23);
            this.btnListButton.TabIndex = 1;
            this.btnListButton.Text = "List Data";
            this.btnListButton.UseVisualStyleBackColor = true;
            this.btnListButton.Click += new System.EventHandler(this.btnListButton_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(251, 405);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "List Data 2";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // clbDatasets
            // 
            this.clbDatasets.FormattingEnabled = true;
            this.clbDatasets.Location = new System.Drawing.Point(-1, 3);
            this.clbDatasets.Name = "clbDatasets";
            this.clbDatasets.Size = new System.Drawing.Size(125, 439);
            this.clbDatasets.TabIndex = 3;
            this.clbDatasets.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.clbDatasets_ItemCheck);
            // 
            // btnLoadData
            // 
            this.btnLoadData.Location = new System.Drawing.Point(345, 405);
            this.btnLoadData.Name = "btnLoadData";
            this.btnLoadData.Size = new System.Drawing.Size(84, 23);
            this.btnLoadData.TabIndex = 5;
            this.btnLoadData.Text = "Show On Map";
            this.btnLoadData.UseVisualStyleBackColor = true;
            // 
            // rtbDatasetsToLoad
            // 
            this.rtbDatasetsToLoad.Location = new System.Drawing.Point(130, 3);
            this.rtbDatasetsToLoad.Name = "rtbDatasetsToLoad";
            this.rtbDatasetsToLoad.Size = new System.Drawing.Size(312, 254);
            this.rtbDatasetsToLoad.TabIndex = 6;
            this.rtbDatasetsToLoad.Text = "";
            // 
            // Database
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(441, 440);
            this.Controls.Add(this.rtbDatasetsToLoad);
            this.Controls.Add(this.btnLoadData);
            this.Controls.Add(this.clbDatasets);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnListButton);
            this.Name = "Database";
            this.Text = "Database";
            this.Load += new System.EventHandler(this.Database_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnListButton;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckedListBox clbDatasets;
        private System.Windows.Forms.Button btnLoadData;
        private System.Windows.Forms.RichTextBox rtbDatasetsToLoad;
    }
}