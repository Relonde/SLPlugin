using LabApi.Events.Arguments.PlayerEvents;
using LabApi.Events.CustomHandlers;

namespace SLPlugin.Events;

// Extremely complicated code in this one.
public sealed class UnlimitedAmmoEventsHandler: CustomEventsHandler {
	/// <inheritdoc />
	public override void OnPlayerShootingWeapon(PlayerShootingWeaponEventArgs ev) {
		if (Storage.UnlimitedAmmoUsers.Contains(ev.Player.UserId))
			ev.FirearmItem.StoredAmmo = ev.FirearmItem.MaxAmmo;
	}

	/// <inheritdoc />
	public override void OnPlayerShotWeapon(PlayerShotWeaponEventArgs ev) {
		if (Storage.UnlimitedAmmoUsers.Contains(ev.Player.UserId))
			ev.FirearmItem.StoredAmmo = ev.FirearmItem.MaxAmmo;
	}
}