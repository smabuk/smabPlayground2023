namespace smabPlayground2023.SharedUi.Games.Labyrinth.Models;

public abstract record BoardPosition(int Col, int Row)
{
	public int Col { get; init; } = Col is >= (-1) ? Col : throw new ArgumentException("Col is invalid");
	public int Row { get; init; } = Row is >= (-1) ? Row : throw new ArgumentException("Row is invalid");
}

public record             EmptyBoardPos(int Col, int Row)                    : BoardPosition(Col, Row);
public record          BoardPosWithPush(int Col, int Row)                    : BoardPosition(Col, Row);
public record      BoardPosWithMazeTile(int Col, int Row, MazeTile MazeTile) : BoardPosition(Col, Row);
public record BoardPosWithExtraMazeTile(int Col, int Row, MazeTile MazeTile) : BoardPosition(Col, Row);

public static class BoardPositionExtensions
{
	public static bool IsAt(this BoardPosition boardPosition, int col, int row) => (boardPosition.Col == col && boardPosition.Row == row);
}
