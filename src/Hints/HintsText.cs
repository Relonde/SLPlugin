using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using CustomPlayerEffects;
using HintServiceMeow.Core.Models.Arguments;
using LabApi.Features.Wrappers;
using PlayerRoles;
using PlayerRoles.Voice;

// Apparently HSM has a stroke if the methods to get text are static.
// ReSharper disable MemberCanBeMadeStatic.Global

namespace SLPlugin.Hints;

public sealed class HintsText {
	private static Player? _staffChatSender;
	private static string? _staffChatMessage;
	private static bool _staffChatActiveMessage;

	/// <summary>
	/// Gets the text for the killCountHint.
	/// </summary>
	public string GetKillCountText(AutoContentUpdateArg ev) {
		if (!SLPlugin.Instance!.Config.ShowKillCountHint)
			return string.Empty;

		var player = Player.Get(ev.PlayerDisplay.ReferenceHub);

		if (player == null)
			return string.Empty;

		var builder = new StringBuilder();
		var number = 0;

		if (Storage.Kills.TryGetValue(player.UserId, out var kills))
			number = kills;

		builder.Append(SLPlugin.Instance.Config.KillCountHintText);
		builder.Replace("$NUM", number.ToString());

		return builder.ToString();
	}

	/// <summary>
	/// Gets the text for the effectListHint
	/// </summary>
	public string GetEffectListText(AutoContentUpdateArg ev) {
		if (!SLPlugin.Instance!.Config.ShowEffectListHint)
			return string.Empty;

		var player = Player.Get(ev.PlayerDisplay.ReferenceHub);

		if (player == null || player.Role is RoleTypeId.None or RoleTypeId.Spectator or RoleTypeId.Overwatch)
			return string.Empty;

		var lines = new List<string>();
		var effects = player.ReferenceHub.playerEffectsController.AllEffects;

		if (effects == null)
			return string.Empty;

		// Apparently NW thinks Enums are illegal or something. Seriously, why can't they just add something like
		// RoleTypeId in a PlayerRoleBase to effects? I love northwood.
		foreach (var effect in effects) {
			var effectType = effect.GetType();
			var name = effectType switch {
				{} when effectType == typeof(AmnesiaVision) => "Amnesia",
				{} when effectType == typeof(AnomalousRegeneration) => "Anomalous Regeneration",
				{} when effectType == typeof(AnomalousTarget) => "Anomalous Target",
				{} when effectType == typeof(AntiScp207) => "SCP-207?",
				{} when effectType == typeof(Asphyxiated) => "Asphyxiated",
				{} when effectType == typeof(Bleeding) => "Bleeding",
				{} when effectType == typeof(Blindness) => "Blindness",
				{} when effectType == typeof(Blurred) => "Blurred",
				{} when effectType == typeof(Burned) => "Burned",
				{} when effectType == typeof(CardiacArrest) => "Cardiac Arrest",
				{} when effectType == typeof(Concussed) => "Concussed",
				{} when effectType == typeof(Corroding) => "Corroding",
				{} when effectType == typeof(DamageReduction) => "Damage Reduction",
				{} when effectType == typeof(Deafened) => "Deafened",
				{} when effectType == typeof(Decontaminating) => "Decontaminating",
				{} when effectType == typeof(Disabled) => "Disabled",
				{} when effectType == typeof(Ensnared) => "Ensnared",
				{} when effectType == typeof(Exhausted) => "Exhausted",
				{} when effectType == typeof(Fade) => "Fade",
				{} when effectType == typeof(Flashed) => "Flashed",
				{} when effectType == typeof(Ghostly) => "Ghostly",
				{} when effectType == typeof(HeavyFooted) => "Heavy Footed",
				{} when effectType == typeof(Hemorrhage) => "Hemorrhage",
				{} when effectType == typeof(Invigorated) => "Invigorated",
				{} when effectType == typeof(Invisible) => "Invisible",
				{} when effectType == typeof(Lightweight) => "Lightweight",
				{} when effectType == typeof(Metal) => "Metal",
				{} when effectType == typeof(MovementBoost) => "Movement Boost",
				{} when effectType == typeof(NightVision) => "Night Vision",
				{} when effectType == typeof(OrangeCandy) => "Glow",
				{} when effectType == typeof(Poisoned) => "Poisoned",
				{} when effectType == typeof(RainbowTaste) => "Rainbow Taste",
				{} when effectType == typeof(Scp1344) => "SCP-1344",
				{} when effectType == typeof(Scp1853) => "SCP-1853",
				{} when effectType == typeof(Scp207) => "SCP-207",
				{} when effectType == typeof(SeveredEyes) => "Severed Eyes",
				{} when effectType == typeof(SeveredHands) => "Severed Hands",
				{} when effectType == typeof(SilentWalk) => "Silent Walk",
				{} when effectType == typeof(Sinkhole) => "Sinkhole",
				{} when effectType == typeof(Slowness) => "Slowness",
				{} when effectType == typeof(SpawnProtected) => "Spawn Protection",
				{} when effectType == typeof(Stained) => "Stained",
				{} when effectType == typeof(Strangled) => "Strangled",
				{} when effectType == typeof(SugarRush) => "Sugar Rush",
				{} when effectType == typeof(Traumatized) => "Traumatized",
				{} when effectType == typeof(Vitality) => "Vitality",
				{} when effectType == typeof(WhiteCandy) => "White Candy Ghostly",
				_ => string.Empty
			};

			var suffix = effectType switch {
				{} when effectType == typeof(AntiScp207) => "x",
				{} when effectType == typeof(Blindness) => "%",
				{} when effectType == typeof(Blurred) => "%",
				{} when effectType == typeof(DamageReduction) => "%",
				{} when effectType == typeof(Deafened) => "%",
				{} when effectType == typeof(Fade) => "%",
				{} when effectType == typeof(HeavyFooted) => "%",
				{} when effectType == typeof(Lightweight) => "%",
				{} when effectType == typeof(MovementBoost) => "%",
				{} when effectType == typeof(NightVision) => "%",
				{} when effectType == typeof(Scp1853) => " danger",
				{} when effectType == typeof(Scp207) => "x",
				{} when effectType == typeof(Slowness) => "%",
				_ => string.Empty
			};

			if (name == string.Empty || effect.Intensity == 0)
				continue;

			var line = string.Empty;
			var showIntensity = effect.Intensity > 1;
			var showDuration = effect.Duration != 0;
			var prefix = effect.Classification switch {
				StatusEffectBase.EffectClassification.Positive => "<color=#b4ffb4>",
				StatusEffectBase.EffectClassification.Mixed => "<color=#ffb4ff>",
				StatusEffectBase.EffectClassification.Negative => "<color=#ff7a7a>",
				StatusEffectBase.EffectClassification.Technical => "<color=#f5de5d>",
				_ => "<color=#ffffff>"
			};

			line += $"{prefix}{name}";

			if (showIntensity || showDuration)
				line += " (";

			var intensityText = effect.Intensity.ToString();

			if (effectType == typeof(Scp1853)) {
				intensityText = ((float) effect.Intensity / 4).ToString(CultureInfo.InvariantCulture);
				showIntensity = true;
			}

			if (showIntensity) {
				line += $"{intensityText}{suffix}";

				if (showDuration)
					line += ", ";
			}

			if (showDuration)
				line += $"{Math.Round(effect.TimeLeft)}s";

			if (showIntensity || showDuration)
				line += ")";

			line += "</color>";
			lines.Add(line);
		}

		var output = string.Empty;

		if (player.IsGodModeEnabled)
			output += "<color=#f5de5d>God Mode</color>\n";

		if (player.IsBypassEnabled)
			output += "<color=#f5de5d>Bypass Mode</color>\n";

		if (!player.IsSpectatable)
			output += "<color=#f5de5d>Not Spectatable</color>\n";

		if (Intercom.HasOverride(player.ReferenceHub))
			output += "<color=#f5de5d>Global Intercom</color>\n";

		output += lines.Aggregate(string.Empty, static (current, line) => current + $"{line}\n");
		output.TrimEnd('\n');

		return output;
	}

	public string GetStaffChatTitle(AutoContentUpdateArg ev) {
		// I really don't like the x ? y : z thing, but ig it works.
		return _staffChatActiveMessage ? SLPlugin.Instance!.Config.CustomStaffChatTitle : string.Empty;
	}

	public string GetStaffChatSender(AutoContentUpdateArg ev) {
		if (!_staffChatActiveMessage)
			return string.Empty;

		var name = string.Empty;

		if (_staffChatSender.ReferenceHub.serverRoles.HasBadgeHidden)
			name += $"<color=#A0A0A0>[{_staffChatSender.UserGroup!.BadgeText}]</color>";
		else
			name += $"<color=#{_staffChatSender.ReferenceHub.serverRoles.CurrentColor.ColorHex}>" +
			        $"[{_staffChatSender.ReferenceHub.serverRoles.MyText.Trim('[', ']')}]</color> ";

		name += _staffChatSender.DisplayName;

		return name;
	}

	public string GetStaffChatMessage(AutoContentUpdateArg ev) {
		return _staffChatActiveMessage ? _staffChatMessage : string.Empty;
	}

	internal static void ChangeMessage(Player sender, string message) {
		_staffChatSender = sender;
		_staffChatMessage = message;
		_staffChatActiveMessage = true;
	}

	/// <summary>
	/// Checks if a message is the same as a known one, and if so, clears it. This is so whenever a message is sent, a
	/// delay of however long can be added, and if no new message has been sent, it'll hide. But if a new one is sent,
	/// a new x second timer starts and the other one is ignored.
	/// </summary>
	internal static void RemoveMessage(string knownMessage) {
		// Note: if two people send the same message, it will clear after the first 5s, but that prob won't happen
		// a lot, so its probably fine.
		// TODO: fix ^^^, make unique id for each message?
		if (_staffChatMessage != knownMessage)
			return;

		_staffChatActiveMessage = false;
		_staffChatMessage = null;
		_staffChatSender = null;
	}
}