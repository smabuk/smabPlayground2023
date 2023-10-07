namespace smabPlayground2023.Tests.WebTests;

public class WebTestBase : IClassFixture<WebApplicationFactory<Program>>
{
	protected readonly WebApplicationFactory<Program> Factory = new();
	protected IBrowsingContext AngleSharp => BrowsingContext.New(Configuration.Default);

}
