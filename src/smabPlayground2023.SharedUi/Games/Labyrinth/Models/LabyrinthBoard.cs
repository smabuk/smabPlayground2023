using static smabPlayground2023.SharedUi.Games.Labyrinth.Models.Treasure;
namespace smabPlayground2023.SharedUi.Games.Labyrinth.Models;

public class LabyrinthBoard
{
	private readonly int _boardSize = 7;
	private readonly MazeTile[,] _maze;
	private readonly int _lowerBound = -1;
	private readonly int _upperBound = 7;

	private MazeTile _spareMazeTile;
	private (int Col, int Row) _spareMazeTilePosition;

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

		_spareMazeTile = ShiftingMazeTiles[tileIndex];
		_spareMazeTilePosition = (_lowerBound, _upperBound);

		static bool IsFixed(int col, int row) => col % 2 == 0 && row % 2 == 0;
	}

	public (int Col, int Row) ExtraMazeTilePosition => _spareMazeTilePosition;
	public MazeTile ExtraMazeTile => _spareMazeTile;
	public List<MazeTile> MazeTiles => [.. _maze.GetAllByRow()];
	public IEnumerable<MazeTile> GetRow(int row) => [.. _maze.GetRow(row)];

	public bool Push(MazeTile tile, int col, int row)
	{
		if (_spareMazeTilePosition.Col == col && _spareMazeTilePosition.Row == row) {
			return false;
		}

		if (!(col == _lowerBound || col == _upperBound || row == _lowerBound || row == _upperBound)) {
			return false;
		}

		List<MazeTile> tiles;
		MazeTile extra;
		if (col == _lowerBound) {
			tiles = [.. _maze.GetRow(row)];
			extra = tiles[^1];
			tiles = [tile, .. tiles];
			_spareMazeTilePosition = (_upperBound, row);
		} else if (col == _upperBound) {
			tiles = [.. _maze.GetRow(row)];
			extra = tiles[0];
			tiles = [.. tiles[1..], tile];
			_spareMazeTilePosition = (_lowerBound, row);
		} else if (row == _lowerBound) {
			tiles = [.. _maze.GetCol(col)];
			extra = tiles[^1];
			tiles = [tile, .. tiles];
			_spareMazeTilePosition = (col, _upperBound);
		} else {
			tiles = [.. _maze.GetCol(col)];
			extra = tiles[0];
			tiles = [.. tiles[1..], tile];
			_spareMazeTilePosition = (col, _lowerBound);
		}

		_spareMazeTile = extra;
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

	public void RotateExtraMazeTile(int amount = 90) => _spareMazeTile = _spareMazeTile.Rotate(amount);

	public static List<MazeTile> FixedMazeTiles => [
		new(GreenPlayer, false, true, true, false, 0),
		new(Ring, true, true, false, true, 180),
		new(Map, true, true, false, true, 180),
		new(RedPlayer, false, true, true, false, 90),

		new(Candle, true, true, false, true, 90),
		new(TreasureChest, true, true, false, true, 90),
		new(Crown, true, true, false, true, 180),
		new(Book, true, true, false, true, 270),

		new(Knight, true, true, false, true, 90),
		new(Emerald, true, true, false, true, 0),
		new(Keys, true, true, false, true, 270),
		new(Money, true, true, false, true, 270),

		new(BluePlayer, false, true, true, false, 270),
		new(Sword, true, true, false, true, 0),
		new(Skeleton, true, true, false, true, 0),
		new(YellowPlayer, false, true, true, false, 180),
	];

	public List<MazeTile> ShiftingMazeTiles = [.. CreateShuffledMazeTiles()];

	public static IEnumerable<MazeTile> CreateShuffledMazeTiles()
	{
		MazeTile[] tiles = [
			new(Spider, false, true, true, false, Random.Shared.Next(0, 4) * 90),
			new(Ghost, true, true, false, true, Random.Shared.Next(0, 4) * 90),
			new(Sorceress, true, true, false, true, Random.Shared.Next(0, 4) * 90),
			new(Pig, true, true, false, true, Random.Shared.Next(0, 4) * 90),

			new(Moth, false, true, true, false, Random.Shared.Next(0, 4) * 90),
			new(Owl, false, true, true, false, Random.Shared.Next(0, 4) * 90),
			new(Genie, true, true, false, true, Random.Shared.Next(0, 4) * 90),
			new(Mouse, false, false, true, true, Random.Shared.Next(0, 4) * 90),

			new(Lizard, false, true, true, false, Random.Shared.Next(0, 4) * 90),
			new(Bat, true, true, false, true, Random.Shared.Next(0, 4) * 90),
			new(Dragon, true, true, false, true, Random.Shared.Next(0, 4) * 90),
			new(Beetle, false, true, true, false, Random.Shared.Next(0, 4) * 90),

			.. Enumerable.Range(0, 12).Select(_ => new MazeTile(None, true, false, true, false, Random.Shared.Next(0, 4) * 90)),
			.. Enumerable.Range(0, 10).Select(_ => new MazeTile(None, false, true, true, false, Random.Shared.Next(0, 4) * 90)),
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
