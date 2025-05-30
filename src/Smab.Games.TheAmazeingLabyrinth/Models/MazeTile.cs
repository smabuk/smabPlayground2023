﻿namespace Smab.Games.TheAmazeingLabyrinth.Models;

public sealed record MazeTile(Treasure Treasure, Directions Exits, int Orientation)
{
	public int Orientation { get; init; }
		= Orientation is 0 or 90 or 180 or 270
			? Orientation
			: throw new ArgumentException("Orientation must be 0, 90, 180 or 270.");
}
