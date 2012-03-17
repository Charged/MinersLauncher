namespace ChargedMinersLauncher {
    partial class SettingsForm {
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
            this.gFullscreen = new System.Windows.Forms.GroupBox();
            this.xFullscreen = new System.Windows.Forms.CheckBox();
            this.gWindowed = new System.Windows.Forms.GroupBox();
            this.lWindowSize = new System.Windows.Forms.Label();
            this.nX = new System.Windows.Forms.Label();
            this.nWinHeight = new System.Windows.Forms.NumericUpDown();
            this.nWinWidth = new System.Windows.Forms.NumericUpDown();
            this.gFullscreen.SuspendLayout();
            this.gWindowed.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nWinHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nWinWidth)).BeginInit();
            this.SuspendLayout();
            // 
            // bDefaults
            // 
            this.bDefaults.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bDefaults.Location = new System.Drawing.Point( 12, 148 );
            this.bDefaults.Name = "bDefaults";
            this.bDefaults.Size = new System.Drawing.Size( 75, 23 );
            this.bDefaults.TabIndex = 0;
            this.bDefaults.Text = "Defaults";
            this.bDefaults.UseVisualStyleBackColor = true;
            this.bDefaults.Click += new System.EventHandler( this.ResetDefaults );
            // 
            // bOK
            // 
            this.bOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bOK.Location = new System.Drawing.Point( 216, 148 );
            this.bOK.Name = "bOK";
            this.bOK.Size = new System.Drawing.Size( 75, 23 );
            this.bOK.TabIndex = 1;
            this.bOK.Text = "OK";
            this.bOK.UseVisualStyleBackColor = true;
            this.bOK.Click += new System.EventHandler( this.bOK_Click );
            // 
            // bCancel
            // 
            this.bCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bCancel.Location = new System.Drawing.Point( 135, 148 );
            this.bCancel.Name = "bCancel";
            this.bCancel.Size = new System.Drawing.Size( 75, 23 );
            this.bCancel.TabIndex = 2;
            this.bCancel.Text = "Cancel";
            this.bCancel.UseVisualStyleBackColor = true;
            this.bCancel.Click += new System.EventHandler( this.bCancel_Click );
            // 
            // cResolutions
            // 
            this.cResolutions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cResolutions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cResolutions.Enabled = false;
            this.cResolutions.FormattingEnabled = true;
            this.cResolutions.Location = new System.Drawing.Point( 164, 19 );
            this.cResolutions.Name = "cResolutions";
            this.cResolutions.Size = new System.Drawing.Size( 109, 21 );
            this.cResolutions.TabIndex = 4;
            // 
            // xResizableWindow
            // 
            this.xResizableWindow.AutoSize = true;
            this.xResizableWindow.Location = new System.Drawing.Point( 6, 20 );
            this.xResizableWindow.Name = "xResizableWindow";
            this.xResizableWindow.Size = new System.Drawing.Size( 251, 17 );
            this.xResizableWindow.TabIndex = 5;
            this.xResizableWindow.Text = "Resizable window (may not work on all systems)";
            this.xResizableWindow.UseVisualStyleBackColor = true;
            // 
            // gFullscreen
            // 
            this.gFullscreen.Controls.Add( this.xFullscreen );
            this.gFullscreen.Controls.Add( this.cResolutions );
            this.gFullscreen.Location = new System.Drawing.Point( 12, 89 );
            this.gFullscreen.Name = "gFullscreen";
            this.gFullscreen.Padding = new System.Windows.Forms.Padding( 3, 3, 3, 5 );
            this.gFullscreen.Size = new System.Drawing.Size( 279, 48 );
            this.gFullscreen.TabIndex = 6;
            this.gFullscreen.TabStop = false;
            this.gFullscreen.Text = "Fullscreen Settings";
            // 
            // xFullscreen
            // 
            this.xFullscreen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.xFullscreen.AutoSize = true;
            this.xFullscreen.Location = new System.Drawing.Point( 6, 21 );
            this.xFullscreen.Name = "xFullscreen";
            this.xFullscreen.Size = new System.Drawing.Size( 94, 17 );
            this.xFullscreen.TabIndex = 8;
            this.xFullscreen.Text = "Run fullscreen";
            this.xFullscreen.UseVisualStyleBackColor = true;
            this.xFullscreen.CheckedChanged += new System.EventHandler( this.xFullscreen_CheckedChanged );
            // 
            // gWindowed
            // 
            this.gWindowed.Controls.Add( this.lWindowSize );
            this.gWindowed.Controls.Add( this.nX );
            this.gWindowed.Controls.Add( this.nWinHeight );
            this.gWindowed.Controls.Add( this.nWinWidth );
            this.gWindowed.Controls.Add( this.xResizableWindow );
            this.gWindowed.Location = new System.Drawing.Point( 12, 12 );
            this.gWindowed.Name = "gWindowed";
            this.gWindowed.Padding = new System.Windows.Forms.Padding( 3, 3, 3, 5 );
            this.gWindowed.Size = new System.Drawing.Size( 279, 71 );
            this.gWindowed.TabIndex = 7;
            this.gWindowed.TabStop = false;
            this.gWindowed.Text = "Windowed Mode Settings";
            // 
            // lWindowSize
            // 
            this.lWindowSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lWindowSize.AutoSize = true;
            this.lWindowSize.Location = new System.Drawing.Point( 52, 45 );
            this.lWindowSize.Name = "lWindowSize";
            this.lWindowSize.Size = new System.Drawing.Size( 67, 13 );
            this.lWindowSize.TabIndex = 7;
            this.lWindowSize.Text = "Window size";
            // 
            // nX
            // 
            this.nX.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.nX.AutoSize = true;
            this.nX.Location = new System.Drawing.Point( 193, 45 );
            this.nX.Name = "nX";
            this.nX.Size = new System.Drawing.Size( 12, 13 );
            this.nX.TabIndex = 6;
            this.nX.Text = "x";
            // 
            // nWinHeight
            // 
            this.nWinHeight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.nWinHeight.Location = new System.Drawing.Point( 211, 43 );
            this.nWinHeight.Maximum = new decimal( new int[] {
            10000,
            0,
            0,
            0} );
            this.nWinHeight.Name = "nWinHeight";
            this.nWinHeight.Size = new System.Drawing.Size( 62, 20 );
            this.nWinHeight.TabIndex = 2;
            // 
            // nWinWidth
            // 
            this.nWinWidth.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.nWinWidth.Location = new System.Drawing.Point( 125, 43 );
            this.nWinWidth.Maximum = new decimal( new int[] {
            10000,
            0,
            0,
            0} );
            this.nWinWidth.Name = "nWinWidth";
            this.nWinWidth.Size = new System.Drawing.Size( 62, 20 );
            this.nWinWidth.TabIndex = 1;
            // 
            // OptionsForm
            // 
            this.AcceptButton = this.bOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.bCancel;
            this.ClientSize = new System.Drawing.Size( 303, 183 );
            this.Controls.Add( this.gWindowed );
            this.Controls.Add( this.gFullscreen );
            this.Controls.Add( this.bCancel );
            this.Controls.Add( this.bOK );
            this.Controls.Add( this.bDefaults );
            this.Name = "OptionsForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Settings";
            this.gFullscreen.ResumeLayout( false );
            this.gFullscreen.PerformLayout();
            this.gWindowed.ResumeLayout( false );
            this.gWindowed.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nWinHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nWinWidth)).EndInit();
            this.ResumeLayout( false );

        }

        #endregion

        private System.Windows.Forms.Button bDefaults;
        private System.Windows.Forms.Button bOK;
        private System.Windows.Forms.Button bCancel;
        private System.Windows.Forms.ComboBox cResolutions;
        private System.Windows.Forms.CheckBox xResizableWindow;
        private System.Windows.Forms.GroupBox gFullscreen;
        private System.Windows.Forms.GroupBox gWindowed;
        private System.Windows.Forms.Label nX;
        private System.Windows.Forms.NumericUpDown nWinHeight;
        private System.Windows.Forms.NumericUpDown nWinWidth;
        private System.Windows.Forms.Label lWindowSize;
        private System.Windows.Forms.CheckBox xFullscreen;
    }
}