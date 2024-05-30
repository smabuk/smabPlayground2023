namespace smabPlayground2023.SharedUi.Games.Labyrinth.Models;

public abstract record BoardPosition(int Col, int Row);


public record EmptyBoardTile(int Col, int Row) : BoardPosition(Col, Row);
public record ExtraMazeTile(MazeTile MazeTile, int Col, int Row) : BoardPosition(Col, Row);
public record BoardMazeTile(MazeTile MazeTile, int Col, int Row) : BoardPosition(Col, Row);
public record PushableMazeTile(int Col, int Row) : BoardPosition(Col, Row);

