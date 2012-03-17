namespace ChargedMinersLauncher {
    sealed partial class ServerListForm {
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
            this.dgvServerList = new System.Windows.Forms.DataGridView();
            this.tURL = new System.Windows.Forms.TextBox();
            this.bConnect = new System.Windows.Forms.Button();
            this.tFilter = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.xHideEmpty = new System.Windows.Forms.CheckBox();
            this.xHideFull = new System.Windows.Forms.CheckBox();
            this.bSettings = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvServerList)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvServerList
            // 
            this.dgvServerList.AllowUserToAddRows = false;
            this.dgvServerList.AllowUserToDeleteRows = false;
            this.dgvServerList.AllowUserToResizeRows = false;
            this.dgvServerList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvServerList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvServerList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvServerList.Location = new System.Drawing.Point( 3, 41 );
            this.dgvServerList.MultiSelect = false;
            this.dgvServerList.Name = "dgvServerList";
            this.dgvServerList.ReadOnly = true;
            this.dgvServerList.RowHeadersVisible = false;
            this.dgvServerList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvServerList.Size = new System.Drawing.Size( 505, 364 );
            this.dgvServerList.TabIndex = 0;
            this.dgvServerList.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler( this.dgvServerList_CellDoubleClick );
            this.dgvServerList.KeyDown += new System.Windows.Forms.KeyEventHandler( this.dgvServerList_KeyDown );
            this.dgvServerList.SelectionChanged += new System.EventHandler( this.dgvServerList_SelectionChanged );
            // 
            // tURL
            // 
            this.tURL.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tURL.Location = new System.Drawing.Point( 87, 416 );
            this.tURL.Name = "tURL";
            this.tURL.Size = new System.Drawing.Size( 337, 20 );
            this.tURL.TabIndex = 1;
            this.tURL.TextChanged += new System.EventHandler( this.tURL_TextChanged );
            // 
            // bConnect
            // 
            this.bConnect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bConnect.Location = new System.Drawing.Point( 430, 414 );
            this.bConnect.Name = "bConnect";
            this.bConnect.Size = new System.Drawing.Size( 75, 23 );
            this.bConnect.TabIndex = 2;
            this.bConnect.Text = "Connect";
            this.bConnect.UseVisualStyleBackColor = true;
            this.bConnect.Click += new System.EventHandler( this.bConnect_Click );
            // 
            // tFilter
            // 
            this.tFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tFilter.Location = new System.Drawing.Point( 408, 12 );
            this.tFilter.Name = "tFilter";
            this.tFilter.Size = new System.Drawing.Size( 100, 20 );
            this.tFilter.TabIndex = 3;
            this.tFilter.TextChanged += new System.EventHandler( this.UpdateFilters );
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point( 361, 15 );
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size( 41, 13 );
            this.label1.TabIndex = 4;
            this.label1.Text = "Search";
            // 
            // xHideEmpty
            // 
            this.xHideEmpty.Appearance = System.Windows.Forms.Appearance.Button;
            this.xHideEmpty.AutoSize = true;
            this.xHideEmpty.Location = new System.Drawing.Point( 3, 12 );
            this.xHideEmpty.Name = "xHideEmpty";
            this.xHideEmpty.Size = new System.Drawing.Size( 71, 23 );
            this.xHideEmpty.TabIndex = 5;
            this.xHideEmpty.Text = "Hide Empty";
            this.xHideEmpty.UseVisualStyleBackColor = true;
            this.xHideEmpty.CheckedChanged += new System.EventHandler( this.UpdateFilters );
            // 
            // xHideFull
            // 
            this.xHideFull.Appearance = System.Windows.Forms.Appearance.Button;
            this.xHideFull.AutoSize = true;
            this.xHideFull.Location = new System.Drawing.Point( 80, 12 );
            this.xHideFull.Name = "xHideFull";
            this.xHideFull.Size = new System.Drawing.Size( 58, 23 );
            this.xHideFull.TabIndex = 6;
            this.xHideFull.Text = "Hide Full";
            this.xHideFull.UseVisualStyleBackColor = true;
            this.xHideFull.CheckedChanged += new System.EventHandler( this.UpdateFilters );
            // 
            // bSettings
            // 
            this.bSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bSettings.Location = new System.Drawing.Point( 6, 414 );
            this.bSettings.Name = "bSettings";
            this.bSettings.Size = new System.Drawing.Size( 75, 23 );
            this.bSettings.TabIndex = 7;
            this.bSettings.Text = "Settings";
            this.bSettings.UseVisualStyleBackColor = true;
            this.bSettings.Click += new System.EventHandler( this.bSettings_Click );
            // 
            // ServerListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size( 511, 446 );
            this.Controls.Add( this.bSettings );
            this.Controls.Add( this.xHideFull );
            this.Controls.Add( this.xHideEmpty );
            this.Controls.Add( this.label1 );
            this.Controls.Add( this.tFilter );
            this.Controls.Add( this.bConnect );
            this.Controls.Add( this.tURL );
            this.Controls.Add( this.dgvServerList );
            this.Name = "ServerListForm";
            this.Padding = new System.Windows.Forms.Padding( 3, 0, 3, 0 );
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Charged-Miners: Servers";
            ((System.ComponentModel.ISupportInitialize)(this.dgvServerList)).EndInit();
            this.ResumeLayout( false );
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvServerList;
        private System.Windows.Forms.TextBox tURL;
        private System.Windows.Forms.Button bConnect;
        private System.Windows.Forms.TextBox tFilter;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox xHideEmpty;
        private System.Windows.Forms.CheckBox xHideFull;
        private System.Windows.Forms.Button bSettings;

    }
}