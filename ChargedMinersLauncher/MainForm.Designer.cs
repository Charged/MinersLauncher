namespace ChargedMinersLauncher {
    sealed partial class MainForm {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.lChargedMiners = new System.Windows.Forms.Label();
            this.bSignIn = new System.Windows.Forms.Button();
            this.tabs = new TablessControl();
            this.tabProgress = new System.Windows.Forms.TabPage();
            this.lStatus2 = new System.Windows.Forms.Label();
            this.bCancel = new System.Windows.Forms.Button();
            this.pbSigningIn = new System.Windows.Forms.ProgressBar();
            this.lStatus = new System.Windows.Forms.Label();
            this.tabUpdate = new System.Windows.Forms.TabPage();
            this.bUpdateNo = new System.Windows.Forms.Button();
            this.bUpdateYes = new System.Windows.Forms.Button();
            this.lUpdatePrompt = new System.Windows.Forms.Label();
            this.tabSignIn = new System.Windows.Forms.TabPage();
            this.lSignInStatus = new System.Windows.Forms.Label();
            this.xRemember = new System.Windows.Forms.CheckBox();
            this.lPassword = new System.Windows.Forms.Label();
            this.tUsername = new System.Windows.Forms.TextBox();
            this.lUsername = new System.Windows.Forms.Label();
            this.tPassword = new System.Windows.Forms.TextBox();
            this.tabs.SuspendLayout();
            this.tabProgress.SuspendLayout();
            this.tabUpdate.SuspendLayout();
            this.tabSignIn.SuspendLayout();
            this.SuspendLayout();
            // 
            // lChargedMiners
            // 
            this.lChargedMiners.Dock = System.Windows.Forms.DockStyle.Top;
            this.lChargedMiners.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lChargedMiners.Location = new System.Drawing.Point(0, 0);
            this.lChargedMiners.Name = "lChargedMiners";
            this.lChargedMiners.Size = new System.Drawing.Size(302, 37);
            this.lChargedMiners.TabIndex = 0;
            this.lChargedMiners.Text = "Charged-Miners";
            this.lChargedMiners.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // bSignIn
            // 
            this.bSignIn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bSignIn.Enabled = false;
            this.bSignIn.Location = new System.Drawing.Point(213, 133);
            this.bSignIn.Name = "bSignIn";
            this.bSignIn.Size = new System.Drawing.Size(75, 23);
            this.bSignIn.TabIndex = 5;
            this.bSignIn.Text = "Sign In";
            this.bSignIn.UseVisualStyleBackColor = true;
            this.bSignIn.Click += new System.EventHandler(this.bSignIn_Click);
            // 
            // tabs
            // 
            this.tabs.Controls.Add(this.tabSignIn);
            this.tabs.Controls.Add(this.tabProgress);
            this.tabs.Controls.Add(this.tabUpdate);
            this.tabs.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tabs.Location = new System.Drawing.Point(0, 40);
            this.tabs.Name = "tabs";
            this.tabs.SelectedIndex = 0;
            this.tabs.Size = new System.Drawing.Size(302, 188);
            this.tabs.TabIndex = 7;
            // 
            // tabProgress
            // 
            this.tabProgress.BackColor = System.Drawing.SystemColors.Control;
            this.tabProgress.Controls.Add(this.lStatus2);
            this.tabProgress.Controls.Add(this.bCancel);
            this.tabProgress.Controls.Add(this.pbSigningIn);
            this.tabProgress.Controls.Add(this.lStatus);
            this.tabProgress.Location = new System.Drawing.Point(4, 22);
            this.tabProgress.Name = "tabProgress";
            this.tabProgress.Padding = new System.Windows.Forms.Padding(3);
            this.tabProgress.Size = new System.Drawing.Size(294, 162);
            this.tabProgress.TabIndex = 1;
            this.tabProgress.Text = "Progress";
            // 
            // lStatus2
            // 
            this.lStatus2.Dock = System.Windows.Forms.DockStyle.Top;
            this.lStatus2.Location = new System.Drawing.Point(3, 46);
            this.lStatus2.Name = "lStatus2";
            this.lStatus2.Size = new System.Drawing.Size(288, 20);
            this.lStatus2.TabIndex = 3;
            this.lStatus2.Text = "Status2";
            this.lStatus2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // bCancel
            // 
            this.bCancel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bCancel.Location = new System.Drawing.Point(89, 133);
            this.bCancel.Name = "bCancel";
            this.bCancel.Size = new System.Drawing.Size(116, 23);
            this.bCancel.TabIndex = 2;
            this.bCancel.Text = "Cancel";
            this.bCancel.UseVisualStyleBackColor = true;
            this.bCancel.Click += new System.EventHandler(this.bCancel_Click);
            // 
            // pbSigningIn
            // 
            this.pbSigningIn.Dock = System.Windows.Forms.DockStyle.Top;
            this.pbSigningIn.Location = new System.Drawing.Point(3, 23);
            this.pbSigningIn.Name = "pbSigningIn";
            this.pbSigningIn.Size = new System.Drawing.Size(288, 23);
            this.pbSigningIn.TabIndex = 1;
            // 
            // lStatus
            // 
            this.lStatus.Dock = System.Windows.Forms.DockStyle.Top;
            this.lStatus.Location = new System.Drawing.Point(3, 3);
            this.lStatus.Name = "lStatus";
            this.lStatus.Size = new System.Drawing.Size(288, 20);
            this.lStatus.TabIndex = 0;
            this.lStatus.Text = "Status";
            this.lStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tabUpdate
            // 
            this.tabUpdate.BackColor = System.Drawing.SystemColors.Control;
            this.tabUpdate.Controls.Add(this.bUpdateNo);
            this.tabUpdate.Controls.Add(this.bUpdateYes);
            this.tabUpdate.Controls.Add(this.lUpdatePrompt);
            this.tabUpdate.Location = new System.Drawing.Point(4, 22);
            this.tabUpdate.Name = "tabUpdate";
            this.tabUpdate.Padding = new System.Windows.Forms.Padding(3);
            this.tabUpdate.Size = new System.Drawing.Size(294, 162);
            this.tabUpdate.TabIndex = 2;
            this.tabUpdate.Text = "UpdatePrompt";
            // 
            // bUpdateNo
            // 
            this.bUpdateNo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bUpdateNo.Location = new System.Drawing.Point(168, 133);
            this.bUpdateNo.Name = "bUpdateNo";
            this.bUpdateNo.Size = new System.Drawing.Size(120, 23);
            this.bUpdateNo.TabIndex = 3;
            this.bUpdateNo.Text = "No";
            this.bUpdateNo.UseVisualStyleBackColor = true;
            this.bUpdateNo.Click += new System.EventHandler(this.bUpdateNo_Click);
            // 
            // bUpdateYes
            // 
            this.bUpdateYes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bUpdateYes.Location = new System.Drawing.Point(6, 133);
            this.bUpdateYes.Name = "bUpdateYes";
            this.bUpdateYes.Size = new System.Drawing.Size(120, 23);
            this.bUpdateYes.TabIndex = 1;
            this.bUpdateYes.Text = "Yes, Update";
            this.bUpdateYes.UseVisualStyleBackColor = true;
            this.bUpdateYes.Click += new System.EventHandler(this.bUpdateYes_Click);
            // 
            // lUpdatePrompt
            // 
            this.lUpdatePrompt.Dock = System.Windows.Forms.DockStyle.Top;
            this.lUpdatePrompt.Location = new System.Drawing.Point(3, 3);
            this.lUpdatePrompt.Name = "lUpdatePrompt";
            this.lUpdatePrompt.Size = new System.Drawing.Size(288, 26);
            this.lUpdatePrompt.TabIndex = 0;
            this.lUpdatePrompt.Text = "An update for ChargedMiners is available.\r\nWould you like to download and apply i" +
    "t?";
            this.lUpdatePrompt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tabSignIn
            // 
            this.tabSignIn.BackColor = System.Drawing.SystemColors.Control;
            this.tabSignIn.Controls.Add(this.lSignInStatus);
            this.tabSignIn.Controls.Add(this.xRemember);
            this.tabSignIn.Controls.Add(this.bSignIn);
            this.tabSignIn.Controls.Add(this.lPassword);
            this.tabSignIn.Controls.Add(this.tUsername);
            this.tabSignIn.Controls.Add(this.lUsername);
            this.tabSignIn.Controls.Add(this.tPassword);
            this.tabSignIn.Location = new System.Drawing.Point(4, 22);
            this.tabSignIn.Name = "tabSignIn";
            this.tabSignIn.Padding = new System.Windows.Forms.Padding(3);
            this.tabSignIn.Size = new System.Drawing.Size(294, 162);
            this.tabSignIn.TabIndex = 0;
            this.tabSignIn.Text = "Sign In";
            // 
            // lSignInStatus
            // 
            this.lSignInStatus.Dock = System.Windows.Forms.DockStyle.Top;
            this.lSignInStatus.ForeColor = System.Drawing.Color.Red;
            this.lSignInStatus.Location = new System.Drawing.Point(3, 3);
            this.lSignInStatus.Name = "lSignInStatus";
            this.lSignInStatus.Size = new System.Drawing.Size(288, 20);
            this.lSignInStatus.TabIndex = 7;
            this.lSignInStatus.Text = "SignInStatus";
            this.lSignInStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // xRemember
            // 
            this.xRemember.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.xRemember.AutoSize = true;
            this.xRemember.Location = new System.Drawing.Point(81, 137);
            this.xRemember.Name = "xRemember";
            this.xRemember.Size = new System.Drawing.Size(77, 17);
            this.xRemember.TabIndex = 6;
            this.xRemember.Text = "Remember";
            this.xRemember.UseVisualStyleBackColor = true;
            // 
            // lPassword
            // 
            this.lPassword.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lPassword.AutoSize = true;
            this.lPassword.Location = new System.Drawing.Point(22, 109);
            this.lPassword.Name = "lPassword";
            this.lPassword.Size = new System.Drawing.Size(53, 13);
            this.lPassword.TabIndex = 3;
            this.lPassword.Text = "Password";
            // 
            // tUsername
            // 
            this.tUsername.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tUsername.Location = new System.Drawing.Point(81, 80);
            this.tUsername.Name = "tUsername";
            this.tUsername.Size = new System.Drawing.Size(207, 20);
            this.tUsername.TabIndex = 2;
            this.tUsername.TextChanged += new System.EventHandler(this.OnUsernameOrPasswordChanged);
            // 
            // lUsername
            // 
            this.lUsername.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lUsername.AutoSize = true;
            this.lUsername.Location = new System.Drawing.Point(22, 83);
            this.lUsername.Name = "lUsername";
            this.lUsername.Size = new System.Drawing.Size(55, 13);
            this.lUsername.TabIndex = 1;
            this.lUsername.Text = "Username";
            // 
            // tPassword
            // 
            this.tPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tPassword.Location = new System.Drawing.Point(81, 106);
            this.tPassword.Name = "tPassword";
            this.tPassword.Size = new System.Drawing.Size(207, 20);
            this.tPassword.TabIndex = 4;
            this.tPassword.UseSystemPasswordChar = true;
            this.tPassword.TextChanged += new System.EventHandler(this.OnUsernameOrPasswordChanged);
            // 
            // MainForm
            // 
            this.AcceptButton = this.bSignIn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(302, 228);
            this.Controls.Add(this.lChargedMiners);
            this.Controls.Add(this.tabs);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Charged-Miners Launcher";
            this.tabs.ResumeLayout(false);
            this.tabProgress.ResumeLayout(false);
            this.tabUpdate.ResumeLayout(false);
            this.tabSignIn.ResumeLayout(false);
            this.tabSignIn.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button bSignIn;
        private System.Windows.Forms.TextBox tUsername;
        private System.Windows.Forms.TextBox tPassword;
        private System.Windows.Forms.Label lUsername;
        private System.Windows.Forms.Label lPassword;
        private System.Windows.Forms.Label lChargedMiners;
        private System.Windows.Forms.CheckBox xRemember;
        private TablessControl tabs;
        private System.Windows.Forms.TabPage tabSignIn;
        private System.Windows.Forms.TabPage tabProgress;
        private System.Windows.Forms.TabPage tabUpdate;
        private System.Windows.Forms.ProgressBar pbSigningIn;
        private System.Windows.Forms.Label lStatus;
        private System.Windows.Forms.Button bCancel;
        private System.Windows.Forms.Button bUpdateNo;
        private System.Windows.Forms.Button bUpdateYes;
        private System.Windows.Forms.Label lUpdatePrompt;
        private System.Windows.Forms.Label lStatus2;
        private System.Windows.Forms.Label lSignInStatus;
    }
}

