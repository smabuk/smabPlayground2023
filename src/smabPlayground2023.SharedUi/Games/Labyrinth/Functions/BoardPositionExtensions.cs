namespace smabPlayground2023.SharedUi.Games.Labyrinth.Functions;

public static class BoardPositionExtensions
{
	public static bool IsAt(this BoardPosition boardPosition, int col, int row) => boardPosition.Col == col && boardPosition.Row == row;
}
