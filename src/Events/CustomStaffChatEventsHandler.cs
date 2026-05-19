using System.Linq;
using LabApi.Events.Arguments.ServerEvents;
using LabApi.Events.CustomHandlers;
using LabApi.Features.Console;
using LabApi.Features.Wrappers;
using SLPlugin.Hints;

namespace SLPlugin.Events;

// Incredible naming here
public sealed class CustomStaffChatEventsHandler: CustomEventsHandler {
	/// <inheritdoc />
	public override void OnServerSendingAdminChat(SendingAdminChatEventArgs ev) {
		ev.IsAllowed = false;

		var player = Player.Get(ev.Sender);

		if (!player!.HasPermission(PlayerPermissions.AdminChat))
			return;

		HintsManager.ShowStaffChatHints(player, ev.Message);

		var targets = Player.GetAll().Where(static p => p.HasPermission(PlayerPermissions.AdminChat)).ToList();

		foreach (var target in targets) {
			target.ReferenceHub.encryptedChannelManager.TrySendMessageToClient(
				$"{player.ReferenceHub.netId}!{ev.Message}", EncryptedChannelManager.EncryptedChannel.AdminChat);
			target.ClearBroadcasts();
		}
	}
}