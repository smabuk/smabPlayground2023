using System.Text.Json;

namespace smabPlayground2023.Tests.Dotnet09Tests;

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

	[Fact]
	public void Json_Should_RespectNullableAnnotations()
	{
		JsonSerializer.Serialize(new SomeObject(null!, 10, 20), JsonSerializerOptions.Web)
			.ShouldBe("""{"string":null,"number":10,"optionalNumber":20}""");

		JsonSerializerOptions webOptionsWithRespectNullable = new(JsonSerializerDefaults.Web)
		{
			RespectNullableAnnotations = true
		};
		_ = Should.Throw<JsonException>(() =>
			JsonSerializer.Serialize(new SomeObject(null!, 10, 20), webOptionsWithRespectNullable)
			.ShouldBe("""{"string":null,"number":10,"optionalNumber":20}""")
			);
	}

	private record class SomeObject(string String, int Number, int? OptionalNumber = null);
}
