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
            this.flow = new System.Windows.Forms.FlowLayoutPanel();
            this.tabs = new System.Windows.Forms.TabControl();
            this.tabSignIn = new System.Windows.Forms.TabPage();
            this.cSignInUsername = new System.Windows.Forms.ComboBox();
            this.lSignInStatus = new System.Windows.Forms.Label();
            this.tSignInUrl = new System.Windows.Forms.TextBox();
            this.lSignInUrl = new System.Windows.Forms.Label();
            this.tSignInPassword = new System.Windows.Forms.TextBox();
            this.lSignInUsername = new System.Windows.Forms.Label();
            this.lSignInPassword = new System.Windows.Forms.Label();
            this.bSignIn = new System.Windows.Forms.Button();
            this.tabResume = new System.Windows.Forms.TabPage();
            this.tResumeUri = new System.Windows.Forms.TextBox();
            this.lResumeUri = new System.Windows.Forms.Label();
            this.bResume = new System.Windows.Forms.Button();
            this.tResumeServerName = new System.Windows.Forms.TextBox();
            this.lResumeServerName = new System.Windows.Forms.Label();
            this.tResumeServerIP = new System.Windows.Forms.TextBox();
            this.tResumeUsername = new System.Windows.Forms.TextBox();
            this.lResumeServerIP = new System.Windows.Forms.Label();
            this.lResumeUsername = new System.Windows.Forms.Label();
            this.tabDirect = new System.Windows.Forms.TabPage();
            this.lDirectStatus = new System.Windows.Forms.Label();
            this.tDirectServerIP = new System.Windows.Forms.TextBox();
            this.tDirectUsername = new System.Windows.Forms.TextBox();
            this.lDirectServerIP = new System.Windows.Forms.Label();
            this.lDirectUsername = new System.Windows.Forms.Label();
            this.bDirectConnect = new System.Windows.Forms.Button();
            this.tDirectUrl = new System.Windows.Forms.TextBox();
            this.lDirectUrl = new System.Windows.Forms.Label();
            this.tabOptions = new System.Windows.Forms.TabPage();
            this.xFailSafe = new System.Windows.Forms.CheckBox();
            this.xMultiUser = new System.Windows.Forms.CheckBox();
            this.lOptionsSaved = new System.Windows.Forms.Label();
            this.lGameUpdates = new System.Windows.Forms.Label();
            this.cGameUpdates = new System.Windows.Forms.ComboBox();
            this.xRememberServer = new System.Windows.Forms.CheckBox();
            this.xRememberPassword = new System.Windows.Forms.CheckBox();
            this.lStartingTab = new System.Windows.Forms.Label();
            this.cStartingTab = new System.Windows.Forms.ComboBox();
            this.xRememberUsername = new System.Windows.Forms.CheckBox();
            this.tabTools = new System.Windows.Forms.TabPage();
            this.bForgetActiveAccount = new System.Windows.Forms.Button();
            this.lToolStatus = new System.Windows.Forms.Label();
            this.tPastebinUrl = new System.Windows.Forms.TextBox();
            this.bDeleteData = new System.Windows.Forms.Button();
            this.bResetSettings = new System.Windows.Forms.Button();
            this.bUploadLog = new System.Windows.Forms.Button();
            this.bOpenDataDir = new System.Windows.Forms.Button();
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
            this.tabs.SuspendLayout();
            this.tabSignIn.SuspendLayout();
            this.tabResume.SuspendLayout();
            this.tabDirect.SuspendLayout();
            this.tabOptions.SuspendLayout();
            this.tabTools.SuspendLayout();
            this.panelStatus.SuspendLayout();
            this.panelUpdatePrompt.SuspendLayout();
            this.SuspendLayout();
            // 
            // flow
            // 
            this.flow.Controls.Add(this.tabs);
            this.flow.Controls.Add(this.panelStatus);
            this.flow.Controls.Add(this.panelUpdatePrompt);
            this.flow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flow.FlowDirection = System.Windows.Forms.FlowDirection.BottomUp;
            this.flow.Location = new System.Drawing.Point(0, 0);
            this.flow.Name = "flow";
            this.flow.Size = new System.Drawing.Size(280, 187);
            this.flow.TabIndex = 8;
            // 
            // tabs
            // 
            this.tabs.Controls.Add(this.tabSignIn);
            this.tabs.Controls.Add(this.tabResume);
            this.tabs.Controls.Add(this.tabDirect);
            this.tabs.Controls.Add(this.tabOptions);
            this.tabs.Controls.Add(this.tabTools);
            this.tabs.Location = new System.Drawing.Point(0, 2);
            this.tabs.Margin = new System.Windows.Forms.Padding(0);
            this.tabs.Name = "tabs";
            this.tabs.SelectedIndex = 0;
            this.tabs.Size = new System.Drawing.Size(280, 185);
            this.tabs.TabIndex = 0;
            this.tabs.SelectedIndexChanged += new System.EventHandler(this.tabs_SelectedIndexChanged);
            // 
            // tabSignIn
            // 
            this.tabSignIn.Controls.Add(this.cSignInUsername);
            this.tabSignIn.Controls.Add(this.lSignInStatus);
            this.tabSignIn.Controls.Add(this.tSignInUrl);
            this.tabSignIn.Controls.Add(this.lSignInUrl);
            this.tabSignIn.Controls.Add(this.tSignInPassword);
            this.tabSignIn.Controls.Add(this.lSignInUsername);
            this.tabSignIn.Controls.Add(this.lSignInPassword);
            this.tabSignIn.Controls.Add(this.bSignIn);
            this.tabSignIn.Location = new System.Drawing.Point(4, 22);
            this.tabSignIn.Name = "tabSignIn";
            this.tabSignIn.Padding = new System.Windows.Forms.Padding(3);
            this.tabSignIn.Size = new System.Drawing.Size(272, 159);
            this.tabSignIn.TabIndex = 0;
            this.tabSignIn.Text = "Sign In";
            this.tabSignIn.UseVisualStyleBackColor = true;
            // 
            // cSignInUsername
            // 
            this.cSignInUsername.FormattingEnabled = true;
            this.cSignInUsername.Location = new System.Drawing.Point(79, 6);
            this.cSignInUsername.Name = "cSignInUsername";
            this.cSignInUsername.Size = new System.Drawing.Size(187, 21);
            this.cSignInUsername.TabIndex = 1;
            this.cSignInUsername.SelectedIndexChanged += new System.EventHandler(this.cSignInUsername_SelectedIndexChanged);
            this.cSignInUsername.TextChanged += new System.EventHandler(this.cSignInUsername_TextChanged);
            // 
            // lSignInStatus
            // 
            this.lSignInStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lSignInStatus.ForeColor = System.Drawing.Color.Red;
            this.lSignInStatus.Location = new System.Drawing.Point(6, 87);
            this.lSignInStatus.Name = "lSignInStatus";
            this.lSignInStatus.Size = new System.Drawing.Size(260, 40);
            this.lSignInStatus.TabIndex = 7;
            this.lSignInStatus.Text = "lSignInStatus";
            this.lSignInStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tSignInUrl
            // 
            this.tSignInUrl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tSignInUrl.Location = new System.Drawing.Point(79, 58);
            this.tSignInUrl.Name = "tSignInUrl";
            this.tSignInUrl.Size = new System.Drawing.Size(187, 20);
            this.tSignInUrl.TabIndex = 5;
            this.tSignInUrl.TextChanged += new System.EventHandler(this.SignInFieldChanged);
            // 
            // lSignInUrl
            // 
            this.lSignInUrl.AutoSize = true;
            this.lSignInUrl.Location = new System.Drawing.Point(23, 61);
            this.lSignInUrl.Name = "lSignInUrl";
            this.lSignInUrl.Size = new System.Drawing.Size(50, 26);
            this.lSignInUrl.TabIndex = 4;
            this.lSignInUrl.Text = "URL\r\n(optional)";
            this.lSignInUrl.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // tSignInPassword
            // 
            this.tSignInPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tSignInPassword.Location = new System.Drawing.Point(79, 32);
            this.tSignInPassword.Name = "tSignInPassword";
            this.tSignInPassword.Size = new System.Drawing.Size(187, 20);
            this.tSignInPassword.TabIndex = 3;
            this.tSignInPassword.UseSystemPasswordChar = true;
            this.tSignInPassword.TextChanged += new System.EventHandler(this.SignInFieldChanged);
            // 
            // lSignInUsername
            // 
            this.lSignInUsername.AutoSize = true;
            this.lSignInUsername.Location = new System.Drawing.Point(20, 9);
            this.lSignInUsername.Name = "lSignInUsername";
            this.lSignInUsername.Size = new System.Drawing.Size(55, 13);
            this.lSignInUsername.TabIndex = 0;
            this.lSignInUsername.Text = "Username";
            // 
            // lSignInPassword
            // 
            this.lSignInPassword.AutoSize = true;
            this.lSignInPassword.Location = new System.Drawing.Point(20, 35);
            this.lSignInPassword.Name = "lSignInPassword";
            this.lSignInPassword.Size = new System.Drawing.Size(53, 13);
            this.lSignInPassword.TabIndex = 2;
            this.lSignInPassword.Text = "Password";
            // 
            // bSignIn
            // 
            this.bSignIn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bSignIn.Enabled = false;
            this.bSignIn.Location = new System.Drawing.Point(186, 130);
            this.bSignIn.Name = "bSignIn";
            this.bSignIn.Size = new System.Drawing.Size(80, 23);
            this.bSignIn.TabIndex = 6;
            this.bSignIn.Text = "Sign In";
            this.bSignIn.UseVisualStyleBackColor = true;
            this.bSignIn.Click += new System.EventHandler(this.bSignIn_Click);
            // 
            // tabResume
            // 
            this.tabResume.Controls.Add(this.tResumeUri);
            this.tabResume.Controls.Add(this.lResumeUri);
            this.tabResume.Controls.Add(this.bResume);
            this.tabResume.Controls.Add(this.tResumeServerName);
            this.tabResume.Controls.Add(this.lResumeServerName);
            this.tabResume.Controls.Add(this.tResumeServerIP);
            this.tabResume.Controls.Add(this.tResumeUsername);
            this.tabResume.Controls.Add(this.lResumeServerIP);
            this.tabResume.Controls.Add(this.lResumeUsername);
            this.tabResume.Location = new System.Drawing.Point(4, 22);
            this.tabResume.Name = "tabResume";
            this.tabResume.Padding = new System.Windows.Forms.Padding(3);
            this.tabResume.Size = new System.Drawing.Size(272, 159);
            this.tabResume.TabIndex = 1;
            this.tabResume.Text = "Resume";
            this.tabResume.UseVisualStyleBackColor = true;
            // 
            // tResumeUri
            // 
            this.tResumeUri.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tResumeUri.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tResumeUri.Location = new System.Drawing.Point(79, 9);
            this.tResumeUri.Name = "tResumeUri";
            this.tResumeUri.ReadOnly = true;
            this.tResumeUri.Size = new System.Drawing.Size(187, 13);
            this.tResumeUri.TabIndex = 2;
            // 
            // lResumeUri
            // 
            this.lResumeUri.AutoSize = true;
            this.lResumeUri.Location = new System.Drawing.Point(13, 9);
            this.lResumeUri.Name = "lResumeUri";
            this.lResumeUri.Size = new System.Drawing.Size(60, 13);
            this.lResumeUri.TabIndex = 1;
            this.lResumeUri.Text = "Direct URL";
            // 
            // bResume
            // 
            this.bResume.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bResume.Enabled = false;
            this.bResume.Location = new System.Drawing.Point(186, 130);
            this.bResume.Name = "bResume";
            this.bResume.Size = new System.Drawing.Size(80, 23);
            this.bResume.TabIndex = 0;
            this.bResume.Text = "Reconnect";
            this.bResume.UseVisualStyleBackColor = true;
            this.bResume.Click += new System.EventHandler(this.bResume_Click);
            // 
            // tResumeServerName
            // 
            this.tResumeServerName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tResumeServerName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tResumeServerName.Location = new System.Drawing.Point(79, 87);
            this.tResumeServerName.Name = "tResumeServerName";
            this.tResumeServerName.ReadOnly = true;
            this.tResumeServerName.Size = new System.Drawing.Size(187, 13);
            this.tResumeServerName.TabIndex = 8;
            // 
            // lResumeServerName
            // 
            this.lResumeServerName.AutoSize = true;
            this.lResumeServerName.Location = new System.Drawing.Point(6, 87);
            this.lResumeServerName.Name = "lResumeServerName";
            this.lResumeServerName.Size = new System.Drawing.Size(67, 13);
            this.lResumeServerName.TabIndex = 7;
            this.lResumeServerName.Text = "Server name";
            // 
            // tResumeServerIP
            // 
            this.tResumeServerIP.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tResumeServerIP.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tResumeServerIP.Location = new System.Drawing.Point(79, 35);
            this.tResumeServerIP.Name = "tResumeServerIP";
            this.tResumeServerIP.ReadOnly = true;
            this.tResumeServerIP.Size = new System.Drawing.Size(187, 13);
            this.tResumeServerIP.TabIndex = 4;
            // 
            // tResumeUsername
            // 
            this.tResumeUsername.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tResumeUsername.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tResumeUsername.Location = new System.Drawing.Point(79, 61);
            this.tResumeUsername.Name = "tResumeUsername";
            this.tResumeUsername.ReadOnly = true;
            this.tResumeUsername.Size = new System.Drawing.Size(187, 13);
            this.tResumeUsername.TabIndex = 6;
            this.tResumeUsername.Text = " ";
            // 
            // lResumeServerIP
            // 
            this.lResumeServerIP.AutoSize = true;
            this.lResumeServerIP.Location = new System.Drawing.Point(22, 35);
            this.lResumeServerIP.Name = "lResumeServerIP";
            this.lResumeServerIP.Size = new System.Drawing.Size(51, 13);
            this.lResumeServerIP.TabIndex = 3;
            this.lResumeServerIP.Text = "Server IP";
            // 
            // lResumeUsername
            // 
            this.lResumeUsername.AutoSize = true;
            this.lResumeUsername.Location = new System.Drawing.Point(18, 61);
            this.lResumeUsername.Name = "lResumeUsername";
            this.lResumeUsername.Size = new System.Drawing.Size(55, 13);
            this.lResumeUsername.TabIndex = 5;
            this.lResumeUsername.Text = "Username";
            // 
            // tabDirect
            // 
            this.tabDirect.Controls.Add(this.lDirectStatus);
            this.tabDirect.Controls.Add(this.tDirectServerIP);
            this.tabDirect.Controls.Add(this.tDirectUsername);
            this.tabDirect.Controls.Add(this.lDirectServerIP);
            this.tabDirect.Controls.Add(this.lDirectUsername);
            this.tabDirect.Controls.Add(this.bDirectConnect);
            this.tabDirect.Controls.Add(this.tDirectUrl);
            this.tabDirect.Controls.Add(this.lDirectUrl);
            this.tabDirect.Location = new System.Drawing.Point(4, 22);
            this.tabDirect.Name = "tabDirect";
            this.tabDirect.Padding = new System.Windows.Forms.Padding(3);
            this.tabDirect.Size = new System.Drawing.Size(272, 159);
            this.tabDirect.TabIndex = 2;
            this.tabDirect.Text = "Direct";
            this.tabDirect.UseVisualStyleBackColor = true;
            // 
            // lDirectStatus
            // 
            this.lDirectStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lDirectStatus.ForeColor = System.Drawing.Color.Red;
            this.lDirectStatus.Location = new System.Drawing.Point(6, 81);
            this.lDirectStatus.Name = "lDirectStatus";
            this.lDirectStatus.Size = new System.Drawing.Size(260, 45);
            this.lDirectStatus.TabIndex = 7;
            this.lDirectStatus.Text = "lDirectStatus";
            this.lDirectStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tDirectServerIP
            // 
            this.tDirectServerIP.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tDirectServerIP.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tDirectServerIP.Location = new System.Drawing.Point(79, 35);
            this.tDirectServerIP.Name = "tDirectServerIP";
            this.tDirectServerIP.ReadOnly = true;
            this.tDirectServerIP.Size = new System.Drawing.Size(187, 13);
            this.tDirectServerIP.TabIndex = 4;
            // 
            // tDirectUsername
            // 
            this.tDirectUsername.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tDirectUsername.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tDirectUsername.Location = new System.Drawing.Point(79, 61);
            this.tDirectUsername.Name = "tDirectUsername";
            this.tDirectUsername.ReadOnly = true;
            this.tDirectUsername.Size = new System.Drawing.Size(187, 13);
            this.tDirectUsername.TabIndex = 6;
            // 
            // lDirectServerIP
            // 
            this.lDirectServerIP.AutoSize = true;
            this.lDirectServerIP.Location = new System.Drawing.Point(22, 35);
            this.lDirectServerIP.Name = "lDirectServerIP";
            this.lDirectServerIP.Size = new System.Drawing.Size(51, 13);
            this.lDirectServerIP.TabIndex = 3;
            this.lDirectServerIP.Text = "Server IP";
            // 
            // lDirectUsername
            // 
            this.lDirectUsername.AutoSize = true;
            this.lDirectUsername.Location = new System.Drawing.Point(18, 61);
            this.lDirectUsername.Name = "lDirectUsername";
            this.lDirectUsername.Size = new System.Drawing.Size(55, 13);
            this.lDirectUsername.TabIndex = 5;
            this.lDirectUsername.Text = "Username";
            // 
            // bDirectConnect
            // 
            this.bDirectConnect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bDirectConnect.Enabled = false;
            this.bDirectConnect.Location = new System.Drawing.Point(186, 130);
            this.bDirectConnect.Name = "bDirectConnect";
            this.bDirectConnect.Size = new System.Drawing.Size(80, 23);
            this.bDirectConnect.TabIndex = 2;
            this.bDirectConnect.Text = "Connect";
            this.bDirectConnect.UseVisualStyleBackColor = true;
            this.bDirectConnect.Click += new System.EventHandler(this.bDirectConnect_Click);
            // 
            // tDirectUrl
            // 
            this.tDirectUrl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tDirectUrl.Location = new System.Drawing.Point(79, 6);
            this.tDirectUrl.Name = "tDirectUrl";
            this.tDirectUrl.Size = new System.Drawing.Size(187, 20);
            this.tDirectUrl.TabIndex = 1;
            this.tDirectUrl.TextChanged += new System.EventHandler(this.tDirectUrl_TextChanged);
            // 
            // lDirectUrl
            // 
            this.lDirectUrl.AutoSize = true;
            this.lDirectUrl.Location = new System.Drawing.Point(13, 9);
            this.lDirectUrl.Name = "lDirectUrl";
            this.lDirectUrl.Size = new System.Drawing.Size(60, 13);
            this.lDirectUrl.TabIndex = 0;
            this.lDirectUrl.Text = "Direct URL";
            this.lDirectUrl.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // tabOptions
            // 
            this.tabOptions.Controls.Add(this.xFailSafe);
            this.tabOptions.Controls.Add(this.xMultiUser);
            this.tabOptions.Controls.Add(this.lOptionsSaved);
            this.tabOptions.Controls.Add(this.lGameUpdates);
            this.tabOptions.Controls.Add(this.cGameUpdates);
            this.tabOptions.Controls.Add(this.xRememberServer);
            this.tabOptions.Controls.Add(this.xRememberPassword);
            this.tabOptions.Controls.Add(this.lStartingTab);
            this.tabOptions.Controls.Add(this.cStartingTab);
            this.tabOptions.Controls.Add(this.xRememberUsername);
            this.tabOptions.Location = new System.Drawing.Point(4, 22);
            this.tabOptions.Name = "tabOptions";
            this.tabOptions.Padding = new System.Windows.Forms.Padding(3);
            this.tabOptions.Size = new System.Drawing.Size(272, 159);
            this.tabOptions.TabIndex = 3;
            this.tabOptions.Text = "Options";
            this.tabOptions.UseVisualStyleBackColor = true;
            // 
            // xFailSafe
            // 
            this.xFailSafe.AutoSize = true;
            this.xFailSafe.Location = new System.Drawing.Point(172, 60);
            this.xFailSafe.Name = "xFailSafe";
            this.xFailSafe.Size = new System.Drawing.Size(94, 17);
            this.xFailSafe.TabIndex = 9;
            this.xFailSafe.Text = "Fail-safe mode";
            this.xFailSafe.UseVisualStyleBackColor = true;
            this.xFailSafe.CheckedChanged += new System.EventHandler(this.xFailSafe_CheckedChanged);
            // 
            // xMultiUser
            // 
            this.xMultiUser.AutoSize = true;
            this.xMultiUser.Location = new System.Drawing.Point(172, 83);
            this.xMultiUser.Name = "xMultiUser";
            this.xMultiUser.Size = new System.Drawing.Size(90, 17);
            this.xMultiUser.TabIndex = 8;
            this.xMultiUser.Text = "Multiple users";
            this.xMultiUser.UseVisualStyleBackColor = true;
            this.xMultiUser.CheckedChanged += new System.EventHandler(this.SaveLauncherSettings);
            // 
            // lOptionsSaved
            // 
            this.lOptionsSaved.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lOptionsSaved.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lOptionsSaved.Location = new System.Drawing.Point(6, 126);
            this.lOptionsSaved.Name = "lOptionsSaved";
            this.lOptionsSaved.Size = new System.Drawing.Size(260, 30);
            this.lOptionsSaved.TabIndex = 7;
            this.lOptionsSaved.Text = "lOptionsSaved";
            this.lOptionsSaved.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lGameUpdates
            // 
            this.lGameUpdates.AutoSize = true;
            this.lGameUpdates.Location = new System.Drawing.Point(6, 36);
            this.lGameUpdates.Name = "lGameUpdates";
            this.lGameUpdates.Size = new System.Drawing.Size(76, 13);
            this.lGameUpdates.TabIndex = 2;
            this.lGameUpdates.Text = "Game updates";
            // 
            // cGameUpdates
            // 
            this.cGameUpdates.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cGameUpdates.FormattingEnabled = true;
            this.cGameUpdates.Items.AddRange(new object[] {
            "Disabled",
            "Ask to install",
            "Automatic"});
            this.cGameUpdates.Location = new System.Drawing.Point(88, 33);
            this.cGameUpdates.Name = "cGameUpdates";
            this.cGameUpdates.Size = new System.Drawing.Size(178, 21);
            this.cGameUpdates.TabIndex = 3;
            this.cGameUpdates.SelectedIndexChanged += new System.EventHandler(this.SaveLauncherSettings);
            // 
            // xRememberServer
            // 
            this.xRememberServer.AutoSize = true;
            this.xRememberServer.Location = new System.Drawing.Point(6, 106);
            this.xRememberServer.Name = "xRememberServer";
            this.xRememberServer.Size = new System.Drawing.Size(159, 17);
            this.xRememberServer.TabIndex = 6;
            this.xRememberServer.Text = "Remember last-joined server";
            this.xRememberServer.UseVisualStyleBackColor = true;
            this.xRememberServer.CheckedChanged += new System.EventHandler(this.SaveLauncherSettings);
            // 
            // xRememberPassword
            // 
            this.xRememberPassword.AutoSize = true;
            this.xRememberPassword.Location = new System.Drawing.Point(6, 83);
            this.xRememberPassword.Name = "xRememberPassword";
            this.xRememberPassword.Size = new System.Drawing.Size(136, 17);
            this.xRememberPassword.TabIndex = 5;
            this.xRememberPassword.Text = "Remember password(s)";
            this.xRememberPassword.UseVisualStyleBackColor = true;
            this.xRememberPassword.CheckedChanged += new System.EventHandler(this.SaveLauncherSettings);
            // 
            // lStartingTab
            // 
            this.lStartingTab.AutoSize = true;
            this.lStartingTab.Location = new System.Drawing.Point(21, 9);
            this.lStartingTab.Name = "lStartingTab";
            this.lStartingTab.Size = new System.Drawing.Size(61, 13);
            this.lStartingTab.TabIndex = 0;
            this.lStartingTab.Text = "Starting tab";
            // 
            // cStartingTab
            // 
            this.cStartingTab.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cStartingTab.FormattingEnabled = true;
            this.cStartingTab.Items.AddRange(new object[] {
            "Sign In",
            "Resume",
            "Direct"});
            this.cStartingTab.Location = new System.Drawing.Point(88, 6);
            this.cStartingTab.Name = "cStartingTab";
            this.cStartingTab.Size = new System.Drawing.Size(178, 21);
            this.cStartingTab.TabIndex = 1;
            this.cStartingTab.SelectedIndexChanged += new System.EventHandler(this.SaveLauncherSettings);
            // 
            // xRememberUsername
            // 
            this.xRememberUsername.AutoSize = true;
            this.xRememberUsername.Location = new System.Drawing.Point(6, 60);
            this.xRememberUsername.Name = "xRememberUsername";
            this.xRememberUsername.Size = new System.Drawing.Size(126, 17);
            this.xRememberUsername.TabIndex = 4;
            this.xRememberUsername.Text = "Remember username\r\n";
            this.xRememberUsername.UseVisualStyleBackColor = true;
            this.xRememberUsername.CheckedChanged += new System.EventHandler(this.SaveLauncherSettings);
            // 
            // tabTools
            // 
            this.tabTools.Controls.Add(this.bForgetActiveAccount);
            this.tabTools.Controls.Add(this.lToolStatus);
            this.tabTools.Controls.Add(this.tPastebinUrl);
            this.tabTools.Controls.Add(this.bDeleteData);
            this.tabTools.Controls.Add(this.bResetSettings);
            this.tabTools.Controls.Add(this.bUploadLog);
            this.tabTools.Controls.Add(this.bOpenDataDir);
            this.tabTools.Location = new System.Drawing.Point(4, 22);
            this.tabTools.Name = "tabTools";
            this.tabTools.Padding = new System.Windows.Forms.Padding(3);
            this.tabTools.Size = new System.Drawing.Size(272, 159);
            this.tabTools.TabIndex = 5;
            this.tabTools.Text = "Tools";
            this.tabTools.UseVisualStyleBackColor = true;
            // 
            // bForgetActiveAccount
            // 
            this.bForgetActiveAccount.Enabled = false;
            this.bForgetActiveAccount.Location = new System.Drawing.Point(6, 6);
            this.bForgetActiveAccount.Name = "bForgetActiveAccount";
            this.bForgetActiveAccount.Size = new System.Drawing.Size(260, 23);
            this.bForgetActiveAccount.TabIndex = 10;
            this.bForgetActiveAccount.Text = "Forget account {0}";
            this.bForgetActiveAccount.UseVisualStyleBackColor = true;
            this.bForgetActiveAccount.Click += new System.EventHandler(this.bForgetActiveAccount_Click);
            // 
            // lToolStatus
            // 
            this.lToolStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lToolStatus.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lToolStatus.Location = new System.Drawing.Point(6, 119);
            this.lToolStatus.Name = "lToolStatus";
            this.lToolStatus.Size = new System.Drawing.Size(260, 37);
            this.lToolStatus.TabIndex = 5;
            this.lToolStatus.Text = "lToolStatus";
            this.lToolStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tPastebinUrl
            // 
            this.tPastebinUrl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tPastebinUrl.Location = new System.Drawing.Point(137, 95);
            this.tPastebinUrl.Name = "tPastebinUrl";
            this.tPastebinUrl.ReadOnly = true;
            this.tPastebinUrl.Size = new System.Drawing.Size(129, 20);
            this.tPastebinUrl.TabIndex = 4;
            // 
            // bDeleteData
            // 
            this.bDeleteData.Location = new System.Drawing.Point(137, 35);
            this.bDeleteData.Name = "bDeleteData";
            this.bDeleteData.Size = new System.Drawing.Size(125, 23);
            this.bDeleteData.TabIndex = 1;
            this.bDeleteData.Text = "Delete all saved data";
            this.bDeleteData.UseVisualStyleBackColor = true;
            this.bDeleteData.Click += new System.EventHandler(this.bDeleteData_Click);
            // 
            // bResetSettings
            // 
            this.bResetSettings.Location = new System.Drawing.Point(6, 35);
            this.bResetSettings.Name = "bResetSettings";
            this.bResetSettings.Size = new System.Drawing.Size(125, 23);
            this.bResetSettings.TabIndex = 0;
            this.bResetSettings.Text = "Reset all settings";
            this.bResetSettings.UseVisualStyleBackColor = true;
            this.bResetSettings.Click += new System.EventHandler(this.bResetSettings_Click);
            // 
            // bUploadLog
            // 
            this.bUploadLog.Location = new System.Drawing.Point(6, 93);
            this.bUploadLog.Name = "bUploadLog";
            this.bUploadLog.Size = new System.Drawing.Size(125, 23);
            this.bUploadLog.TabIndex = 3;
            this.bUploadLog.Text = "Upload logfile";
            this.bUploadLog.UseVisualStyleBackColor = true;
            this.bUploadLog.Click += new System.EventHandler(this.bUploadLog_Click);
            // 
            // bOpenDataDir
            // 
            this.bOpenDataDir.Location = new System.Drawing.Point(6, 64);
            this.bOpenDataDir.Name = "bOpenDataDir";
            this.bOpenDataDir.Size = new System.Drawing.Size(125, 23);
            this.bOpenDataDir.TabIndex = 2;
            this.bOpenDataDir.Text = "Open data directory";
            this.bOpenDataDir.UseVisualStyleBackColor = true;
            this.bOpenDataDir.Click += new System.EventHandler(this.bOpenDataDir_Click);
            // 
            // panelStatus
            // 
            this.panelStatus.Controls.Add(this.lStatus2);
            this.panelStatus.Controls.Add(this.bCancel);
            this.panelStatus.Controls.Add(this.pbSigningIn);
            this.panelStatus.Controls.Add(this.lStatus);
            this.panelStatus.Location = new System.Drawing.Point(280, 2);
            this.panelStatus.Margin = new System.Windows.Forms.Padding(0);
            this.panelStatus.Name = "panelStatus";
            this.panelStatus.Size = new System.Drawing.Size(280, 185);
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
            this.bCancel.Location = new System.Drawing.Point(102, 159);
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
            this.panelUpdatePrompt.Location = new System.Drawing.Point(560, 2);
            this.panelUpdatePrompt.Margin = new System.Windows.Forms.Padding(0);
            this.panelUpdatePrompt.Name = "panelUpdatePrompt";
            this.panelUpdatePrompt.Size = new System.Drawing.Size(280, 185);
            this.panelUpdatePrompt.TabIndex = 0;
            // 
            // bUpdateNo
            // 
            this.bUpdateNo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bUpdateNo.Location = new System.Drawing.Point(157, 159);
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
            this.bUpdateYes.Location = new System.Drawing.Point(3, 159);
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
            this.lUpdatePrompt.Size = new System.Drawing.Size(280, 156);
            this.lUpdatePrompt.TabIndex = 1;
            this.lUpdatePrompt.Text = "An update for ChargedMiners is available.\r\nWould you like to download and apply i" +
    "t?";
            this.lUpdatePrompt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(280, 187);
            this.Controls.Add(this.flow);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(285, 212);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Charged-Miners Launcher 1.20";
            this.flow.ResumeLayout(false);
            this.tabs.ResumeLayout(false);
            this.tabSignIn.ResumeLayout(false);
            this.tabSignIn.PerformLayout();
            this.tabResume.ResumeLayout(false);
            this.tabResume.PerformLayout();
            this.tabDirect.ResumeLayout(false);
            this.tabDirect.PerformLayout();
            this.tabOptions.ResumeLayout(false);
            this.tabOptions.PerformLayout();
            this.tabTools.ResumeLayout(false);
            this.tabTools.PerformLayout();
            this.panelStatus.ResumeLayout(false);
            this.panelUpdatePrompt.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

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
        private System.Windows.Forms.Button bSignIn;
        private System.Windows.Forms.Label lSignInPassword;
        private System.Windows.Forms.Label lSignInUsername;
        private System.Windows.Forms.TextBox tSignInPassword;
        private System.Windows.Forms.TabControl tabs;
        private System.Windows.Forms.TabPage tabResume;
        private System.Windows.Forms.Button bResume;
        private System.Windows.Forms.TextBox tResumeServerName;
        private System.Windows.Forms.Label lResumeServerName;
        private System.Windows.Forms.TextBox tResumeServerIP;
        private System.Windows.Forms.TextBox tResumeUsername;
        private System.Windows.Forms.Label lResumeServerIP;
        private System.Windows.Forms.Label lResumeUsername;
        private System.Windows.Forms.TabPage tabSignIn;
        private System.Windows.Forms.TabPage tabDirect;
        private System.Windows.Forms.TextBox tDirectServerIP;
        private System.Windows.Forms.TextBox tDirectUsername;
        private System.Windows.Forms.Label lDirectServerIP;
        private System.Windows.Forms.Label lDirectUsername;
        private System.Windows.Forms.Button bDirectConnect;
        private System.Windows.Forms.TextBox tDirectUrl;
        private System.Windows.Forms.Label lDirectUrl;
        private System.Windows.Forms.TabPage tabOptions;
        private System.Windows.Forms.TextBox tSignInUrl;
        private System.Windows.Forms.Label lSignInUrl;
        private System.Windows.Forms.CheckBox xRememberServer;
        private System.Windows.Forms.CheckBox xRememberPassword;
        private System.Windows.Forms.Label lStartingTab;
        private System.Windows.Forms.ComboBox cStartingTab;
        private System.Windows.Forms.CheckBox xRememberUsername;
        private System.Windows.Forms.Label lGameUpdates;
        private System.Windows.Forms.ComboBox cGameUpdates;
        private System.Windows.Forms.TabPage tabTools;
        private System.Windows.Forms.TextBox tPastebinUrl;
        private System.Windows.Forms.Button bDeleteData;
        private System.Windows.Forms.Button bResetSettings;
        private System.Windows.Forms.Button bUploadLog;
        private System.Windows.Forms.Button bOpenDataDir;
        private System.Windows.Forms.Label lSignInStatus;
        private System.Windows.Forms.TextBox tResumeUri;
        private System.Windows.Forms.Label lResumeUri;
        private System.Windows.Forms.Label lDirectStatus;
        private System.Windows.Forms.Label lOptionsSaved;
        private System.Windows.Forms.Label lToolStatus;
        private System.Windows.Forms.ComboBox cSignInUsername;
        private System.Windows.Forms.CheckBox xMultiUser;
        private System.Windows.Forms.Button bForgetActiveAccount;
        private System.Windows.Forms.CheckBox xFailSafe;
    }
}

