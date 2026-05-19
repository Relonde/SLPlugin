# SCP:SL plugin

It's an SCP:SL plugin. I couldn't think of a creative name for it.

## Features

In order that I implemented them:

* A simple kill counter, that for players with DNT disabled, tracks their
  kills in the current round. Whenever the round restarts, their counter is
  reset to zero. People can see their count by a small hint in the bottom
  right (if enabled in the config), or by running `.kills` or `.k`.
* A hint, similar to the one in Brights that shows a players current
  effects. Unlike the one in Brights however, the colours are actually good
  and not `#ff0000` and `#00ff00`. It supports showing the time
  remaining and intensity as well. It also shows if a player is in god
  mode, has bypass, is on the global intercom, etc.
* An `unlimited_ammo` (`uammo` or `ua`) command to allow server staff to
  never need to reload. This works by setting their gun (upon shooting)
  to have max ammo for that gun, which causes it to always be full.
* A custom staff chat UI that overrides the default one, which is
  configurable by the server owner. It supports changing the style to
  whatever the server wants, and it still allows messages to appear
  normally in the RA.

## Permissions

`slplugin.uammo` - Controls if a player can use the `unlimited_ammo` command.
`slplugin.scsendnotif` - Controls if a player can manually send staff chat
notifications in the style the plugin sends (if enabled).