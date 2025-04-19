namespace Smab.Games.TheAmazeingLabyrinth.Functions;

public static class TreasureExtensions
{
	extension(Treasure treasure)
	{
		public bool IsPlayer   => treasure is > NoPlayer;
		public bool IsTreasure => treasure is > NoTreasure and < NoPlayer;
	}

	extension(TreasureCard)
	{
		public static IEnumerable<TreasureCard> CreateTreasureCardDeck(int noOfCards = 24)
		{
			Span<TreasureCard> deck = [.. GetAllTreasures().Select(t => new TreasureCard(t))];
			Random.Shared.Shuffle(deck);
			return [.. deck[..noOfCards]];
		}

		public static IEnumerable<Treasure> GetAllTreasures()
			=> Enum.GetValues<Treasure>().Where(t => t.IsTreasure);
	}
}
