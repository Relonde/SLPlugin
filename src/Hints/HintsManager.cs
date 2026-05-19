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

	// ReSharper disable once AsyncVoidMethod
	public static async void ShowStaffChatHint(Player sender, string message) {
		if (!SLPlugin.Instance!.Config.UseCustomStaffChat)
			return;

		var name = string.Empty;
		var displays = (from player in Player.GetAll()
		                where player.HasPermission(PlayerPermissions.AdminChat)
		                select PlayerDisplay.Get(player)).ToList();

		if (sender.ReferenceHub.serverRoles.HasBadgeHidden)
			name += $"<color=#A0A0A0>[{sender.UserGroup!.BadgeText}]</color>";
		else
			name += $"<color=#{sender.ReferenceHub.serverRoles.CurrentColor.ColorHex}>" +
			        $"[{sender.ReferenceHub.serverRoles.MyText.Trim('[', ']')}]</color> ";

		name += sender.DisplayName;

		var hints = new List<Hint> {
			new Hint {
				Id = "staffChat1",
				YCoordinate = 60,
				Text = $"<b>{SLPlugin.Instance.Config.Name} Staff Chat</b>",
				FontSize = 22,
				SyncSpeed = HintSyncSpeed.Normal
			},
			new Hint {
				Id = "staffChat2",
				YCoordinate = 85,
				Text = $"<b>{name}</b>",
				FontSize = 28,
				SyncSpeed = HintSyncSpeed.Normal
			},
			new Hint {
				Id = "staffChat3",
				YCoordinate = 115,
				Text = message,
				FontSize = 32,
				SyncSpeed = HintSyncSpeed.Normal
			}
		};

		foreach (var display in displays) {
			foreach (var hint in hints) {
				display.AddHint(hint);
			}
		}

		// 5 secs
		await Task.Delay(new TimeSpan(0, 0, 5));

		foreach (var display in displays) {
			foreach (var hint in hints) {
				display.RemoveHint(hint);
			}
		}
	}
}