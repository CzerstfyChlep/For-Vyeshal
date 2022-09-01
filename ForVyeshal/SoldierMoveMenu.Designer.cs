namespace ForVyeshal
{
    partial class SoldierMoveMenu
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
            this.Panel = new System.Windows.Forms.FlowLayoutPanel();
            this.Move = new System.Windows.Forms.Button();
            this.Cancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Panel
            // 
            this.Panel.AutoScroll = true;
            this.Panel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.Panel.Location = new System.Drawing.Point(12, 12);
            this.Panel.Name = "Panel";
            this.Panel.Size = new System.Drawing.Size(355, 310);
            this.Panel.TabIndex = 0;
            // 
            // Move
            // 
            this.Move.Location = new System.Drawing.Point(292, 331);
            this.Move.Name = "Move";
            this.Move.Size = new System.Drawing.Size(75, 23);
            this.Move.TabIndex = 1;
            this.Move.Text = "Move\r\n";
            this.Move.UseVisualStyleBackColor = true;
            this.Move.Click += new System.EventHandler(this.Move_Click);
            // 
            // Cancel
            // 
            this.Cancel.Location = new System.Drawing.Point(12, 331);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(75, 23);
            this.Cancel.TabIndex = 2;
            this.Cancel.Text = "Cancel";
            this.Cancel.UseVisualStyleBackColor = true;
            this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // SoldierMoveMenu
            // 
            this.AcceptButton = this.Move;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(379, 362);
            this.ControlBox = false;
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.Move);
            this.Controls.Add(this.Panel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "SoldierMoveMenu";
            this.Text = "SoldierMoveMenu";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel Panel;
        private System.Windows.Forms.Button Move;
        private System.Windows.Forms.Button Cancel;
    }
}