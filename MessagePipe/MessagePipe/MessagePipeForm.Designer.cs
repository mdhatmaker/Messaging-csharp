namespace MessagePipe
{
    partial class MessagePipeForm
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
            this.button1 = new System.Windows.Forms.Button();
            this.listDisplay = new System.Windows.Forms.ListBox();
            this.lblInfo = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Dock = System.Windows.Forms.DockStyle.Top;
            this.button1.Location = new System.Drawing.Point(0, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(434, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Test something...";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // listDisplay
            // 
            this.listDisplay.BackColor = System.Drawing.Color.Black;
            this.listDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listDisplay.ForeColor = System.Drawing.Color.Green;
            this.listDisplay.FormattingEnabled = true;
            this.listDisplay.Location = new System.Drawing.Point(0, 23);
            this.listDisplay.Name = "listDisplay";
            this.listDisplay.Size = new System.Drawing.Size(434, 334);
            this.listDisplay.TabIndex = 1;
            // 
            // lblInfo
            // 
            this.lblInfo.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblInfo.Location = new System.Drawing.Point(0, 344);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(434, 13);
            this.lblInfo.TabIndex = 2;
            // 
            // MessagePipeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(434, 357);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.listDisplay);
            this.Controls.Add(this.button1);
            this.Name = "MessagePipeForm";
            this.Text = "Hydra Message Pipe";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ListBox listDisplay;
        private System.Windows.Forms.Label lblInfo;
    }
}

