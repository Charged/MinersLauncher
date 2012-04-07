// Part of ChargedMinersLaunher | Copyright (c) 2012 Matvei Stefarov <me@matvei.org> | BSD-3 | See LICENSE.txt
using System;
using System.Windows.Forms;

namespace ChargedMinersLauncher {
    public sealed partial class LoadingForm : Form {
        public LoadingForm( string msg ) {
            InitializeComponent();
            Text = msg;
        }

        public void SetText( string text ) {
            if( InvokeRequired ) {
                Invoke( (Action<string>)SetText, text );
            }
            Text = text;
        }
    }
}
