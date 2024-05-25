using System.Text.Json;

namespace smabPlayground2023.Tests.Dotnet90Tests;

public class JsonTests
{
	[Fact]
	public void Json_ShouldSerializeWith_IndentOptions()
	{
		JsonSerializerOptions options = new()
		{
			WriteIndented = true,
			IndentCharacter = ' ',
			IndentSize = 6,
			PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
		};

		JsonSerializer.Serialize(new { String = "something", Number = 10 }, options)
			.ShouldBe("""
				{
				      "string": "something",
				      "number": 10
				}
				""");
	}

	[Fact]
	public void Json_ShouldSerializeWith_WebDefaults()
	{
		JsonSerializer.Serialize(new { String = "something", Number = 10 }, JsonSerializerOptions.Web)
			.ShouldBe("""{"string":"something","number":10}""");
	}
}
