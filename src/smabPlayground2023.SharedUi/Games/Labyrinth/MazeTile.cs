namespace smabPlayground2023.SharedUi.Games.Labyrinth;

public record MazeTile(Treasure Treasure, bool NorthExit, bool EastExit, bool SouthExit, bool WestExit, int Orientation = 0);
