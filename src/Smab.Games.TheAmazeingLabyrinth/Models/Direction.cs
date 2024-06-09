namespace Smab.Games.TheAmazeingLabyrinth.Models;

[Flags]
public enum Direction
{
	NoDirection = 0,

	North = 1,
	East  = 2,
	South = 4,
	West  = 8,

	NorthEastWest = North | East | West,
	EastSouth     = East  | South,
	NorthSouth    = North | South,
}
