using System;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace ChargedMinersLauncher {
    class ClassiCubeSession : IPlaySession {
        const string ClassiCubeNet = "http://www.classicube.net",
                     LoginUri = "http://www.classicube.net/acc/login";

        static readonly Regex
            CsrfToken = new Regex( @"name=""csrf_token"" type=""hidden"" value=""([^""]+)"">" ),
            LoggedInAs = new Regex( @"<a href=""/acc"" class=""button"">([a-zA-Z0-9_\.]{2,16})</a>" );

        const string LoginFailedMessage = "Login failed";


        public ClassiCubeSession( string minecraftUsername ) {
            MinecraftUsername = minecraftUsername;
            LoginUsername = minecraftUsername;
        }

        public override void Login( string password, bool rememberSession ) {
            if( password == null )
                throw new ArgumentNullException( "password" );

            // todo: remember sessions
            MainForm.SetSignInStatus( "Connecting to ClassiCube.net..." );

            // download the login page
            string loginPage = DownloadString( LoginUri, ClassiCubeNet );
            Debug.WriteLine( loginPage );

            // See if we're already logged in
            if( LoggedInAs.IsMatch( loginPage ) ) {
                // todo
            }

            Match csrfTokenMatch = CsrfToken.Match( loginPage );
            if( !csrfTokenMatch.Success ) {
                // todo: handle ClassiCube derping
            }

            string csrfToken = csrfTokenMatch.Groups[1].Value;
            string loginString = String.Format( "csrf_token={0}&username={1}&password={2}",
                                                Uri.EscapeDataString( csrfToken ),
                                                Uri.EscapeDataString( LoginUsername ),
                                                Uri.EscapeDataString( password ) );
            if( rememberSession ) {
                loginString += "&remember_me=y";
            }

            string loginResponse = UploadString( LoginUri, LoginUri, loginString );


            // See if now we're logged in
            if( loginResponse.Contains( LoginFailedMessage ) ) {
                Status = LoginResult.WrongUsernameOrPass;

            } else if( LoggedInAs.IsMatch( loginResponse ) ) {
                MinecraftUsername = LoggedInAs.Match( loginResponse ).Groups[1].Value;
                // todo: get play session ID
                Status = LoginResult.Success;
                // todo: save cookie

            } else {
                MainForm.Log( "CC.Login: Something went wrong: " + loginResponse );
                Status = LoginResult.UnrecognizedResponse;
            }
        }

        public override void CancelAsync() {
            throw new NotImplementedException();
        }
    }
}