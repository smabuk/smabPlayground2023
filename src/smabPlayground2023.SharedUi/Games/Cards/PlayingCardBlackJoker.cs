namespace smabPlayground2023.SharedUi.Games.Cards;
public record PlayingCardBlackJoker() : PlayingCard(0, 0)
{
	public override string ToString() => "Black Joker";
	public override string ToUnicode() => char.ConvertFromUtf32(0x1F0CF) + char.ConvertFromUtf32(0xFE0E);
}
