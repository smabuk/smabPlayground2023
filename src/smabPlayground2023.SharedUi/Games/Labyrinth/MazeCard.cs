namespace smabPlayground2023.SharedUi.Games.Labyrinth;

public record MazeCard(Treasure Treasure, bool NorthExit, bool EastExit, bool SouthExit, bool WestExit, int Orientation = 0)
{
	public string Name => Treasure.ToString();
}
