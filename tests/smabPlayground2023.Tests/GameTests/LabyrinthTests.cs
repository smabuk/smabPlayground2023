using smabPlayground2023.SharedUi.Games.Labyrinth;
using smabPlayground2023.SharedUi.Games.Labyrinth.Models;

namespace smabPlayground2023.Tests.GameTests;
public  class LabyrinthTests
{
	[Fact]
	public void Game_Should()
	{
		LabyrinthGame game = new();

		game.BoardSize.ShouldBe(7);
		game.Board.ExtraMazeTilePosition.ShouldBe((-1, 7));
		game.Board.MazeTiles.Count.ShouldBe(49);
	}

	[Theory]
	[InlineData(Treasure.Bat,   Direction.North | Direction.East | Direction.West)]
	[InlineData(Treasure.Mouse, Direction.South | Direction.West)]
	public void MazeTile_WithTreasure_ShouldHave_Exits(Treasure treasure, Direction expectedDirections)
	{
		LabyrinthGame game = new();
		List<MazeTile> allTiles = [game.Board.ExtraMazeTile, .. game.Board.MazeTiles];
		MazeTile tile = LabyrinthBoard.CreateShuffledMazeTiles().Where(m => m.Treasure == treasure).Single();
		tile.Exits.ShouldBe(expectedDirections);
	}
}
