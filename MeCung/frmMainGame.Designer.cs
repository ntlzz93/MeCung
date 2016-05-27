namespace MeCung
{
    partial class frmMainGame
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
            this.btnRandom = new System.Windows.Forms.Button();
            this.btnSelfCreate = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnRandom
            // 
            this.btnRandom.Location = new System.Drawing.Point(630, 23);
            this.btnRandom.Name = "btnRandom";
            this.btnRandom.Size = new System.Drawing.Size(88, 39);
            this.btnRandom.TabIndex = 0;
            this.btnRandom.Text = "Random Battle";
            this.btnRandom.UseVisualStyleBackColor = true;
            this.btnRandom.Click += new System.EventHandler(this.btnRandom_Click);
            // 
            // btnSelfCreate
            // 
            this.btnSelfCreate.Location = new System.Drawing.Point(630, 85);
            this.btnSelfCreate.Name = "btnSelfCreate";
            this.btnSelfCreate.Size = new System.Drawing.Size(88, 39);
            this.btnSelfCreate.TabIndex = 1;
            this.btnSelfCreate.Text = "Create Battle";
            this.btnSelfCreate.UseVisualStyleBackColor = true;
            this.btnSelfCreate.Click += new System.EventHandler(this.btnSelfCreate_Click);
            // 
            // frmMainGame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(730, 568);
            this.Controls.Add(this.btnSelfCreate);
            this.Controls.Add(this.btnRandom);
            this.Name = "frmMainGame";
            this.Text = "Mê Cung";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnRandom;
        private System.Windows.Forms.Button btnSelfCreate;
    }
}

