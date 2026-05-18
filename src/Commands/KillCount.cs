using System;
using CommandSystem;
using LabApi.Features.Wrappers;

namespace SLPlugin.Commands;

[CommandHandler(typeof(ClientCommandHandler))]
public sealed class KillCount: ICommand {
	public string Command => "killcount";
	public string[] Aliases => ["k", "kills"];
	public string Description => "Shows your kill count.";

	public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response) {
		// This should never be null, unless NW managed to write the world's shittiest code in a game ever. But then
		// again, it is Northwood, so who knows.
		// ReSharper disable once NullableWarningSuppressionIsUsed
		var player = Player.Get(sender)!;

		if (player.DoNotTrack) {
			response = "Your kill count is not tracked because you have Do Not Track enabled.";

			return false;
		}

		if (!Storage.Kills.TryGetValue(player.UserId, out var value)) {
			response = "Kill count: 0";

			return true;
		}

		response = $"Kill count: {value}";

		return true;
	}
}