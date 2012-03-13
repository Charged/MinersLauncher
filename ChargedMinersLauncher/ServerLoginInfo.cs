using System.Net;

namespace ChargedMinersLauncher {
    sealed class ServerLoginInfo {
        public ServerLoginInfo( IPAddress ip, int port, string user, string authToken ) {
            IP = ip;
            Port = port;
            User = user;
            AuthToken = authToken;
        }

        public IPAddress IP { get; private set; }
        public int Port { get; private set; }
        public string User { get; private set; }
        public string AuthToken { get; private set; }
    }
}