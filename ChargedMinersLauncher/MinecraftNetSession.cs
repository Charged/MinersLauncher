using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace ChargedMinersLauncher {
    sealed class MinecraftNetSession {
        public static MinecraftNetSession Instance { get; set; }

        public const string RefererUri = "http://www.minecraft.net/",
                            LoginUri = "http://www.minecraft.net/login",
                            LoginSecureUri = "https://www.minecraft.net/login",
                            PlayUri = "http://www.minecraft.net/classic/play/",
                            ServerListUri = "http://www.minecraft.net/classic/list",
                            CookieContainerFile = "saved-session.dat";

        static readonly Regex PlayIP = new Regex( @"name=""server"" value=""([^""]+)""" ),
                              PlayPort = new Regex( @"name=""port"" value=""(\d+)""" ),
                              PlayAuthToken = new Regex( @"name=""mppass"" value=""([0-9a-f]+)""" );

        static readonly Regex LoginAuthToken = new Regex( @"<input type=""hidden"" name=""authenticityToken"" value=""([0-9a-f]+)"">" );

        static readonly Regex ServerListEntry = new Regex( @"<a href=""/classic/play/([0-9a-f]+)"">([^<]+)</a>\s+</td>\s+<td>(\d+)</td>\s+<td>(\d+)</td>\s+<td>(\d+\w)</td>" );


        public string UsernameForLogin { get; set; }
        public string Username { get; private set; }
        public string Password { get; private set; }
        public LoginResult Status { get; set; }

        public MinecraftNetSession( string usernameForLogin, string username, string password ) {
            if( usernameForLogin == null ) throw new ArgumentNullException( "usernameForLogin" );
            if( username == null ) throw new ArgumentNullException( "username" );
            if( password == null ) throw new ArgumentNullException( "password" );
            UsernameForLogin = usernameForLogin;
            Username = username;
            Password = password;
        }


        public LoginResult Login( bool remember ) {
            LoadCookie( remember );

            string loginPage = DownloadString( LoginUri, RefererUri );
            if( loginPage.IndexOf( "Logged in as " + Username, StringComparison.Ordinal ) != -1 ) {
                Status = LoginResult.Success;
                SaveCookie();
                return Status;
            }

            string authToken = LoginAuthToken.Match( loginPage ).Groups[1].Value;

            string loginString = String.Format( "username={0}&password={1}&authenticityToken={2}",
                                                Uri.EscapeDataString( UsernameForLogin ),
                                                Uri.EscapeDataString( Password ),
                                                Uri.EscapeDataString( authToken ) );
            if( remember ) {
                loginString += "&remember=true";
            }

            string loginResponse = UploadString( LoginSecureUri, LoginUri, loginString );
            if( loginResponse.Contains( "Oops, unknown username or password." ) ) {
                Status = LoginResult.WrongUsernameOrPass;

            } else if( loginResponse.IndexOf( "Logged in as " + Username, StringComparison.Ordinal ) != -1 ) {
                Status = LoginResult.Success;
                SaveCookie();

            } else {
                Status = LoginResult.Error;
            }
            return Status;
        }


        void LoadCookie( bool remember ) {
            string cookieFile = Path.Combine( ChargedMinersSettings.ConfigPath, CookieContainerFile );
            if( File.Exists( cookieFile ) ) {
                if( remember ) {
                    BinaryFormatter formatter = new BinaryFormatter();
                    using( Stream s = File.OpenRead( cookieFile ) ) {
                        cookieJar = (CookieContainer)formatter.Deserialize( s );
                    }
                    CookieCollection cookies = cookieJar.GetCookies( new Uri( "http://www.minecraft.net/" ) );
                    bool found = false;
                    foreach( Cookie c in cookies ) {
                        if( c.Value.Contains( "username%3A" + Username ) ) {
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
            if( Directory.Exists( ChargedMinersSettings.ConfigPath ) ) {
                Directory.CreateDirectory( ChargedMinersSettings.ConfigPath );
            }
            string cookieFile = Path.Combine( ChargedMinersSettings.ConfigPath, CookieContainerFile );
            BinaryFormatter formatter = new BinaryFormatter();
            using( Stream s = File.Create( cookieFile ) ) {
                formatter.Serialize( s, cookieJar );
            }
        }


        public ServerInfo[] GetServerList() {
            if( Status != LoginResult.Success ) throw new InvalidOperationException( "Not logged in" );
            string serverPage = DownloadString( ServerListUri, RefererUri );
            List<ServerInfo> list = new List<ServerInfo>();
            int matchNumber = 0;
            foreach( Match match in ServerListEntry.Matches( serverPage ) ) {
                string hash = match.Groups[1].Value;

                // minecraft.net escaping bug workaround
                string name = HttpUtility.HtmlDecode( match.Groups[2].Value ).Replace( "&hellip;", "…" );

                int players;
                if( !Int32.TryParse( match.Groups[3].Value, out players ) ) {
                    continue;
                }

                int maxPlayers;
                if( !Int32.TryParse( match.Groups[4].Value, out maxPlayers ) ) {
                    continue;
                }

                TimeSpan uptime;
                if( !Util.TryParseMiniTimespan( match.Groups[5].Value, out uptime ) ) {
                    continue;
                }
                uptime = uptime.Subtract( new TimeSpan( matchNumber ) ); // to preserve sort order

                list.Add( new ServerInfo( hash, name, players, maxPlayers, uptime ) );
                matchNumber++;
            }

            return list.ToArray();
        }


        public ServerLoginInfo GetServerInfo( string serverHash ) {
            if( serverHash == null ) throw new ArgumentNullException( "serverHash" );
            if( Status != LoginResult.Success ) throw new InvalidOperationException( "Not logged in" );
            string playPage = DownloadString( PlayUri + serverHash, RefererUri );
            Match ipMatch = PlayIP.Match( playPage );
            if( !ipMatch.Success ) return null;
            string rawIP = ipMatch.Groups[1].Value;
            string rawPort = PlayPort.Match( playPage ).Groups[1].Value;
            string authToken = PlayAuthToken.Match( playPage ).Groups[1].Value;
            return new ServerLoginInfo( IPAddress.Parse( rawIP ), Int32.Parse( rawPort ), Username, authToken );
        }


        #region Networking

        const string UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 6.1; en-US; rv:1.9.2.22) Gecko/20110902 Firefox/3.6.22";
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