using static smabPlayground2023.SharedUi.Games.Labyrinth.Treasure;
namespace smabPlayground2023.SharedUi.Games.Labyrinth;

public class LabyrinthBoard
{

	private readonly MazeCard[,] _maze = new MazeCard[7, 7];
	private readonly MazeCard _spareMazeCard;

	public LabyrinthBoard()
	{
		int cardIndex = 0;
		for (int row = 0; row < 7; row++) {
			for (int col = 0; col < 7; col++) {
				_maze[col, row] = IsFixed(col, row)
					? FixedMazeCards[(row * 4 / 2) + (col / 2)]
					: ShiftingMazeCards[cardIndex++];
			}
		}

		_spareMazeCard = ShiftingMazeCards[cardIndex];

		static bool IsFixed(int col, int row) => col % 2 == 0 && row % 2 == 0;
	}

	public List<MazeCard> MazeCards => [.. _maze.GetAllByRow()];
	public IEnumerable<MazeCard> GetRow(int row) => [.. _maze.GetRow(row)];

	public static readonly List<MazeCard> FixedMazeCards = [
		new(GreenPlayer, false, true, true, false),
		new(Ring, false, true, true, true),
		new(Map, false, true, true, true),
		new(RedPlayer, false, false, true, true),

		new(Candle, true, true, true, false),
		new(TreasureChest, true, true, true, false),
		new(Crown, false, true, true, true),
		new(Book, true, false, true, true),

		new(Knight, true, true, true, false),
		new(Emerald, true, true, false, true),
		new(Keys, true, false, true, true),
		new(Money, true, false, true, true),

		new(BluePlayer, true, true, false, false),
		new(Sword, true, true, false, true),
		new(Skeleton, true, true, false, true),
		new(YellowPlayer, true, false, false, true),
	];

	public List<MazeCard> ShiftingMazeCards = [.. CreateShuffledMazeCards()];

	public static IEnumerable<MazeCard> CreateShuffledMazeCards()
	{
		MazeCard[] cards = [
			new(Spider, false, true, true, false, Random.Shared.Next(0, 4) * 90),
			new (Ghost, true, true, false, true, Random.Shared.Next(0, 4) * 90),
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
		TreasureCard[] deck = [
			new(Bat),
			new(Beetle),
			new(Book),
			new(Candle),
			new(Crown),
			new(Dragon),
			new(Emerald),
			new(Genie),
			new(Ghost),
			new(Keys),
			new(Knight),
			new(Lizard),
			new(Map),
			new(Money),
			new(Moth),
			new(Mouse),
			new(Owl),
			new(Pig),
			new(Ring),
			new(Skeleton),
			new(Sorceress),
			new(Spider),
			new(Sword),
			new(TreasureChest),
			];

		Random.Shared.Shuffle(deck);
		return [.. deck];
	}
}
