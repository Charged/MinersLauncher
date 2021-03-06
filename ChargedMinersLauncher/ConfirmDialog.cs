﻿using System.Windows.Forms;

namespace ChargedMinersLauncher {
    public partial class ConfirmDialog : Form {
        ConfirmDialog() {
            InitializeComponent();
        }

        public static bool Show( string heading, string message ) {
            ConfirmDialog dialog = new ConfirmDialog {
                Text = heading,
                lMessage = {
                    Text = message
                }
            };
            return ( dialog.ShowDialog() == DialogResult.OK );
        }
    }
}