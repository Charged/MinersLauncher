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
            this.gGraphics = new System.Windows.Forms.GroupBox();
            this.lViewDistance = new System.Windows.Forms.Label();
            this.tbViewDistance = new System.Windows.Forms.TrackBar();
            this.xShadows = new System.Windows.Forms.CheckBox();
            this.xFog = new System.Windows.Forms.CheckBox();
            this.xAntiAlias = new System.Windows.Forms.CheckBox();
            this.gWindowed = new System.Windows.Forms.GroupBox();
            this.lWindowMode = new System.Windows.Forms.Label();
            this.cWindowMode = new System.Windows.Forms.ComboBox();
            this.lResolution = new System.Windows.Forms.Label();
            this.lX = new System.Windows.Forms.Label();
            this.nWinHeight = new System.Windows.Forms.NumericUpDown();
            this.nWinWidth = new System.Windows.Forms.NumericUpDown();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lKeySelector = new System.Windows.Forms.Label();
            this.bKeySelector = new System.Windows.Forms.Button();
            this.lKeyChat = new System.Windows.Forms.Label();
            this.bKeyChat = new System.Windows.Forms.Button();
            this.lKeyFlightMode = new System.Windows.Forms.Label();
            this.bKeyFlightMode = new System.Windows.Forms.Button();
            this.lKeyRun = new System.Windows.Forms.Label();
            this.bKeyRun = new System.Windows.Forms.Button();
            this.lKeyCrouch = new System.Windows.Forms.Label();
            this.bKeyCrouch = new System.Windows.Forms.Button();
            this.lKeyJump = new System.Windows.Forms.Label();
            this.bKeyJump = new System.Windows.Forms.Button();
            this.lKeyFlyDown = new System.Windows.Forms.Label();
            this.bKeyFlyDown = new System.Windows.Forms.Button();
            this.lKeyFlyUp = new System.Windows.Forms.Label();
            this.bKeyFlyUp = new System.Windows.Forms.Button();
            this.lKeyRight = new System.Windows.Forms.Label();
            this.lKeyLeft = new System.Windows.Forms.Label();
            this.bKeyRight = new System.Windows.Forms.Button();
            this.bKeyLeft = new System.Windows.Forms.Button();
            this.lKeyBackward = new System.Windows.Forms.Label();
            this.bKeyBackward = new System.Windows.Forms.Button();
            this.lKeyForward = new System.Windows.Forms.Label();
            this.bKeyForward = new System.Windows.Forms.Button();
            this.gGraphics.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbViewDistance)).BeginInit();
            this.gWindowed.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nWinHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nWinWidth)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // bDefaults
            // 
            this.bDefaults.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bDefaults.Location = new System.Drawing.Point(337, 252);
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
            this.bOK.Location = new System.Drawing.Point(516, 252);
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
            this.bCancel.Location = new System.Drawing.Point(435, 252);
            this.bCancel.Name = "bCancel";
            this.bCancel.Size = new System.Drawing.Size(75, 23);
            this.bCancel.TabIndex = 3;
            this.bCancel.Text = "Cancel";
            this.bCancel.UseVisualStyleBackColor = true;
            this.bCancel.Click += new System.EventHandler(this.bCancel_Click);
            // 
            // cResolutions
            // 
            this.cResolutions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cResolutions.Enabled = false;
            this.cResolutions.FormattingEnabled = true;
            this.cResolutions.Location = new System.Drawing.Point(100, 44);
            this.cResolutions.Name = "cResolutions";
            this.cResolutions.Size = new System.Drawing.Size(148, 21);
            this.cResolutions.TabIndex = 1;
            // 
            // gGraphics
            // 
            this.gGraphics.Controls.Add(this.lViewDistance);
            this.gGraphics.Controls.Add(this.tbViewDistance);
            this.gGraphics.Controls.Add(this.xShadows);
            this.gGraphics.Controls.Add(this.xFog);
            this.gGraphics.Controls.Add(this.xAntiAlias);
            this.gGraphics.Location = new System.Drawing.Point(337, 120);
            this.gGraphics.Name = "gGraphics";
            this.gGraphics.Padding = new System.Windows.Forms.Padding(3, 3, 3, 5);
            this.gGraphics.Size = new System.Drawing.Size(254, 126);
            this.gGraphics.TabIndex = 1;
            this.gGraphics.TabStop = false;
            this.gGraphics.Text = "Graphics Options";
            // 
            // lViewDistance
            // 
            this.lViewDistance.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lViewDistance.AutoSize = true;
            this.lViewDistance.Location = new System.Drawing.Point(6, 53);
            this.lViewDistance.Name = "lViewDistance";
            this.lViewDistance.Size = new System.Drawing.Size(93, 13);
            this.lViewDistance.TabIndex = 3;
            this.lViewDistance.Text = "View distance: {0}";
            // 
            // tbViewDistance
            // 
            this.tbViewDistance.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbViewDistance.LargeChange = 1;
            this.tbViewDistance.Location = new System.Drawing.Point(6, 69);
            this.tbViewDistance.Maximum = 24;
            this.tbViewDistance.Minimum = 1;
            this.tbViewDistance.Name = "tbViewDistance";
            this.tbViewDistance.Size = new System.Drawing.Size(242, 45);
            this.tbViewDistance.TabIndex = 4;
            this.tbViewDistance.Value = 8;
            this.tbViewDistance.Scroll += new System.EventHandler(this.tbViewDistance_Scroll);
            // 
            // xShadows
            // 
            this.xShadows.AutoSize = true;
            this.xShadows.Location = new System.Drawing.Point(100, 19);
            this.xShadows.Name = "xShadows";
            this.xShadows.Size = new System.Drawing.Size(70, 17);
            this.xShadows.TabIndex = 2;
            this.xShadows.Text = "Shadows";
            this.xShadows.UseVisualStyleBackColor = true;
            // 
            // xFog
            // 
            this.xFog.AutoSize = true;
            this.xFog.Location = new System.Drawing.Point(186, 19);
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
            // gWindowed
            // 
            this.gWindowed.Controls.Add(this.lWindowMode);
            this.gWindowed.Controls.Add(this.cWindowMode);
            this.gWindowed.Controls.Add(this.cResolutions);
            this.gWindowed.Controls.Add(this.lResolution);
            this.gWindowed.Controls.Add(this.lX);
            this.gWindowed.Controls.Add(this.nWinHeight);
            this.gWindowed.Controls.Add(this.nWinWidth);
            this.gWindowed.Location = new System.Drawing.Point(337, 12);
            this.gWindowed.Name = "gWindowed";
            this.gWindowed.Padding = new System.Windows.Forms.Padding(3, 3, 3, 5);
            this.gWindowed.Size = new System.Drawing.Size(254, 102);
            this.gWindowed.TabIndex = 0;
            this.gWindowed.TabStop = false;
            this.gWindowed.Text = "Resolution";
            // 
            // lWindowMode
            // 
            this.lWindowMode.AutoSize = true;
            this.lWindowMode.Location = new System.Drawing.Point(19, 20);
            this.lWindowMode.Name = "lWindowMode";
            this.lWindowMode.Size = new System.Drawing.Size(75, 13);
            this.lWindowMode.TabIndex = 6;
            this.lWindowMode.Text = "Window mode";
            // 
            // cWindowMode
            // 
            this.cWindowMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cWindowMode.FormattingEnabled = true;
            this.cWindowMode.Items.AddRange(new object[] {
            "Window (fixed)",
            "Window (resizable)",
            "Fullscreen"});
            this.cWindowMode.Location = new System.Drawing.Point(100, 17);
            this.cWindowMode.Name = "cWindowMode";
            this.cWindowMode.Size = new System.Drawing.Size(128, 21);
            this.cWindowMode.TabIndex = 5;
            // 
            // lResolution
            // 
            this.lResolution.AutoSize = true;
            this.lResolution.Location = new System.Drawing.Point(37, 47);
            this.lResolution.Name = "lResolution";
            this.lResolution.Size = new System.Drawing.Size(57, 13);
            this.lResolution.TabIndex = 1;
            this.lResolution.Text = "Resolution";
            // 
            // lX
            // 
            this.lX.AutoSize = true;
            this.lX.Location = new System.Drawing.Point(168, 73);
            this.lX.Name = "lX";
            this.lX.Size = new System.Drawing.Size(12, 13);
            this.lX.TabIndex = 3;
            this.lX.Text = "x";
            // 
            // nWinHeight
            // 
            this.nWinHeight.Location = new System.Drawing.Point(186, 71);
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
            this.nWinWidth.Location = new System.Drawing.Point(100, 71);
            this.nWinWidth.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nWinWidth.Name = "nWinWidth";
            this.nWinWidth.Size = new System.Drawing.Size(62, 20);
            this.nWinWidth.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lKeySelector);
            this.groupBox1.Controls.Add(this.bKeySelector);
            this.groupBox1.Controls.Add(this.lKeyChat);
            this.groupBox1.Controls.Add(this.bKeyChat);
            this.groupBox1.Controls.Add(this.lKeyFlightMode);
            this.groupBox1.Controls.Add(this.bKeyFlightMode);
            this.groupBox1.Controls.Add(this.lKeyRun);
            this.groupBox1.Controls.Add(this.bKeyRun);
            this.groupBox1.Controls.Add(this.lKeyCrouch);
            this.groupBox1.Controls.Add(this.bKeyCrouch);
            this.groupBox1.Controls.Add(this.lKeyJump);
            this.groupBox1.Controls.Add(this.bKeyJump);
            this.groupBox1.Controls.Add(this.lKeyFlyDown);
            this.groupBox1.Controls.Add(this.bKeyFlyDown);
            this.groupBox1.Controls.Add(this.lKeyFlyUp);
            this.groupBox1.Controls.Add(this.bKeyFlyUp);
            this.groupBox1.Controls.Add(this.lKeyRight);
            this.groupBox1.Controls.Add(this.lKeyLeft);
            this.groupBox1.Controls.Add(this.bKeyRight);
            this.groupBox1.Controls.Add(this.bKeyLeft);
            this.groupBox1.Controls.Add(this.lKeyBackward);
            this.groupBox1.Controls.Add(this.bKeyBackward);
            this.groupBox1.Controls.Add(this.lKeyForward);
            this.groupBox1.Controls.Add(this.bKeyForward);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(319, 263);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Controls";
            // 
            // lKeySelector
            // 
            this.lKeySelector.AutoSize = true;
            this.lKeySelector.Location = new System.Drawing.Point(168, 237);
            this.lKeySelector.Name = "lKeySelector";
            this.lKeySelector.Size = new System.Drawing.Size(64, 13);
            this.lKeySelector.TabIndex = 23;
            this.lKeySelector.Text = "Block Menu";
            // 
            // bKeySelector
            // 
            this.bKeySelector.Location = new System.Drawing.Point(238, 232);
            this.bKeySelector.Name = "bKeySelector";
            this.bKeySelector.Size = new System.Drawing.Size(75, 23);
            this.bKeySelector.TabIndex = 22;
            this.bKeySelector.Text = "B";
            this.bKeySelector.UseVisualStyleBackColor = true;
            this.bKeySelector.Click += new System.EventHandler(this.KeyBinding_Click);
            // 
            // lKeyChat
            // 
            this.lKeyChat.AutoSize = true;
            this.lKeyChat.Location = new System.Drawing.Point(24, 237);
            this.lKeyChat.Name = "lKeyChat";
            this.lKeyChat.Size = new System.Drawing.Size(29, 13);
            this.lKeyChat.TabIndex = 21;
            this.lKeyChat.Text = "Chat";
            // 
            // bKeyChat
            // 
            this.bKeyChat.Location = new System.Drawing.Point(59, 232);
            this.bKeyChat.Name = "bKeyChat";
            this.bKeyChat.Size = new System.Drawing.Size(75, 23);
            this.bKeyChat.TabIndex = 20;
            this.bKeyChat.Text = "T";
            this.bKeyChat.UseVisualStyleBackColor = true;
            this.bKeyChat.Click += new System.EventHandler(this.KeyBinding_Click);
            // 
            // lKeyFlightMode
            // 
            this.lKeyFlightMode.AutoSize = true;
            this.lKeyFlightMode.Location = new System.Drawing.Point(164, 195);
            this.lKeyFlightMode.Name = "lKeyFlightMode";
            this.lKeyFlightMode.Size = new System.Drawing.Size(68, 13);
            this.lKeyFlightMode.TabIndex = 19;
            this.lKeyFlightMode.Text = "Toggle Flight";
            // 
            // bKeyFlightMode
            // 
            this.bKeyFlightMode.Location = new System.Drawing.Point(238, 190);
            this.bKeyFlightMode.Name = "bKeyFlightMode";
            this.bKeyFlightMode.Size = new System.Drawing.Size(75, 23);
            this.bKeyFlightMode.TabIndex = 18;
            this.bKeyFlightMode.Text = "Z";
            this.bKeyFlightMode.UseVisualStyleBackColor = true;
            this.bKeyFlightMode.Click += new System.EventHandler(this.KeyBinding_Click);
            // 
            // lKeyRun
            // 
            this.lKeyRun.AutoSize = true;
            this.lKeyRun.Location = new System.Drawing.Point(20, 195);
            this.lKeyRun.Name = "lKeyRun";
            this.lKeyRun.Size = new System.Drawing.Size(27, 13);
            this.lKeyRun.TabIndex = 17;
            this.lKeyRun.Text = "Run";
            // 
            // bKeyRun
            // 
            this.bKeyRun.Location = new System.Drawing.Point(58, 190);
            this.bKeyRun.Name = "bKeyRun";
            this.bKeyRun.Size = new System.Drawing.Size(75, 23);
            this.bKeyRun.TabIndex = 16;
            this.bKeyRun.Text = "Shift";
            this.bKeyRun.UseVisualStyleBackColor = true;
            this.bKeyRun.Click += new System.EventHandler(this.KeyBinding_Click);
            // 
            // lKeyCrouch
            // 
            this.lKeyCrouch.AutoSize = true;
            this.lKeyCrouch.Location = new System.Drawing.Point(11, 166);
            this.lKeyCrouch.Name = "lKeyCrouch";
            this.lKeyCrouch.Size = new System.Drawing.Size(41, 13);
            this.lKeyCrouch.TabIndex = 15;
            this.lKeyCrouch.Text = "Crouch";
            // 
            // bKeyCrouch
            // 
            this.bKeyCrouch.Location = new System.Drawing.Point(58, 161);
            this.bKeyCrouch.Name = "bKeyCrouch";
            this.bKeyCrouch.Size = new System.Drawing.Size(75, 23);
            this.bKeyCrouch.TabIndex = 14;
            this.bKeyCrouch.Text = "Left Control";
            this.bKeyCrouch.UseVisualStyleBackColor = true;
            this.bKeyCrouch.Click += new System.EventHandler(this.KeyBinding_Click);
            // 
            // lKeyJump
            // 
            this.lKeyJump.AutoSize = true;
            this.lKeyJump.Location = new System.Drawing.Point(20, 137);
            this.lKeyJump.Name = "lKeyJump";
            this.lKeyJump.Size = new System.Drawing.Size(32, 13);
            this.lKeyJump.TabIndex = 13;
            this.lKeyJump.Text = "Jump";
            // 
            // bKeyJump
            // 
            this.bKeyJump.Location = new System.Drawing.Point(58, 132);
            this.bKeyJump.Name = "bKeyJump";
            this.bKeyJump.Size = new System.Drawing.Size(75, 23);
            this.bKeyJump.TabIndex = 12;
            this.bKeyJump.Text = "Space";
            this.bKeyJump.UseVisualStyleBackColor = true;
            this.bKeyJump.Click += new System.EventHandler(this.KeyBinding_Click);
            // 
            // lKeyFlyDown
            // 
            this.lKeyFlyDown.AutoSize = true;
            this.lKeyFlyDown.Location = new System.Drawing.Point(181, 165);
            this.lKeyFlyDown.Name = "lKeyFlyDown";
            this.lKeyFlyDown.Size = new System.Drawing.Size(51, 13);
            this.lKeyFlyDown.TabIndex = 11;
            this.lKeyFlyDown.Text = "Fly Down";
            // 
            // bKeyFlyDown
            // 
            this.bKeyFlyDown.Location = new System.Drawing.Point(238, 161);
            this.bKeyFlyDown.Name = "bKeyFlyDown";
            this.bKeyFlyDown.Size = new System.Drawing.Size(75, 23);
            this.bKeyFlyDown.TabIndex = 10;
            this.bKeyFlyDown.Text = "Q";
            this.bKeyFlyDown.UseVisualStyleBackColor = true;
            this.bKeyFlyDown.Click += new System.EventHandler(this.KeyBinding_Click);
            // 
            // lKeyFlyUp
            // 
            this.lKeyFlyUp.AutoSize = true;
            this.lKeyFlyUp.Location = new System.Drawing.Point(195, 137);
            this.lKeyFlyUp.Name = "lKeyFlyUp";
            this.lKeyFlyUp.Size = new System.Drawing.Size(37, 13);
            this.lKeyFlyUp.TabIndex = 9;
            this.lKeyFlyUp.Text = "Fly Up";
            // 
            // bKeyFlyUp
            // 
            this.bKeyFlyUp.Location = new System.Drawing.Point(238, 132);
            this.bKeyFlyUp.Name = "bKeyFlyUp";
            this.bKeyFlyUp.Size = new System.Drawing.Size(75, 23);
            this.bKeyFlyUp.TabIndex = 8;
            this.bKeyFlyUp.Text = "E";
            this.bKeyFlyUp.UseVisualStyleBackColor = true;
            this.bKeyFlyUp.Click += new System.EventHandler(this.KeyBinding_Click);
            // 
            // lKeyRight
            // 
            this.lKeyRight.AutoSize = true;
            this.lKeyRight.Location = new System.Drawing.Point(224, 94);
            this.lKeyRight.Name = "lKeyRight";
            this.lKeyRight.Size = new System.Drawing.Size(32, 13);
            this.lKeyRight.TabIndex = 7;
            this.lKeyRight.Text = "Right";
            // 
            // lKeyLeft
            // 
            this.lKeyLeft.AutoSize = true;
            this.lKeyLeft.Location = new System.Drawing.Point(66, 94);
            this.lKeyLeft.Name = "lKeyLeft";
            this.lKeyLeft.Size = new System.Drawing.Size(25, 13);
            this.lKeyLeft.TabIndex = 6;
            this.lKeyLeft.Text = "Left";
            // 
            // bKeyRight
            // 
            this.bKeyRight.Location = new System.Drawing.Point(203, 63);
            this.bKeyRight.Name = "bKeyRight";
            this.bKeyRight.Size = new System.Drawing.Size(75, 23);
            this.bKeyRight.TabIndex = 5;
            this.bKeyRight.Text = "D";
            this.bKeyRight.UseVisualStyleBackColor = true;
            this.bKeyRight.Click += new System.EventHandler(this.KeyBinding_Click);
            // 
            // bKeyLeft
            // 
            this.bKeyLeft.Location = new System.Drawing.Point(41, 63);
            this.bKeyLeft.Name = "bKeyLeft";
            this.bKeyLeft.Size = new System.Drawing.Size(75, 23);
            this.bKeyLeft.TabIndex = 4;
            this.bKeyLeft.Text = "A";
            this.bKeyLeft.UseVisualStyleBackColor = true;
            this.bKeyLeft.Click += new System.EventHandler(this.KeyBinding_Click);
            // 
            // lKeyBackward
            // 
            this.lKeyBackward.AutoSize = true;
            this.lKeyBackward.Location = new System.Drawing.Point(132, 94);
            this.lKeyBackward.Name = "lKeyBackward";
            this.lKeyBackward.Size = new System.Drawing.Size(55, 13);
            this.lKeyBackward.TabIndex = 3;
            this.lKeyBackward.Text = "Backward";
            // 
            // bKeyBackward
            // 
            this.bKeyBackward.Location = new System.Drawing.Point(122, 63);
            this.bKeyBackward.Name = "bKeyBackward";
            this.bKeyBackward.Size = new System.Drawing.Size(75, 23);
            this.bKeyBackward.TabIndex = 2;
            this.bKeyBackward.Text = "S";
            this.bKeyBackward.UseVisualStyleBackColor = true;
            this.bKeyBackward.Click += new System.EventHandler(this.KeyBinding_Click);
            // 
            // lKeyForward
            // 
            this.lKeyForward.AutoSize = true;
            this.lKeyForward.Location = new System.Drawing.Point(137, 16);
            this.lKeyForward.Name = "lKeyForward";
            this.lKeyForward.Size = new System.Drawing.Size(45, 13);
            this.lKeyForward.TabIndex = 1;
            this.lKeyForward.Text = "Forward";
            // 
            // bKeyForward
            // 
            this.bKeyForward.Location = new System.Drawing.Point(122, 32);
            this.bKeyForward.Name = "bKeyForward";
            this.bKeyForward.Size = new System.Drawing.Size(75, 23);
            this.bKeyForward.TabIndex = 0;
            this.bKeyForward.Text = "W";
            this.bKeyForward.UseVisualStyleBackColor = true;
            this.bKeyForward.Click += new System.EventHandler(this.KeyBinding_Click);
            // 
            // SettingsForm
            // 
            this.AcceptButton = this.bOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.bCancel;
            this.ClientSize = new System.Drawing.Size(603, 287);
            this.Controls.Add(this.groupBox1);
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
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button bDefaults;
        private System.Windows.Forms.Button bOK;
        private System.Windows.Forms.Button bCancel;
        private System.Windows.Forms.ComboBox cResolutions;
        private System.Windows.Forms.GroupBox gGraphics;
        private System.Windows.Forms.GroupBox gWindowed;
        private System.Windows.Forms.Label lX;
        private System.Windows.Forms.NumericUpDown nWinHeight;
        private System.Windows.Forms.NumericUpDown nWinWidth;
        private System.Windows.Forms.Label lResolution;
        private System.Windows.Forms.TrackBar tbViewDistance;
        private System.Windows.Forms.Label lViewDistance;
        private System.Windows.Forms.CheckBox xShadows;
        private System.Windows.Forms.CheckBox xFog;
        private System.Windows.Forms.CheckBox xAntiAlias;
        private System.Windows.Forms.Label lWindowMode;
        private System.Windows.Forms.ComboBox cWindowMode;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button bKeyForward;
        private System.Windows.Forms.Label lKeyForward;
        private System.Windows.Forms.Label lKeyRight;
        private System.Windows.Forms.Label lKeyLeft;
        private System.Windows.Forms.Button bKeyRight;
        private System.Windows.Forms.Button bKeyLeft;
        private System.Windows.Forms.Label lKeyBackward;
        private System.Windows.Forms.Button bKeyBackward;
        private System.Windows.Forms.Label lKeyFlyDown;
        private System.Windows.Forms.Button bKeyFlyDown;
        private System.Windows.Forms.Label lKeyFlyUp;
        private System.Windows.Forms.Button bKeyFlyUp;
        private System.Windows.Forms.Label lKeySelector;
        private System.Windows.Forms.Button bKeySelector;
        private System.Windows.Forms.Label lKeyChat;
        private System.Windows.Forms.Button bKeyChat;
        private System.Windows.Forms.Label lKeyFlightMode;
        private System.Windows.Forms.Button bKeyFlightMode;
        private System.Windows.Forms.Label lKeyRun;
        private System.Windows.Forms.Button bKeyRun;
        private System.Windows.Forms.Label lKeyCrouch;
        private System.Windows.Forms.Button bKeyCrouch;
        private System.Windows.Forms.Label lKeyJump;
        private System.Windows.Forms.Button bKeyJump;
    }
}