namespace Smab.Games.TheAmazeingLabyrinth.Models;

public sealed record LabyrinthGame(
	ImmutableList<Player> Players = null!,
	LabyrinthBoard Board = null!,
	ImmutableList<TreasureCard> TreasureCards = null!)
{
	public LabyrinthBoard Board { get; init; } = Board ?? CreateBoard(7);
	public ImmutableList<Player> Players { get; init; } = Players ?? [];
	public ImmutableList<TreasureCard> TreasureCards { get; init; } = TreasureCards ?? [.. CreateTreasureCardDeck()];
	public int BoardSize => Board.Maze.GetUpperBound(0) + 1;
}
