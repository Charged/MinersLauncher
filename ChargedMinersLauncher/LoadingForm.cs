using System;
using System.Windows.Forms;

namespace ChargedMinersLauncher {
    public partial class LoadingForm : Form {
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
