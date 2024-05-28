namespace smabPlayground2023.SharedUi.Games.Labyrinth;

public record TreasureCard(Treasure Treasure)
{
	public string Name => Treasure.ToName();
}
