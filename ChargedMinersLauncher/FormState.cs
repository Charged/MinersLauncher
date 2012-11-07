// Part of ChargedMinersLauncher | Copyright (c) 2012 Matvei Stefarov <me@matvei.org> | BSD-3 | See LICENSE.txt

namespace ChargedMinersLauncher {
    enum FormState {
        AtSignInForm,
        SigningIn,
        WaitingForUpdater,
        UnrecoverableError,
        PromptingToUpdate,
        DownloadingBinary,
    };
}