// Part of ChargedMinersLaunher | Copyright (c) 2012 Matvei Stefarov <me@matvei.org> | BSD-3 | See LICENSE.txt
using System;
using System.Drawing;
using System.Windows.Forms;

namespace ChargedMinersLauncher {
    sealed partial class WarningForm : Form {
        const TextFormatFlags TextRendererFlags = TextFormatFlags.WordBreak;

        WarningForm( string title, string text ) {
            InitializeComponent();
            Text = title;
            tText.Text = text;
            
            // calculate ideal window size
            Size sz = new Size( tText.ClientSize.Width, int.MaxValue );
            sz = TextRenderer.MeasureText( tText.Text, tText.Font, sz, TextRendererFlags );

            // resize window to fit
            int textBoxBorders = tText.Height - tText.ClientSize.Height;
            int verticalMargin = tText.Margin.Top + tText.Margin.Bottom + bOK.Margin.Top + bOK.Margin.Bottom;
            int horizontalMargin = tText.Margin.Left + tText.Margin.Right;
            int verticalPadding = Padding.Top + Padding.Bottom;
            int horizontalPadding = Padding.Left + Padding.Right;
            ClientSize = new Size {
                Height = sz.Height + verticalMargin + verticalPadding + textBoxBorders + bOK.Height,
                Width = sz.Width + horizontalMargin + horizontalPadding
            };
        }


        protected override void OnSizeChanged( EventArgs e ) {
            // resize button
            bOK.Top = ClientRectangle.Height - bOK.Height - bOK.Margin.Bottom;
            bOK.Left = ( ClientRectangle.Width - bOK.Width ) / 2 - bOK.Margin.Left;

            // resize text area
            tText.Top = tText.Margin.Top;
            tText.Left = tText.Margin.Left;
            tText.Height = bOK.Top - tText.Margin.Top - tText.Margin.Bottom;
            tText.Width = ClientRectangle.Width - tText.Margin.Left - tText.Margin.Right;

            base.OnSizeChanged( e );
        }


        public static void Show( string title, string text ) {
            WarningForm box = new WarningForm( title, text );
            box.ShowDialog();
        }
    }
}
