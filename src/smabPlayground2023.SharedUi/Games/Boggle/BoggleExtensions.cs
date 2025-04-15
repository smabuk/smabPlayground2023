using static smabPlayground2023.SharedUi.Games.Boggle.Boggle;

using Position = (int Col, int Row);

namespace smabPlayground2023.SharedUi.Games.Boggle;

internal static class BoggleExtensions
{
	private static readonly string NoArrowDirection = "NONE";

	extension(BoggleSlot boggleSlot)
	{
		internal BoggleSlot SetNoArrowDirection()
			=> boggleSlot with { ArrowDirection = NoArrowDirection };

		internal BoggleSlot SetArrowDirection(Position? prevPosition, int newCol, int newRow)
		{
			string arrowDirection = prevPosition.HasValue
				? (newCol - prevPosition.Value.Col, newRow - prevPosition.Value.Row) switch
				{
					( 0,  0) => "END",
					( 0, -1) => "N",
					( 1,  0) => "E",
					( 0,  1) => "S",
					(-1,  0) => "W",
					( 1, -1) => "NE",
					( 1,  1) => "SE",
					(-1,  1) => "SW",
					(-1, -1) => "NW",
					_ => ""
				}
				: "START";
			return boggleSlot with { ArrowDirection = arrowDirection };
		}
	}
}
