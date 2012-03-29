using System.Net;

namespace ChargedMinersLauncher {
    sealed class ServerLoginInfo {
        public ServerLoginInfo( IPAddress ip, int port, string user, string authToken ) {
            IP = ip;
            Port = port;
            User = user;
            AuthToken = authToken;
        }

        public IPAddress IP { get; set; }
        public int Port { get; set; }
        public string User { get; set; }
        public string AuthToken { get; set; }
    }
}