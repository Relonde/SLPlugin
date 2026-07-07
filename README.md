> [!WARNING]
> **This is not a real SL plugin that should be used**. Unless you have been sent this repo by me (or by someone who I sent it to), there isn't much interesting here. It's a generic SL plugin. ~~I'm also in the process of rewriting it, since it has some issues, primarily being the seperation of things.~~ There's also a list of some others below.

> [!NOTE]
> This plugin has been deprecated in favor of another one which I have made. It replaces the functionality of this one, though its private as of writing (7 July). Please DM me on Discord for access.

# SCP:SL plugin

It's an SCP:SL plugin. I couldn't think of a creative name for it.

## Features

In order that I implemented them:

* A simple kill counter, that for players with DNT disabled, tracks their
  kills in the current round. Whenever the round restarts, their counter is
  reset to zero. People can see their count by a small hint in the bottom
  right (if enabled in the config), or by running `.kills` or `.k`.
* A hint, similar to the one in Brights that shows a players current
  effects. It supports showing the time
  remaining and intensity as well. It also shows if a player is in god
  mode, has bypass, is on the global intercom, etc.
* An `unlimited_ammo` (`uammo` or `ua`) command to allow server staff to
  never need to reload. This works by setting their gun (upon shooting)
  to have max ammo for that gun, which causes it to always be full.
* A custom staff chat UI that overrides the default one, which is
  configurable by the server owner. It supports changing the style to
  whatever the server wants, and it still allows messages to appear
  normally in the RA.
* `fakename` (or `fn` or `spoof`) command to allow staff to fake their
  nicknames. Unlike using the base game `setnick`, this is completely
  invisible to players and a star is **not** displayed after their name.
  It completely replaces the name sent to the clients, though the server
  (as seen in the RA player list and staff chat messages) will still see
  the players actual name, though that's fine since only staff will be
  able to see information that would use their real name.

## Known issues

* The hints for the custom staff chat (if enabled in the config) are only
  added at round start if a user has the `AdminChat` permission. This means
  if someone loses their permissions while in a round, they will still be
  able to see staff chat (but not send messages) until the round restarts.
  I could fix this, but I'm tired as hell, so I'm not going to right now.
* The project overly contains `!` to suppress nullable warnings. I believe
  it should never be null for the usages, but this could definitely be
  improved/fixed.
* The Dictionary containing spoofed player names `Config.FakeNames` is
  reset upon full server restart, because apparently the shit config
  system LabAPI uses can't handle dictionaries, because of course it
  fucking can't. This should be pretty easy to fix by just storing it as
  some encoded string, but I want to just finish this, so I will do that
  whenever I do that.

## Permissions

* `slplugin.uammo` - Controls if a player can use the `unlimited_ammo`
  command.
* `slplugin.fakename` - Allows a user to fake their name with the
  `fakename` command.
