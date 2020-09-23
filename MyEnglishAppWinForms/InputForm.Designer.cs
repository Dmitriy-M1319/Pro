namespace MyEnglishAppWinForms
{
    partial class InputForm
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.textBoxEnglishWords = new System.Windows.Forms.TextBox();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.buttonSetListOfWords = new System.Windows.Forms.Button();
            this.labelRussianWords = new System.Windows.Forms.Label();
            this.labelEnglishWords = new System.Windows.Forms.Label();
            this.textBoxRussianWords = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.textBoxEnglishWords);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(561, 450);
            this.splitContainer1.SplitterDistance = 166;
            this.splitContainer1.TabIndex = 0;
            // 
            // textBoxEnglishWords
            // 
            this.textBoxEnglishWords.Location = new System.Drawing.Point(10, 10);
            this.textBoxEnglishWords.Multiline = true;
            this.textBoxEnglishWords.Name = "textBoxEnglishWords";
            this.textBoxEnglishWords.Size = new System.Drawing.Size(133, 426);
            this.textBoxEnglishWords.TabIndex = 1;
            // 
            // splitContainer2
            // 
            this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.buttonSetListOfWords);
            this.splitContainer2.Panel1.Controls.Add(this.labelRussianWords);
            this.splitContainer2.Panel1.Controls.Add(this.labelEnglishWords);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.textBoxRussianWords);
            this.splitContainer2.Size = new System.Drawing.Size(391, 450);
            this.splitContainer2.SplitterDistance = 187;
            this.splitContainer2.TabIndex = 0;
            // 
            // buttonSetListOfWords
            // 
            this.buttonSetListOfWords.Location = new System.Drawing.Point(26, 190);
            this.buttonSetListOfWords.Name = "buttonSetListOfWords";
            this.buttonSetListOfWords.Size = new System.Drawing.Size(131, 63);
            this.buttonSetListOfWords.TabIndex = 0;
            this.buttonSetListOfWords.Text = "ДОБАВИТЬ";
            this.buttonSetListOfWords.UseVisualStyleBackColor = true;
            this.buttonSetListOfWords.Click += new System.EventHandler(this.buttonSetListOfWords_Click);
            // 
            // labelRussianWords
            // 
            this.labelRussianWords.AutoSize = true;
            this.labelRussianWords.Location = new System.Drawing.Point(23, 334);
            this.labelRussianWords.Name = "labelRussianWords";
            this.labelRussianWords.Size = new System.Drawing.Size(0, 13);
            this.labelRussianWords.TabIndex = 0;
            // 
            // labelEnglishWords
            // 
            this.labelEnglishWords.AutoSize = true;
            this.labelEnglishWords.Location = new System.Drawing.Point(23, 48);
            this.labelEnglishWords.Name = "labelEnglishWords";
            this.labelEnglishWords.Size = new System.Drawing.Size(0, 13);
            this.labelEnglishWords.TabIndex = 0;
            // 
            // textBoxRussianWords
            // 
            this.textBoxRussianWords.Location = new System.Drawing.Point(34, 10);
            this.textBoxRussianWords.Multiline = true;
            this.textBoxRussianWords.Name = "textBoxRussianWords";
            this.textBoxRussianWords.Size = new System.Drawing.Size(133, 426);
            this.textBoxRussianWords.TabIndex = 2;
            // 
            // InputForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(561, 450);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "InputForm";
            this.Text = "InputForm";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TextBox textBoxEnglishWords;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Button buttonSetListOfWords;
        private System.Windows.Forms.Label labelRussianWords;
        private System.Windows.Forms.Label labelEnglishWords;
        private System.Windows.Forms.TextBox textBoxRussianWords;
    }
}