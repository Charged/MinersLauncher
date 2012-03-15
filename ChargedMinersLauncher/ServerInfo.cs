using System;

namespace ChargedMinersLauncher {
    sealed class ServerInfo {
        public ServerInfo( string hash, string name, int players, int maxPlayers, TimeSpan uptime ) {
            Hash = hash;
            Name = name;
            Players = players;
            MaxPlayers = maxPlayers;
            Uptime = uptime;
        }

        internal string Hash { get; private set; }

        public string Name { get; private set; }
        public int Players { get; private set; }
        public int MaxPlayers { get; private set; }
        public TimeSpan Uptime { get; private set; }
    }
}