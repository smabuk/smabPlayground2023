using System.Reflection;

namespace smabPlayground2023.Tests.Dotnet90Tests;

public class LinqTests
{
	private const string LoremIpsumText = """
			Lorem ipsum dolor sit amet, consectetur adipiscing elit.
			Sed non risus. Suspendisse lectus tortor, dignissim sit amet, 
			adipiscing nec, ultricies sed, dolor. Cras elementum ultrices amet diam.
			""";

	/// <summary>
	/// CountBy lets you quickly calculate the frequency of each key.
	/// </summary>
	[Fact]
	public void CountBy_Should_Mirror_GroupBy_Then_Count()
	{
		IEnumerable<string> words = LoremIpsumText
			.Split(new char[] { ' ', '.', ',' }, StringSplitOptions.RemoveEmptyEntries)
			.Select(word => word.ToLowerInvariant());

		IEnumerable<KeyValuePair<string, int>> expected = words
			.GroupBy(word => word, (word, words) => new KeyValuePair<string, int>(word, words.Count()));

		IEnumerable<KeyValuePair<string, int>> actual = words.CountBy(word => word);

		actual.Count().ShouldBe(23);
		actual.Count().ShouldBe(expected.Count());

		actual.MaxBy(pair => pair.Value).ShouldBe(expected.MaxBy(pair => pair.Value));
	}

	/// <summary>
	/// Index() makes it possible to quickly extract the implicit index of an enumerable. 
	/// </summary>
	[Fact]
	public void Index_Should_Provide_Index()
	{
		IEnumerable<(int Index, string Word)> actual = LoremIpsumText
			.Split(new char[] { ' ', '.', ',' }, StringSplitOptions.RemoveEmptyEntries)
			.Select(word => word.ToLowerInvariant())
			.Index();

		actual.Count().ShouldBe(27);
		actual.First().Index.ShouldBe(0);
		actual.Last().Index.ShouldBe(26);
	}

	/// <summary>
	/// AggregateBy lets you implement more general-purpose workflows. 
	/// </summary>
	[Fact]
	public void AggregateBy_Should_Provide_Total_Scores()
	{
		(string Id, int Score)[] data =
			[
				("0", 42),
				("1",  5),
				("2",  4),
				("1", 10),
				("0", 25),
			];

		IEnumerable<KeyValuePair<string, int>> expected = [
				new KeyValuePair<string, int>("0", 67),
				new KeyValuePair<string, int>("1", 15),
				new KeyValuePair<string, int>("2",  4),
			];

		IEnumerable<KeyValuePair<string, int>> actual =
			data
			.AggregateBy(
				keySelector: entry => entry.Id,
				seed: 0,
				func: (totalScore, current) => totalScore + current.Score
				);

		actual.Count().ShouldBe(3);
		actual.ShouldBe(expected);
	}
}
