using LabApi.Events.Arguments.PlayerEvents;
using LabApi.Events.CustomHandlers;
using SLPlugin.Hints;

namespace SLPlugin.Events;

public sealed class HintsEventsHandler: CustomEventsHandler {
	/// <inheritdoc />
	public override void OnPlayerJoined(PlayerJoinedEventArgs ev) {
		if (SLPlugin.Instance!.Config.ShowHints)
			HintsManager.AddToPlayer(ev.Player);
	}
}