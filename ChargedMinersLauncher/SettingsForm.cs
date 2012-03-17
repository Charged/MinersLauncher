using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace ChargedMinersLauncher {
    public partial class SettingsForm : Form {
        const string ConfigFileName = "settings.ini";
        readonly string ConfigFullFileName;
        ChargedMinersSettings settings;
        ChargedMinersSettings defaults = new ChargedMinersSettings();
        ScreenResolution[] resolutions;

        readonly int defaultResolution;

        public SettingsForm() {
            InitializeComponent();
            ConfigFullFileName = Path.Combine(
                Path.Combine( Environment.GetFolderPath( Environment.SpecialFolder.ApplicationData ), "charge" ),
                ConfigFileName );

            if( File.Exists( ConfigFullFileName ) ) {
                settings = new ChargedMinersSettings( File.ReadAllLines( ConfigFullFileName ) );
            } else {
                settings = new ChargedMinersSettings();
            }

            resolutions = ScreenResolutionLister.GetList();
            ScreenResolution currentRes = ScreenResolutionLister.GetCurrentResolution();
            cResolutions.Items.AddRange( resolutions.Select( r => String.Format( "{0} x {1}", r.Width, r.Height ) ).ToArray() );
            for( int i = 0; i < resolutions.Length; i++ ) {
                if( settings.Fullscreen && settings.Width == resolutions[i].Width && settings.Height == resolutions[i].Height ||
                    !settings.Fullscreen && resolutions[i] == currentRes ) {
                    defaultResolution = i;
                    cResolutions.SelectedIndex = i;
                    break;
                }
            }

            ResetDefaults( null, null );
        }


        private void ResetDefaults( object sender, EventArgs e ) {
            xFullscreen.Checked = settings.Fullscreen;
            nWinWidth.Value = settings.Width;
            nWinHeight.Value = settings.Height;
            xResizableWindow.Checked = settings.ForceResizeEnable;
            cResolutions.SelectedIndex = defaultResolution;
        }


        private void xFullscreen_CheckedChanged( object sender, EventArgs e ) {
            gWindowed.Enabled = !xFullscreen.Checked;
            cResolutions.Enabled = xFullscreen.Checked;
        }


        private void bOK_Click( object sender, EventArgs e ) {
            settings.Fullscreen = xFullscreen.Checked;
            if( settings.Fullscreen ) {
                ScreenResolution selectedResolution = resolutions[cResolutions.SelectedIndex];
                settings.Width = selectedResolution.Width;
                settings.Height = selectedResolution.Height;
            } else {
                settings.Width = (int)nWinWidth.Value;
                settings.Height = (int)nWinHeight.Value;
            }
            settings.ForceResizeEnable = xResizableWindow.Checked;

            string tempFileName = ConfigFullFileName + ".tmp";
            File.WriteAllLines( tempFileName, settings.Serialize() );
            Util.MoveOrReplace( tempFileName, ConfigFullFileName );
            Close();
        }


        private void bCancel_Click( object sender, EventArgs e ) {
            Close();
        }
    }
}
