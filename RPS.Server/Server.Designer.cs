
namespace RPS.Server
{
    partial class ServerForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.CreateButton = new System.Windows.Forms.Button();
            this.textinfo = new System.Windows.Forms.Label();
            this.NumOfPlayersLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // CreateButton
            // 
            this.CreateButton.Location = new System.Drawing.Point(237, 268);
            this.CreateButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.CreateButton.Name = "CreateButton";
            this.CreateButton.Size = new System.Drawing.Size(132, 31);
            this.CreateButton.TabIndex = 0;
            this.CreateButton.Text = "Create Server";
            this.CreateButton.UseVisualStyleBackColor = true;
            this.CreateButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // textinfo
            // 
            this.textinfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textinfo.Location = new System.Drawing.Point(70, 45);
            this.textinfo.Name = "textinfo";
            this.textinfo.Size = new System.Drawing.Size(445, 200);
            this.textinfo.TabIndex = 1;
            this.textinfo.Text = " ";
            // 
            // NumOfPlayersLabel
            // 
            this.NumOfPlayersLabel.AutoSize = true;
            this.NumOfPlayersLabel.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.NumOfPlayersLabel.Location = new System.Drawing.Point(191, 324);
            this.NumOfPlayersLabel.Name = "NumOfPlayersLabel";
            this.NumOfPlayersLabel.Size = new System.Drawing.Size(223, 20);
            this.NumOfPlayersLabel.TabIndex = 2;
            this.NumOfPlayersLabel.Text = "Number Of Players Connected: 0";
            // 
            // ServerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(582, 353);
            this.Controls.Add(this.NumOfPlayersLabel);
            this.Controls.Add(this.textinfo);
            this.Controls.Add(this.CreateButton);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "ServerForm";
            this.Text = "Server";
            this.Load += new System.EventHandler(this.ServerForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button CreateButton;
        private System.Windows.Forms.Label textinfo;
        private System.Windows.Forms.Label NumOfPlayersLabel;
    }
}

