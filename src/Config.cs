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
}