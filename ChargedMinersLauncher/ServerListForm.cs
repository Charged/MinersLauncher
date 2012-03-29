using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Net;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;
using System.Text.RegularExpressions;


namespace ChargedMinersLauncher {
    sealed partial class ServerListForm : Form {
        static readonly Regex PlayLinkHash = new Regex( @"^http://www.minecraft.net/classic/play/([0-9a-fA-F]{28,32})/?(\?override=(true|1))?$" );
        static readonly Regex PlayLinkDirect = new Regex( @"^mc://(\d{1,3}\.){3}\d{1,3}:\d{1,5}/[a-zA-Z0-9_\.]{2,16}/.*$" );
        static readonly Regex PlayLinkIPPort = new Regex( @"^http://www.minecraft.net/classic/play/?\?ip=(\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3})&port=(\d{1,5})$" );
        static readonly Regex PlayLinkOverride = new Regex( @"\?override=(true|1)$" );

        readonly SortableBindingList<ServerInfo> boundList = new SortableBindingList<ServerInfo>();
        readonly ServerInfo[] originalList;
        readonly HashSet<ServerInfo> listedServers = new HashSet<ServerInfo>();
        string activeHash;


        public ServerListForm( ServerInfo[] list ) {
            InitializeComponent();
            originalList = list;
            foreach( ServerInfo listItem in list ) {
                boundList.Add( listItem );
                listedServers.Add( listItem );
            }
            boundList.PreSortProperty = "Uptime";
            dgvServerList.DataSource = boundList;

            dgvServerList.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvServerList.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvServerList.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;


            dgvServerList.Sort( dgvServerList.Columns[1], ListSortDirection.Descending );
            foreach( DataGridViewColumn column in dgvServerList.Columns ) {
                column.SortMode = DataGridViewColumnSortMode.Automatic;
            }

            dgvServerList.CellFormatting += dgvServerList_CellFormatting;
            KeyPreview = true;
        }


        protected override bool ProcessCmdKey( ref Message msg, Keys keyData ) {
            if( keyData == ( Keys.Control | Keys.F ) ) {
                tFilter.Focus();
                return true;
            }

            return base.ProcessCmdKey( ref msg, keyData );
        }


        protected override void OnShown( EventArgs e ) {
            base.OnShown( e );
            dgvServerList.Focus();
        }


        static void dgvServerList_CellFormatting( object sender, DataGridViewCellFormattingEventArgs e ) {
            if( e.ColumnIndex != 3 ) return;
            TimeSpan val = (TimeSpan)e.Value;
            if( val.TotalSeconds < 60 ) {
                e.Value = String.Format( "{0} sec", (int)Math.Round( val.TotalSeconds ) );
            } else if( val.TotalMinutes < 60 ) {
                e.Value = String.Format( "{0} min", (int)Math.Round( val.TotalMinutes ) );
            } else if( val.TotalHours < 24 ) {
                e.Value = String.Format( "{0} hours", (int)Math.Round( val.TotalHours ) );
            } else if( val.TotalDays < 15 ) {
                e.Value = String.Format( "{0} days", (int)Math.Round( val.TotalDays ) );
            }
        }


        private void dgvServerList_CellDoubleClick( object sender, DataGridViewCellEventArgs e ) {
            if( e.RowIndex == -1 ) return;
            StartLoadingInfo( boundList[e.RowIndex].Hash );
        }


        private void dgvServerList_KeyDown( object sender, KeyEventArgs e ) {
            if( e.KeyCode == Keys.Enter && dgvServerList.SelectedRows.Count > 0 &&
                dgvServerList.SelectedRows[0].Index != -1 ) {
                StartLoadingInfo( boundList[dgvServerList.SelectedRows[0].Index].Hash );
                e.Handled = true;
            } else if( e.KeyCode == Keys.Tab ) {
                int i = Controls.IndexOf( dgvServerList );
                if( e.Shift ) {
                    Controls[( i - 1 ) % Controls.Count].Focus();
                } else {
                    Controls[( i + 1 ) % Controls.Count].Focus();
                }
                e.Handled = true;
            } else if( e.KeyCode == Keys.Left || e.KeyCode == Keys.Right ) {
                e.Handled = true;
            }
        }


        private void dgvServerList_SelectionChanged( object sender, EventArgs e ) {
            if( dgvServerList.SelectedRows.Count < 1 ) return;
            int index = dgvServerList.SelectedRows[0].Index;
            if( index == -1 ) return;
            ServerInfo info = boundList[index];
            tURL.Text = MinecraftNetSession.PlayUri + info.Hash;
        }


        private void tURL_TextChanged( object sender, EventArgs e ) {
            Match match = PlayLinkHash.Match( tURL.Text );
            if( match.Success ) {
                tURL.BackColor = SystemColors.Window;
                activeHash = match.Groups[1].Value;
                bConnect.Enabled = true;
            } else if( PlayLinkDirect.IsMatch( tURL.Text ) || PlayLinkIPPort.IsMatch(tURL.Text) ) {
                tURL.BackColor = SystemColors.Window;
                activeHash = null;
                bConnect.Enabled = true;

            }else{
                tURL.BackColor = Color.Yellow;
                activeHash = null;
                bConnect.Enabled = false;
            }
        }

        private void bConnect_Click( object sender, EventArgs e ) {
            StartLoadingInfo( activeHash );
        }


        void StartLoadingInfo( string hash ) {
            if( !File.Exists( SignInForm.ChargeBinary ) ) {
                MessageBox.Show( "Charge.exe not found!" );
                return;
            }
            if( hash != null ) {
                activeHash = hash;
                LoadingForm progressBox = new LoadingForm( "Fetching server info..." );
                progressBox.Shown += ( s2, e2 ) => ThreadPool.QueueUserWorkItem( FetchInfo, progressBox );
                progressBox.ShowDialog();
            } else if( PlayLinkDirect.IsMatch( tURL.Text ) ) {
                Launch( tURL.Text );
            } else if( PlayLinkIPPort.IsMatch( tURL.Text ) ) {
                Match match = PlayLinkIPPort.Match( tURL.Text );
                string url = String.Format( "mc://{0}:{1}/{2}/{3}",
                                            match.Groups[1].Value,
                                            match.Groups[2].Value,
                                            MinecraftNetSession.Instance.Username,
                                            new String( '0', 32 ) );
                Launch( url );
            }
        }


        void FetchInfo( object param ) {
            LoadingForm progressBox = (LoadingForm)param;
            ServerLoginInfo info = MinecraftNetSession.Instance.GetServerInfo( activeHash );
            if( info == null ) {
                MessageBox.Show( "Could not fetch server data! Maybe it's offline?" );
                progressBox.Invoke( (Action)progressBox.Close );
                return;
            }
            if( PlayLinkOverride.IsMatch( tURL.Text ) ) {
                info.IP = IPAddress.Loopback;
            }
            string url = String.Format( "mc://{0}:{1}/{2}/{3}",
                                        info.IP,
                                        info.Port,
                                        info.User,
                                        info.AuthToken );
            Launch( url );
        }

        
        void Launch( string url ) {
            Process.Start( SignInForm.ChargeBinary, url );
            Application.Exit();
        }


        void UpdateFilters( object sender, EventArgs e ) {
            dgvServerList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            foreach( ServerInfo info in originalList ) {
                bool pass = (tFilter.Text.Length == 0 || info.Name.IndexOf( tFilter.Text, StringComparison.OrdinalIgnoreCase ) > -1) &&
                            (!xHideEmpty.Checked || info.Players > 0) &&
                            (!xHideFull.Checked || info.Players < info.MaxPlayers);
                bool listed = listedServers.Contains( info );
                if( pass && !listed ) {
                    listedServers.Add( info );
                    boundList.Add( info );
                } else if( !pass && listed ) {
                    listedServers.Remove( info );
                    boundList.Remove( info );
                }
            }
            ListSortDirection order = (dgvServerList.SortOrder == SortOrder.Ascending ? ListSortDirection.Ascending : ListSortDirection.Descending);
            dgvServerList.Sort( dgvServerList.SortedColumn ?? dgvServerList.Columns[1], order );
            dgvServerList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }


        void bSettings_Click( object sender, EventArgs e ) {
            new SettingsForm().ShowDialog();
        }
    }
}