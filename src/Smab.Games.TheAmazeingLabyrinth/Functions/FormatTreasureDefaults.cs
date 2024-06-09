namespace Smab.Games.TheAmazeingLabyrinth.Functions;

public delegate string FormatTreasure(Treasure treasure);

public static class FormatTreasureDefaults
{
	public static FormatTreasure FormatFullName => treasure => treasure switch
	{
		NoTreasure          => "",
		TreasureChest => "Treasure Chest",
		BluePlayer    => "Blue Player",
		GreenPlayer   => "Green Player",
		RedPlayer     => "Red Player",
		YellowPlayer  => "Yellow Player",
		_ => $"{treasure}",
	};

	public static FormatTreasure DefaultEmoji => treasure => treasure switch
	{
		NoTreasure          => "",
		Bat           => "\uD83E\uDD87",
		Beetle        => "\uD83E\uDEB2",
		Book          => "\uD83D\uDCD3",
		Candle        => "\uD83D\uDD6F\uFE0F",
		Crown         => "\uD83D\uDC51",
		Dragon        => "\uD83D\uDC09",
		Emerald       => "\uD83D\uDC8E",                               // Gem Stone
		Genie         => "\uD83E\uDDDE\u200D\u2642\uFE0F",             // Man Genie
		Ghost         => "\uD83D\uDC7B",
		Keys          => "\uD83D\uDDDD\uFE0F",
		Knight        => "\uD83E\uDD77",                               // Ninja
		Lizard        => "\uD83E\uDD8E",
		Map           => "\uD83D\uDDFA\uFE0F",
		Money         => "\uD83D\uDCB0",
		Moth          => "\uD83E\uDD8B",                               // Butterfly
		Mouse         => "\uD83D\uDC01",
		Owl           => "\uD83E\uDD89",
		Pig           => "\uD83D\uDC16",
		Ring          => "\uD83D\uDC8D",
		Skeleton      => "\uD83D\uDC80",                               // Skull
		Sorceress     => "\uD83E\uDDD9\uD83C\uDFFB\u200D\u2640\uFE0F", // Woman Light-skinned Mage
		Spider        => "\uD83D\uDD77\uFE0F",
		Sword         => "\uD83D\uDDE1\uFE0F",                         // Dagger
		TreasureChest => "\uD83D\uDCB7",                               // Pound Banknotes

		BluePlayer   => "\uD83D\uDD35",                                // Blue Circle
		GreenPlayer  => "\uD83D\uDFE2",
		RedPlayer    => "\uD83D\uDD34",
		YellowPlayer => "\uD83D\uDFE1",
		_ => $"{treasure}",
	};
}
