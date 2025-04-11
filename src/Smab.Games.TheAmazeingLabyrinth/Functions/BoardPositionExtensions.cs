namespace Smab.Games.TheAmazeingLabyrinth.Functions;

public static class BoardPositionExtensions
{
	extension(BoardPosition boardPosition)
	{
		public bool IsAt(int col, int row) => boardPosition.Col == col && boardPosition.Row == row;

		public bool IsAtTheSamePosition(BoardPosition position2)
			=> boardPosition.Col == position2.Col && boardPosition.Row == position2.Row;

		public bool IsOnTheSameRowOrCol(BoardPosition position2)
			=> boardPosition.Col == position2.Col || boardPosition.Row == position2.Row;
	}
}
