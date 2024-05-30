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
	[InlineData(Treasure.Bat, Direction.North | Direction.East | Direction.West)]
	public void MazeTile_WithTreasure_ShouldHave_Pathways(Treasure treasure, Direction expectedDirections)
	{
		LabyrinthGame game = new();
		List<MazeTile> allTiles = [game.Board.ExtraMazeTile, .. game.Board.MazeTiles];
		MazeTile tile = allTiles.Where(m => m.Treasure == treasure).Single();
		tile.NorthExit.ShouldBe(expectedDirections.HasFlag(Direction.North));
		tile.EastExit.ShouldBe(expectedDirections.HasFlag(Direction.East));
		tile.SouthExit.ShouldBe(expectedDirections.HasFlag(Direction.South));
		tile.WestExit.ShouldBe(expectedDirections.HasFlag(Direction.West));
	}
}
