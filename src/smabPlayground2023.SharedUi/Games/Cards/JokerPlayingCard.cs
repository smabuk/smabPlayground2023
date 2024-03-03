namespace smabPlayground2023.SharedUi.Games.Cards;
public record JokerPlayingCard(JokerType Type) : PlayingCard(0, 0)
{
	public override string ToString() => $"{Type} Joker";
	public override string ToUnicode() => Type switch
	{
		JokerType.Black => char.ConvertFromUtf32(0x1F0CF),
		JokerType.Red   => char.ConvertFromUtf32(0x1F0BF),
		JokerType.White => char.ConvertFromUtf32(0x1F0DF),
		_ => throw new NotImplementedException(),
	} + char.ConvertFromUtf32(0xFE0E);
}
