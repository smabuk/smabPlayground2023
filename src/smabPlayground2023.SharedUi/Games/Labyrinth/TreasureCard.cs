namespace smabPlayground2023.SharedUi.Games.Labyrinth;

public record TreasureCard(Treasure Treasure)
{
	public string Name => Treasure.ToName();
}


public static class TreasureCardExtensions
{
	public static IEnumerable<TreasureCard> GetShuffledDeck()
	{
		TreasureCard[] deck = [.. TreasureExtensions.GetAllTreasures().Select(t => new TreasureCard(t))];
		Random.Shared.Shuffle(deck);
		return [.. deck[..24]];
	}
}
