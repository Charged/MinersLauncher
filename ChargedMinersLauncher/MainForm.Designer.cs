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
            this.tabs = new System.Windows.Forms.TabControl();
            this.tabSignIn = new System.Windows.Forms.TabPage();
            this.lSignInStatus = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.lSignInUrl = new System.Windows.Forms.Label();
            this.tUsername = new System.Windows.Forms.TextBox();
            this.tPassword = new System.Windows.Forms.TextBox();
            this.lUsername = new System.Windows.Forms.Label();
            this.lPassword = new System.Windows.Forms.Label();
            this.bSignIn = new System.Windows.Forms.Button();
            this.tabResume = new System.Windows.Forms.TabPage();
            this.bResume = new System.Windows.Forms.Button();
            this.tResumeServerName = new System.Windows.Forms.TextBox();
            this.lResumeServerName = new System.Windows.Forms.Label();
            this.tResumeServerIP = new System.Windows.Forms.TextBox();
            this.tResumeUsername = new System.Windows.Forms.TextBox();
            this.lResumeServerIP = new System.Windows.Forms.Label();
            this.lResumeUsername = new System.Windows.Forms.Label();
            this.tabDirect = new System.Windows.Forms.TabPage();
            this.tDirectServerIP = new System.Windows.Forms.TextBox();
            this.tDirectUsername = new System.Windows.Forms.TextBox();
            this.lDirectServerIP = new System.Windows.Forms.Label();
            this.lDirectUsername = new System.Windows.Forms.Label();
            this.bDirectConnect = new System.Windows.Forms.Button();
            this.tDirectUrl = new System.Windows.Forms.TextBox();
            this.lDirectUrl = new System.Windows.Forms.Label();
            this.tabOptions = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cStartingTab = new System.Windows.Forms.ComboBox();
            this.xRemember = new System.Windows.Forms.CheckBox();
            this.tabTools = new System.Windows.Forms.TabPage();
            this.tPastebinUrl = new System.Windows.Forms.TextBox();
            this.bDeleteData = new System.Windows.Forms.Button();
            this.bResetSettings = new System.Windows.Forms.Button();
            this.bUploadLog = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
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
            // lChargedMiners
            // 
            this.lChargedMiners.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lChargedMiners.Location = new System.Drawing.Point(6, 3);
            this.lChargedMiners.Name = "lChargedMiners";
            this.lChargedMiners.Size = new System.Drawing.Size(260, 37);
            this.lChargedMiners.TabIndex = 0;
            this.lChargedMiners.Text = "Charged-Miners";
            this.lChargedMiners.TextAlign = System.Drawing.ContentAlignment.TopCenter;
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
            this.flow.Size = new System.Drawing.Size(279, 187);
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
            this.tabs.TabIndex = 18;
            this.tabs.SelectedIndexChanged += new System.EventHandler(this.tabs_SelectedIndexChanged);
            // 
            // tabSignIn
            // 
            this.tabSignIn.Controls.Add(this.lSignInStatus);
            this.tabSignIn.Controls.Add(this.textBox1);
            this.tabSignIn.Controls.Add(this.lSignInUrl);
            this.tabSignIn.Controls.Add(this.tUsername);
            this.tabSignIn.Controls.Add(this.lChargedMiners);
            this.tabSignIn.Controls.Add(this.tPassword);
            this.tabSignIn.Controls.Add(this.lUsername);
            this.tabSignIn.Controls.Add(this.lPassword);
            this.tabSignIn.Controls.Add(this.bSignIn);
            this.tabSignIn.Location = new System.Drawing.Point(4, 22);
            this.tabSignIn.Name = "tabSignIn";
            this.tabSignIn.Padding = new System.Windows.Forms.Padding(3);
            this.tabSignIn.Size = new System.Drawing.Size(272, 159);
            this.tabSignIn.TabIndex = 0;
            this.tabSignIn.Text = "Sign In";
            this.tabSignIn.UseVisualStyleBackColor = true;
            // 
            // lSignInStatus
            // 
            this.lSignInStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lSignInStatus.ForeColor = System.Drawing.Color.Red;
            this.lSignInStatus.Location = new System.Drawing.Point(6, 131);
            this.lSignInStatus.Name = "lSignInStatus";
            this.lSignInStatus.Size = new System.Drawing.Size(174, 20);
            this.lSignInStatus.TabIndex = 14;
            this.lSignInStatus.Text = "SignInStatus";
            this.lSignInStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(79, 99);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(187, 20);
            this.textBox1.TabIndex = 15;
            this.textBox1.UseSystemPasswordChar = true;
            // 
            // lSignInUrl
            // 
            this.lSignInUrl.AutoSize = true;
            this.lSignInUrl.Location = new System.Drawing.Point(23, 102);
            this.lSignInUrl.Name = "lSignInUrl";
            this.lSignInUrl.Size = new System.Drawing.Size(50, 26);
            this.lSignInUrl.TabIndex = 14;
            this.lSignInUrl.Text = "URL\r\n(optional)";
            this.lSignInUrl.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // tUsername
            // 
            this.tUsername.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tUsername.Location = new System.Drawing.Point(79, 47);
            this.tUsername.Name = "tUsername";
            this.tUsername.Size = new System.Drawing.Size(187, 20);
            this.tUsername.TabIndex = 9;
            this.tUsername.TextChanged += new System.EventHandler(this.OnUsernameOrPasswordChanged);
            // 
            // tPassword
            // 
            this.tPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tPassword.Location = new System.Drawing.Point(79, 73);
            this.tPassword.Name = "tPassword";
            this.tPassword.Size = new System.Drawing.Size(187, 20);
            this.tPassword.TabIndex = 11;
            this.tPassword.UseSystemPasswordChar = true;
            this.tPassword.TextChanged += new System.EventHandler(this.OnUsernameOrPasswordChanged);
            // 
            // lUsername
            // 
            this.lUsername.AutoSize = true;
            this.lUsername.Location = new System.Drawing.Point(20, 50);
            this.lUsername.Name = "lUsername";
            this.lUsername.Size = new System.Drawing.Size(55, 13);
            this.lUsername.TabIndex = 8;
            this.lUsername.Text = "Username";
            // 
            // lPassword
            // 
            this.lPassword.AutoSize = true;
            this.lPassword.Location = new System.Drawing.Point(20, 76);
            this.lPassword.Name = "lPassword";
            this.lPassword.Size = new System.Drawing.Size(53, 13);
            this.lPassword.TabIndex = 10;
            this.lPassword.Text = "Password";
            // 
            // bSignIn
            // 
            this.bSignIn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bSignIn.Enabled = false;
            this.bSignIn.Location = new System.Drawing.Point(186, 130);
            this.bSignIn.Name = "bSignIn";
            this.bSignIn.Size = new System.Drawing.Size(80, 23);
            this.bSignIn.TabIndex = 12;
            this.bSignIn.Text = "Sign In";
            this.bSignIn.UseVisualStyleBackColor = true;
            this.bSignIn.Click += new System.EventHandler(this.bSignIn_Click);
            // 
            // tabResume
            // 
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
            // bResume
            // 
            this.bResume.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bResume.Location = new System.Drawing.Point(186, 129);
            this.bResume.Name = "bResume";
            this.bResume.Size = new System.Drawing.Size(80, 23);
            this.bResume.TabIndex = 31;
            this.bResume.Text = "Reconnect";
            this.bResume.UseVisualStyleBackColor = true;
            // 
            // tResumeServerName
            // 
            this.tResumeServerName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tResumeServerName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tResumeServerName.Location = new System.Drawing.Point(79, 61);
            this.tResumeServerName.Name = "tResumeServerName";
            this.tResumeServerName.ReadOnly = true;
            this.tResumeServerName.Size = new System.Drawing.Size(187, 13);
            this.tResumeServerName.TabIndex = 30;
            // 
            // lResumeServerName
            // 
            this.lResumeServerName.AutoSize = true;
            this.lResumeServerName.Location = new System.Drawing.Point(6, 61);
            this.lResumeServerName.Name = "lResumeServerName";
            this.lResumeServerName.Size = new System.Drawing.Size(67, 13);
            this.lResumeServerName.TabIndex = 29;
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
            this.tResumeServerIP.TabIndex = 28;
            // 
            // tResumeUsername
            // 
            this.tResumeUsername.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tResumeUsername.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tResumeUsername.Location = new System.Drawing.Point(79, 9);
            this.tResumeUsername.Name = "tResumeUsername";
            this.tResumeUsername.ReadOnly = true;
            this.tResumeUsername.Size = new System.Drawing.Size(187, 13);
            this.tResumeUsername.TabIndex = 27;
            // 
            // lResumeServerIP
            // 
            this.lResumeServerIP.AutoSize = true;
            this.lResumeServerIP.Location = new System.Drawing.Point(22, 35);
            this.lResumeServerIP.Name = "lResumeServerIP";
            this.lResumeServerIP.Size = new System.Drawing.Size(51, 13);
            this.lResumeServerIP.TabIndex = 26;
            this.lResumeServerIP.Text = "Server IP";
            // 
            // lResumeUsername
            // 
            this.lResumeUsername.AutoSize = true;
            this.lResumeUsername.Location = new System.Drawing.Point(18, 9);
            this.lResumeUsername.Name = "lResumeUsername";
            this.lResumeUsername.Size = new System.Drawing.Size(55, 13);
            this.lResumeUsername.TabIndex = 25;
            this.lResumeUsername.Text = "Username";
            // 
            // tabDirect
            // 
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
            // tDirectServerIP
            // 
            this.tDirectServerIP.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tDirectServerIP.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tDirectServerIP.Location = new System.Drawing.Point(79, 61);
            this.tDirectServerIP.Name = "tDirectServerIP";
            this.tDirectServerIP.ReadOnly = true;
            this.tDirectServerIP.Size = new System.Drawing.Size(187, 13);
            this.tDirectServerIP.TabIndex = 24;
            // 
            // tDirectUsername
            // 
            this.tDirectUsername.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tDirectUsername.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tDirectUsername.Location = new System.Drawing.Point(79, 35);
            this.tDirectUsername.Name = "tDirectUsername";
            this.tDirectUsername.ReadOnly = true;
            this.tDirectUsername.Size = new System.Drawing.Size(187, 13);
            this.tDirectUsername.TabIndex = 23;
            // 
            // lDirectServerIP
            // 
            this.lDirectServerIP.AutoSize = true;
            this.lDirectServerIP.Location = new System.Drawing.Point(22, 61);
            this.lDirectServerIP.Name = "lDirectServerIP";
            this.lDirectServerIP.Size = new System.Drawing.Size(51, 13);
            this.lDirectServerIP.TabIndex = 22;
            this.lDirectServerIP.Text = "Server IP";
            // 
            // lDirectUsername
            // 
            this.lDirectUsername.AutoSize = true;
            this.lDirectUsername.Location = new System.Drawing.Point(18, 35);
            this.lDirectUsername.Name = "lDirectUsername";
            this.lDirectUsername.Size = new System.Drawing.Size(55, 13);
            this.lDirectUsername.TabIndex = 21;
            this.lDirectUsername.Text = "Username";
            // 
            // bDirectConnect
            // 
            this.bDirectConnect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bDirectConnect.Enabled = false;
            this.bDirectConnect.Location = new System.Drawing.Point(186, 129);
            this.bDirectConnect.Name = "bDirectConnect";
            this.bDirectConnect.Size = new System.Drawing.Size(80, 23);
            this.bDirectConnect.TabIndex = 20;
            this.bDirectConnect.Text = "Connect";
            this.bDirectConnect.UseVisualStyleBackColor = true;
            // 
            // tDirectUrl
            // 
            this.tDirectUrl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tDirectUrl.Location = new System.Drawing.Point(79, 6);
            this.tDirectUrl.Name = "tDirectUrl";
            this.tDirectUrl.Size = new System.Drawing.Size(187, 20);
            this.tDirectUrl.TabIndex = 18;
            // 
            // lDirectUrl
            // 
            this.lDirectUrl.AutoSize = true;
            this.lDirectUrl.Location = new System.Drawing.Point(44, 9);
            this.lDirectUrl.Name = "lDirectUrl";
            this.lDirectUrl.Size = new System.Drawing.Size(29, 13);
            this.lDirectUrl.TabIndex = 19;
            this.lDirectUrl.Text = "URL";
            this.lDirectUrl.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // tabOptions
            // 
            this.tabOptions.Controls.Add(this.label2);
            this.tabOptions.Controls.Add(this.comboBox1);
            this.tabOptions.Controls.Add(this.checkBox2);
            this.tabOptions.Controls.Add(this.checkBox1);
            this.tabOptions.Controls.Add(this.label1);
            this.tabOptions.Controls.Add(this.cStartingTab);
            this.tabOptions.Controls.Add(this.xRemember);
            this.tabOptions.Location = new System.Drawing.Point(4, 22);
            this.tabOptions.Name = "tabOptions";
            this.tabOptions.Padding = new System.Windows.Forms.Padding(3);
            this.tabOptions.Size = new System.Drawing.Size(272, 159);
            this.tabOptions.TabIndex = 3;
            this.tabOptions.Text = "Options";
            this.tabOptions.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 13);
            this.label2.TabIndex = 20;
            this.label2.Text = "Game updates";
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Disabled",
            "Ask to install",
            "Automatic"});
            this.comboBox1.Location = new System.Drawing.Point(88, 33);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(178, 21);
            this.comboBox1.TabIndex = 19;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(6, 106);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(159, 17);
            this.checkBox2.TabIndex = 18;
            this.checkBox2.Text = "Remember last-joined server";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(6, 83);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(125, 17);
            this.checkBox1.TabIndex = 17;
            this.checkBox1.Text = "Remember password";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 16;
            this.label1.Text = "Starting tab";
            // 
            // cStartingTab
            // 
            this.cStartingTab.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cStartingTab.FormattingEnabled = true;
            this.cStartingTab.Items.AddRange(new object[] {
            "Sign In",
            "Resume",
            "URL",
            "Direct"});
            this.cStartingTab.Location = new System.Drawing.Point(88, 6);
            this.cStartingTab.Name = "cStartingTab";
            this.cStartingTab.Size = new System.Drawing.Size(178, 21);
            this.cStartingTab.TabIndex = 15;
            // 
            // xRemember
            // 
            this.xRemember.AutoSize = true;
            this.xRemember.Location = new System.Drawing.Point(6, 60);
            this.xRemember.Name = "xRemember";
            this.xRemember.Size = new System.Drawing.Size(126, 17);
            this.xRemember.TabIndex = 14;
            this.xRemember.Text = "Remember username\r\n";
            this.xRemember.UseVisualStyleBackColor = true;
            // 
            // tabTools
            // 
            this.tabTools.Controls.Add(this.tPastebinUrl);
            this.tabTools.Controls.Add(this.bDeleteData);
            this.tabTools.Controls.Add(this.bResetSettings);
            this.tabTools.Controls.Add(this.bUploadLog);
            this.tabTools.Controls.Add(this.button1);
            this.tabTools.Location = new System.Drawing.Point(4, 22);
            this.tabTools.Name = "tabTools";
            this.tabTools.Padding = new System.Windows.Forms.Padding(3);
            this.tabTools.Size = new System.Drawing.Size(272, 159);
            this.tabTools.TabIndex = 5;
            this.tabTools.Text = "Tools";
            this.tabTools.UseVisualStyleBackColor = true;
            // 
            // tPastebinUrl
            // 
            this.tPastebinUrl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tPastebinUrl.Location = new System.Drawing.Point(137, 66);
            this.tPastebinUrl.Name = "tPastebinUrl";
            this.tPastebinUrl.ReadOnly = true;
            this.tPastebinUrl.Size = new System.Drawing.Size(129, 20);
            this.tPastebinUrl.TabIndex = 5;
            // 
            // bDeleteData
            // 
            this.bDeleteData.Location = new System.Drawing.Point(137, 6);
            this.bDeleteData.Name = "bDeleteData";
            this.bDeleteData.Size = new System.Drawing.Size(125, 23);
            this.bDeleteData.TabIndex = 4;
            this.bDeleteData.Text = "Delete all saved data";
            this.bDeleteData.UseVisualStyleBackColor = true;
            // 
            // bResetSettings
            // 
            this.bResetSettings.Location = new System.Drawing.Point(6, 6);
            this.bResetSettings.Name = "bResetSettings";
            this.bResetSettings.Size = new System.Drawing.Size(125, 23);
            this.bResetSettings.TabIndex = 3;
            this.bResetSettings.Text = "Reset all settings";
            this.bResetSettings.UseVisualStyleBackColor = true;
            // 
            // bUploadLog
            // 
            this.bUploadLog.Location = new System.Drawing.Point(6, 64);
            this.bUploadLog.Name = "bUploadLog";
            this.bUploadLog.Size = new System.Drawing.Size(125, 23);
            this.bUploadLog.TabIndex = 2;
            this.bUploadLog.Text = "Upload logfile";
            this.bUploadLog.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(6, 35);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(125, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Open data folder";
            this.button1.UseVisualStyleBackColor = true;
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
            this.ClientSize = new System.Drawing.Size(279, 187);
            this.Controls.Add(this.flow);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Charged-Miners Launcher 1.1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
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
        private System.Windows.Forms.Button bSignIn;
        private System.Windows.Forms.Label lPassword;
        private System.Windows.Forms.TextBox tUsername;
        private System.Windows.Forms.Label lUsername;
        private System.Windows.Forms.TextBox tPassword;
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
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label lSignInUrl;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cStartingTab;
        private System.Windows.Forms.CheckBox xRemember;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.TabPage tabTools;
        private System.Windows.Forms.TextBox tPastebinUrl;
        private System.Windows.Forms.Button bDeleteData;
        private System.Windows.Forms.Button bResetSettings;
        private System.Windows.Forms.Button bUploadLog;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label lSignInStatus;
    }
}

