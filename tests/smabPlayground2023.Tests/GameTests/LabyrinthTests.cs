using Smab.Games.TheAmazeingLabyrinth;
using Smab.Games.TheAmazeingLabyrinth.Functions;
using Smab.Games.TheAmazeingLabyrinth.Models;

using static Smab.Games.TheAmazeingLabyrinth.Functions.LabyrinthBoardExtensions;

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
	[InlineData(Treasure.Bat,   "NES", Directions.North | Directions.East | Directions.West)]
	[InlineData(Treasure.Mouse, "NW",  Directions.South | Directions.West)]
	public void MazeTile_WithTreasure_WithOrientation90_ShouldHave_Exits(Treasure treasure, string expectedPath, Directions expectedDirections)
	{
		MazeTile tile = _shuffledTiles.Where(m => m.Treasure == treasure).Single() with { Orientation = 90 };
		tile.Exits.ShouldBe(expectedDirections);
		tile.Paths().ToShortString().ShouldBe(expectedPath);
	}

	[Theory]
	[InlineData(Directions.NoDirection, 0, Directions.NoDirection)]
	[InlineData(Directions.North,   0, Directions.North)]
	[InlineData(Directions.North,  90, Directions.East)]
	[InlineData(Directions.North, 180, Directions.South)]
	[InlineData(Directions.North, 270, Directions.West)]
	[InlineData(Directions.North | Directions.East | Directions.West, 90, Directions.North | Directions.East | Directions.South)]
	[InlineData(Directions.North | Directions.East | Directions.South | Directions.West, 270, Directions.North | Directions.East | Directions.South | Directions.West)]
	[InlineData(Directions.North, -90, Directions.West)]
	public void Direction_GivenRotation_ShouldBe(Directions direction, int rotation, Directions expectedDirection)
	{
		direction.Rotate(rotation).ShouldBe(expectedDirection);
	}

	[Theory]
	[InlineData(Directions.NoDirection, "")]
	[InlineData(Directions.North, "N")]
	[InlineData(Directions.East,  "E")]
	[InlineData(Directions.South, "S")]
	[InlineData(Directions.West,  "W")]
	[InlineData(Directions.North | Directions.East | Directions.West, "NEW")]
	[InlineData(Directions.North | Directions.East | Directions.South | Directions.West, "NESW")]
	public void Direction_ToShortString_ShouldBe(Directions direction, string expectedString)
	{
		direction.ToShortString().ShouldBe(expectedString);
	}

	[Theory]
	[InlineData(Directions.NoDirection, 0, Directions.NoDirection)]
	[InlineData(Directions.North,   0, Directions.North)]
	[InlineData(Directions.North,  90, Directions.East)]
	[InlineData(Directions.North, 180, Directions.South)]
	[InlineData(Directions.North, 270, Directions.West)]
	[InlineData(Directions.North | Directions.East | Directions.West, 90, Directions.North | Directions.East | Directions.South)]
	[InlineData(Directions.North | Directions.East | Directions.South | Directions.West, 270, Directions.North | Directions.East | Directions.South | Directions.West)]
	public void Tile_GivenOrientation_Paths_ShouldBe(Directions exits, int orientation, Directions expectedPaths)
	{
		MazeTile tile = new(Treasure.Bat, exits, orientation);
		tile.Paths().ShouldBe(expectedPaths);
	}

	[Theory]
	[InlineData(-1,  1, Directions.East)]
	[InlineData(-1,  2, Directions.NoDirection)]
	[InlineData(-1,  3, Directions.East)]
	[InlineData(-1,  5, Directions.East)]
	[InlineData( 7,  1, Directions.West)]
	[InlineData( 1, -1, Directions.South)]
	[InlineData( 1,  7, Directions.North)]
	public void PushDirection_ShouldBe(int col, int row, Directions expectedDirection)
	{
		LabyrinthGame game = new();
		game.Board.PushDirection(col, row).ShouldBe(expectedDirection);
	}

	[Fact]
	public void Game_Player_Should_MoveTo()
	{
		LabyrinthGame game = new([new Player(Treasure.BluePlayer, 1, 0)]);
		game = game.AddPlayers(Treasure.GreenPlayer);

		game.Players.Count.ShouldBe(2);
		Player bluePlayer = GetBluePlayer(game);
		Player greenPlayer = GetGreenPlayer(game);

		bluePlayer.Col.ShouldBe(1);
		bluePlayer.Row.ShouldBe(0);

		greenPlayer.Col.ShouldBe(0);
		greenPlayer.Row.ShouldBe(0);

		game = game.PushTheTile(1, -1);
		bluePlayer = GetBluePlayer(game);
		bluePlayer.Col.ShouldBe(1);
		bluePlayer.Row.ShouldBe(1);

		for (int i = 0; i < 5; i++) {
			game = game.PushTheTile(1, -1);
		}

		bluePlayer = GetBluePlayer(game);
		bluePlayer.Col.ShouldBe(1);
		bluePlayer.Row.ShouldBe(6);


		game = game.PushTheTile(1, -1);
		bluePlayer = GetBluePlayer(game);
		bluePlayer.Col.ShouldBe(1);
		// Wraps around and row becomes 0
		bluePlayer.Row.ShouldBe(0);

		static Player GetBluePlayer(LabyrinthGame game) => game.Players.First(p => p.Home is Treasure.BluePlayer);
		static Player GetGreenPlayer(LabyrinthGame game) => game.Players.First(p => p.Home is Treasure.GreenPlayer);
	}


}
