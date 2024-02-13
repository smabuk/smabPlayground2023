using static smabPlayground2023.Client.Components.Pages.Games.Boggle.Boggle;

namespace smabPlayground2023.Client.Components.Pages.Games.Boggle;

internal static class BoggleExtensions
{
	public static string NoArrowDirection = "NONE";

	internal static BoggleSlot SetNoArrowDirection(this BoggleSlot boggleSlot)
		=> boggleSlot with { ArrowDirection = NoArrowDirection };

	internal static BoggleSlot SetArrowDirection(this BoggleSlot boggleSlot, int? prevCol, int? prevRow, int newCol, int newRow)
	{
		string arrowDirection = prevCol is null || prevRow is null
			? "START"
			: (newCol - prevCol, newRow - prevRow) switch
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
				};
		return boggleSlot with { ArrowDirection = arrowDirection };
	}

}
