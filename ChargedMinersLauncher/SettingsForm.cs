﻿using System;
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
                settings = new ChargedMinersSettings( File.ReadAllLines( ChargedMinersSettings.ConfigFileFullName ) );
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

            ApplySettings();
        }


        private void ResetDefaults( object sender, EventArgs e ) {
            xFullscreen.Checked = defaults.Fullscreen;
            nWinWidth.Value = defaults.Width;
            nWinHeight.Value = defaults.Height;
            xResizableWindow.Checked = defaults.ForceResizeEnable;
            cResolutions.SelectedIndex = defaultResolution;
        }

        private void ApplySettings() {
            xFullscreen.Checked = settings.Fullscreen;
            nWinWidth.Value = settings.Width;
            nWinHeight.Value = settings.Height;
            xResizableWindow.Checked = settings.ForceResizeEnable;
            cResolutions.SelectedIndex = defaultResolution;
            xAntiAlias.Checked = settings.AntiAliasEnabled;
            xFog.Checked = settings.FogEnabled;
            xShadows.Checked = settings.ShadowsEnabled;
            tbViewDistance.Value = Math.Max( tbViewDistance.Minimum, Math.Min( settings.ViewDistance / 32, tbViewDistance.Maximum ) );
            lViewDistance.Text = String.Format( "View distance: {0}", tbViewDistance.Value * 32 );
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


        private void bCancel_Click( object sender, EventArgs e ) {
            Close();
        }

        private void tbViewDistance_Scroll( object sender, EventArgs e ) {
            lViewDistance.Text = String.Format( "View distance: {0}", tbViewDistance.Value * 32 );
        }
    }
}
