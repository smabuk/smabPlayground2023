namespace smabPlayground2023.SharedUi.Games.Labyrinth;
public static class ArrayExtensions
{
	public static IEnumerable<T> GetAllByRow<T>(this T[,] array) => [
		.. array.GetRow(0),
		.. array.GetRow(1),
		.. array.GetRow(2),
		.. array.GetRow(3),
		.. array.GetRow(4),
		.. array.GetRow(5),
		.. array.GetRow(6),
		];

	public static IEnumerable<T> GetCol<T>(this T[,] array, int col) => Enumerable.Range(0, 7).Select(row => array[col, row]);

	public static IEnumerable<T> GetRow<T>(this T[,] array, int row) => Enumerable.Range(0, 7).Select(col => array[col, row]);
}
