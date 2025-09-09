namespace smabPlayground2023.Tests.Dotnet09Tests;

public class ParamsCollectionsTests
{
	[Fact]
	public void ParamsCollections_CanBe_NotArrays()
	{
		GetSpanInts(1, 2, 3).ShouldBe([1, 2, 3]);
		GetIEnumerableInts(1, 2, 3).ShouldBe([1, 2, 3]);

		int[] GetSpanInts(params ReadOnlySpan<int> ints) => ints.ToArray();
		int[] GetIEnumerableInts(params IEnumerable<int> ints) => [.. ints];
	}
}
