// Part of ChargedMinersLauncher | Copyright (c) 2012 Matvei Stefarov <me@matvei.org> | BSD-3 | See LICENSE.txt

namespace ChargedMinersLauncher {
    enum LoginResult {
        Success,
        WrongUsernameOrPass,
        MigratedAccount,
        UnrecognizedResponse,
        Error,
        Canceled
    }
}
