﻿namespace smabPlayground2023.SharedUi.Games.Labyrinth.Models;

public sealed record LabyrinthGame(
	ImmutableList<Player> Players = null!,
	LabyrinthBoard Board = null!,
	ImmutableList<TreasureCard> TreasureCards = null!)
{
	public LabyrinthBoard Board { get; init; } = Board ?? CreateBoard(7);
	public ImmutableList<Player> Players { get; init; } = Players ?? [new (BluePlayer), new(RedPlayer)];
	public ImmutableList<TreasureCard> TreasureCards { get; init; } = TreasureCards ?? [.. CreateTreasureCardDeck()];
	public int BoardSize => Board.Maze.GetUpperBound(0) + 1;
}
