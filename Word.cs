class Word(string wordIn, string wordOut, string languageIn, string languageOut)
{
  public string WordIn { get; } = wordIn;
  public string WordOut { get; } = wordOut;
  public string LanguageIn { get; } = languageIn;
  public string LanguageOut { get; } = languageOut;

  // UC3: språkparet som text, så att det kan visas och jämföras på ett och samma sätt (t.ex. "swedish → english")
  public string LanguagePair { get; } = $"{languageIn} → {languageOut}";
}
