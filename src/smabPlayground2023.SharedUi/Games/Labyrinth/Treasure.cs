namespace smabPlayground2023.SharedUi.Games.Labyrinth;

public enum Treasure
{
	None,

	Bat,
	Beetle,
	Book,
	Candle,
	Crown,
	Dragon,
	Emerald,
	Genie,
	Ghost,
	Keys,
	Knight,
	Lizard,
	Map,
	Money,
	Moth,
	Mouse,
	Owl,
	Pig,
	Ring,
	Skeleton,
	Sorceress,
	Spider,
	Sword,
	TreasureChest,

	BluePlayer,
	GreenPlayer,
	RedPlayer,
	YellowPlayer,

	Diamond = Crown,
	Horse = Spider,
}

public static class TreasureExtensions
{
	private static readonly string EmojiModifier = char.ConvertFromUtf32(0xFE0F);
	
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
			Treasure.Bat           => EmojiFromCode(0x1f987),
			Treasure.Beetle        => EmojiFromCode(0x1fab2),
			Treasure.Book          => EmojiFromCode(0x1f4d3),
			Treasure.Candle        => EmojiFromCode(0x1f56f),
			Treasure.Crown         => EmojiFromCode(0x1f451),
			Treasure.Dragon        => EmojiFromCode(0x1f432),
			Treasure.Emerald       => EmojiFromCode(0x1f48e),
			Treasure.Genie         => EmojiFromCode(0x1f9de),
			Treasure.Ghost         => EmojiFromCode(0x1f47b),
			Treasure.Keys          => EmojiFromCode(0x1f511),
			Treasure.Knight        => EmojiFromCode(0x265e),
			Treasure.Lizard        => EmojiFromCode(0x1f98e),
			Treasure.Map           => EmojiFromCode(0x1f5fa),
			Treasure.Money         => EmojiFromCode(0x1f4b0),
			Treasure.Moth          => EmojiFromCode(0x1f98b), // Butterfly
			Treasure.Mouse         => EmojiFromCode(0x1f401),
			Treasure.Owl           => EmojiFromCode(0x1f989),
			Treasure.Pig           => EmojiFromCode(0x1f437),
			Treasure.Ring          => EmojiFromCode(0x1f48d),
			Treasure.Skeleton      => EmojiFromCode(0x1f480),
			Treasure.Sorceress     => EmojiFromCode(0x1fa84), // Wand
			Treasure.Spider        => EmojiFromCode(0x1f577),
			Treasure.Sword         => EmojiFromCode(0x2694),
			Treasure.TreasureChest => EmojiFromCode(0x1f911), // Money mouth face
			Treasure.BluePlayer    => EmojiFromCode(0x1f535),
			Treasure.GreenPlayer   => EmojiFromCode(0x1f7e2),
			Treasure.RedPlayer     => EmojiFromCode(0x1f534),
			Treasure.YellowPlayer  => EmojiFromCode(0x1f7e1),
			_ => $"{treasure}",
		};
	}

	private static string EmojiFromCode(Int32 code) => $"{char.ConvertFromUtf32(code)}{EmojiModifier}";
}
