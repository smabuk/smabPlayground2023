using static smabPlayground2023.SharedUi.Games.Labyrinth.Direction;

namespace smabPlayground2023.SharedUi.Games.Labyrinth.Models;

public static class MazeTileExtensions
{
	public static MazeTile Rotate(this MazeTile tile, int amount) => tile with { Orientation = (tile.Orientation + amount + 360) % 360 };

	public static bool HasExit(this MazeTile tile, ReadOnlySpan<char> exit)
	{
		return exit[0] switch
		{
			'N' or 'n' => tile.HasPath(North),
			'E' or 'e' => tile.HasPath(East),
			'S' or 's' => tile.HasPath(South),
			'W' or 'w' => tile.HasPath(West),
			_ => throw new ArgumentOutOfRangeException(nameof(exit)),
		};
	}

	public static bool HasPath(this MazeTile tile, Direction direction) => (direction, tile.Orientation) switch
	{
		(North,   0 ) => tile.Exits.HasFlag(North),
		(North,  90 ) => tile.Exits.HasFlag(West),
		(North, 180 ) => tile.Exits.HasFlag(South),
		(North, 270 ) => tile.Exits.HasFlag(East),

		(East,    0 ) => tile.Exits.HasFlag(East),
		(East,   90 ) => tile.Exits.HasFlag(North),
		(East,  180 ) => tile.Exits.HasFlag(West),
		(East,  270 ) => tile.Exits.HasFlag(South),

		(South,    0 ) => tile.Exits.HasFlag(South),
		(South,   90 ) => tile.Exits.HasFlag(East),
		(South,  180 ) => tile.Exits.HasFlag(West),
		(South,  270 ) => tile.Exits.HasFlag(North),

		(West,     0 ) => tile.Exits.HasFlag(West),
		(West,    90 ) => tile.Exits.HasFlag(South),
		(West,   180 ) => tile.Exits.HasFlag(East),
		(West,   270 ) => tile.Exits.HasFlag(North),

		_ => false,
	};

	public static string PathDirections(this MazeTile tile)
		=> $"{(tile.HasPath(North) ? "N" : "")}{(tile.HasPath(East) ? "E" : "")}{(tile.HasPath(South) ? "S" : "")}{(tile.HasPath(West) ? "W" : "")}";
}
