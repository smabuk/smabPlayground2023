namespace smabPlayground2023.SharedUi.Games.Labyrinth;
public static class ArrayExtensions
{
	public const int COL_DIMENSION = 0;
	public const int ROW_DIMENSION = 1;

	public static IEnumerable<T> GetAllByRow<T>(this T[,] array)
		=> Enumerable.Range(0, array.GetUpperBound(ROW_DIMENSION)).SelectMany(row => array.GetRow(row));

	public static IEnumerable<T> GetCol<T>(this T[,] array, int col)
		=> Enumerable.Range(0, array.GetUpperBound(ROW_DIMENSION) + 1).Select(row => array[col, row]);

	public static IEnumerable<T> GetRow<T>(this T[,] array, int row)
		=> Enumerable.Range(0, array.GetUpperBound(COL_DIMENSION) + 1).Select(col => array[col, row]);
}
