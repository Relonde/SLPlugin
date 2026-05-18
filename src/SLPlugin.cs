using System;
using System.Collections.Generic;
using LabApi.Events.CustomHandlers;
using LabApi.Features;
using LabApi.Loader.Features.Plugins;
using SLPlugin.Events;

namespace SLPlugin;

/// <summary>
/// Base of the plugin
/// </summary>
public sealed class SLPlugin: Plugin {
	/// <inheritdoc />
	public override string Name => "SLPlugin";

	/// <inheritdoc />
	public override string Description => "It's an SCPSL plugin";

	/// <inheritdoc />
	public override string Author => "Relonde";

	/// <inheritdoc />
	public override Version Version => new(1, 1, 1);

	/// <inheritdoc />
	public override Version RequiredApiVersion => new(LabApiProperties.CompiledVersion);

	/// <summary>
	/// List of all event handlers used by the plugin.
	/// </summary>
	private static readonly List<CustomEventsHandler> EventHandlers = [];

	/// <inheritdoc />
	public override void Enable() {
		EventHandlers.Add(new KillCountEventsHandler());

		foreach (var handler in EventHandlers) {
			CustomHandlersManager.RegisterEventsHandler(handler);
		}
	}

	/// <inheritdoc />
	public override void Disable() {
		foreach (var handler in EventHandlers) {
			CustomHandlersManager.UnregisterEventsHandler(handler);
		}
	}
}