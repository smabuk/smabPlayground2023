using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });


using HttpClient httpClient = new() { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) };
string[] words = [];
try {
	words = (await httpClient.GetStringAsync("data/words.txt")).ReplaceLineEndings().Split(Environment.NewLine);
}
catch (Exception) {
	Console.WriteLine("words.txt not found.");
}

Smab.DiceAndTiles.DictionaryOfWords dictionaryOfWords = words is [] ? new() : new(words);
_ = builder.Services.AddSingleton(dictionaryOfWords);

await builder.Build().RunAsync();
