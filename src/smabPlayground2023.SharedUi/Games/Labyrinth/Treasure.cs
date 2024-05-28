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
	public static string ToName(this Treasure treasure)
	{
		return treasure switch
		{
			Treasure.TreasureChest => "Treasure Chest",
			Treasure.BluePlayer    => "Blue Player",
			Treasure.GreenPlayer   => "Green Player",
			Treasure.RedPlayer     => "Red Player",
			Treasure.YellowPlayer  => "Yellow Player",
			_ => $"{treasure}",
		};
	}
}
