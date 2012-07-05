// Part of ChargedMinersLaunher | Copyright (c) 2012 Matvei Stefarov <me@matvei.org> | BSD-3 | See LICENSE.txt
using System;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.RegularExpressions;

namespace ChargedMinersLauncher {
    sealed class MinecraftNetSession {
        public static MinecraftNetSession Instance { get; set; }

        const string RefererUri = "http://www.minecraft.net/",
                     LoginUri = "http://www.minecraft.net/login",
                     LoginSecureUri = "https://www.minecraft.net/login";

        static readonly Regex
            LoginAuthToken = new Regex( @"<input type=""hidden"" name=""authenticityToken"" value=""([0-9a-f]+)"">" ),
            LoggedInAs = new Regex( @"<span class=""logged-in"">\s*Logged in as ([a-zA-Z0-9_\.]{2,16})" );

        public string LoginUsername { get; private set; }
        public string MinercraftUsername { get; private set; }
        public string Password { get; private set; }
        public LoginResult Status { get; set; }
        public Exception LoginException { get; set; }

        public string PlaySessionCookie {
            get {
                CookieCollection cookies = cookieJar.GetCookies( new Uri( "http://www.minecraft.net/" ) );
                return cookies["PLAY_SESSION"].Value;
            }
        }


        public MinecraftNetSession( string loginUsername, string minercraftUsername, string password ) {
            if( loginUsername == null ) throw new ArgumentNullException( "loginUsername" );
            if( password == null ) throw new ArgumentNullException( "password" );
            LoginUsername = loginUsername;
            MinercraftUsername = minercraftUsername;
            Password = password;
        }


        public LoginResult Login( bool remember ) {
            LoadCookie( remember );

            string loginPage = DownloadString( LoginUri, RefererUri );
            if( LoggedInAs.IsMatch( loginPage ) ) {
                MinercraftUsername = LoggedInAs.Match( loginPage ).Groups[1].Value;
                Status = LoginResult.Success;
                SaveCookie();
                return Status;
            }

            string authToken = LoginAuthToken.Match( loginPage ).Groups[1].Value;

            string loginString = String.Format( "username={0}&password={1}&authenticityToken={2}",
                                                Uri.EscapeDataString( LoginUsername ),
                                                Uri.EscapeDataString( Password ),
                                                Uri.EscapeDataString( authToken ) );
            if( remember ) {
                loginString += "&remember=true";
            }

            string loginResponse = UploadString( LoginSecureUri, LoginUri, loginString );
            if( loginResponse.Contains( "Oops, unknown username or password." ) ) {
                Status = LoginResult.WrongUsernameOrPass;

            } else if( LoggedInAs.IsMatch( loginResponse ) ) {
                MinercraftUsername = LoggedInAs.Match( loginResponse ).Groups[1].Value;
                Status = LoginResult.Success;
                SaveCookie();

            } else {
                Status = LoginResult.Error;
            }
            return Status;
        }


        void LoadCookie( bool remember ) {
            string cookieFile = Paths.CookieContainerFile;
            if( File.Exists( cookieFile ) ) {
                if( remember ) {
                    BinaryFormatter formatter = new BinaryFormatter();
                    using( Stream s = File.OpenRead( cookieFile ) ) {
                        cookieJar = (CookieContainer)formatter.Deserialize( s );
                    }
                    CookieCollection cookies = cookieJar.GetCookies( new Uri( "http://www.minecraft.net/" ) );
                    bool found = false;
                    foreach( Cookie c in cookies ) {
                        if( c.Value.Contains( "username%3A" + MinercraftUsername ) ) {
                            found = true;
                            break;
                        }
                    }
                    if( !found ) {
                        cookieJar = new CookieContainer();
                    }
                } else {
                    File.Delete( cookieFile );
                }
            } else {
                cookieJar = new CookieContainer();
            }
        }


        void SaveCookie() {
            string cookieFile = Paths.CookieContainerFile;
            BinaryFormatter formatter = new BinaryFormatter();
            using( Stream s = File.Create( cookieFile ) ) {
                formatter.Serialize( s, cookieJar );
            }
        }


        #region Networking

        const string UserAgent = "Charged-Miners Launcher";
        const int Timeout = 15000;
        CookieContainer cookieJar;


        HttpWebResponse MakeRequest( string uri, string referer, string dataToPost ) {
            var request = (HttpWebRequest)WebRequest.Create( uri );
            request.UserAgent = UserAgent;
            request.ReadWriteTimeout = Timeout;
            request.Timeout = Timeout;
            request.Referer = referer;
            request.KeepAlive = true;
            request.CookieContainer = cookieJar;
            if( dataToPost != null ) {
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                byte[] data = Encoding.UTF8.GetBytes( dataToPost );
                request.ContentLength = data.Length;
                using( Stream stream = request.GetRequestStream() ) {
                    stream.Write( data, 0, data.Length );
                }
            }
            return (HttpWebResponse)request.GetResponse();
        }


        string DownloadString( string uri, string referer ) {
            var response = MakeRequest( uri, referer, null );
            using( Stream stream = response.GetResponseStream() ) {
                if( stream == null ) throw new IOException();
                using( StreamReader reader = new StreamReader( stream ) ) {
                    return reader.ReadToEnd();
                }
            }
        }


        string UploadString( string uri, string referer, string dataToPost ) {
            var response = MakeRequest( uri, referer, dataToPost );
            using( Stream stream = response.GetResponseStream() ) {
                if( stream == null ) throw new IOException();
                using( StreamReader reader = new StreamReader( stream ) ) {
                    return reader.ReadToEnd();
                }
            }
        }

        #endregion
    }
}