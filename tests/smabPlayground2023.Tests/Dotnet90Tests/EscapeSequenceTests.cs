namespace smabPlayground2023.Tests.Dotnet90Tests;

public class EscapeSequenceTests
{
	[Theory]
	[InlineData('\u001b')]
	[InlineData(27)]
	public void Escape_EscapeSequemce_ShouldBe(char c)
	{
		'\e'.ShouldBe(c);
	}
}
