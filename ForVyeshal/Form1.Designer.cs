namespace ForVyeshal
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
            this.DevButton = new System.Windows.Forms.Button();
            this.FortsButton = new System.Windows.Forms.Button();
            this.ControlButton = new System.Windows.Forms.Button();
            this.PopButton = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.ProvinceOverview = new System.Windows.Forms.GroupBox();
            this.MoreButton = new System.Windows.Forms.Button();
            this.Panel = new System.Windows.Forms.FlowLayoutPanel();
            this.FactoriesLabel = new System.Windows.Forms.Label();
            this.FarmsLabel = new System.Windows.Forms.Label();
            this.DevelopmentLabel = new System.Windows.Forms.Label();
            this.PopulationLabel = new System.Windows.Forms.Label();
            this.FarmsButton = new System.Windows.Forms.Button();
            this.FactoriesButton = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.ProvinceOverview.SuspendLayout();
            this.SuspendLayout();
            // 
            // DevButton
            // 
            this.DevButton.Location = new System.Drawing.Point(640, 12);
            this.DevButton.Name = "DevButton";
            this.DevButton.Size = new System.Drawing.Size(210, 23);
            this.DevButton.TabIndex = 0;
            this.DevButton.Text = "Development";
            this.DevButton.UseVisualStyleBackColor = true;
            this.DevButton.Click += new System.EventHandler(this.DevButton_Click);
            // 
            // FortsButton
            // 
            this.FortsButton.Location = new System.Drawing.Point(640, 41);
            this.FortsButton.Name = "FortsButton";
            this.FortsButton.Size = new System.Drawing.Size(210, 23);
            this.FortsButton.TabIndex = 1;
            this.FortsButton.Text = "Forts";
            this.FortsButton.UseVisualStyleBackColor = true;
            this.FortsButton.Click += new System.EventHandler(this.FortsButton_Click);
            // 
            // ControlButton
            // 
            this.ControlButton.Location = new System.Drawing.Point(640, 70);
            this.ControlButton.Name = "ControlButton";
            this.ControlButton.Size = new System.Drawing.Size(210, 23);
            this.ControlButton.TabIndex = 2;
            this.ControlButton.Text = "Control";
            this.ControlButton.UseVisualStyleBackColor = true;
            this.ControlButton.Click += new System.EventHandler(this.ControlButton_Click);
            // 
            // PopButton
            // 
            this.PopButton.Location = new System.Drawing.Point(640, 99);
            this.PopButton.Name = "PopButton";
            this.PopButton.Size = new System.Drawing.Size(210, 23);
            this.PopButton.TabIndex = 3;
            this.PopButton.Text = "Popularity";
            this.PopButton.UseVisualStyleBackColor = true;
            this.PopButton.Click += new System.EventHandler(this.PopButton_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(754, 472);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(96, 29);
            this.button2.TabIndex = 6;
            this.button2.Text = "start";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // ProvinceOverview
            // 
            this.ProvinceOverview.BackColor = System.Drawing.Color.White;
            this.ProvinceOverview.Controls.Add(this.MoreButton);
            this.ProvinceOverview.Controls.Add(this.Panel);
            this.ProvinceOverview.Controls.Add(this.FactoriesLabel);
            this.ProvinceOverview.Controls.Add(this.FarmsLabel);
            this.ProvinceOverview.Controls.Add(this.DevelopmentLabel);
            this.ProvinceOverview.Controls.Add(this.PopulationLabel);
            this.ProvinceOverview.Location = new System.Drawing.Point(640, 157);
            this.ProvinceOverview.Name = "ProvinceOverview";
            this.ProvinceOverview.Size = new System.Drawing.Size(210, 309);
            this.ProvinceOverview.TabIndex = 7;
            this.ProvinceOverview.TabStop = false;
            this.ProvinceOverview.Text = "None";
            // 
            // MoreButton
            // 
            this.MoreButton.Location = new System.Drawing.Point(9, 91);
            this.MoreButton.Name = "MoreButton";
            this.MoreButton.Size = new System.Drawing.Size(195, 23);
            this.MoreButton.TabIndex = 6;
            this.MoreButton.Text = "More";
            this.MoreButton.UseVisualStyleBackColor = true;
            this.MoreButton.Click += new System.EventHandler(this.MoreButton_Click);
            // 
            // Panel
            // 
            this.Panel.AutoScroll = true;
            this.Panel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.Panel.Location = new System.Drawing.Point(9, 120);
            this.Panel.Name = "Panel";
            this.Panel.Size = new System.Drawing.Size(200, 183);
            this.Panel.TabIndex = 5;
            // 
            // FactoriesLabel
            // 
            this.FactoriesLabel.AutoSize = true;
            this.FactoriesLabel.Location = new System.Drawing.Point(6, 64);
            this.FactoriesLabel.Name = "FactoriesLabel";
            this.FactoriesLabel.Size = new System.Drawing.Size(53, 13);
            this.FactoriesLabel.TabIndex = 3;
            this.FactoriesLabel.Text = "Factories:";
            // 
            // FarmsLabel
            // 
            this.FarmsLabel.AutoSize = true;
            this.FarmsLabel.Location = new System.Drawing.Point(6, 48);
            this.FarmsLabel.Name = "FarmsLabel";
            this.FarmsLabel.Size = new System.Drawing.Size(38, 13);
            this.FarmsLabel.TabIndex = 2;
            this.FarmsLabel.Text = "Farms:";
            // 
            // DevelopmentLabel
            // 
            this.DevelopmentLabel.AutoSize = true;
            this.DevelopmentLabel.Location = new System.Drawing.Point(6, 32);
            this.DevelopmentLabel.Name = "DevelopmentLabel";
            this.DevelopmentLabel.Size = new System.Drawing.Size(73, 13);
            this.DevelopmentLabel.TabIndex = 1;
            this.DevelopmentLabel.Text = "Development:";
            // 
            // PopulationLabel
            // 
            this.PopulationLabel.Location = new System.Drawing.Point(6, 16);
            this.PopulationLabel.Name = "PopulationLabel";
            this.PopulationLabel.Size = new System.Drawing.Size(198, 16);
            this.PopulationLabel.TabIndex = 0;
            this.PopulationLabel.Text = "Population:";
            // 
            // FarmsButton
            // 
            this.FarmsButton.Location = new System.Drawing.Point(640, 128);
            this.FarmsButton.Name = "FarmsButton";
            this.FarmsButton.Size = new System.Drawing.Size(108, 23);
            this.FarmsButton.TabIndex = 8;
            this.FarmsButton.Text = "Farms\r\n";
            this.FarmsButton.UseVisualStyleBackColor = true;
            this.FarmsButton.Click += new System.EventHandler(this.FarmsButton_Click);
            // 
            // FactoriesButton
            // 
            this.FactoriesButton.Location = new System.Drawing.Point(754, 128);
            this.FactoriesButton.Name = "FactoriesButton";
            this.FactoriesButton.Size = new System.Drawing.Size(100, 23);
            this.FactoriesButton.TabIndex = 9;
            this.FactoriesButton.Text = "Factories";
            this.FactoriesButton.UseVisualStyleBackColor = true;
            this.FactoriesButton.Click += new System.EventHandler(this.FactoriesButton_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(332, 458);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 11;
            this.button3.Text = "Terrain";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(413, 458);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 12;
            this.button4.Text = "Railroads";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(862, 513);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.FactoriesButton);
            this.Controls.Add(this.FarmsButton);
            this.Controls.Add(this.ProvinceOverview);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.PopButton);
            this.Controls.Add(this.ControlButton);
            this.Controls.Add(this.FortsButton);
            this.Controls.Add(this.DevButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Form1";
            this.Text = "For Vyeshal";
            this.ProvinceOverview.ResumeLayout(false);
            this.ProvinceOverview.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button DevButton;
        private System.Windows.Forms.Button FortsButton;
        private System.Windows.Forms.Button ControlButton;
        private System.Windows.Forms.Button PopButton;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.GroupBox ProvinceOverview;
        private System.Windows.Forms.Label PopulationLabel;
        private System.Windows.Forms.Label DevelopmentLabel;
        private System.Windows.Forms.Label FactoriesLabel;
        private System.Windows.Forms.Label FarmsLabel;
        private System.Windows.Forms.Button FarmsButton;
        private System.Windows.Forms.Button FactoriesButton;
        private System.Windows.Forms.Button MoreButton;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.FlowLayoutPanel Panel;
    }
}

