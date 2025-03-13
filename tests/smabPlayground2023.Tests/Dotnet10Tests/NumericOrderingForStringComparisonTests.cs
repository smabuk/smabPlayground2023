using System.Globalization;

namespace smabPlayground2023.Tests.Dotnet10Tests;

public class NumericOrderingForStringComparisonTests
{
	static readonly StringComparer _numericStringComparer = StringComparer.Create(CultureInfo.CurrentCulture, CompareOptions.NumericOrdering);
	static readonly HashSet<string> _stringHashSet = new(_numericStringComparer) { "1", "2", "03", "0004", "5.0", "6", "7", "8" };
	
	[Theory]
	[InlineData(new string[] { "abc123x", "abc3x" }, new string[] { "abc3x", "abc123x" })]
	[InlineData(new string[] { "abc123x", "abc03x" }, new string[] { "abc03x", "abc123x" })]
	[InlineData(new string[] { "abc00123x123y", "abc123x03y" }, new string[] { "abc123x03y", "abc00123x123y" })]
	public void NumericOrdering_ShouldBe(string[] input, string[] expected)
	{
		string[] actual = [..input.Order(_numericStringComparer)];
		actual.ShouldBe(expected);
	}
	
	[Theory]
	[InlineData("abc123x", "abc00123x", true )]
	[InlineData("abc123x", "abc23x", false )]
	public void NumericOrdering_Equality_ShouldBe(string inputA, string inputB, bool expected)
	{
		bool actual = _numericStringComparer.Equals(inputA, inputB);
		actual.ShouldBe(expected);
	}
	
	[Theory]
	[InlineData("1", true)]
	[InlineData("2", true)]
	[InlineData("0002", true)]
	[InlineData("03", true)]
	[InlineData("3", true)]
	[InlineData("5", false)]
	[InlineData("5.0", true)]
	public void NumericOrdering_WithHashSets_ShouldExist(string input, bool expected)
	{
		_stringHashSet.Contains(input).ShouldBe(expected);
	}
}
