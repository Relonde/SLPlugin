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
  and not `#ff0000` and `#00ff00`. It also supports showing the time
  remaining and intensity as well.