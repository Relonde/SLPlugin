using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HintServiceMeow.Core.Enum;
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
				YCoordinate = 1065,
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

		// I could make the allowed players into a list stored elsewhere and updated on joins, leaves, and roles being
		// updated, but I'm fucking tired right now, so I'm not going to do that and it's probably fine
		// TODO: that ^^^
		if (player.HasPermission(PlayerPermissions.AdminChat) && SLPlugin.Instance.Config.UseCustomStaffChat) {
			hints.Add(new Hint {
				Id = "staffChatTitle",
				YCoordinate = 60,
				AutoText = hintsText.GetStaffChatTitle,
				FontSize = 22,
				SyncSpeed = HintSyncSpeed.Fastest
			});

			hints.Add(new Hint {
				Id = "staffChatSender",
				YCoordinate = 85,
				AutoText = hintsText.GetStaffChatSender,
				FontSize = 28,
				SyncSpeed = HintSyncSpeed.Fastest
			});

			hints.Add(new Hint {
				Id = "staffChatMessage",
				YCoordinate = 115,
				AutoText = hintsText.GetStaffChatMessage,
				FontSize = 32,
				SyncSpeed = HintSyncSpeed.Fastest
			});
		}

		foreach (var hint in hints) {
			display.AddHint(hint);
		}
	}

	// ReSharper disable once AsyncVoidMethod
	public static async void ShowStaffChatHints(Player sender, string message) {
		// There's a better way to do this, but technically it works, so I don't give a shit
		HintsText.ChangeMessage(sender, message);

		await Task.Delay(new TimeSpan(0, 0, SLPlugin.Instance!.Config.CustomStaffChatMessageTime));

		HintsText.RemoveMessage(message);
	}
}