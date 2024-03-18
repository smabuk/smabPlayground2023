using System.Text.Json.Serialization;

namespace smabPlayground2023.SharedUi.Games.Cards;

[JsonDerivedType(typeof(JokerPlayingCard)      , typeDiscriminator: "J")]
[JsonDerivedType(typeof(FrenchTarotPlayingCard), typeDiscriminator: "FT")]
public record PlayingCard(int Value, Suit Suit)
{
	public static readonly string Back = char.ConvertFromUtf32(0x1F0A0);

	public override string ToString()
	{
		return Value switch
		{
			 1 => "Ace",
			11 => "Jack",
			12 => "Queen",
			13 => "King",
			_ => Value.ToString(),
		} + Suit switch
		{
			Suit.Clubs    => " of Clubs",
			Suit.Diamonds => " of Diamonds",
			Suit.Hearts   => " of Hearts",
			Suit.Spades   => " of Spades",
			_ => "",
		};
	}

	public virtual string ToUnicode()
	{
		int suitOffset = Suit switch {
			Suit.Clubs    => 0x1F0D1,
			Suit.Diamonds => 0x1F0C1,
			Suit.Hearts   => 0x1F0B1,
			Suit.Spades   => 0x1F0A1,
			_ => throw new NotImplementedException(), };

		// Unicode is A,2,3,4,5,6,7,8,9,10,J,C,Q,K (where C is knight in French)
		int valueOffset = Value + (Value >= 12 ? 0 : -1);
		return char.ConvertFromUtf32(suitOffset + valueOffset);
	}

	public static IEnumerable<PlayingCard> CreateDeck
		=> Enum.GetValues<Suit>()
		.SelectMany(s => Enumerable.Range(1, 13).Select(v => new PlayingCard(v, s)));

	public static IEnumerable<PlayingCard> CreateDeckWithJokers => [
		.. CreateDeck,
		new JokerPlayingCard(JokerType.Red),
		new JokerPlayingCard(JokerType.Black),
		];

	public static IEnumerable<PlayingCard> CreateDeckWithAllJokers => [
		.. CreateDeckWithJokers,
		new JokerPlayingCard(JokerType.White),
		];
}
