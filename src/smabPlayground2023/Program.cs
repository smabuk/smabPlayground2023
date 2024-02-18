using System.Globalization;
using System.Reflection;

using Smab.DictionaryOfWords.CSW21;

using smabPlayground2023.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
	.AddRazorComponents()
	.AddInteractiveServerComponents()
	.AddInteractiveWebAssemblyComponents();

builder.Services.AddLocalization();
builder.Services.AddHealthChecks();

builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient();

//string? dictionaryFileName = builder.Configuration.GetValue<string>("DictionaryOfWords");
//Smab.DiceAndTiles.DictionaryOfWords dictionaryOfWords = File.Exists(dictionaryFileName) ? new(dictionaryFileName) : new();
Smab.DictionaryOfWords.IDictionaryService dictionaryOfWords = new CSW21Dictionary();
_ = builder.Services.AddSingleton(dictionaryOfWords);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
	app.UseWebAssemblyDebugging();
} else {
	_ = app.UseExceptionHandler("/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	_ = app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAntiforgery();

app.MapRazorComponents<App>()
	.AddInteractiveServerRenderMode()
	.AddInteractiveWebAssemblyRenderMode()
	.AddAdditionalAssemblies(typeof(smabPlayground2023.Client.Components.Pages.Counter).Assembly);

app.UseRequestLocalization(
	new RequestLocalizationOptions()
		.SetDefaultCulture("en-GB")
		.AddSupportedCultures("en-GB")
		.AddSupportedUICultures(cultures)
	);

app.MapHealthChecks("/healthz");

app.Run();



public partial class Program
{
	private static readonly string[] cultures = CultureInfo
		.GetCultures(CultureTypes.AllCultures)
		.Select(c => c.Name)
		.ToArray();

	public static string SiteName { get; set; } = "smabPlayground2023";

	public static string Name { get; } = typeof(Program)
		.Assembly
		.GetName()
		.Name ?? "No assembly name";
	public static Version Version { get; } = typeof(Program)
		.Assembly
		.GetName()
		.Version ?? new();
	public static string ProductVersion { get; } =
		VersionWithoutGuid(typeof(Program)
		.Assembly
		.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?
		.InformationalVersion ?? "");
	public static string FrameworkVersion { get; } = System.Runtime.InteropServices.RuntimeInformation.FrameworkDescription;

	private static string VersionWithoutGuid(string version)
	{
		int indexOfPlus = version.IndexOf('+');
		return (indexOfPlus > 0) ? version[..indexOfPlus] : version;
	}
} // also required so you can reference it from tests
