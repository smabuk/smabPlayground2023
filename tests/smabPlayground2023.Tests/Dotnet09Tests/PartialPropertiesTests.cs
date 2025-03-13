using System.Text.RegularExpressions;

namespace smabPlayground2023.Tests.Dotnet09Tests;
public partial class PartialPropertiesTests
{
	[GeneratedRegex("abc|def")]
	private static partial Regex NewMethod { get; }

	[Theory]
	[InlineData("ab", false, 0)]
	[InlineData("abc", true, 1)]
	[InlineData("abcdefabdefabc", true, 4)]
	void GeneratedRegex_ShouldWorkWith_PartialProperties(string input, bool expectedMatchResult, int expectedMatchCount)
	{
		bool oldMatchResult = OldRegex.OldMethod().IsMatch(input);
		bool newMatchResult = NewMethod.IsMatch(input);

		oldMatchResult.ShouldBe(expectedMatchResult);
		newMatchResult.ShouldBe(expectedMatchResult);

		int oldMatchCount = OldRegex.OldMethod().Matches(input).Count();
		int newMatchCount = NewMethod.Matches(input).Count();

		oldMatchCount.ShouldBe(expectedMatchCount);
		newMatchCount.ShouldBe(expectedMatchCount);
	}

	private static partial class OldRegex
	{
		[GeneratedRegex("abc|def")]
		public static partial Regex OldMethod();
	}
}

