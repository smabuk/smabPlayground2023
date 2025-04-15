namespace smabPlayground2023.Tests.Dotnet10Tests;

public class NullConditionalAssignmentTests
{
	private class Customer
	{
		public string? Name { get; set; }
		public int Age { get; set; }
	}

	[Fact]
	public void Assignment_ShouldSucceed()
	{
		Customer? customer = new();
		customer?.Name = "Default Name";
		customer?.Name.ShouldBe("Default Name");
	}

	[Fact]
	public void Assignment_ShouldFail()
	{
		Customer? customer = null;
		customer?.Name = "Default Name";
		customer?.Name.ShouldBeNull();
	}
}
