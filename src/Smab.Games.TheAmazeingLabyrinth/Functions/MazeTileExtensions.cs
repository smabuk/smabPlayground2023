namespace Smab.Games.TheAmazeingLabyrinth.Functions;

public static class MazeTileExtensions
{
	extension(MazeTile tile)
	{
		public MazeTile Rotate(int amount) => tile with { Orientation = (tile.Orientation + amount + 360) % 360 };

		public Directions Paths() => tile.Exits.Rotate(tile.Orientation);
	}
}
