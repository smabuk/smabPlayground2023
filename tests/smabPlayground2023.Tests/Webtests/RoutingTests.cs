using Xunit.Abstractions;

namespace smabPlayground2023.Tests.WebTests;
public class RoutingTests(ITestOutputHelper testOutputHelper) : WebTestBase
{
	[Theory]
	[InlineData("/", "home")]
	[InlineData("/weather", "weather forecast")]
	public async Task Route_Returns_Expected_Page(string route, string expectedTitle)
	{
		testOutputHelper.WriteLine($"Loading: {route}");
		const string siteNameSuffix = " - smabPlayground2023";
		expectedTitle += siteNameSuffix;

		HttpClient client = Factory.CreateClient();
		HttpResponseMessage response = await client.GetAsync(route);
		response.IsSuccessStatusCode.ShouldBe(true);

		string html = await response.Content.ReadAsStringAsync();
		IDocument document = await AngleSharp.OpenAsync(req => req.Content(html));
		string? title = document.Title;
		title.ShouldBe(expectedTitle, StringCompareShould.IgnoreCase);
	}

}
