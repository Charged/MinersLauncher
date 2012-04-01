namespace ChargedMinersLauncher {
    sealed partial class SettingsForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose( bool disposing ) {
            if( disposing && (components != null) ) {
                components.Dispose();
            }
            base.Dispose( disposing );
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.bDefaults = new System.Windows.Forms.Button();
            this.bOK = new System.Windows.Forms.Button();
            this.bCancel = new System.Windows.Forms.Button();
            this.cResolutions = new System.Windows.Forms.ComboBox();
            this.xResizableWindow = new System.Windows.Forms.CheckBox();
            this.gGraphics = new System.Windows.Forms.GroupBox();
            this.tbViewDistance = new System.Windows.Forms.TrackBar();
            this.lViewDistance = new System.Windows.Forms.Label();
            this.xShadows = new System.Windows.Forms.CheckBox();
            this.xFog = new System.Windows.Forms.CheckBox();
            this.xAntiAlias = new System.Windows.Forms.CheckBox();
            this.xFullscreen = new System.Windows.Forms.CheckBox();
            this.gWindowed = new System.Windows.Forms.GroupBox();
            this.lWindowSize = new System.Windows.Forms.Label();
            this.lX = new System.Windows.Forms.Label();
            this.nWinHeight = new System.Windows.Forms.NumericUpDown();
            this.nWinWidth = new System.Windows.Forms.NumericUpDown();
            this.gGraphics.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbViewDistance)).BeginInit();
            this.gWindowed.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nWinHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nWinWidth)).BeginInit();
            this.SuspendLayout();
            // 
            // bDefaults
            // 
            this.bDefaults.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bDefaults.Location = new System.Drawing.Point(12, 211);
            this.bDefaults.Name = "bDefaults";
            this.bDefaults.Size = new System.Drawing.Size(75, 23);
            this.bDefaults.TabIndex = 4;
            this.bDefaults.Text = "Defaults";
            this.bDefaults.UseVisualStyleBackColor = true;
            this.bDefaults.Click += new System.EventHandler(this.ResetDefaults);
            // 
            // bOK
            // 
            this.bOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bOK.Location = new System.Drawing.Point(217, 211);
            this.bOK.Name = "bOK";
            this.bOK.Size = new System.Drawing.Size(75, 23);
            this.bOK.TabIndex = 2;
            this.bOK.Text = "OK";
            this.bOK.UseVisualStyleBackColor = true;
            this.bOK.Click += new System.EventHandler(this.bOK_Click);
            // 
            // bCancel
            // 
            this.bCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bCancel.Location = new System.Drawing.Point(136, 211);
            this.bCancel.Name = "bCancel";
            this.bCancel.Size = new System.Drawing.Size(75, 23);
            this.bCancel.TabIndex = 3;
            this.bCancel.Text = "Cancel";
            this.bCancel.UseVisualStyleBackColor = true;
            this.bCancel.Click += new System.EventHandler(this.bCancel_Click);
            // 
            // cResolutions
            // 
            this.cResolutions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cResolutions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cResolutions.Enabled = false;
            this.cResolutions.FormattingEnabled = true;
            this.cResolutions.Location = new System.Drawing.Point(165, 19);
            this.cResolutions.Name = "cResolutions";
            this.cResolutions.Size = new System.Drawing.Size(109, 21);
            this.cResolutions.TabIndex = 1;
            // 
            // xResizableWindow
            // 
            this.xResizableWindow.AutoSize = true;
            this.xResizableWindow.Location = new System.Drawing.Point(6, 46);
            this.xResizableWindow.Name = "xResizableWindow";
            this.xResizableWindow.Size = new System.Drawing.Size(251, 17);
            this.xResizableWindow.TabIndex = 0;
            this.xResizableWindow.Text = "Resizable window (may not work on all systems)";
            this.xResizableWindow.UseVisualStyleBackColor = true;
            // 
            // gGraphics
            // 
            this.gGraphics.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gGraphics.Controls.Add(this.tbViewDistance);
            this.gGraphics.Controls.Add(this.lViewDistance);
            this.gGraphics.Controls.Add(this.xShadows);
            this.gGraphics.Controls.Add(this.xFog);
            this.gGraphics.Controls.Add(this.xAntiAlias);
            this.gGraphics.Location = new System.Drawing.Point(12, 115);
            this.gGraphics.Name = "gGraphics";
            this.gGraphics.Padding = new System.Windows.Forms.Padding(3, 3, 3, 5);
            this.gGraphics.Size = new System.Drawing.Size(280, 90);
            this.gGraphics.TabIndex = 1;
            this.gGraphics.TabStop = false;
            this.gGraphics.Text = "Graphics Settings";
            // 
            // tbViewDistance
            // 
            this.tbViewDistance.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbViewDistance.LargeChange = 1;
            this.tbViewDistance.Location = new System.Drawing.Point(94, 32);
            this.tbViewDistance.Maximum = 24;
            this.tbViewDistance.Minimum = 1;
            this.tbViewDistance.Name = "tbViewDistance";
            this.tbViewDistance.Size = new System.Drawing.Size(179, 45);
            this.tbViewDistance.TabIndex = 4;
            this.tbViewDistance.Value = 8;
            this.tbViewDistance.Scroll += new System.EventHandler(this.tbViewDistance_Scroll);
            // 
            // lViewDistance
            // 
            this.lViewDistance.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lViewDistance.AutoSize = true;
            this.lViewDistance.Location = new System.Drawing.Point(161, 16);
            this.lViewDistance.Name = "lViewDistance";
            this.lViewDistance.Size = new System.Drawing.Size(93, 13);
            this.lViewDistance.TabIndex = 3;
            this.lViewDistance.Text = "View distance: {0}";
            // 
            // xShadows
            // 
            this.xShadows.AutoSize = true;
            this.xShadows.Location = new System.Drawing.Point(6, 65);
            this.xShadows.Name = "xShadows";
            this.xShadows.Size = new System.Drawing.Size(70, 17);
            this.xShadows.TabIndex = 2;
            this.xShadows.Text = "Shadows";
            this.xShadows.UseVisualStyleBackColor = true;
            // 
            // xFog
            // 
            this.xFog.AutoSize = true;
            this.xFog.Location = new System.Drawing.Point(6, 42);
            this.xFog.Name = "xFog";
            this.xFog.Size = new System.Drawing.Size(44, 17);
            this.xFog.TabIndex = 1;
            this.xFog.Text = "Fog";
            this.xFog.UseVisualStyleBackColor = true;
            // 
            // xAntiAlias
            // 
            this.xAntiAlias.AutoSize = true;
            this.xAntiAlias.Location = new System.Drawing.Point(6, 19);
            this.xAntiAlias.Name = "xAntiAlias";
            this.xAntiAlias.Size = new System.Drawing.Size(82, 17);
            this.xAntiAlias.TabIndex = 0;
            this.xAntiAlias.Text = "Anti-aliasing";
            this.xAntiAlias.UseVisualStyleBackColor = true;
            // 
            // xFullscreen
            // 
            this.xFullscreen.AutoSize = true;
            this.xFullscreen.Location = new System.Drawing.Point(6, 21);
            this.xFullscreen.Name = "xFullscreen";
            this.xFullscreen.Size = new System.Drawing.Size(94, 17);
            this.xFullscreen.TabIndex = 0;
            this.xFullscreen.Text = "Run fullscreen";
            this.xFullscreen.UseVisualStyleBackColor = true;
            this.xFullscreen.CheckedChanged += new System.EventHandler(this.xFullscreen_CheckedChanged);
            // 
            // gWindowed
            // 
            this.gWindowed.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gWindowed.Controls.Add(this.cResolutions);
            this.gWindowed.Controls.Add(this.xFullscreen);
            this.gWindowed.Controls.Add(this.lWindowSize);
            this.gWindowed.Controls.Add(this.lX);
            this.gWindowed.Controls.Add(this.nWinHeight);
            this.gWindowed.Controls.Add(this.nWinWidth);
            this.gWindowed.Controls.Add(this.xResizableWindow);
            this.gWindowed.Location = new System.Drawing.Point(12, 12);
            this.gWindowed.Name = "gWindowed";
            this.gWindowed.Padding = new System.Windows.Forms.Padding(3, 3, 3, 5);
            this.gWindowed.Size = new System.Drawing.Size(280, 97);
            this.gWindowed.TabIndex = 0;
            this.gWindowed.TabStop = false;
            this.gWindowed.Text = "Windowed Mode Settings";
            // 
            // lWindowSize
            // 
            this.lWindowSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lWindowSize.AutoSize = true;
            this.lWindowSize.Location = new System.Drawing.Point(53, 71);
            this.lWindowSize.Name = "lWindowSize";
            this.lWindowSize.Size = new System.Drawing.Size(67, 13);
            this.lWindowSize.TabIndex = 1;
            this.lWindowSize.Text = "Window size";
            // 
            // lX
            // 
            this.lX.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lX.AutoSize = true;
            this.lX.Location = new System.Drawing.Point(194, 71);
            this.lX.Name = "lX";
            this.lX.Size = new System.Drawing.Size(12, 13);
            this.lX.TabIndex = 3;
            this.lX.Text = "x";
            // 
            // nWinHeight
            // 
            this.nWinHeight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.nWinHeight.Location = new System.Drawing.Point(212, 69);
            this.nWinHeight.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nWinHeight.Name = "nWinHeight";
            this.nWinHeight.Size = new System.Drawing.Size(62, 20);
            this.nWinHeight.TabIndex = 4;
            // 
            // nWinWidth
            // 
            this.nWinWidth.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.nWinWidth.Location = new System.Drawing.Point(126, 69);
            this.nWinWidth.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nWinWidth.Name = "nWinWidth";
            this.nWinWidth.Size = new System.Drawing.Size(62, 20);
            this.nWinWidth.TabIndex = 2;
            // 
            // SettingsForm
            // 
            this.AcceptButton = this.bOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.bCancel;
            this.ClientSize = new System.Drawing.Size(304, 246);
            this.Controls.Add(this.gWindowed);
            this.Controls.Add(this.gGraphics);
            this.Controls.Add(this.bCancel);
            this.Controls.Add(this.bOK);
            this.Controls.Add(this.bDefaults);
            this.Name = "SettingsForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Settings";
            this.gGraphics.ResumeLayout(false);
            this.gGraphics.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbViewDistance)).EndInit();
            this.gWindowed.ResumeLayout(false);
            this.gWindowed.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nWinHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nWinWidth)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button bDefaults;
        private System.Windows.Forms.Button bOK;
        private System.Windows.Forms.Button bCancel;
        private System.Windows.Forms.ComboBox cResolutions;
        private System.Windows.Forms.CheckBox xResizableWindow;
        private System.Windows.Forms.GroupBox gGraphics;
        private System.Windows.Forms.GroupBox gWindowed;
        private System.Windows.Forms.Label lX;
        private System.Windows.Forms.NumericUpDown nWinHeight;
        private System.Windows.Forms.NumericUpDown nWinWidth;
        private System.Windows.Forms.Label lWindowSize;
        private System.Windows.Forms.CheckBox xFullscreen;
        private System.Windows.Forms.TrackBar tbViewDistance;
        private System.Windows.Forms.Label lViewDistance;
        private System.Windows.Forms.CheckBox xShadows;
        private System.Windows.Forms.CheckBox xFog;
        private System.Windows.Forms.CheckBox xAntiAlias;
    }
}