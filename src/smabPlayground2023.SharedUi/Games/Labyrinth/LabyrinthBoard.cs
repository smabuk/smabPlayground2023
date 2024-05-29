using static smabPlayground2023.SharedUi.Games.Labyrinth.Treasure;
namespace smabPlayground2023.SharedUi.Games.Labyrinth;

public class LabyrinthBoard
{
	private readonly int _boardSize = 7;
	private readonly MazeCard[,] _maze;
	private readonly MazeCard _spareMazeCard;
	private readonly (int col, int row) _spareMazeCardPosition;

	public LabyrinthBoard(int boardSize = 7)
	{
		_boardSize = boardSize;
		_maze = new MazeCard[boardSize, boardSize];

		int cardIndex = 0;
		for (int row = 0; row < _boardSize; row++) {
			for (int col = 0; col < _boardSize; col++) {
				_maze[col, row] = IsFixed(col, row)
					? FixedMazeCards[(row * (_boardSize + 1) / 4) + (col / 2)]
					: ShiftingMazeCards[cardIndex++];
			}
		}

		_spareMazeCard = ShiftingMazeCards[cardIndex];
		_spareMazeCardPosition = (-1, _boardSize);

		static bool IsFixed(int col, int row) => col % 2 == 0 && row % 2 == 0;
	}

	public (int col, int row) ExtraMazeCardPosition => _spareMazeCardPosition;
	public MazeCard ExtraMazeCard => _spareMazeCard;
	public List<MazeCard> MazeCards => [.. _maze.GetAllByRow()];
	public IEnumerable<MazeCard> GetRow(int row) => [.. _maze.GetRow(row)];

	public static List<MazeCard> FixedMazeCards => [
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

	public List<MazeCard> ShiftingMazeCards = [.. CreateShuffledMazeCards()];

	public static IEnumerable<MazeCard> CreateShuffledMazeCards()
	{
		MazeCard[] cards = [
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

			.. Enumerable.Range(0, 12).Select(_ => new MazeCard(None, true, false, true, false, Random.Shared.Next(0, 4) * 90)),
			.. Enumerable.Range(0, 10).Select(_ => new MazeCard(None, false, true, true, false, Random.Shared.Next(0, 4) * 90)),
		];

		Random.Shared.Shuffle(cards);
		return [.. cards];
	}
	public static IEnumerable<TreasureCard> CreateTreasureCardDeck()
	{
		TreasureCard[] deck = [.. TreasureExtensions.GetAllTreasures().Select(t => new TreasureCard(t))];
		Random.Shared.Shuffle(deck);
		return [.. deck[..24]];
	}
}
