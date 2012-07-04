using System;
using System.Windows.Forms;


sealed class TablessControl : TabControl {
    // based on http://social.msdn.microsoft.com/forums/en-US/winforms/thread/c290832f-3b84-4200-aa4a-7a5dc4b8b5bb/
    protected override void WndProc( ref Message m ) {
        // Hide tabs by trapping the TCM_ADJUSTRECT message
        if( m.Msg == 0x1328 && !DesignMode ) m.Result = (IntPtr)1;
        else base.WndProc( ref m );
    }

    // based on http://dotnetrix.co.uk/tabcontrol.htm
    protected override bool ProcessCmdKey( ref Message msg, Keys keyData ) {
        if( keyData == ( Keys.Tab | Keys.Control ) || keyData == ( Keys.Tab | Keys.Control | Keys.Shift) )
            return true;
        return base.ProcessCmdKey( ref msg, keyData );
    }
}