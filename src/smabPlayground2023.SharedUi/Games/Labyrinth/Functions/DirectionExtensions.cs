namespace smabPlayground2023.SharedUi.Games.Labyrinth.Functions;

public static class DirectionExtensions
{
	/// <summary>
	/// This returns the directions after they have been rotated through <paramref name="amount"/> degrees clockwise.
	/// </summary>
	/// <param name="direction"></param>
	/// <param name="amount">The number of degrees to rotate. Must be one of 0, 90, 180 or 270,</param>
	/// <returns>Directions after they have been rotated through <paramref name="tile.Orientation"/> degrees clockwise.</returns>
	public static Direction Rotate(this Direction direction, int amount)
	{
		ArgumentOutOfRangeException.ThrowIfNotEqual(amount % 90, 0, nameof(amount));

		int allValuesMask = 15;
		if (amount == 0 || ((int)direction ^ allValuesMask) == 0) {
			return direction;
		}

		if (amount < 0) {
			amount = 360 + (amount % 360);
		}

		int positions = amount / 90;
		int noOfEnums = 4;
		return (Direction)((((int)direction << positions) | ((int)direction >> (noOfEnums - positions))) & allValuesMask);
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="direction"></param>
	/// <returns>A string with each direction represented by their initial letter. Always in the order NESW.</returns>
	/// <example>North and West = "NW".</example>
	public static string ToShortString(this Direction direction)
	{
		return direction == NoDirection
			? ""
			: $"{(direction.HasFlag(North) ? "N" : "")}{(direction.HasFlag(East) ? "E" : "")}{(direction.HasFlag(South) ? "S" : "")}{(direction.HasFlag(West) ? "W" : "")}";
		;
	}

}
