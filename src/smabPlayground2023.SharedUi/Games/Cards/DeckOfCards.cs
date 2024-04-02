namespace smabPlayground2023.SharedUi.Games.Cards;
public class DeckOfCards(DeckType deckType)
{
	public DeckType DeckType { get; set; } = deckType;
	private List<PlayingCard> deck = [];

	public PlayingCard this[int index] {
		get => deck[index];
		set => deck[index] = value;
	}

	public DeckOfCards Shuffle()
	{
		PlayingCard[] deckArray = [.. deck];
		Random.Shared.Shuffle(deckArray);
		deck = [..deckArray];
		return this;
	}
}

public enum DeckType
{
	DealFromTheTop,
	DealFromTheBottom,
	Shoe = DealFromTheBottom,
}
