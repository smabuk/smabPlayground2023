namespace smabPlayground2023.SharedUi.Games.Labyrinth.Models;
public class LabyrinthGame(int boardSize = 7)
{
	public readonly int BoardSize = boardSize;
	
	readonly List<TreasureCard> _deck = [.. LabyrinthBoard.CreateTreasureCardDeck()];
	readonly List<Player> _players = [new(Treasure.BluePlayer)];
	readonly LabyrinthBoard _board = new(boardSize);
	
	public BoardPosWithExtraMazeTile ExtraMazeTile => _board.ExtraMazeTile;


	public void PushTheTile(int col, int row) => _board.Push(ExtraMazeTile, col, row);

	public void RotateClockwise() => _board.RotateExtraMazeTile(90);

	public void RotateAntiClockwise() => _board.RotateExtraMazeTile(-90);

	public BoardPosition[,] AsGrid()
	{
		LabyrinthBoard board = _board;
		int boardSize = BoardSize;
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

		grid[ExtraMazeTile.Col, ExtraMazeTile.Row] = ExtraMazeTile;

		for (int row = 0; row < boardSize; row++) {
			List<MazeTile> tiles = [.. board.GetRow(row)];
			for (int col = 0; col < boardSize; col++) {
				grid[col, row] = new BoardPosWithMazeTile(col, row, tiles[col]);
			}
		}

		return grid;
	}

}
