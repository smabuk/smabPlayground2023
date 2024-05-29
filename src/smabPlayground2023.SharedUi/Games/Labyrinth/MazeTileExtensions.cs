namespace smabPlayground2023.SharedUi.Games.Labyrinth;

public static class MazeTileExtensions
{

	public static MazeTile Rotate(this MazeTile tile, int amount) => tile with { Orientation = (tile.Orientation + amount) % 360 };

	public static bool HasNorthExit(this MazeTile tile) => tile.Orientation switch
	{
		0 => tile.NorthExit,
		90 => tile.WestExit,
		180 => tile.SouthExit,
		270 => tile.EastExit,
		_ => false,
	};

	public static bool HasEastExit(this MazeTile tile) => tile.Orientation switch
	{
		0 => tile.EastExit,
		90 => tile.NorthExit,
		180 => tile.WestExit,
		270 => tile.SouthExit,
		_ => false,
	};

	public static bool HasSouthExit(this MazeTile tile) => tile.Orientation switch
	{
		0 => tile.SouthExit,
		90 => tile.EastExit,
		180 => tile.WestExit,
		270 => tile.NorthExit,
		_ => false,
	};

	public static bool HasWestExit(this MazeTile tile) => tile.Orientation switch
	{
		0 => tile.WestExit,
		90 => tile.SouthExit,
		180 => tile.EastExit,
		270 => tile.NorthExit,
		_ => false,
	};

	public static bool HasExit(this MazeTile tile, ReadOnlySpan<char> exit)
	{
		return exit[0] switch
		{
			'N' or 'n' => tile.HasNorthExit(),
			'E' or 'e' => tile.HasEastExit(),
			'S' or 's' => tile.HasSouthExit(),
			'W' or 'w' => tile.HasWestExit(),
			_ => throw new ArgumentOutOfRangeException(nameof(exit)),
		};
	}

	public static string PathDirections(this MazeTile tile)
		=> $"{(tile.HasNorthExit() ? "N" : "")}{(tile.HasEastExit() ? "E" : "")}{(tile.HasSouthExit() ? "S" : "")}{(tile.HasWestExit() ? "W" : "")}";
}
