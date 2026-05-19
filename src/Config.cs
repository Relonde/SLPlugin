using System.ComponentModel;

// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global

namespace SLPlugin;

/// <summary>
/// Configuration for the plugin
/// </summary>
public sealed class Config {
	[Description("The short name of the server.")]
	public string Name { get; set; } = "My Server";

	[Description("If hints from the plugin should be shown. If disabled, this overrides the values of other " +
	             "hint configs.")]
	public bool ShowHints { get; set; } = true;

	[Description("If the hint showing a players kill count should be enabled.")]
	public bool ShowKillCountHint { get; set; } = true;

	[Description("The text used for the kill count hint, if enabled. $NUM is replaced by the number of kills.")]
	public string KillCountHintText { get; set; } = "<color=red>$NUM</color> kills";

	[Description("If the hint showing a players current effects should be enabled.")]
	public bool ShowEffectListHint { get; set; } = true;

	[Description("If the default staff chat broadcasts should be replaced with a custom UI.")]
	public bool UseCustomStaffChat { get; set; } = true;

	[Description("If notifications should be sent in staff chat for certain behaviours (ex. teamkills)")]
	public bool StaffChatNotifications { get; set; } = true;

	[Description("The title/header shown above staff chat messages (if custom staff chat is enabled).")]
	public string CustomStaffChatTitle { get; set; } = "<color=red>Staff Chat</color>";

	[Description("How long messages should appear in the custom staff chat when sent, in seconds.")]
	public int CustomStaffChatMessageTime { get; set; } = 6;
}