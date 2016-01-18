namespace LiveUpdate
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
            this.elementHost1 = new System.Windows.Forms.Integration.ElementHost();
            this.UpdateButton = new System.Windows.Forms.Button();
            this.UpdateStopButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // elementHost1
            // 
            this.elementHost1.Location = new System.Drawing.Point(30, 2);
            this.elementHost1.Name = "elementHost1";
            this.elementHost1.Size = new System.Drawing.Size(282, 262);
            this.elementHost1.TabIndex = 0;
            this.elementHost1.Text = "elementHost1";
            this.elementHost1.Child = null;
            // 
            // UpdateButton
            // 
            this.UpdateButton.Location = new System.Drawing.Point(52, 270);
            this.UpdateButton.Name = "UpdateButton";
            this.UpdateButton.Size = new System.Drawing.Size(114, 23);
            this.UpdateButton.TabIndex = 1;
            this.UpdateButton.Text = "Start Update";
            this.UpdateButton.UseVisualStyleBackColor = true;
            this.UpdateButton.Click += new System.EventHandler(this.UpdateButton_Click);
            // 
            // UpdateStopButton
            // 
            this.UpdateStopButton.Location = new System.Drawing.Point(184, 270);
            this.UpdateStopButton.Name = "UpdateStopButton";
            this.UpdateStopButton.Size = new System.Drawing.Size(117, 23);
            this.UpdateStopButton.TabIndex = 2;
            this.UpdateStopButton.Text = "Stop Update";
            this.UpdateStopButton.UseVisualStyleBackColor = true;
            this.UpdateStopButton.Click += new System.EventHandler(this.UpdateStopButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(350, 310);
            this.Controls.Add(this.UpdateStopButton);
            this.Controls.Add(this.UpdateButton);
            this.Controls.Add(this.elementHost1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Integration.ElementHost elementHost1;
        private System.Windows.Forms.Button UpdateButton;
        private System.Windows.Forms.Button UpdateStopButton;
    }
}

