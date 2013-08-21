namespace ChargedMinersLauncher {
    interface IPlaySession {
        string LoginUsername { get; }
        string MinecraftUsername { get; }
        LoginResult Status { get; set; }

        void Login( string password, bool rememberSession );

        void CancelAsync();
    }
}