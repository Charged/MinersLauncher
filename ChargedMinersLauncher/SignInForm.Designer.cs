namespace ChargedMinersLauncher {
    sealed partial class SignInForm {
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
            this.bSignIn = new System.Windows.Forms.Button();
            this.tabs = new TablessControl();
            this.tabSignIn = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.xRemember = new System.Windows.Forms.CheckBox();
            this.lPassword = new System.Windows.Forms.Label();
            this.tUsername = new System.Windows.Forms.TextBox();
            this.lUsername = new System.Windows.Forms.Label();
            this.tPassword = new System.Windows.Forms.TextBox();
            this.tabSignInProgress = new System.Windows.Forms.TabPage();
            this.bCancel = new System.Windows.Forms.Button();
            this.pbSigningIn = new System.Windows.Forms.ProgressBar();
            this.lSigningIn = new System.Windows.Forms.Label();
            this.tabUpdate = new System.Windows.Forms.TabPage();
            this.tabDownload = new System.Windows.Forms.TabPage();
            this.tabs.SuspendLayout();
            this.tabSignIn.SuspendLayout();
            this.tabSignInProgress.SuspendLayout();
            this.SuspendLayout();
            // 
            // bSignIn
            // 
            this.bSignIn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bSignIn.Enabled = false;
            this.bSignIn.Location = new System.Drawing.Point(170, 83);
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
            this.tabs.Controls.Add(this.tabSignInProgress);
            this.tabs.Controls.Add(this.tabUpdate);
            this.tabs.Controls.Add(this.tabDownload);
            this.tabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabs.Location = new System.Drawing.Point(0, 0);
            this.tabs.Name = "tabs";
            this.tabs.SelectedIndex = 0;
            this.tabs.Size = new System.Drawing.Size(261, 138);
            this.tabs.TabIndex = 7;
            // 
            // tabSignIn
            // 
            this.tabSignIn.BackColor = System.Drawing.SystemColors.Control;
            this.tabSignIn.Controls.Add(this.label1);
            this.tabSignIn.Controls.Add(this.xRemember);
            this.tabSignIn.Controls.Add(this.bSignIn);
            this.tabSignIn.Controls.Add(this.lPassword);
            this.tabSignIn.Controls.Add(this.tUsername);
            this.tabSignIn.Controls.Add(this.lUsername);
            this.tabSignIn.Controls.Add(this.tPassword);
            this.tabSignIn.Location = new System.Drawing.Point(4, 22);
            this.tabSignIn.Name = "tabSignIn";
            this.tabSignIn.Padding = new System.Windows.Forms.Padding(3);
            this.tabSignIn.Size = new System.Drawing.Size(253, 112);
            this.tabSignIn.TabIndex = 0;
            this.tabSignIn.Text = "Sign In";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(14, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(224, 37);
            this.label1.TabIndex = 0;
            this.label1.Text = "Charged-Miners";
            // 
            // xRemember
            // 
            this.xRemember.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.xRemember.AutoSize = true;
            this.xRemember.Location = new System.Drawing.Point(14, 87);
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
            this.lPassword.Location = new System.Drawing.Point(20, 59);
            this.lPassword.Name = "lPassword";
            this.lPassword.Size = new System.Drawing.Size(53, 13);
            this.lPassword.TabIndex = 3;
            this.lPassword.Text = "Password";
            // 
            // tUsername
            // 
            this.tUsername.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tUsername.Location = new System.Drawing.Point(79, 30);
            this.tUsername.Name = "tUsername";
            this.tUsername.Size = new System.Drawing.Size(166, 20);
            this.tUsername.TabIndex = 2;
            this.tUsername.TextChanged += new System.EventHandler(this.OnTextChanged);
            // 
            // lUsername
            // 
            this.lUsername.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lUsername.AutoSize = true;
            this.lUsername.Location = new System.Drawing.Point(20, 33);
            this.lUsername.Name = "lUsername";
            this.lUsername.Size = new System.Drawing.Size(55, 13);
            this.lUsername.TabIndex = 1;
            this.lUsername.Text = "Username";
            // 
            // tPassword
            // 
            this.tPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tPassword.Location = new System.Drawing.Point(79, 56);
            this.tPassword.Name = "tPassword";
            this.tPassword.Size = new System.Drawing.Size(166, 20);
            this.tPassword.TabIndex = 4;
            this.tPassword.UseSystemPasswordChar = true;
            this.tPassword.TextChanged += new System.EventHandler(this.OnTextChanged);
            // 
            // tabSignInProgress
            // 
            this.tabSignInProgress.BackColor = System.Drawing.SystemColors.Control;
            this.tabSignInProgress.Controls.Add(this.bCancel);
            this.tabSignInProgress.Controls.Add(this.pbSigningIn);
            this.tabSignInProgress.Controls.Add(this.lSigningIn);
            this.tabSignInProgress.Location = new System.Drawing.Point(4, 22);
            this.tabSignInProgress.Name = "tabSignInProgress";
            this.tabSignInProgress.Padding = new System.Windows.Forms.Padding(3);
            this.tabSignInProgress.Size = new System.Drawing.Size(253, 112);
            this.tabSignInProgress.TabIndex = 1;
            this.tabSignInProgress.Text = "Sign In Progress";
            // 
            // bCancel
            // 
            this.bCancel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bCancel.Location = new System.Drawing.Point(89, 81);
            this.bCancel.Name = "bCancel";
            this.bCancel.Size = new System.Drawing.Size(75, 23);
            this.bCancel.TabIndex = 2;
            this.bCancel.Text = "Cancel";
            this.bCancel.UseVisualStyleBackColor = true;
            this.bCancel.Click += new System.EventHandler(this.bCancel_Click);
            // 
            // pbSigningIn
            // 
            this.pbSigningIn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbSigningIn.Location = new System.Drawing.Point(6, 19);
            this.pbSigningIn.Name = "pbSigningIn";
            this.pbSigningIn.Size = new System.Drawing.Size(241, 23);
            this.pbSigningIn.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.pbSigningIn.TabIndex = 1;
            // 
            // lSigningIn
            // 
            this.lSigningIn.AutoSize = true;
            this.lSigningIn.Location = new System.Drawing.Point(3, 3);
            this.lSigningIn.Name = "lSigningIn";
            this.lSigningIn.Size = new System.Drawing.Size(93, 13);
            this.lSigningIn.TabIndex = 0;
            this.lSigningIn.Text = "Signing in as {0}...";
            // 
            // tabUpdate
            // 
            this.tabUpdate.BackColor = System.Drawing.SystemColors.Control;
            this.tabUpdate.Location = new System.Drawing.Point(4, 22);
            this.tabUpdate.Name = "tabUpdate";
            this.tabUpdate.Padding = new System.Windows.Forms.Padding(3);
            this.tabUpdate.Size = new System.Drawing.Size(253, 112);
            this.tabUpdate.TabIndex = 2;
            this.tabUpdate.Text = "Update";
            // 
            // tabDownload
            // 
            this.tabDownload.BackColor = System.Drawing.SystemColors.Control;
            this.tabDownload.Location = new System.Drawing.Point(4, 22);
            this.tabDownload.Name = "tabDownload";
            this.tabDownload.Padding = new System.Windows.Forms.Padding(3);
            this.tabDownload.Size = new System.Drawing.Size(253, 112);
            this.tabDownload.TabIndex = 3;
            this.tabDownload.Text = "Download";
            // 
            // SignInForm
            // 
            this.AcceptButton = this.bSignIn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(261, 138);
            this.Controls.Add(this.tabs);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SignInForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Charged-Miners Launcher";
            this.tabs.ResumeLayout(false);
            this.tabSignIn.ResumeLayout(false);
            this.tabSignIn.PerformLayout();
            this.tabSignInProgress.ResumeLayout(false);
            this.tabSignInProgress.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button bSignIn;
        private System.Windows.Forms.TextBox tUsername;
        private System.Windows.Forms.TextBox tPassword;
        private System.Windows.Forms.Label lUsername;
        private System.Windows.Forms.Label lPassword;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox xRemember;
        private TablessControl tabs;
        private System.Windows.Forms.TabPage tabSignIn;
        private System.Windows.Forms.TabPage tabSignInProgress;
        private System.Windows.Forms.TabPage tabUpdate;
        private System.Windows.Forms.TabPage tabDownload;
        private System.Windows.Forms.ProgressBar pbSigningIn;
        private System.Windows.Forms.Label lSigningIn;
        private System.Windows.Forms.Button bCancel;
    }
}

