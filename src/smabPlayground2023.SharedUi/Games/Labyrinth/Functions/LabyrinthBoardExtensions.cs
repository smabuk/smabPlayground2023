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

		List<MazeTile> oldTiles = (col == lowerBound || col == upperBound)
			? [.. maze.GetRow(row)]
			: [.. maze.GetCol(col)];

		MazeTile newExtra = (col == lowerBound || row == lowerBound)
			? oldTiles[^1]
			: oldTiles[0];

		List<MazeTile> newTiles = (col == lowerBound || row == lowerBound)
			? [board.PositionWithExtra.MazeTile, .. oldTiles]
			: [.. oldTiles[1..], board.PositionWithExtra.MazeTile];


		int newExtraCol = col;
		int newExtraRow = row;
		if (col == lowerBound || col == upperBound) {
			newExtraCol = col == lowerBound ? upperBound : lowerBound;
			for (int i = 0; i < upperBound; i++) {
				maze[i, row] = newTiles[i];
			}
		} else {
			newExtraRow = row == lowerBound ? upperBound : lowerBound;
			for (int i = 0; i < upperBound; i++) {
				maze[col, i] = newTiles[i];
			}
		}

		return board with { Maze = maze, PositionWithExtra = new(newExtraCol, newExtraRow, newExtra) };
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
