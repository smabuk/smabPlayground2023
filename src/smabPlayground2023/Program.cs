using System.Reflection;

using smabPlayground2023;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
	.AddRazorComponents()
	.AddServerComponents()
	.AddWebAssemblyComponents();

builder.Services.AddLocalization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment()) {
	_ = app.UseExceptionHandler("/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	_ = app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.MapRazorComponents<App>()
	.AddServerRenderMode()
	.AddWebAssemblyRenderMode();

app.UseRequestLocalization(
	new RequestLocalizationOptions()
			.SetDefaultCulture(cultures[0])
			.AddSupportedCultures(cultures[0])
			.AddSupportedUICultures(cultures));

app.Run();

public partial class Program
{
	public static string SiteName { get; set; } = "smabPlayground2023";
	public static string Name { get; } = typeof(Program).Assembly
							.GetName().Name ?? "No assembly name";
	public static Version Version { get; } = typeof(Program).Assembly
							.GetName().Version ?? new();
	public  static string ProductVersion { get; } = typeof(Program).Assembly
							.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?
							.InformationalVersion ?? "";
	public static string FrameworkVersion { get; } = System.Runtime.InteropServices.RuntimeInformation.FrameworkDescription;

	private static readonly string[] cultures = new[] { "en-GB", "en-US", "en", "fr", "es" };
} // also required so you can reference it from tests
