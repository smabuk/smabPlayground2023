﻿using static smabPlayground2023.SharedUi.Games.Labyrinth.Models.Treasure;
namespace smabPlayground2023.SharedUi.Games.Labyrinth.Models;

public class LabyrinthBoard
{
	private readonly int _boardSize = 7;
	private readonly MazeTile[,] _maze;
	private readonly int _lowerBound = -1;
	private readonly int _upperBound = 7;

	public LabyrinthBoard(int boardSize = 7)
	{
		_boardSize = boardSize;
		_upperBound = boardSize;
		_maze = new MazeTile[boardSize, boardSize];

		int tileIndex = 0;
		for (int row = 0; row < _boardSize; row++) {
			for (int col = 0; col < _boardSize; col++) {
				_maze[col, row] = IsFixed(col, row)
					? FixedMazeTiles[(row * (_boardSize + 1) / 4) + (col / 2)]
					: ShiftingMazeTiles[tileIndex++];
			}
		}

		ExtraMazeTile = new(_lowerBound, _upperBound, ShiftingMazeTiles[tileIndex]);

		static bool IsFixed(int col, int row) => col % 2 == 0 && row % 2 == 0;
	}

	public BoardPosWithExtraMazeTile ExtraMazeTile { get; internal set; }
	public List<MazeTile> MazeTiles => [.. _maze.GetAllByRow()];
	public IEnumerable<MazeTile> GetRow(int row) => [.. _maze.GetRow(row)];

	public bool Push(BoardPosWithExtraMazeTile extraMazeTile, int col, int row)
	{
		MazeTile tile = extraMazeTile.MazeTile;
		if (extraMazeTile.Col == col && extraMazeTile.Row == row) {
			return false;
		}

		if (!(col == _lowerBound || col == _upperBound || row == _lowerBound || row == _upperBound)) {
			return false;
		}

		List<MazeTile> tiles;
		MazeTile newExtra;
		int newCol = col;
		int newRow = row;
		if (col == _lowerBound) {
			tiles = [.. _maze.GetRow(row)];
			newExtra = tiles[^1];
			tiles = [tile, .. tiles];
			newCol = _upperBound;
		} else if (col == _upperBound) {
			tiles = [.. _maze.GetRow(row)];
			newExtra = tiles[0];
			tiles = [.. tiles[1..], tile];
			newCol = _lowerBound;
		} else if (row == _lowerBound) {
			tiles = [.. _maze.GetCol(col)];
			newExtra = tiles[^1];
			tiles = [tile, .. tiles];
			newRow = _upperBound;
		} else {
			tiles = [.. _maze.GetCol(col)];
			newExtra = tiles[0];
			tiles = [.. tiles[1..], tile];
			newRow = _lowerBound;
		}

		ExtraMazeTile = new(newCol, newRow, newExtra);
		if (col == _lowerBound || col == _upperBound) {
			for (int i = 0; i < _upperBound; i++) {
				_maze[i, row] = tiles[i];
			}
		} else {
			for (int i = 0; i < _upperBound; i++) {
				_maze[col, i] = tiles[i];
			}
		}

		return true;
	}

	public void RotateExtraMazeTile(int amount = 90) => ExtraMazeTile = ExtraMazeTile with { MazeTile = ExtraMazeTile.MazeTile.Rotate(amount)};

	public static List<MazeTile> FixedMazeTiles => [
		new(GreenPlayer,   Direction.EastSouth,       0),
		new(Ring,          Direction.NorthEastWest, 180),
		new(Map,           Direction.NorthEastWest, 180),
		new(RedPlayer,     Direction.EastSouth,      90),

		new(Candle,        Direction.NorthEastWest,  90),
		new(TreasureChest, Direction.NorthEastWest,  90),
		new(Crown,         Direction.NorthEastWest, 180),
		new(Book,          Direction.NorthEastWest, 270),

		new(Knight,        Direction.NorthEastWest,  90),
		new(Emerald,       Direction.NorthEastWest,   0),
		new(Keys,          Direction.NorthEastWest, 270),
		new(Money,         Direction.NorthEastWest, 270),

		new(BluePlayer,    Direction.EastSouth,     270),
		new(Sword,         Direction.NorthEastWest,   0),
		new(Skeleton,      Direction.NorthEastWest,   0),
		new(YellowPlayer,  Direction.EastSouth,     180),
	];

	public List<MazeTile> ShiftingMazeTiles = [.. CreateShuffledMazeTiles()];

	public static IEnumerable<MazeTile> CreateShuffledMazeTiles()
	{
		MazeTile[] tiles = [
			new(Spider, Direction.EastSouth, Random.Shared.Next(0, 4) * 90),
			new(Ghost, Direction.NorthEastWest, Random.Shared.Next(0, 4) * 90),
			new(Sorceress, Direction.NorthEastWest, Random.Shared.Next(0, 4) * 90),
			new(Pig, Direction.NorthEastWest, Random.Shared.Next(0, 4) * 90),

			new(Moth, Direction.EastSouth, Random.Shared.Next(0, 4) * 90),
			new(Owl, Direction.EastSouth, Random.Shared.Next(0, 4) * 90),
			new(Genie, Direction.NorthEastWest, Random.Shared.Next(0, 4) * 90),
			new(Mouse, Direction.South | Direction.West, Random.Shared.Next(0, 4) * 90),

			new(Lizard, Direction.EastSouth, Random.Shared.Next(0, 4) * 90),
			new(Bat, Direction.NorthEastWest, Random.Shared.Next(0, 4) * 90),
			new(Dragon, Direction.NorthEastWest, Random.Shared.Next(0, 4) * 90),
			new(Beetle, Direction.EastSouth, Random.Shared.Next(0, 4) * 90),

			.. Enumerable.Range(0, 12).Select(_ => new MazeTile(None, Direction.NorthSouth, Random.Shared.Next(0, 4) * 90)),
			.. Enumerable.Range(0, 10).Select(_ => new MazeTile(None, Direction.EastSouth, Random.Shared.Next(0, 4) * 90)),
		];

		Random.Shared.Shuffle(tiles);
		return [.. tiles];
	}
	public static IEnumerable<TreasureCard> CreateTreasureCardDeck()
	{
		TreasureCard[] deck = [.. TreasureExtensions.GetAllTreasures().Select(t => new TreasureCard(t))];
		Random.Shared.Shuffle(deck);
		return [.. deck[..24]];
	}
}