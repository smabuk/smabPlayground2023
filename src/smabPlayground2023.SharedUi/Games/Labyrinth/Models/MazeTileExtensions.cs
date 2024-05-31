using static smabPlayground2023.SharedUi.Games.Labyrinth.Direction;

namespace smabPlayground2023.SharedUi.Games.Labyrinth.Models;

public static class MazeTileExtensions
{

	public static MazeTile Rotate(this MazeTile tile, int amount) => tile with { Orientation = (tile.Orientation + amount + 360) % 360 };

	public static bool HasNorthExit(this MazeTile tile) => tile.Orientation switch
	{
		  0 => tile.Exits.HasFlag(North),
		 90 => tile.Exits.HasFlag(West),
		180 => tile.Exits.HasFlag(South),
		270 => tile.Exits.HasFlag(East),
		_ => false,
	};

	public static bool HasEastExit(this MazeTile tile) => tile.Orientation switch
	{
		  0 => tile.Exits.HasFlag(East),
		 90 => tile.Exits.HasFlag(North),
		180 => tile.Exits.HasFlag(West),
		270 => tile.Exits.HasFlag(South),
		_ => false,
	};

	public static bool HasSouthExit(this MazeTile tile) => tile.Orientation switch
	{
		  0 => tile.Exits.HasFlag(South),
		 90 => tile.Exits.HasFlag(East),
		180 => tile.Exits.HasFlag(West),
		270 => tile.Exits.HasFlag(North),
		_ => false,
	};

	public static bool HasWestExit(this MazeTile tile) => tile.Orientation switch
	{
		  0 => tile.Exits.HasFlag(West),
		 90 => tile.Exits.HasFlag(South),
		180 => tile.Exits.HasFlag(East),
		270 => tile.Exits.HasFlag(North),
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
