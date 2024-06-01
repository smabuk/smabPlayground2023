namespace smabPlayground2023.SharedUi.Games.Labyrinth.Functions;

public static class LabyrinthGameExtensions
{
	public static BoardPosition[,] AsGrid(this LabyrinthGame game)
	{
		LabyrinthBoard board = game.Board;
		int boardSize = game.BoardSize;
		int gridSize = boardSize + 2;
		BoardPosition[,] grid = (BoardPosition[,])Array.CreateInstance(typeof(BoardPosition), [gridSize, gridSize], [-1, -1]);

		for (int j = -1; j < gridSize - 1; j++) {
			for (int i = -1; i < gridSize - 1; i += boardSize + 1) {
				if (j % 2 == 1 && j > 0 && j < boardSize) {
					grid[i, j] = new BoardPosWithPush(i, j);
					grid[j, i] = new BoardPosWithPush(j, i);

				} else {
					grid[i, j] = new EmptyBoardPos(i, j);
					grid[j, i] = new EmptyBoardPos(j, i);
				}
			}
		}

		grid[game.Board.PositionWithExtra.Col, game.Board.PositionWithExtra.Row] = game.Board.PositionWithExtra;

		for (int row = 0; row < boardSize; row++) {
			List<MazeTile> tiles = [.. board.GetRow(row)];
			for (int col = 0; col < boardSize; col++) {
				grid[col, row] = new BoardPosWithMazeTile(col, row, tiles[col]);
			}
		}

		return grid;
	}

	public static LabyrinthGame PushTheTile(this LabyrinthGame game, int col, int row) => game with { Board = game.Board.Push(col, row) };
	public static LabyrinthGame Rotate(this LabyrinthGame game, int amount) => game with { Board = game.Board.RotateExtraMazeTile(amount) };
	public static LabyrinthGame RotateClockwise(this LabyrinthGame game) => game with { Board = game.Board.RotateExtraMazeTile(90) };
	public static LabyrinthGame RotateAntiClockwise(this LabyrinthGame game) => game with { Board = game.Board.RotateExtraMazeTile(-90) };
}
