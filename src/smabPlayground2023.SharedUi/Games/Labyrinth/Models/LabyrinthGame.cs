namespace smabPlayground2023.SharedUi.Games.Labyrinth.Models;
public class LabyrinthGame(int boardSize = 7)
{
	public readonly int BoardSize = boardSize;
	public List<TreasureCard> Deck { get; init; } = [.. LabyrinthBoard.CreateTreasureCardDeck()];
	public List<Player> Players { get; init; } = [new(Treasure.BluePlayer)];
	public LabyrinthBoard Board { get; init; } = new(boardSize);
}
