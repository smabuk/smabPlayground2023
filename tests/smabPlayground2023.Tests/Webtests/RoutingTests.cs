namespace smabPlayground2023.Tests.Webtests;
public class RoutingTests : WebTestBase
{
	[Theory]
	[InlineData("/", "index")]
	public async Task Route_Returns_Expected_Page(string route, string expectedTitle)
	{
		HttpClient client = Factory.CreateClient();
		HttpResponseMessage response = await client.GetAsync(route);
		response.IsSuccessStatusCode.ShouldBe(true);

		string html = await response.Content.ReadAsStringAsync();
		IDocument document = await AngleSharp.OpenAsync(req => req.Content(html));
		string? title = document.Title;
		title.ShouldBe(expectedTitle, StringCompareShould.IgnoreCase);
	}

}
