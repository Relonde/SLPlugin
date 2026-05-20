using LabApi.Events.Arguments.PlayerEvents;
using LabApi.Events.CustomHandlers;

namespace SLPlugin.Events;

public class FakeNameEventsHandler: CustomEventsHandler {
	// I swear to god there was an OnPlayerJoining event, apparently not?
	public override void OnPlayerJoined(PlayerJoinedEventArgs ev) {
		if (SLPlugin.Instance!.Config.FakeNames.TryGetValue(ev.Player.UserId, out var name))
			ev.Player.ReferenceHub.nicknameSync.Network_myNickSync = name;
	}
}