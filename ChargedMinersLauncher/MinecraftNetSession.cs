// Part of ChargedMinersLauncher | Copyright (c) 2012-2013 Matvei Stefarov <me@matvei.org> | BSD-3 | See LICENSE.txt
using System;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.RegularExpressions;

namespace ChargedMinersLauncher {
    sealed class MinecraftNetSession : IPlaySession {
        const string MinecraftNet = "http://minecraft.net/",
                     LoginSecureUri = "https://minecraft.net/login",
                     LogoutUri = "http://minecraft.net/logout";

        static readonly Regex
            LoginAuthToken = new Regex( @"<input type=""hidden"" name=""authenticityToken"" value=""([0-9a-f]+)"">" ),
            LoggedInAs = new Regex( @"<span class=""logged-in"">\s*Logged in as ([a-zA-Z0-9_\.]{2,16})" );

        const string MigratedAccountMessage = "Your account has been migrated",
                     WrongUsernameOrPasswordMessage = "Oops, unknown username or password.";


        public MinecraftNetSession( string loginUsername, string minecraftUsername ) {
            if( loginUsername == null ) {
                throw new ArgumentNullException( "loginUsername" );
            }
            if( minecraftUsername == null ) {
                throw new ArgumentNullException( "minecraftUsername" );
            }
            LoginUsername = loginUsername;
            MinecraftUsername = minecraftUsername;
        }


        volatile bool cancel;
        Cookie PlaySessionCookie {
            get {
                CookieCollection cookies = CookieJar.GetCookies( new Uri( MinecraftNet ) );
                return cookies["PLAY_SESSION"];
            }
        }

        public override string SessionString {
            get {
                return PlaySessionCookie.Value;
            }
        }


        public override void Login( string password, bool rememberSession ) {
            if( password == null ) throw new ArgumentNullException( "password" );

            bool restoredSession = LoadCookie( rememberSession );
            MainForm.SetSignInStatus( "Connecting to Minecraft.net..." );

            // check if cancel is needed
            if( cancel ) {
                Status = LoginResult.Canceled;
                cancel = false;
                return;
            }

            // download the login page
            string loginPage = DownloadString( LoginSecureUri, MinecraftNet );

            // See if we're already logged in
            if( LoggedInAs.IsMatch( loginPage ) ) {
                string loggedInUsername = LoggedInAs.Match( loginPage ).Groups[1].Value;
                if( rememberSession && PlaySessionCookie != null &&
                    MinecraftUsername.Equals( loggedInUsername, StringComparison.OrdinalIgnoreCase ) ) {
                    // If player is already logged in with the right account: reuse a previous session
                    MinecraftUsername = loggedInUsername;
                    MainForm.Log( "MC.Login: Restored session for " + MinecraftUsername );
                    Status = LoginResult.Success;
                    SaveCookie();
                    return;
                } else {
                    // If we're not supposed to reuse session, if old username is different,
                    // or if there is no play session cookie set - relog
                    MainForm.SetSignInStatus( "Switching accounts..." );
                    DownloadString( LogoutUri, MinecraftNet );
                    loginPage = DownloadString( LoginSecureUri, LogoutUri );
                }
            }

            // Extract authenticityToken from the login page
            Match authTokenMatch = LoginAuthToken.Match( loginPage );
            if( !authTokenMatch.Success ) {
                if( restoredSession ) {
                    // restoring session failed; log out and retry
                    DownloadString( LogoutUri, MinecraftNet );
                    ResetCookies();
                    MainForm.Log( "MC.Login: Unrecognized page; retrying" );
                    Login( password, rememberSession );
                    return;
                } else {
                    // something unexpected happened, panic!
                    MainForm.Log( "MC.Login: Unrecognized page: " + loginPage );
                    Status = LoginResult.UnrecognizedResponse;
                }
                return;
            }
            string authToken = authTokenMatch.Groups[1].Value;

            // Build up form data
            string loginString = String.Format( "username={0}&password={1}&authenticityToken={2}",
                                                Uri.EscapeDataString( LoginUsername ),
                                                Uri.EscapeDataString( password ),
                                                Uri.EscapeDataString( authToken ) );
            if( rememberSession ) {
                loginString += "&remember=true";
            }

            // check if cancel is needed
            if( cancel ) {
                Status = LoginResult.Canceled;
                cancel = false;
                return;
            }

            // POST to the login form
            MainForm.SetSignInStatus( "Sending login information..." );
            string loginResponse = UploadString( LoginSecureUri, LoginSecureUri, loginString );

            // check if cancel is needed
            if( cancel ) {
                Status = LoginResult.Canceled;
                cancel = false;
                return;
            }

            // Check the response
            if( loginResponse.Contains( WrongUsernameOrPasswordMessage ) ) {
                Status = LoginResult.WrongUsernameOrPass;

            } else if( LoggedInAs.IsMatch( loginResponse ) ) {
                MinecraftUsername = LoggedInAs.Match( loginResponse ).Groups[1].Value;
                if( PlaySessionCookie == null ) {
                    CookieCollection cookies = CookieJar.GetCookies( new Uri( MinecraftNet ) );
                    MainForm.Log( "MC.Login: No play session. There were " + cookies.Count + " cookies served:" );
                    foreach( Cookie cookie in cookies ) {
                        MainForm.Log( "  " + cookie );
                    }
                    Status = LoginResult.NoPlaySession;
                    return;
                }
                Status = LoginResult.Success;
                SaveCookie();

            } else if( loginResponse.Contains( MigratedAccountMessage ) ) {
                Status = LoginResult.MigratedAccount;

            } else {
                MainForm.Log( "MC.Login: Unrecognized response: " + loginResponse );
                Status = LoginResult.UnrecognizedResponse;
            }
        }


        public override void CancelAsync() {
            cancel = true;
        }


        // Handle saved minecraft.net sessions
        bool LoadCookie( bool remember ) {
            if( File.Exists( Paths.CookieContainerFile ) ) {
                if( remember ) {
                    // load a saved session
                    BinaryFormatter formatter = new BinaryFormatter();
                    using( Stream s = File.OpenRead( Paths.CookieContainerFile ) ) {
                        CookieJar = (CookieContainer)formatter.Deserialize( s );
                    }
                    CookieCollection cookies = CookieJar.GetCookies( new Uri( MinecraftNet ) );
                    foreach( Cookie c in cookies ) {
                        // look for a cookie that corresponds to the current minecraft username
                        int start = c.Value.IndexOf( "username%3A" + MinecraftUsername,
                                                     StringComparison.OrdinalIgnoreCase );
                        if( start != -1 ) {
                            MainForm.Log( "MC.LoadCookie: Loaded saved session for " + MinecraftUsername );
                            return true;
                        }
                    }

                    // if saved session was not for the current username, discard it
                    MainForm.Log( "MC.LoadCookie: Discarded a saved session (username mismatch)" );
                    ResetCookies();

                } else {
                    // discard a saved session
                    MainForm.Log( "MC.LoadCookie: Discarded a saved session" );
                    ResetCookies();
                }

            } else {
                // no session saved
                ResetCookies();
            }
            return false;
        }


        void ResetCookies() {
            File.Delete( Paths.CookieContainerFile );
            CookieJar = new CookieContainer();
        }


        void SaveCookie() {
            BinaryFormatter formatter = new BinaryFormatter();
            using( Stream s = File.Create( Paths.CookieContainerFile ) ) {
                formatter.Serialize( s, CookieJar );
            }
        }
    }
}