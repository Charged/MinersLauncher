namespace ChargedMinersLauncher {
    partial class LoginForm {
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
            this.tUsername = new System.Windows.Forms.TextBox();
            this.tPassword = new System.Windows.Forms.TextBox();
            this.lUsername = new System.Windows.Forms.Label();
            this.lPassword = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.xRemember = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // bSignIn
            // 
            this.bSignIn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bSignIn.Enabled = false;
            this.bSignIn.Location = new System.Drawing.Point( 161, 119 );
            this.bSignIn.Name = "bSignIn";
            this.bSignIn.Size = new System.Drawing.Size( 75, 23 );
            this.bSignIn.TabIndex = 0;
            this.bSignIn.Text = "Sign In";
            this.bSignIn.UseVisualStyleBackColor = true;
            this.bSignIn.Click += new System.EventHandler( this.bSignIn_Click );
            // 
            // tUsername
            // 
            this.tUsername.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tUsername.Location = new System.Drawing.Point( 73, 67 );
            this.tUsername.Name = "tUsername";
            this.tUsername.Size = new System.Drawing.Size( 163, 20 );
            this.tUsername.TabIndex = 1;
            this.tUsername.TextChanged += new System.EventHandler( this.OnTextChanged );
            // 
            // tPassword
            // 
            this.tPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tPassword.Location = new System.Drawing.Point( 73, 93 );
            this.tPassword.Name = "tPassword";
            this.tPassword.Size = new System.Drawing.Size( 163, 20 );
            this.tPassword.TabIndex = 2;
            this.tPassword.UseSystemPasswordChar = true;
            this.tPassword.TextChanged += new System.EventHandler( this.OnTextChanged );
            // 
            // lUsername
            // 
            this.lUsername.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lUsername.AutoSize = true;
            this.lUsername.Location = new System.Drawing.Point( 12, 70 );
            this.lUsername.Name = "lUsername";
            this.lUsername.Size = new System.Drawing.Size( 55, 13 );
            this.lUsername.TabIndex = 3;
            this.lUsername.Text = "Username";
            // 
            // lPassword
            // 
            this.lPassword.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lPassword.AutoSize = true;
            this.lPassword.Location = new System.Drawing.Point( 14, 96 );
            this.lPassword.Name = "lPassword";
            this.lPassword.Size = new System.Drawing.Size( 53, 13 );
            this.lPassword.TabIndex = 4;
            this.lPassword.Text = "Password";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font( "Segoe UI", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)) );
            this.label1.Location = new System.Drawing.Point( 12, 9 );
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size( 224, 37 );
            this.label1.TabIndex = 5;
            this.label1.Text = "Charged-Miners";
            // 
            // xRemember
            // 
            this.xRemember.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.xRemember.AutoSize = true;
            this.xRemember.Location = new System.Drawing.Point( 73, 123 );
            this.xRemember.Name = "xRemember";
            this.xRemember.Size = new System.Drawing.Size( 77, 17 );
            this.xRemember.TabIndex = 6;
            this.xRemember.Text = "Remember";
            this.xRemember.UseVisualStyleBackColor = true;
            // 
            // LoginForm
            // 
            this.AcceptButton = this.bSignIn;
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size( 248, 154 );
            this.Controls.Add( this.xRemember );
            this.Controls.Add( this.label1 );
            this.Controls.Add( this.lPassword );
            this.Controls.Add( this.lUsername );
            this.Controls.Add( this.tPassword );
            this.Controls.Add( this.tUsername );
            this.Controls.Add( this.bSignIn );
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Charged-Miners Launcher";
            this.ResumeLayout( false );
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bSignIn;
        private System.Windows.Forms.TextBox tUsername;
        private System.Windows.Forms.TextBox tPassword;
        private System.Windows.Forms.Label lUsername;
        private System.Windows.Forms.Label lPassword;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox xRemember;
    }
}

