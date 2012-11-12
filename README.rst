======
Charged-Miners Launcher
======

GUI launcher for Charged-Miners client: https://github.com/Charged/Miners

.. image:: http://i.imgur.com/FNy8v.png

Handles software updates and login for Charged-Miners.
Requires Microsoft.NET Framework 3.5.
Tested only on Windows, but should work with Mono.

Latest binaries can be found at https://github.com/Charged/Miners/downloads

---------------
 Version history
---------------
- 1.11
    Launcher now works without an internet connection, or when update site is timing out.

    Fixed "Resume" button remaining enabled even if resuming is not available.

    Fixed buttons not getting appropriate focus when changing tabs.

    Fixed a rare NullReferenceExeception in the updater.

    Fixed "Direct" starting tab preference not being saved.

    Allowed hostnames (e.g. "localhost") in minecraft.net IP/port links.

- 1.10
    New user interface! Now with tabs.

    Added more options (starting tab, how to install game updates, whether to remember username/password/server).

    Added some tools (resetting settings, clearing all data, opening data directory, submitting logs).

- 1.03
    Allowed more kinds of mc:// URLs. Port number is now optional, and hostnames can be used in place of IPs.

    Made "www." optional in minecraft.net URLs.

    Made error messages more informative.

    Added logging to %AppData%/charge/launcher.log.

- 1.02
    Switched from using www.minecraft.net to minecraft.net.

    Added a special handler for "Migrated account" error messages.

    Made Enter/Escape buttons work as expected on various dialogues, without needing to click or tab over to the buttons.

    URL now defaults to a direct-connect link for the most-recently-joined server.

- 1.01
    Removed server-list fetching (now handled internally by CM).

    Added automatic updating of CM.

    Redesigned the interface.

- 1.00
    Initial release.
