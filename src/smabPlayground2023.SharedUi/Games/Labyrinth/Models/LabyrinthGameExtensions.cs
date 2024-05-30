namespace smabPlayground2023.SharedUi.Games.Labyrinth.Models;

public static class LabyrinthGameExtensions
{
	public static void PushTheTile(this LabyrinthGame game, int col, int row) => game.Board.Push(game.Board.ExtraMazeTile, col, row);

	public static void RotateClockwise(this LabyrinthGame game) => game.Board.RotateExtraMazeTile(90);

	public static void RotateAntiClockwise(this LabyrinthGame game) => game.Board.RotateExtraMazeTile(-90);

	public static BoardPosition[,] GetMazeTilesAsGrid(this LabyrinthGame game)
	{
		LabyrinthBoard board = game.Board;
		int boardSize = game.BoardSize;
		int gridSize = boardSize + 2;
		BoardPosition[,] grid = (BoardPosition[,])Array.CreateInstance(typeof(BoardPosition), [gridSize, gridSize], [-1, -1]);

		for (int j = -1; j < gridSize - 1; j++) {
			for (int i = -1; i < gridSize - 1; i += boardSize + 1) {
				if (j % 2 == 1 && j > 0 && j < boardSize) {
					grid[i, j] = new PushableMazeTile(i, j);
					grid[j, i] = new PushableMazeTile(j, i);

				} else {
					grid[i, j] = new EmptyBoardTile(i, j);
					grid[j, i] = new EmptyBoardTile(j, i);
				}
			}
		}

		ExtraMazeTile extra = new(board.ExtraMazeTile, board.ExtraMazeTilePosition.Col, board.ExtraMazeTilePosition.Row);
		grid[extra.Col, extra.Row] = extra;

		for (int row = 0; row < boardSize; row++) {
			List<MazeTile> tiles = [.. board.GetRow(row)];
			for (int col = 0; col < boardSize; col++) {
				grid[col, row] = new BoardMazeTile(tiles[col], col, row);
			}
		}

		return grid;
	}
}
