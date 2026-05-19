using System.Collections.Generic;
using System.Text;
using HintServiceMeow.Core.Enum;
using HintServiceMeow.Core.Models.Arguments;
using HintServiceMeow.Core.Utilities;
using LabApi.Features.Console;
using LabApi.Features.Wrappers;
using Hint = HintServiceMeow.Core.Models.Hints.Hint;

namespace SLPlugin.Hints;

/// <summary>
/// Manages hints on the screen of players.
/// </summary>
public static class HintsManager {
	/// <summary>
	/// Adds all hints to a players display.
	/// </summary>
	/// <param name="player">The player to add the hints to.</param>
	public static void AddToPlayer(Player player) {
		if (!SLPlugin.Instance!.Config.ShowHints)
			return;

		var display = PlayerDisplay.Get(player);
		var hintsText = new HintsText();
		var hints = new List<Hint> {
			new Hint {
				Id = "serverNameHint",
				Alignment = HintAlignment.Center,
				YCoordinate = 1060,
				Text = SLPlugin.Instance.Config.Name,
				FontSize = 32,
				SyncSpeed = HintSyncSpeed.Normal
			}
		};

		if (SLPlugin.Instance.Config.ShowKillCountHint)
			hints.Add(new Hint {
				Id = "killCountHint",
				Alignment = HintAlignment.Right,
				YCoordinate = 1060,
				AutoText = hintsText.GetKillCountText,
				FontSize = 20,
				SyncSpeed = HintSyncSpeed.Fast
			});

		if (SLPlugin.Instance.Config.ShowEffectListHint)
			hints.Add(new Hint {
				Id = "effectsListHint",
				Alignment = HintAlignment.Left,
				AutoText = hintsText.GetEffectListText,
				FontSize = 20,
				SyncSpeed = HintSyncSpeed.Fastest
			});

		foreach (var hint in hints) {
			display.AddHint(hint);
		}

		foreach (var hint in display.GetHints()) {
			Logger.Debug(hint.Id);
		}
	}
}