using smabPlayground2023.SharedUi.Games.Labyrinth;
using smabPlayground2023.SharedUi.Games.Labyrinth.Functions;
using smabPlayground2023.SharedUi.Games.Labyrinth.Models;

using static smabPlayground2023.SharedUi.Games.Labyrinth.Functions.LabyrinthBoardExtensions;

namespace smabPlayground2023.Tests.GameTests;

public  class LabyrinthTests
{
	readonly List<MazeTile> _shuffledTiles = [.. CreateShuffledMazeTiles()];

	[Fact]
	public void Game_Should()
	{
		LabyrinthGame game = new();
		game = game.AddPlayers(Treasure.BluePlayer);

		game.BoardSize.ShouldBe(7);
		game.Board.PositionWithExtra.Col.ShouldBe(-1);
		game.Board.PositionWithExtra.Row.ShouldBe(7);

		BoardPosition[,] grid = game.AsGrid();
		grid.LongLength.ShouldBe(81);
		grid[-1, -1].ShouldBeOfType(typeof(EmptyBoardPos));
		grid[-1,  7].ShouldBeOfType(typeof(BoardPosWithExtraMazeTile));
		grid[-1,  1].ShouldBeOfType(typeof(BoardPosWithPush));
		grid[ 0,  0].ShouldBeOfType(typeof(BoardPosWithMazeTile));
		
		((BoardPosWithMazeTile)grid[ 0,  0]).MazeTile.Treasure.ShouldBe(Treasure.GreenPlayer);
		((BoardPosWithMazeTile)grid[ 6,  0]).MazeTile.Treasure.ShouldBe(Treasure.RedPlayer);
		((BoardPosWithMazeTile)grid[ 6,  0]).MazeTile.Orientation.ShouldBe(90);

		game.Board.PositionWithExtra.IsAt(-1, 7).ShouldBeTrue();
		MazeTile extraMazeTile = game.Board.PositionWithExtra.MazeTile;
		int currentOrientation = extraMazeTile.Orientation;
		game = game.RotateClockwise();
		game.Board.PositionWithExtra.MazeTile.Orientation.ShouldBe((currentOrientation + 90) % 360);
		game = game.RotateAntiClockwise();
		game.Board.PositionWithExtra.MazeTile.Orientation.ShouldBe(currentOrientation);

		MazeTile expectedNewExtra = ((BoardPosWithMazeTile)grid[6, 5]).MazeTile;
		game = game.PushTheTile(-1, 5);
		game.Board.PositionWithExtra.MazeTile.ShouldBe(expectedNewExtra);
	}

	[Theory]
	[InlineData(Treasure.Bat,   "NES", Direction.North | Direction.East | Direction.West)]
	[InlineData(Treasure.Mouse, "NW",  Direction.South | Direction.West)]
	public void MazeTile_WithTreasure_WithOrientation90_ShouldHave_Exits(Treasure treasure, string expectedPath, Direction expectedDirections)
	{
		MazeTile tile = _shuffledTiles.Where(m => m.Treasure == treasure).Single() with { Orientation = 90 };
		tile.Exits.ShouldBe(expectedDirections);
		tile.Paths().ToShortString().ShouldBe(expectedPath);
	}

	[Theory]
	[InlineData(Direction.NoDirection, 0, Direction.NoDirection)]
	[InlineData(Direction.North,   0, Direction.North)]
	[InlineData(Direction.North,  90, Direction.East)]
	[InlineData(Direction.North, 180, Direction.South)]
	[InlineData(Direction.North, 270, Direction.West)]
	[InlineData(Direction.North | Direction.East | Direction.West, 90, Direction.North | Direction.East | Direction.South)]
	[InlineData(Direction.North | Direction.East | Direction.South | Direction.West, 270, Direction.North | Direction.East | Direction.South | Direction.West)]
	[InlineData(Direction.North, -90, Direction.West)]
	public void Direction_GivenRotation_ShouldBe(Direction direction, int rotation, Direction expectedDirection)
	{
		direction.Rotate(rotation).ShouldBe(expectedDirection);
	}

	[Theory]
	[InlineData(Direction.NoDirection, "")]
	[InlineData(Direction.North, "N")]
	[InlineData(Direction.East,  "E")]
	[InlineData(Direction.South, "S")]
	[InlineData(Direction.West,  "W")]
	[InlineData(Direction.North | Direction.East | Direction.West, "NEW")]
	[InlineData(Direction.North | Direction.East | Direction.South | Direction.West, "NESW")]
	public void Direction_ToShortString_ShouldBe(Direction direction, string expectedString)
	{
		direction.ToShortString().ShouldBe(expectedString);
	}

	[Theory]
	[InlineData(Direction.NoDirection, 0, Direction.NoDirection)]
	[InlineData(Direction.North,   0, Direction.North)]
	[InlineData(Direction.North,  90, Direction.East)]
	[InlineData(Direction.North, 180, Direction.South)]
	[InlineData(Direction.North, 270, Direction.West)]
	[InlineData(Direction.North | Direction.East | Direction.West, 90, Direction.North | Direction.East | Direction.South)]
	[InlineData(Direction.North | Direction.East | Direction.South | Direction.West, 270, Direction.North | Direction.East | Direction.South | Direction.West)]
	public void Tile_GivenOrientation_Paths_ShouldBe(Direction exits, int orientation, Direction expectedPaths)
	{
		MazeTile tile = new(Treasure.Bat, exits, orientation);
		tile.Paths().ShouldBe(expectedPaths);
	}
}
