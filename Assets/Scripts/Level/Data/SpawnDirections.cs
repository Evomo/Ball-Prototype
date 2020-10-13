using System;

namespace Level.Data {
	[Flags]
	public enum SpawnPosition {
		None,
		North = 1 << 1,
		South = 1 << 2,
		East = 1 << 3,
		West = 1 << 4
	}
}