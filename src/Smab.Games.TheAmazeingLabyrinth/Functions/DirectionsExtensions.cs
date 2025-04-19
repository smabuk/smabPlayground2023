namespace Smab.Games.TheAmazeingLabyrinth.Functions;

public static class DirectionsExtensions
{
	extension(Directions directions)
	{
		/// <summary>
		/// This returns the directions after they have been rotated through <paramref name="amount"/> degrees clockwise.
		/// </summary>
		/// <param name="directions"></param>
		/// <param name="amount">The number of degrees to rotate. Must be one of 0, 90, 180 or 270,</param>
		/// <returns>Directions after they have been rotated through <paramref name="amount"/> degrees clockwise.</returns>
		public Directions Rotate(int amount)
		{
			ArgumentOutOfRangeException.ThrowIfNotEqual(amount % 90, 0, nameof(amount));

			const int NO_OF_ENUMS = 4;
			const int ALL_VALUES_MASK = 15;

			if (amount == 0 || ((int)directions ^ ALL_VALUES_MASK) == 0) {
				return directions;
			}

			if (amount < 0) {
				amount = 360 + (amount % 360);
			}

			int positions = amount / 90;
			return (Directions)((((int)directions << positions) | ((int)directions >> (NO_OF_ENUMS - positions))) & ALL_VALUES_MASK);
		}

		/// <summary>
		/// Returns a string with each direction represented by their initial letter. Always in the order NESW.
		/// </summary>
		/// <param name="direction"></param>
		/// <returns>A string with each direction represented by their initial letter. Always in the order NESW.</returns>
		/// <example>North and West = "NW".</example>
		public string ToShortString()
		{
			return directions is NoDirection
				? ""
				: $"{(directions.HasFlag(North) ? "N" : "")}{(directions.HasFlag(East) ? "E" : "")}{(directions.HasFlag(South) ? "S" : "")}{(directions.HasFlag(West) ? "W" : "")}";
			;
		}
	}

}
