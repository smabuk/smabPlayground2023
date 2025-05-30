﻿namespace Smab.Games.TheAmazeingLabyrinth.Functions;

public static class LabyrinthBoardExtensions
{
	extension(LabyrinthBoard board)
	{
		public IEnumerable<MazeTile> GetRow(int row) => [.. board.Maze.GetRow(row)];

		public LabyrinthBoard Push(int col, int row)
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

		public Directions PushDirection(int col, int row)
		{
			if (board.PositionWithExtra.Col == col && board.PositionWithExtra.Row == row) {
				return NoDirection;
			}

			int lowerBound = -1;
			int upperBound = board.Maze.GetUpperBound(0) + 1;

			return (col, row) switch
			{
				_ when col == lowerBound && row % 2 == 1 => East,
				_ when col == upperBound && row % 2 == 1 => West,
				_ when row == lowerBound && col % 2 == 1 => South,
				_ when row == upperBound && col % 2 == 1 => North,
				_ => NoDirection,
			};
		}

		public LabyrinthBoard RotateExtraMazeTile(int amount = 90)
			=> board with { PositionWithExtra = board.PositionWithExtra with { MazeTile = board.PositionWithExtra.MazeTile.Rotate(amount) } };

	}


	
	extension(LabyrinthBoard)
	{
		public static LabyrinthBoard CreateBoard(int boardSize)
		{
			MazeTile[,] maze = new MazeTile[boardSize, boardSize];
			List<MazeTile> mazeTiles = [.. CreateShuffledMazeTiles()];

			int tileIndex = 0;
			for (int row = 0; row < boardSize; row++) {
				for (int col = 0; col < boardSize; col++) {
					maze[col, row] = IsFixedTilePosition(col, row)
						? LabyrinthBoard.CreateFixedTiles[FixedTilesIndex(boardSize, col, row)]
						: mazeTiles[tileIndex++];
				}
			}

			return new LabyrinthBoard(maze, new(-1, boardSize, mazeTiles[^1]));

			static bool IsFixedTilePosition(int col, int row) => col % 2 == 0 && row % 2 == 0;
			static int FixedTilesIndex(int boardSize, int col, int row) => (row * (boardSize + 1) / 4) + (col / 2);
		}

		private static List<MazeTile> CreateFixedTiles => [
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
			Span<MazeTile> tiles = [
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

			static int RandomOrientation() => Random.Shared.Next(0, 4) * 90;
		}
	}

}
