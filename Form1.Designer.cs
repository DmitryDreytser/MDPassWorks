namespace MDPassWorks
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
            this.DecriptButton = new System.Windows.Forms.Button();
            this.EncriptButton = new System.Windows.Forms.Button();
            this.LoadMoxcel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // DecriptButton
            // 
            this.DecriptButton.Location = new System.Drawing.Point(6, 12);
            this.DecriptButton.Name = "DecriptButton";
            this.DecriptButton.Size = new System.Drawing.Size(95, 23);
            this.DecriptButton.TabIndex = 0;
            this.DecriptButton.Text = "Расшифровать";
            this.DecriptButton.UseVisualStyleBackColor = true;
            this.DecriptButton.Click += new System.EventHandler(this.DecriptButton_Click);
            // 
            // EncriptButton
            // 
            this.EncriptButton.Location = new System.Drawing.Point(6, 50);
            this.EncriptButton.Name = "EncriptButton";
            this.EncriptButton.Size = new System.Drawing.Size(95, 21);
            this.EncriptButton.TabIndex = 0;
            this.EncriptButton.Text = "Зашифровать";
            this.EncriptButton.UseVisualStyleBackColor = true;
            this.EncriptButton.Click += new System.EventHandler(this.EncriptButton_Click);
            // 
            // LoadMoxcel
            // 
            this.LoadMoxcel.Location = new System.Drawing.Point(134, 12);
            this.LoadMoxcel.Name = "LoadMoxcel";
            this.LoadMoxcel.Size = new System.Drawing.Size(95, 23);
            this.LoadMoxcel.TabIndex = 0;
            this.LoadMoxcel.Text = "Moxel";
            this.LoadMoxcel.UseVisualStyleBackColor = true;
            this.LoadMoxcel.Click += new System.EventHandler(this.LoadMoxcel_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(279, 79);
            this.Controls.Add(this.EncriptButton);
            this.Controls.Add(this.LoadMoxcel);
            this.Controls.Add(this.DecriptButton);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button DecriptButton;
        private System.Windows.Forms.Button EncriptButton;
        private System.Windows.Forms.Button LoadMoxcel;
    }
}

