namespace smabPlayground2023.SharedUi.Games.Labyrinth.Models;

public sealed class LabyrinthGame(int boardSize = 7)
{
	public readonly int BoardSize = boardSize;
	
	readonly List<TreasureCard> _deck = [.. CreateTreasureCardDeck()];
	readonly List<Player> _players = [new(BluePlayer)];
	LabyrinthBoard _board = CreateBoard(boardSize);

	private static LabyrinthBoard CreateBoard(int boardSize)
	{
		MazeTile[,] maze = new MazeTile[boardSize, boardSize];
		List<MazeTile> mazeTiles = [.. CreateShuffledMazeTiles()];

		int tileIndex = 0;
		for (int row = 0; row < boardSize; row++) {
			for (int col = 0; col < boardSize; col++) {
				maze[col, row] = IsFixed(col, row)
					? FixedTiles[(row * (boardSize + 1) / 4) + (col / 2)]
					: mazeTiles[tileIndex++];
			}
		}

		return new LabyrinthBoard(maze, new(-1, boardSize, mazeTiles[^1]));

		static bool IsFixed(int col, int row) => col % 2 == 0 && row % 2 == 0;
	}

	public BoardPosWithExtraMazeTile PositionWithExtra => _board.PositionWithExtra;

	public void PushTheTile(int col, int row) => _board = _board.Push(col, row);

	public void RotateClockwise() => _board = _board.RotateExtraMazeTile(90);

	public void RotateAntiClockwise() => _board = _board.RotateExtraMazeTile(-90);

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

		grid[PositionWithExtra.Col, PositionWithExtra.Row] = PositionWithExtra;

		for (int row = 0; row < boardSize; row++) {
			List<MazeTile> tiles = [.. board.GetRow(row)];
			for (int col = 0; col < boardSize; col++) {
				grid[col, row] = new BoardPosWithMazeTile(col, row, tiles[col]);
			}
		}

		return grid;
	}

}
