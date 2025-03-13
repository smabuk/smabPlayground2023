namespace smabPlayground2023.Tests.Dotnet09Tests;

public class EscapeSequenceTests
{
	[Theory]
	[InlineData('\u001b')]
	[InlineData(27)]
	public void Escape_EscapeSequence_ShouldBe(char c)
	{
		'\e'.ShouldBe(c);
	}
}
