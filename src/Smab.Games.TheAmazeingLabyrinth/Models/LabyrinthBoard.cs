namespace Smab.Games.TheAmazeingLabyrinth.Models;

//TODO: change Maze to something immutable
public sealed record LabyrinthBoard(MazeTile[,] Maze, BoardPosWithExtraMazeTile PositionWithExtra);
