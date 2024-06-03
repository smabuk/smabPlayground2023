namespace smabPlayground2023.SharedUi.Games.Labyrinth.Functions;

public static class MazeTileExtensions
{
	public static MazeTile Rotate(this MazeTile tile, int amount) => tile with { Orientation = (tile.Orientation + amount + 360) % 360 };

	public static Direction Paths(this MazeTile tile) => tile.Exits.Rotate(tile.Orientation);
}
