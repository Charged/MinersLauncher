using System.IO;
using System.Net;
using System.Text;

namespace ChargedMinersLauncher {
    abstract class IPlaySession {
        public string LoginUsername { get; protected set; }
        public string MinecraftUsername { get; protected set; }
        public virtual string SessionString { get; protected set; }
        public LoginResult Status { get; set; }

        public abstract void Login( string password, bool rememberSession );

        public abstract void CancelAsync();

        #region Networking

        const string UserAgent = "Charged-Miners Launcher";
        const int Timeout = 15000;
        protected CookieContainer CookieJar = new CookieContainer();


        HttpWebResponse MakeRequest( string uri, string referer, string dataToPost ) {
            var request = (HttpWebRequest)WebRequest.Create( uri );
            request.UserAgent = UserAgent;
            request.ReadWriteTimeout = Timeout;
            request.Timeout = Timeout;
            request.Referer = referer;
            request.KeepAlive = true;
            request.CookieContainer = CookieJar;
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


        protected string DownloadString( string uri, string referer ) {
            var response = MakeRequest( uri, referer, null );
            using( Stream stream = response.GetResponseStream() ) {
                if( stream == null ) throw new IOException( "Null response stream for " + uri );
                using( StreamReader reader = new StreamReader( stream ) ) {
                    return reader.ReadToEnd();
                }
            }
        }


        protected string UploadString( string uri, string referer, string dataToPost ) {
            var response = MakeRequest( uri, referer, dataToPost );
            using( Stream stream = response.GetResponseStream() ) {
                if( stream == null ) throw new IOException( "Null response stream after posting to " + uri );
                using( StreamReader reader = new StreamReader( stream ) ) {
                    return reader.ReadToEnd();
                }
            }
        }

        #endregion
    }
}