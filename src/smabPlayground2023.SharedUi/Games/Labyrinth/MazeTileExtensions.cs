namespace smabPlayground2023.SharedUi.Games.Labyrinth;

public static class MazeTileExtensions
{

	public static MazeTile Rotate(this MazeTile tile, int amount) => tile with { Orientation = (tile.Orientation + amount) % 360 };

	public static bool HasNorthExit(this MazeTile card) => card.Orientation switch
	{
		0 => card.NorthExit,
		90 => card.WestExit,
		180 => card.SouthExit,
		270 => card.EastExit,
		_ => false,
	};

	public static bool HasEastExit(this MazeTile card) => card.Orientation switch
	{
		0 => card.EastExit,
		90 => card.NorthExit,
		180 => card.WestExit,
		270 => card.SouthExit,
		_ => false,
	};

	public static bool HasSouthExit(this MazeTile card) => card.Orientation switch
	{
		0 => card.SouthExit,
		90 => card.EastExit,
		180 => card.WestExit,
		270 => card.NorthExit,
		_ => false,
	};

	public static bool HasWestExit(this MazeTile card) => card.Orientation switch
	{
		0 => card.WestExit,
		90 => card.SouthExit,
		180 => card.EastExit,
		270 => card.NorthExit,
		_ => false,
	};

	public static bool HasExit(this MazeTile card, ReadOnlySpan<char> exit)
	{
		return exit[0] switch
		{
			'N' or 'n' => card.HasNorthExit(),
			'E' or 'e' => card.HasEastExit(),
			'S' or 's' => card.HasSouthExit(),
			'W' or 'w' => card.HasWestExit(),
			_ => throw new ArgumentOutOfRangeException(nameof(exit)),
		};
	}

	public static string PathDirections(this MazeTile card)
		=> $"{(card.HasNorthExit() ? "N" : "")}{(card.HasEastExit() ? "E" : "")}{(card.HasSouthExit() ? "S" : "")}{(card.HasWestExit() ? "W" : "")}";
}
