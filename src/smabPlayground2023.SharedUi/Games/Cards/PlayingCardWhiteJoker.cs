namespace smabPlayground2023.SharedUi.Games.Cards;
public record PlayingCardWhiteJoker() : PlayingCard(0, 0)
{
	public override string ToString() => "White Joker";
	public override string ToUnicode() => char.ConvertFromUtf32(0x1F0DF) + char.ConvertFromUtf32(0xFE0E);
}
