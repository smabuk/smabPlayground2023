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
		game.ExtraMazeTile.Col.ShouldBe(-1);
		game.ExtraMazeTile.Row.ShouldBe(7);

		BoardPosition[,] grid = game.AsGrid();
		grid.LongLength.ShouldBe(81);
		grid[-1, -1].ShouldBeOfType(typeof(EmptyBoardPos));
		grid[-1,  7].ShouldBeOfType(typeof(BoardPosWithExtraMazeTile));
		grid[-1,  1].ShouldBeOfType(typeof(BoardPosWithPush));
		grid[ 0,  0].ShouldBeOfType(typeof(BoardPosWithMazeTile));
		
		((BoardPosWithMazeTile)grid[ 0,  0]).MazeTile.Treasure.ShouldBe(Treasure.GreenPlayer);
		((BoardPosWithMazeTile)grid[ 6,  0]).MazeTile.Treasure.ShouldBe(Treasure.RedPlayer);
		((BoardPosWithMazeTile)grid[ 6,  0]).MazeTile.Orientation.ShouldBe(90);

		game.ExtraMazeTile.IsAt(-1, 7).ShouldBeTrue();
		MazeTile extraMazeTile = game.ExtraMazeTile.MazeTile;
		int currentOrientation = extraMazeTile.Orientation;
		game.RotateClockwise();
		game.ExtraMazeTile.MazeTile.Orientation.ShouldBe((currentOrientation + 90) % 360);
		game.RotateAntiClockwise();
		game.ExtraMazeTile.MazeTile.Orientation.ShouldBe(currentOrientation);

		MazeTile expectedNewExtra = ((BoardPosWithMazeTile)grid[6, 5]).MazeTile;
		game.PushTheTile(-1, 5);
		game.ExtraMazeTile.MazeTile.ShouldBe(expectedNewExtra);
	}

	[Theory]
	[InlineData(Treasure.Bat,   "NES", Direction.North | Direction.East | Direction.West)]
	[InlineData(Treasure.Mouse, "NW",  Direction.South | Direction.West)]
	public void MazeTile_WithTreasure_WithOrientation90_ShouldHave_Exits(Treasure treasure, string expectedPath, Direction expectedDirections)
	{
		LabyrinthGame game = new();
		MazeTile tile = LabyrinthBoard.CreateShuffledMazeTiles().Where(m => m.Treasure == treasure).Single() with { Orientation = 90 };
		tile.Exits.ShouldBe(expectedDirections);
		tile.PathDirections().ShouldBe(expectedPath);
	}
}
