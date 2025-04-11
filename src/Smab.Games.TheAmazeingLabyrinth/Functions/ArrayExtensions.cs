namespace Smab.Games.TheAmazeingLabyrinth.Functions;
public static class ArrayExtensions
{
	public const int COL_DIMENSION = 0;
	public const int ROW_DIMENSION = 1;

	extension<T>(T[,] array)
	{

		public IEnumerable<T> GetAllByRow()
			=> Enumerable.Range(0, array.GetUpperBound(ROW_DIMENSION) + 1).SelectMany(row => array.GetRow(row));

		public IEnumerable<T> GetCol(int col)
			=> Enumerable.Range(0, array.GetUpperBound(ROW_DIMENSION) + 1).Select(row => array[col, row]);

		public IEnumerable<T> GetRow(int row)
			=> Enumerable.Range(0, array.GetUpperBound(COL_DIMENSION) + 1).Select(col => array[col, row]);
	}
}
