namespace smabPlayground2023.SharedUi.Games.Labyrinth.Models;

//TODO: change Maze to something immutable
public sealed record LabyrinthBoard(MazeTile[,] Maze, BoardPosWithExtraMazeTile PositionWithExtra);
