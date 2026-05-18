using LabApi.Events.Arguments.PlayerEvents;
using LabApi.Events.CustomHandlers;

namespace SLPlugin.Events;

public sealed class KillCountEventsHandler: CustomEventsHandler {
	/// <inheritdoc />
	public override void OnPlayerDeath(PlayerDeathEventArgs ev) {
		if (ev.Attacker == null)
			return;

		if (ev.Attacker.DoNotTrack)
			return;

		// If they aren't in the storage already, add them with 1 kill, otherwise +1.
		if (!Storage.Kills.TryAdd(ev.Attacker.UserId, 1)) {
			Storage.Kills[ev.Attacker.UserId] += 1;
		}
	}
}