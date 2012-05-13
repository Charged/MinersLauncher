// Part of ChargedMinersLaunher | Copyright (c) 2012 Matvei Stefarov <me@matvei.org> | BSD-3 | See LICENSE.txt
using System;
using System.Drawing;
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

            resolutions = ScreenResolutionLister.GetList();
            ScreenResolution currentRes = ScreenResolutionLister.GetCurrentResolution();
            cResolutions.Items.AddRange( resolutions.Select( r => String.Format( "{0} x {1}", r.Width, r.Height ) ).ToArray() );
            for( int i = 0; i < resolutions.Length; i++ ) {
                if( settings.WindowMode == WindowMode.Fullscreen && settings.Width == resolutions[i].Width && settings.Height == resolutions[i].Height ||
                    settings.WindowMode != WindowMode.Fullscreen && resolutions[i] == currentRes ) {
                    defaultResolution = i;
                    cResolutions.SelectedIndex = i;
                    break;
                }
            }

            ApplySettings();
        }


        void ResetDefaults( object sender, EventArgs e ) {
            cWindowMode.SelectedIndex = (int)defaults.WindowMode;
            nWinWidth.Value = defaults.Width;
            nWinHeight.Value = defaults.Height;
            cResolutions.SelectedIndex = defaultResolution;
            xAntiAlias.Checked = defaults.AntiAliasEnabled;
            xFog.Checked = defaults.FogEnabled;
            xShadows.Checked = defaults.ShadowsEnabled;
            tbViewDistance.Value = Math.Max( tbViewDistance.Minimum, Math.Min( defaults.ViewDistance / 32, tbViewDistance.Maximum ) );
            lViewDistance.Text = String.Format( "View distance: {0}", tbViewDistance.Value * 32 );
        }


        void ApplySettings() {
            cWindowMode.SelectedIndex = (int)settings.WindowMode;
            nWinWidth.Value = settings.Width;
            nWinHeight.Value = settings.Height;
            cResolutions.SelectedIndex = defaultResolution;
            xAntiAlias.Checked = settings.AntiAliasEnabled;
            xFog.Checked = settings.FogEnabled;
            xShadows.Checked = settings.ShadowsEnabled;
            tbViewDistance.Value = Math.Max( tbViewDistance.Minimum, Math.Min( settings.ViewDistance / 32, tbViewDistance.Maximum ) );
            lViewDistance.Text = String.Format( "View distance: {0}", tbViewDistance.Value * 32 );
        }


        void bOK_Click( object sender, EventArgs e ) {
            settings.WindowMode = (WindowMode)cWindowMode.SelectedIndex;
            if( settings.WindowMode == WindowMode.Fullscreen ) {
                ScreenResolution selectedResolution = resolutions[cResolutions.SelectedIndex];
                settings.Width = selectedResolution.Width;
                settings.Height = selectedResolution.Height;
            } else {
                settings.Width = (int)nWinWidth.Value;
                settings.Height = (int)nWinHeight.Value;
            }
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

        Button activeButton;

        private void KeyBinding_Click( object sender, EventArgs e ) {
            KeyPreview = true;
            Enabled = false;
            activeButton = (Button)sender;
            activeButton.BackColor = SystemColors.Highlight;
        }

        protected override bool ProcessCmdKey( ref Message msg, Keys keyData ) {
            if( activeButton != null ) {
                if( ( keyData & Keys.Escape ) != Keys.Escape ) {
                    activeButton.Text = keyData.ToString();
                }
                activeButton.BackColor = SystemColors.Control;
                KeyPreview = false;
                Enabled = true;
                activeButton = null;
                return true;
            }
            return base.ProcessCmdKey( ref msg, keyData );
        }
    }
}
