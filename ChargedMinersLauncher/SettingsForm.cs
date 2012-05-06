// Part of ChargedMinersLaunher | Copyright (c) 2012 Matvei Stefarov <me@matvei.org> | BSD-3 | See LICENSE.txt
using System;
using System.Linq;
using System.Windows.Forms;
using System.IO;

namespace ChargedMinersLauncher {
    public sealed partial class SettingsForm : Form {
        readonly ChargedMinersSettings settings;
        readonly ChargedMinersSettings defaults = new ChargedMinersSettings();
        readonly ScreenResolution[] resolutions;

        readonly int defaultResolution;


        public SettingsForm() {
            InitializeComponent();
            if( File.Exists( ChargedMinersSettings.ConfigFileFullName ) ) {
                try {
                    settings = new ChargedMinersSettings( File.ReadAllLines( ChargedMinersSettings.ConfigFileFullName ) );
                } catch( Exception ex ) {
                    WarningForm.Show( "Error parsing Charged-Miners settings",
                                       ex.ToString() );
                    settings = new ChargedMinersSettings();
                }
            } else {
                settings = new ChargedMinersSettings();
            }

            // Add a automatic mode
            ScreenResolution currentRes;
            resolutions = new ScreenResolution[] { new ScreenResolution { Width = 0, Height = 0 } };
            cResolutions.Items.Add( "automatic" );

            if (!RuntimeInfo.IsWindows) {
                currentRes = resolutions[0];
            } else {
                var res = ScreenResolutionLister.GetList();
                currentRes = ScreenResolutionLister.GetCurrentResolution();

                cResolutions.Items.AddRange( res.Select( r => String.Format( "{0} x {1}", r.Width, r.Height ) ).ToArray() );
                resolutions = resolutions.Concat( res ).ToArray();
            }

            for( int i = 0; i < resolutions.Length; i++ ) {
                if( settings.Fullscreen && settings.Width == resolutions[i].Width && settings.Height == resolutions[i].Height ||
                    !settings.Fullscreen && resolutions[i] == currentRes ) {
                    defaultResolution = i;
                    cResolutions.SelectedIndex = i;
                    break;
                }
            }

            ApplySettings();
        }


        void ResetDefaults( object sender, EventArgs e ) {
            xFullscreen.Checked = defaults.Fullscreen;
            nWinWidth.Value = defaults.Width;
            nWinHeight.Value = defaults.Height;
            xResizableWindow.Checked = defaults.ForceResizeEnable;
            cResolutions.SelectedIndex = defaultResolution;
            xAntiAlias.Checked = defaults.AntiAliasEnabled;
            xFog.Checked = defaults.FogEnabled;
            xShadows.Checked = defaults.ShadowsEnabled;
            tbViewDistance.Value = Math.Max( tbViewDistance.Minimum, Math.Min( defaults.ViewDistance / 32, tbViewDistance.Maximum ) );
            lViewDistance.Text = String.Format( "View distance: {0}", tbViewDistance.Value * 32 );
        }


        void ApplySettings() {
            xFullscreen.Checked = settings.Fullscreen;
            nWinWidth.Value = Math.Max(settings.Width, 640);
            nWinHeight.Value = Math.Max(settings.Height, 480);
            xResizableWindow.Checked = settings.ForceResizeEnable;
            cResolutions.SelectedIndex = defaultResolution;
            xAntiAlias.Checked = settings.AntiAliasEnabled;
            xFog.Checked = settings.FogEnabled;
            xShadows.Checked = settings.ShadowsEnabled;
            tbViewDistance.Value = Math.Max( tbViewDistance.Minimum, Math.Min( settings.ViewDistance / 32, tbViewDistance.Maximum ) );
            lViewDistance.Text = String.Format( "View distance: {0}", tbViewDistance.Value * 32 );
        }


        void xFullscreen_CheckedChanged( object sender, EventArgs e ) {
            cResolutions.Enabled = xFullscreen.Checked;
            xResizableWindow.Enabled = !xFullscreen.Checked;
            lWindowSize.Enabled = !xFullscreen.Checked;
            nWinWidth.Enabled = !xFullscreen.Checked;
            lX.Enabled = !xFullscreen.Checked;
            nWinHeight.Enabled = !xFullscreen.Checked;
        }


        void bOK_Click( object sender, EventArgs e ) {
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
            settings.AntiAliasEnabled = xAntiAlias.Checked;
            settings.FogEnabled = xFog.Checked;
            settings.ShadowsEnabled = xShadows.Checked;
            settings.ViewDistance = tbViewDistance.Value;

            string tempFileName = ChargedMinersSettings.ConfigFileFullName + ".tmp";

            if( !Directory.Exists( ChargedMinersSettings.ConfigPath ) ) {
                Directory.CreateDirectory( ChargedMinersSettings.ConfigPath );
            }
            File.WriteAllLines( tempFileName, settings.Serialize() );
            Util.MoveOrReplace( tempFileName, ChargedMinersSettings.ConfigFileFullName );
            Close();
        }


        void bCancel_Click( object sender, EventArgs e ) {
            Close();
        }


        void tbViewDistance_Scroll( object sender, EventArgs e ) {
            lViewDistance.Text = String.Format( "View distance: {0}", tbViewDistance.Value * 32 );
        }
    }
}
