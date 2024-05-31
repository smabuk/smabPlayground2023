namespace smabPlayground2023.SharedUi.Games.Labyrinth;

[Flags]
public enum Direction
{
	None  = 0,
	North = 1,
	East  = 2,
	South = 4,
	West  = 8,

	NorthEastWest = North | East | West,
	EastSouth     = East  | South,
	NorthSouth    = North | South,
}
