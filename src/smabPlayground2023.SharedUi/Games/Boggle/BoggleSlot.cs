using Smab.DiceAndTiles;

namespace smabPlayground2023.SharedUi.Games.Boggle;

public partial class Boggle
{
	internal record BoggleSlot(PositionedDie PositionedDie, bool IsSelected = false, string ArrowDirection = "NONE")
	{
		public int Col => PositionedDie.Col;
		public int Row => PositionedDie.Row;

		public (int Col, int Row) Position => (Col, Row);

		public bool IsSelectable => !(PositionedDie.Die.IsBlank || IsSelected);

		public bool IsSameOrAdjacent(int col, int row)
			=> (int.Abs(PositionedDie.Col - col) is 0 or 1) && (int.Abs(PositionedDie.Row - row) is 0 or 1);

	}
}
