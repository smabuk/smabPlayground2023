namespace smabPlayground2023.Tests.WebTests;
public class OneDriveTests : WebTestBase
{
	[Theory]
	[InlineData("0", "Lorem ipsum dolor sit amet")]
	[InlineData("1", "Peugeot 306 Xsi")]
	[InlineData("2", "   1  Sat 26-Dec-2009  2009  Cloudy with a Chance of Meatballs")]
	[InlineData("3", "Error: Invalid file index")]
	[InlineData("-1", "Error: Invalid file index")]
	public async Task OneDrive_File_Contains_Expected_String(string fileNumber, string expectedString)
	{
		HttpClient client = Factory.CreateClient();
		using HttpResponseMessage response = await client.GetAsync($"onedrivefiles01/{fileNumber}");
		response.IsSuccessStatusCode.ShouldBe(true);

		string html = await response.Content.ReadAsStringAsync();
		IDocument document = await AngleSharp.OpenAsync(req => req.Content(html));
		document.Body?.InnerHtml.Contains(expectedString).ShouldBeTrue();
	}

}
