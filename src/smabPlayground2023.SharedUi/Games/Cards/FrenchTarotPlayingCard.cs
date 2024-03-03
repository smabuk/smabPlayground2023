namespace smabPlayground2023.SharedUi.Games.Cards;
public record FrenchTarotPlayingCard(int Value, FrenchTarotSuit FrenchTarotSuit) : PlayingCard(Value, (Suit)FrenchTarotSuit)
{
	public override string ToString()
	{
		return FrenchTarotSuit is FrenchTarotSuit.Trumps
			? Value switch
			{
				 0 => "Fool",
				 1 => $"{Value} (Individual Folly)",
				 2 => $"{Value} (Childhood)",
				 3 => $"{Value} (Youth)",
				 4 => $"{Value} (Maturity)",
				 5 => $"{Value} (Old Age)",
				 6 => $"{Value} (Morning)",
				 7 => $"{Value} (Afternoon)",
				 8 => $"{Value} (Evening)",
				 9 => $"{Value} (Night)",
				10 => $"{Value} (Earth/Air)",
				11 => $"{Value} (Water/Fire)",
				12 => $"{Value} (Dance)",
				13 => $"{Value} (Shopping)",
				14 => $"{Value} (Open air)",
				15 => $"{Value} (Visual arts)",
				16 => $"{Value} (Spring)",
				17 => $"{Value} (Summer)",
				18 => $"{Value} (Autumn)",
				19 => $"{Value} (Winter)",
				20 => $"{Value} (The game)",
				21 => $"{Value} (Collective Folly)",
				_  => $"{Value} of Trumps",
			}
			: Value switch
			{
				11 => $"Jack of {FrenchTarotSuit}",
				12 => $"Knight of {FrenchTarotSuit}",
				13 => $"Queen of {FrenchTarotSuit}",
				14 => $"King of {FrenchTarotSuit}",
				_ => $"{Value} of {FrenchTarotSuit}",
			};
	}

	public override string ToUnicode()
	{
		int suitOffset = FrenchTarotSuit switch
		{
			FrenchTarotSuit.Clubs    => 0x1F0D1,
			FrenchTarotSuit.Diamonds => 0x1F0C1,
			FrenchTarotSuit.Hearts   => 0x1F0B1,
			FrenchTarotSuit.Spades   => 0x1F0A1,
			FrenchTarotSuit.Trumps   => 0x1F0E1,
			_ => throw new NotImplementedException(),
		};

		// Unicode is A,2,3,4,5,6,7,8,9,10,J,C,Q,K (where C is knight in French)
		int valueOffset = Value - 1;
		return char.ConvertFromUtf32(suitOffset + valueOffset);
	}


	public static new IEnumerable<PlayingCard> CreateDeck => [
		.. Enumerable.Range(1, 14).Select(v => new FrenchTarotPlayingCard(v, FrenchTarotSuit.Hearts)),
		.. Enumerable.Range(1, 14).Select(v => new FrenchTarotPlayingCard(v, FrenchTarotSuit.Clubs)),
		.. Enumerable.Range(1, 14).Select(v => new FrenchTarotPlayingCard(v, FrenchTarotSuit.Diamonds)),
		.. Enumerable.Range(1, 14).Select(v => new FrenchTarotPlayingCard(v, FrenchTarotSuit.Spades)),
		.. Enumerable.Range(1, 21).Select(v => new FrenchTarotPlayingCard(v, FrenchTarotSuit.Trumps)),
		new FrenchTarotPlayingCard(0, FrenchTarotSuit.Trumps),
		];

}
