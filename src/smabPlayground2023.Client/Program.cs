using System.Globalization;
using System.Reflection;

using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });


//using HttpClient httpClient = new() { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) };
//string[] words = [];
//try {
//	words = (await httpClient.GetStringAsync("data/words.txt")).ReplaceLineEndings().Split(Environment.NewLine);
//}
//catch (Exception) {
//	Console.WriteLine("words.txt not found.");
//}
//IDictionaryOfWords dictionaryOfWords = words is [] ? new DictionaryOfWords() : new DictionaryOfWords(words);

Smab.DiceAndTiles.IDictionaryOfWords dictionaryOfWords = new DictionaryService.EmbeddedDictionaryOfWords();
_ = builder.Services.AddSingleton(dictionaryOfWords);

await builder.Build().RunAsync();

namespace smabPlayground2023.Client
{
	public partial class ClientProgram
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
}
