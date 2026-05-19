using System;
using System.Diagnostics.CodeAnalysis;
using CommandSystem;
using LabApi.Features.Permissions;
using LabApi.Features.Wrappers;

namespace SLPlugin.Commands;

[CommandHandler(typeof(RemoteAdminCommandHandler))]
public class UnlimitedAmmo: ICommand {
	public string Command => "unlimited_ammo";
	public string[] Aliases => ["ua", "uammo"];
	public string Description => "Controls whether you have unlimited ammo in your guns.";

	public bool Execute(ArraySegment<string> arguments, ICommandSender sender, [UnscopedRef] out string response) {
		// ReSharper disable once NullableWarningSuppressionIsUsed
		var player = Player.Get(sender)!;
		var id = player.UserId;

		if (!player.HasPermissions("slplugin.uammo")) {
			response = "You do not have permission to run this command.";

			return false;
		}

		if (arguments[0] == null) {
			response = "Usage: unlimited_ammo (true/false)";

			return false;
		}

		var parsed = bool.TryParse(arguments[0], out var result);

		// Invalid bool
		if (!parsed) {
			response = "Usage: unlimited_ammo (true/false)";

			return false;
		}

		if (result) {
			if (Storage.UnlimitedAmmoUsers.Contains(id)) {
				response = "You already have unlimited ammo enabled";

				return false;
			}

			Storage.UnlimitedAmmoUsers.Add(id);
		} else {
			if (!Storage.UnlimitedAmmoUsers.Contains(id)) {
				response = "You already have unlimited ammo disabled";

				return false;
			}

			Storage.UnlimitedAmmoUsers.Remove(id);
		}

		response = "Done!";

		return true;
	}
}