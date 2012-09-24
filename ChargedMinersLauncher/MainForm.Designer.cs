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
            this.flow = new System.Windows.Forms.FlowLayoutPanel();
            this.panelSignIn = new System.Windows.Forms.Panel();
            this.bGo = new System.Windows.Forms.Button();
            this.lUri = new System.Windows.Forms.Label();
            this.tUri = new System.Windows.Forms.TextBox();
            this.lSignInStatus = new System.Windows.Forms.Label();
            this.xRemember = new System.Windows.Forms.CheckBox();
            this.bSignIn = new System.Windows.Forms.Button();
            this.lPassword = new System.Windows.Forms.Label();
            this.tUsername = new System.Windows.Forms.TextBox();
            this.lUsername = new System.Windows.Forms.Label();
            this.tPassword = new System.Windows.Forms.TextBox();
            this.panelStatus = new System.Windows.Forms.Panel();
            this.lStatus2 = new System.Windows.Forms.Label();
            this.bCancel = new System.Windows.Forms.Button();
            this.pbSigningIn = new System.Windows.Forms.ProgressBar();
            this.lStatus = new System.Windows.Forms.Label();
            this.panelUpdatePrompt = new System.Windows.Forms.Panel();
            this.bUpdateNo = new System.Windows.Forms.Button();
            this.bUpdateYes = new System.Windows.Forms.Button();
            this.lUpdatePrompt = new System.Windows.Forms.Label();
            this.flow.SuspendLayout();
            this.panelSignIn.SuspendLayout();
            this.panelStatus.SuspendLayout();
            this.panelUpdatePrompt.SuspendLayout();
            this.SuspendLayout();
            // 
            // lChargedMiners
            // 
            this.lChargedMiners.Dock = System.Windows.Forms.DockStyle.Top;
            this.lChargedMiners.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lChargedMiners.Location = new System.Drawing.Point(0, 0);
            this.lChargedMiners.Name = "lChargedMiners";
            this.lChargedMiners.Size = new System.Drawing.Size(286, 37);
            this.lChargedMiners.TabIndex = 0;
            this.lChargedMiners.Text = "Charged-Miners";
            this.lChargedMiners.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // flow
            // 
            this.flow.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flow.Controls.Add(this.panelSignIn);
            this.flow.Controls.Add(this.panelStatus);
            this.flow.Controls.Add(this.panelUpdatePrompt);
            this.flow.FlowDirection = System.Windows.Forms.FlowDirection.BottomUp;
            this.flow.Location = new System.Drawing.Point(0, 40);
            this.flow.Name = "flow";
            this.flow.Size = new System.Drawing.Size(286, 143);
            this.flow.TabIndex = 8;
            // 
            // panelSignIn
            // 
            this.panelSignIn.Controls.Add(this.bGo);
            this.panelSignIn.Controls.Add(this.lUri);
            this.panelSignIn.Controls.Add(this.tUri);
            this.panelSignIn.Controls.Add(this.lSignInStatus);
            this.panelSignIn.Controls.Add(this.xRemember);
            this.panelSignIn.Controls.Add(this.bSignIn);
            this.panelSignIn.Controls.Add(this.lPassword);
            this.panelSignIn.Controls.Add(this.tUsername);
            this.panelSignIn.Controls.Add(this.lUsername);
            this.panelSignIn.Controls.Add(this.tPassword);
            this.panelSignIn.Location = new System.Drawing.Point(3, 5);
            this.panelSignIn.Name = "panelSignIn";
            this.panelSignIn.Size = new System.Drawing.Size(280, 135);
            this.panelSignIn.TabIndex = 2;
            // 
            // bGo
            // 
            this.bGo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bGo.Enabled = false;
            this.bGo.Location = new System.Drawing.Point(247, 104);
            this.bGo.Name = "bGo";
            this.bGo.Size = new System.Drawing.Size(30, 23);
            this.bGo.TabIndex = 17;
            this.bGo.Text = "Go";
            this.bGo.UseVisualStyleBackColor = true;
            this.bGo.Click += new System.EventHandler(this.bGo_Click);
            // 
            // lUri
            // 
            this.lUri.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lUri.AutoSize = true;
            this.lUri.Location = new System.Drawing.Point(29, 109);
            this.lUri.Name = "lUri";
            this.lUri.Size = new System.Drawing.Size(29, 13);
            this.lUri.TabIndex = 16;
            this.lUri.Text = "URL";
            this.lUri.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // tUri
            // 
            this.tUri.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tUri.Location = new System.Drawing.Point(64, 106);
            this.tUri.Name = "tUri";
            this.tUri.Size = new System.Drawing.Size(177, 20);
            this.tUri.TabIndex = 15;
            this.tUri.TextChanged += new System.EventHandler(this.tURL_TextChanged);
            // 
            // lSignInStatus
            // 
            this.lSignInStatus.Dock = System.Windows.Forms.DockStyle.Top;
            this.lSignInStatus.ForeColor = System.Drawing.Color.Red;
            this.lSignInStatus.Location = new System.Drawing.Point(0, 0);
            this.lSignInStatus.Name = "lSignInStatus";
            this.lSignInStatus.Size = new System.Drawing.Size(280, 20);
            this.lSignInStatus.TabIndex = 14;
            this.lSignInStatus.Text = "SignInStatus";
            this.lSignInStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // xRemember
            // 
            this.xRemember.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.xRemember.AutoSize = true;
            this.xRemember.Location = new System.Drawing.Point(64, 79);
            this.xRemember.Name = "xRemember";
            this.xRemember.Size = new System.Drawing.Size(77, 17);
            this.xRemember.TabIndex = 13;
            this.xRemember.Text = "Remember";
            this.xRemember.UseVisualStyleBackColor = true;
            // 
            // bSignIn
            // 
            this.bSignIn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bSignIn.Enabled = false;
            this.bSignIn.Location = new System.Drawing.Point(197, 75);
            this.bSignIn.Name = "bSignIn";
            this.bSignIn.Size = new System.Drawing.Size(80, 23);
            this.bSignIn.TabIndex = 12;
            this.bSignIn.Text = "Sign In";
            this.bSignIn.UseVisualStyleBackColor = true;
            this.bSignIn.Click += new System.EventHandler(this.bSignIn_Click);
            // 
            // lPassword
            // 
            this.lPassword.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lPassword.AutoSize = true;
            this.lPassword.Location = new System.Drawing.Point(5, 52);
            this.lPassword.Name = "lPassword";
            this.lPassword.Size = new System.Drawing.Size(53, 13);
            this.lPassword.TabIndex = 10;
            this.lPassword.Text = "Password";
            // 
            // tUsername
            // 
            this.tUsername.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tUsername.Location = new System.Drawing.Point(64, 23);
            this.tUsername.Name = "tUsername";
            this.tUsername.Size = new System.Drawing.Size(213, 20);
            this.tUsername.TabIndex = 9;
            this.tUsername.TextChanged += new System.EventHandler(this.OnUsernameOrPasswordChanged);
            // 
            // lUsername
            // 
            this.lUsername.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lUsername.AutoSize = true;
            this.lUsername.Location = new System.Drawing.Point(3, 26);
            this.lUsername.Name = "lUsername";
            this.lUsername.Size = new System.Drawing.Size(55, 13);
            this.lUsername.TabIndex = 8;
            this.lUsername.Text = "Username";
            // 
            // tPassword
            // 
            this.tPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tPassword.Location = new System.Drawing.Point(64, 49);
            this.tPassword.Name = "tPassword";
            this.tPassword.Size = new System.Drawing.Size(213, 20);
            this.tPassword.TabIndex = 11;
            this.tPassword.UseSystemPasswordChar = true;
            this.tPassword.TextChanged += new System.EventHandler(this.OnUsernameOrPasswordChanged);
            // 
            // panelStatus
            // 
            this.panelStatus.Controls.Add(this.lStatus2);
            this.panelStatus.Controls.Add(this.bCancel);
            this.panelStatus.Controls.Add(this.pbSigningIn);
            this.panelStatus.Controls.Add(this.lStatus);
            this.panelStatus.Location = new System.Drawing.Point(289, 5);
            this.panelStatus.Name = "panelStatus";
            this.panelStatus.Size = new System.Drawing.Size(280, 135);
            this.panelStatus.TabIndex = 1;
            // 
            // lStatus2
            // 
            this.lStatus2.Dock = System.Windows.Forms.DockStyle.Top;
            this.lStatus2.Location = new System.Drawing.Point(0, 43);
            this.lStatus2.Name = "lStatus2";
            this.lStatus2.Size = new System.Drawing.Size(280, 20);
            this.lStatus2.TabIndex = 7;
            this.lStatus2.Text = "Status2";
            this.lStatus2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // bCancel
            // 
            this.bCancel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bCancel.Location = new System.Drawing.Point(102, 109);
            this.bCancel.Name = "bCancel";
            this.bCancel.Size = new System.Drawing.Size(76, 23);
            this.bCancel.TabIndex = 6;
            this.bCancel.Text = "Cancel";
            this.bCancel.UseVisualStyleBackColor = true;
            this.bCancel.Click += new System.EventHandler(this.bCancel_Click);
            // 
            // pbSigningIn
            // 
            this.pbSigningIn.Dock = System.Windows.Forms.DockStyle.Top;
            this.pbSigningIn.Location = new System.Drawing.Point(0, 20);
            this.pbSigningIn.Name = "pbSigningIn";
            this.pbSigningIn.Size = new System.Drawing.Size(280, 23);
            this.pbSigningIn.TabIndex = 5;
            // 
            // lStatus
            // 
            this.lStatus.Dock = System.Windows.Forms.DockStyle.Top;
            this.lStatus.Location = new System.Drawing.Point(0, 0);
            this.lStatus.Name = "lStatus";
            this.lStatus.Size = new System.Drawing.Size(280, 20);
            this.lStatus.TabIndex = 4;
            this.lStatus.Text = "Status";
            this.lStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelUpdatePrompt
            // 
            this.panelUpdatePrompt.Controls.Add(this.bUpdateNo);
            this.panelUpdatePrompt.Controls.Add(this.bUpdateYes);
            this.panelUpdatePrompt.Controls.Add(this.lUpdatePrompt);
            this.panelUpdatePrompt.Location = new System.Drawing.Point(575, 5);
            this.panelUpdatePrompt.Name = "panelUpdatePrompt";
            this.panelUpdatePrompt.Size = new System.Drawing.Size(280, 135);
            this.panelUpdatePrompt.TabIndex = 0;
            // 
            // bUpdateNo
            // 
            this.bUpdateNo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bUpdateNo.Location = new System.Drawing.Point(157, 109);
            this.bUpdateNo.Name = "bUpdateNo";
            this.bUpdateNo.Size = new System.Drawing.Size(120, 23);
            this.bUpdateNo.TabIndex = 5;
            this.bUpdateNo.Text = "No";
            this.bUpdateNo.UseVisualStyleBackColor = true;
            this.bUpdateNo.Click += new System.EventHandler(this.bUpdateNo_Click);
            // 
            // bUpdateYes
            // 
            this.bUpdateYes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bUpdateYes.Location = new System.Drawing.Point(3, 109);
            this.bUpdateYes.Name = "bUpdateYes";
            this.bUpdateYes.Size = new System.Drawing.Size(120, 23);
            this.bUpdateYes.TabIndex = 4;
            this.bUpdateYes.Text = "Yes, Update";
            this.bUpdateYes.UseVisualStyleBackColor = true;
            this.bUpdateYes.Click += new System.EventHandler(this.bUpdateYes_Click);
            // 
            // lUpdatePrompt
            // 
            this.lUpdatePrompt.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lUpdatePrompt.Location = new System.Drawing.Point(0, 0);
            this.lUpdatePrompt.Name = "lUpdatePrompt";
            this.lUpdatePrompt.Size = new System.Drawing.Size(280, 106);
            this.lUpdatePrompt.TabIndex = 1;
            this.lUpdatePrompt.Text = "An update for ChargedMiners is available.\r\nWould you like to download and apply i" +
    "t?";
            this.lUpdatePrompt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(286, 183);
            this.Controls.Add(this.flow);
            this.Controls.Add(this.lChargedMiners);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Charged-Miners Launcher";
            this.flow.ResumeLayout(false);
            this.panelSignIn.ResumeLayout(false);
            this.panelSignIn.PerformLayout();
            this.panelStatus.ResumeLayout(false);
            this.panelUpdatePrompt.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lChargedMiners;
        private System.Windows.Forms.FlowLayoutPanel flow;
        private System.Windows.Forms.Panel panelUpdatePrompt;
        private System.Windows.Forms.Button bUpdateNo;
        private System.Windows.Forms.Button bUpdateYes;
        private System.Windows.Forms.Label lUpdatePrompt;
        private System.Windows.Forms.Panel panelStatus;
        private System.Windows.Forms.Label lStatus2;
        private System.Windows.Forms.Button bCancel;
        private System.Windows.Forms.ProgressBar pbSigningIn;
        private System.Windows.Forms.Label lStatus;
        private System.Windows.Forms.Panel panelSignIn;
        private System.Windows.Forms.Label lSignInStatus;
        private System.Windows.Forms.CheckBox xRemember;
        private System.Windows.Forms.Button bSignIn;
        private System.Windows.Forms.Label lPassword;
        private System.Windows.Forms.TextBox tUsername;
        private System.Windows.Forms.Label lUsername;
        private System.Windows.Forms.TextBox tPassword;
        private System.Windows.Forms.TextBox tUri;
        private System.Windows.Forms.Label lUri;
        private System.Windows.Forms.Button bGo;
    }
}

