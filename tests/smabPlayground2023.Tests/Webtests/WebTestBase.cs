namespace smabPlayground2023.Tests.WebTests;

public class WebTestBase : IClassFixture<WebApplicationFactory<Program>>
{
	protected readonly WebApplicationFactory<Program> Factory = new();
	protected static IBrowsingContext AngleSharp => BrowsingContext.New(Configuration.Default);

}
