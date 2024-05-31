namespace smabPlayground2023.SharedUi.Games.Labyrinth.Models;

public record MazeTile(Treasure Treasure, Direction Exits, int Orientation)
{
	public int Orientation { get; init; } = Orientation is 0 or 90 or 180 or 270 ? Orientation
		: throw new ArgumentException("Orientation must be 0, 90, 180 or 270.");
}
