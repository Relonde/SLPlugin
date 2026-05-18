using System.Collections.Generic;

namespace SLPlugin;

public static class Storage {
	/// <summary>
	/// Dictionary storing player kill counts. The key (string) is their user ID, the int is their kill count.
	/// </summary>
	public static readonly Dictionary<string, int> Kills = new();
}