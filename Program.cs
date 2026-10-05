Console.WriteLine("Glosprogram");

/*
List<string> words = [
  "hus", "house",     // jämna index => svenskt uppslag, udda => engelsk översättning
  "hem", "home",
  "stor", "big",      // synonymer får hanteras i en loop
  "stor", "large"
];
*/

List<Word> words = [];

// fyll listan med ord från disk (wordlists)
foreach(string line in File.ReadAllLines("./wordlists/swedish-english.csv"))
{
    string[] wordPair = line.Split(",");
    words.Add(new Word(wordPair[0], wordPair[1], "swedish", "english"));
}

// Dictionary

Dictionary<string, List<Word>>  swedishToEnglish = words
.GroupBy(word => word.WordIn, StringComparer.OrdinalIgnoreCase)
.ToDictionary(
word => word.Key, // nyckeln
word => word.ToList(),    // värdet, typiskt hela objektet (referensen)
StringComparer.OrdinalIgnoreCase
);

while (true)
{
Console.WriteLine("Ange vilket ord du vill översätta");
string? wordToTranslate = Console.ReadLine()!;

if(swedishToEnglish.ContainsKey(wordToTranslate!))
    {foreach(var word in swedishToEnglish[wordToTranslate!])
    {Console.WriteLine(word.WordOut);} }

else
{Console.WriteLine("Detta ordet finns inte i ordlistan");}


}

class Word(string wordIn, string wordOut, string languageIn, string languageOut)
{
  public string WordIn { get; } = wordIn;
  public string WordOut { get; } = wordOut;
  public string LanguageIn { get; } = languageIn;
  public string LanguageOut { get; } = languageOut;
}