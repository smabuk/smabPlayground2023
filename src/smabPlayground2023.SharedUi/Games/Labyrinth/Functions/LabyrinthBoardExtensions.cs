namespace smabPlayground2023.SharedUi.Games.Labyrinth.Functions;

public static class LabyrinthBoardExtensions
{
	public static IEnumerable<MazeTile> GetRow(this LabyrinthBoard board, int row) => [.. board.Maze.GetRow(row)];

	public static LabyrinthBoard Push(this LabyrinthBoard board, int col, int row)
	{
		if (board.PositionWithExtra.Col == col && board.PositionWithExtra.Row == row) {
			return board;
		}

		int lowerBound = -1;
		int upperBound = board.Maze.GetUpperBound(0) + 1;

		if (!(col == lowerBound || col == upperBound || row == lowerBound || row == upperBound)) {
			return board;
		}

		MazeTile[,] maze = (MazeTile[,])board.Maze.Clone();

		List<MazeTile> tiles;
		MazeTile newExtra;
		int newCol = col;
		int newRow = row;

		if (col == lowerBound) {
			tiles = [.. maze.GetRow(row)];
			newExtra = tiles[^1];
			tiles = [board.PositionWithExtra.MazeTile, .. tiles];
			newCol = upperBound;
		} else if (col == upperBound) {
			tiles = [.. maze.GetRow(row)];
			newExtra = tiles[0];
			tiles = [.. tiles[1..], board.PositionWithExtra.MazeTile];
			newCol = lowerBound;
		} else if (row == lowerBound) {
			tiles = [.. maze.GetCol(col)];
			newExtra = tiles[^1];
			tiles = [board.PositionWithExtra.MazeTile, .. tiles];
			newRow = upperBound;
		} else {
			tiles = [.. maze.GetCol(col)];
			newExtra = tiles[0];
			tiles = [.. tiles[1..], board.PositionWithExtra.MazeTile];
			newRow = lowerBound;
		}

		if (col == lowerBound || col == upperBound) {
			for (int i = 0; i < upperBound; i++) {
				maze[i, row] = tiles[i];
			}
		} else {
			for (int i = 0; i < upperBound; i++) {
				maze[col, i] = tiles[i];
			}
		}

		return board with { Maze = maze, PositionWithExtra = new(newCol, newRow, newExtra) };
	}

	public static LabyrinthBoard RotateExtraMazeTile(this LabyrinthBoard board, int amount = 90)
		=> board with { PositionWithExtra = board.PositionWithExtra with { MazeTile = board.PositionWithExtra.MazeTile.Rotate(amount) } };

	public static List<MazeTile> FixedTiles => [
		new(GreenPlayer,   EastSouth,       0),
		new(Ring,          NorthEastWest, 180),
		new(Map,           NorthEastWest, 180),
		new(RedPlayer,     EastSouth,      90),

		new(Candle,        NorthEastWest,  90),
		new(TreasureChest, NorthEastWest,  90),
		new(Crown,         NorthEastWest, 180),
		new(Book,          NorthEastWest, 270),

		new(Knight,        NorthEastWest,  90),
		new(Emerald,       NorthEastWest,   0),
		new(Keys,          NorthEastWest, 270),
		new(Money,         NorthEastWest, 270),

		new(BluePlayer,    EastSouth,     270),
		new(Sword,         NorthEastWest,   0),
		new(Skeleton,      NorthEastWest,   0),
		new(YellowPlayer,  EastSouth,     180),
	];

	public static List<MazeTile> CreateShuffledMazeTiles()
	{
		MazeTile[] tiles = [
			new(Spider,    EastSouth,     RandomOrientation()),
			new(Ghost,     NorthEastWest, RandomOrientation()),
			new(Sorceress, NorthEastWest, RandomOrientation()),
			new(Pig,       NorthEastWest, RandomOrientation()),

			new(Moth,      EastSouth,     RandomOrientation()),
			new(Owl,       EastSouth,     RandomOrientation()),
			new(Genie,     NorthEastWest, RandomOrientation()),
			new(Mouse,     South | West,  RandomOrientation()),

			new(Lizard,    EastSouth,     RandomOrientation()),
			new(Bat,       NorthEastWest, RandomOrientation()),
			new(Dragon,    NorthEastWest, RandomOrientation()),
			new(Beetle,    EastSouth,     RandomOrientation()),

			.. Enumerable.Range(0, 12).Select(_ => new MazeTile(NoTreasure, NorthSouth, RandomOrientation())),
			.. Enumerable.Range(0, 10).Select(_ => new MazeTile(NoTreasure, EastSouth,  RandomOrientation())),
		];

		Random.Shared.Shuffle(tiles);
		return [.. tiles];

		int RandomOrientation() => Random.Shared.Next(0, 4) * 90;
	}

	public static IEnumerable<TreasureCard> CreateTreasureCardDeck()
	{
		TreasureCard[] deck = [.. TreasureExtensions.GetAllTreasures().Select(t => new TreasureCard(t))];
		Random.Shared.Shuffle(deck);
		return [.. deck[..24]];
	}
}
