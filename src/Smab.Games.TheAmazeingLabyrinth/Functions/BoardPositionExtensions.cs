﻿namespace Smab.Games.TheAmazeingLabyrinth.Functions;

public static class BoardPositionExtensions
{
	public static bool IsAt(this BoardPosition boardPosition, int col, int row) => boardPosition.Col == col && boardPosition.Row == row;

	public static bool IsAtTheSamePosition(this BoardPosition position, BoardPosition position2)
		=> position.Col == position2.Col && position.Row == position2.Row;

	public static bool IsOnTheSameRowOrCol(this BoardPosition position, BoardPosition position2)
		=> position.Col == position2.Col || position.Row == position2.Row;
}
