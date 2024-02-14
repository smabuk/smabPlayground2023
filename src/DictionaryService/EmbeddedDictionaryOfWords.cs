using System.Reflection;

using Microsoft.Extensions.FileProviders;

using Smab.DiceAndTiles;

namespace DictionaryService;

public class EmbeddedDictionaryOfWords : IDictionaryOfWords
{
	private readonly DictionaryOfWords dictionaryOfWords;

	public EmbeddedDictionaryOfWords()
	{
		EmbeddedFileProvider embeddedProvider = new(Assembly.GetExecutingAssembly());
		IFileInfo fileInfo = embeddedProvider.GetFileInfo("words.txt");
		using Stream reader = fileInfo.CreateReadStream();
		using StreamReader streamReader = new(reader);
		dictionaryOfWords= new(streamReader.ReadToEnd().ReplaceLineEndings().Split(Environment.NewLine));
	}

	public int Count => dictionaryOfWords.Count;
	public bool HasWords => dictionaryOfWords.HasWords;
	public bool IsWord(string word) => dictionaryOfWords.IsWord(word);
}
