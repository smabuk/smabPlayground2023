namespace Smab.Games.TheAmazeingLabyrinth.Functions;

public static class LabyrinthGameExtensions
{
	extension(LabyrinthGame game)
	{
		public BoardPosition[,] AsGrid()
		{
			LabyrinthBoard board = game.Board;
			int boardSize = game.BoardSize;
			int gridSize = boardSize + 2;
			BoardPosition[,] grid = (BoardPosition[,])Array.CreateInstance(typeof(BoardPosition), [gridSize, gridSize], [-1, -1]);

			for (int j = -1; j < gridSize - 1; j++) {
				for (int i = -1; i < gridSize - 1; i += boardSize + 1) {
					if (j % 2 == 1 && j > 0 && j < boardSize) {
						grid[i, j] = new BoardPosWithPush(i, j);
						grid[j, i] = new BoardPosWithPush(j, i);

					} else {
						grid[i, j] = new EmptyBoardPos(i, j);
						grid[j, i] = new EmptyBoardPos(j, i);
					}
				}
			}

			grid[game.Board.PositionWithExtra.Col, game.Board.PositionWithExtra.Row] = game.Board.PositionWithExtra;

			for (int row = 0; row < boardSize; row++) {
				List<MazeTile> tiles = [.. board.GetRow(row)];
				for (int col = 0; col < boardSize; col++) {
					grid[col, row] = new BoardPosWithMazeTile(col, row, tiles[col]);
				}
			}

			return grid;
		}

		public BoardPosition? GetBoardPositionByTreasure(Treasure treasure)
		{
			LabyrinthBoard board = game.Board;
			int boardSize = game.BoardSize;
			for (int row = 0; row < boardSize; row++) {
				List<MazeTile> tiles = [.. board.GetRow(row)];
				for (int col = 0; col < boardSize; col++) {
					if (tiles[col].Treasure == treasure) {
						return new BoardPosWithMazeTile(col, row, tiles[col]);
					}
				}
			}

			return null;
		}

		public LabyrinthGame AddPlayers(params Treasure[] players)
		{
			List<Player> newPlayers = [
				.. game.Players,
				.. players.Select(t => new Player(t, int.MinValue, int.MinValue))
			];

			for (int i = 0; i < newPlayers.Count; i++) {
				Player player = newPlayers[i];
				if (player.Col < 0) {
					BoardPosition position = game.GetBoardPositionByTreasure(player.Home)!;
					int col = position.Col;
					int row = position.Row;
					newPlayers[i] = player with { Col = col, Row = row };
				}
			}

			return game with { Players = [.. newPlayers.DistinctBy(p => p.Home)] };
		}

		public LabyrinthGame AddPlayers(Treasure player) => game.AddPlayers(player);

		internal List<Player> PushThePlayers(int col, int row)
		{
			List<Player> players = new(game.Players.Count);
			Directions direction = game.Board.PushDirection(col, row);
			foreach (Player player in game.Players) {
				if (player.Col == col && direction is North or South) {
					int newRow = (player.Row + (direction == North ? -1 : 1) + game.BoardSize) % game.BoardSize;
					players.Add(player with { Row = newRow });
				} else if (player.Row == row && direction is East or West) {
					int newCol = (player.Col + (direction == West ? -1 : 1) + game.BoardSize) % game.BoardSize;
					players.Add(player with { Col = newCol });
				} else {
					players.Add(player);
				}
			}

			return players;
		}

		public LabyrinthGame PushTheTile(int col, int row)
			=> game with
			{
				Board = game.Board.Push(col, row),
				Players = [.. game.PushThePlayers(col, row)],
				CurrentPlayerIndex = (game.CurrentPlayerIndex + 1) % game.Players.Count,
				State = GameState.MovePlayer,
			};

		public LabyrinthGame Rotate(int amount)    => game with { Board = game.Board.RotateExtraMazeTile(amount) };
		public LabyrinthGame RotateClockwise()     => game with { Board = game.Board.RotateExtraMazeTile(90) };
		public LabyrinthGame RotateAntiClockwise() => game with { Board = game.Board.RotateExtraMazeTile(-90) };
	}
}
