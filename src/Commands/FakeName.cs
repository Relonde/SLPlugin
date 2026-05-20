using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using CommandSystem;
using LabApi.Features.Permissions;
using LabApi.Features.Wrappers;

namespace SLPlugin.Commands;

[CommandHandler(typeof(RemoteAdminCommandHandler))]
public class FakeName: ICommand {
	public string Command => "fakename";
	public string[] Aliases => ["fn", "spoof"];
	public string Description => "Spoofs your name, hiding your real username completely (to non-admins).";

	// Imagine this didn't return bool, but instead (bool, string)
	public bool Execute(ArraySegment<string> arguments, ICommandSender sender, [UnscopedRef] out string response) {
		// ReSharper disable once NullableWarningSuppressionIsUsed
		var player = Player.Get(sender)!;
		var id = player.UserId;

		if (!player.HasPermissions("slplugin.fakename")) {
			response = "You do not have permission to run this command.";

			return false;
		}

		if (arguments[0] == null) {
			SLPlugin.Instance!.Config.FakeNames.Remove(player.UserId);

			// ReSharper disable once GrammarMistakeInStringLiteral
			response = "Reset your fake name, this will apply upon next round restart or you rejoining.";

			return true;
		}

		var name = arguments.Aggregate(string.Empty, static (current, arg) => current + $"{arg} ");

		name = name.TrimEnd(" ").ToString();

		SLPlugin.Instance!.Config.FakeNames.Add(player.UserId, name);

		response = $"Set your fake name to \"{name}\", this will apply upon next round restart or you " +
		           $"rejoining.\n\nRun \"fakename\" without a name given to reset it.";

		return true;
	}
}