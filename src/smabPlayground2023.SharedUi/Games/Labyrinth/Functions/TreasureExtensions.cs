﻿namespace smabPlayground2023.SharedUi.Games.Labyrinth.Functions;

public static class TreasureExtensions
{
	public static IEnumerable<Treasure> GetAllTreasures()
		=> Enum.GetValues<Treasure>().Where(t => t is > NoTreasure and < NoPlayer);

	public static bool IsPlayer(this Treasure t) => t is > NoPlayer;
}
