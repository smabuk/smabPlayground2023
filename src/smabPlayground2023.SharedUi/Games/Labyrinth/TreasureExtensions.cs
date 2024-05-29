namespace smabPlayground2023.SharedUi.Games.Labyrinth;

public static class TreasureExtensions
{
	private static readonly string EmojiModifier = "\uFE0F";

	public static IEnumerable<Treasure> GetAllTreasures()
		=> Enum.GetValues<Treasure>()
			.Where(t => t is > Treasure.None and < Treasure.NoPlayer);

	public static bool IsPlayer(this Treasure t) => t is > Treasure.NoPlayer;

	public static string ToName(this Treasure treasure)
	{
		return treasure switch
		{
			Treasure.None          => "",
			Treasure.TreasureChest => "Treasure Chest",
			Treasure.BluePlayer    => "Blue Player",
			Treasure.GreenPlayer   => "Green Player",
			Treasure.RedPlayer     => "Red Player",
			Treasure.YellowPlayer  => "Yellow Player",
			_ => $"{treasure}",
		};
	}

	public static string ToEmoji(this Treasure treasure)
	{
		return treasure switch
		{
			Treasure.None => "",
			Treasure.Bat           => "\uD83E\uDD87",
			Treasure.Beetle        => "\uD83E\uDEB2",
			Treasure.Book          => "\uD83D\uDCD3",
			Treasure.Candle        => "\uD83D\uDD6F\uFE0F",
			Treasure.Crown         => "\uD83D\uDC51",
			Treasure.Dragon        => "\uD83D\uDC09",
			Treasure.Emerald       => "\uD83D\uDC8E",                               // Gem Stone
			Treasure.Genie         => "\uD83E\uDDDE\u200D\u2642\uFE0F",             // Man Genie
			Treasure.Ghost         => "\uD83D\uDC7B",
			Treasure.Keys          => "\uD83D\uDDDD\uFE0F",
			Treasure.Knight        => "\uD83E\uDD77",                               // Ninja
			Treasure.Lizard        => "\uD83E\uDD8E",
			Treasure.Map           => "\uD83D\uDDFA\uFE0F",
			Treasure.Money         => "\uD83D\uDCB0",
			Treasure.Moth          => "\uD83E\uDD8B",                               // Butterfly
			Treasure.Mouse         => "\uD83D\uDC01",
			Treasure.Owl           => "\uD83E\uDD89",
			Treasure.Pig           => "\uD83D\uDC16",
			Treasure.Ring          => "\uD83D\uDC8D",
			Treasure.Skeleton      => "\uD83D\uDC80",                               // Skull
			Treasure.Sorceress     => "\uD83E\uDDD9\uD83C\uDFFB\u200D\u2640\uFE0F", // Woman Light-skinned Mage
			Treasure.Spider        => "\uD83D\uDD77\uFE0F",
			Treasure.Sword         => "\uD83D\uDDE1\uFE0F",                         // Dagger
			Treasure.TreasureChest => "\uD83D\uDCB7",                               // Pound Banknotes


			Treasure.BluePlayer    => "\uD83D\uDD35",                               // Blue Circle
			Treasure.GreenPlayer   => "\uD83D\uDFE2",
			Treasure.RedPlayer     => "\uD83D\uDD34",
			Treasure.YellowPlayer  => "\uD83D\uDFE1",
			_ => $"{treasure}",
		};
	}

	private static string EmojiFromCode(Int32 code) => $"{char.ConvertFromUtf32(code)}{EmojiModifier}";
}
