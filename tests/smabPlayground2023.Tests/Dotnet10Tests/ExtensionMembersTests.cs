using static smabPlayground2023.Tests.Dotnet10Tests.ExtensionMembersTests;

namespace smabPlayground2023.Tests.Dotnet10Tests;

public class ExtensionMembersTests
{
	internal record Customer(string? Name, int Age);

	[Fact]
	public void NameIsNull_ShouldBeFalse()
	{
		Customer customer = new("My Name", 22);
		customer.NameIsNull_OldMethod().ShouldBeFalse();

		customer.NameIsNull_NewMethod().ShouldBeFalse();
		customer.NameIsNull_Property.ShouldBeFalse();
	}

	[Fact]
	public void NameIsNull_ShouldBeTrue()
	{
		Customer customer = new(null, 22);
		customer.NameIsNull_OldMethod().ShouldBeTrue();
		
		customer.NameIsNull_NewMethod().ShouldBeTrue();
		customer.NameIsNull_Property.ShouldBeTrue();
	}

	[Fact]
	public void StaticMethod_CreatesCustomer()
	{
		int expectedMinimumAge = Customer.BaseAge;
		expectedMinimumAge.ShouldBeGreaterThan(23);

		Customer customer = Customer.CreateWithDefaults();
		customer.Name.ShouldBe("Default Name");
		customer.Age.ShouldBe(expectedMinimumAge);
	}


}

internal static class ExtensionMembersTestsNewExtensions
{
	extension(Customer customer)
	{
		public bool NameIsNull_NewMethod() => customer?.Name is null;
		public bool NameIsNull_Property => customer?.Name is null;

	}

	extension(Customer)
	{
		public static Customer CreateWithDefaults() => new("Default Name", Customer.BaseAge);
		public static int BaseAge => DateTime.Now.Year - 2001;
	}

}


internal static class ExtensionMembersTestsOldExtensions
{
	public static bool NameIsNull_OldMethod(this Customer customer) => customer?.Name is null;
}
