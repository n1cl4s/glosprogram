# Glosprogram – kodstandard

Det här är ett konsolprogram i C# (.NET 10) för att översätta glosor. Projektet används i undervisning, så koden ska vara lätt att läsa och följa för nybörjare. Välj enkla och tydliga lösningar framför smarta eller kompakta.

## Modell

Arbetet i koden ska ske med Claude Opus 5.5 (`claude-opus-5-5`), fråga om en annan modell behöver användas.

## Projektstruktur

- `Program.cs` använder top-level statements och innehåller programflödet uppifrån och ned: inläsning, uppbyggnad av dictionary och sedan huvudloopen.
- Varje klass ligger i en egen fil som heter som klassen, till exempel `Word.cs`.
- Ordlistor ligger i `wordlists/` och heter `källspråk-målspråk.csv`, till exempel `swedish-english.csv`. Språknamnen skrivs på engelska med gemener.
- Varje rad i en ordlista är ett ordpar: `ord,översättning`. Synonymer läggs som flera rader med samma första ord (`stor,big` och `stor,large`).
- Filer läses med relativa sökvägar, till exempel `./wordlists/...`.

## Språkfunktioner och stil

- `ImplicitUsings` och `Nullable` är påslagna. Skriv inga `using`-satser som redan ingår implicit.
- Skriv ut typerna: `List<Word> words`, `string[] wordPair`, `string? input`. Använd inte `var` för vanliga variabler.
- Använd collection expressions för tomma eller initierade samlingar: `List<Word> words = [];`
- Modellklasser använder primary constructors och get-only properties, så att objekten är oföränderliga:
  ```csharp
  class Word(string wordIn, string wordOut, string languageIn, string languageOut)
  {
    public string WordIn { get; } = wordIn;
    ...
  }
  ```
- Bygg uppslagsstrukturer med LINQ (`GroupBy` + `ToDictionary`) till `Dictionary<string, List<Word>>`, så att synonymer hamnar i en lista under samma nyckel.
- Uppslag ska vara skiftlägesokänsliga. Skicka alltid med `StringComparer.OrdinalIgnoreCase` till både `GroupBy` och `ToDictionary`.

## Formatering

- Indentera med 2 mellanslag.
- Sätt klammerparenteser på egen rad (Allman-stil), även för `if`, `else`, `foreach` och `while`.
- Skriv kedjade LINQ-anrop med en metod per rad.

## Namngivning

- Namnge alla identifierare på engelska.
- Använd PascalCase för klasser och properties (`Word`, `WordIn`, `LanguageOut`).
- Använd camelCase för lokala variabler och parametrar (`words`, `wordPair`, `wordToTranslate`).
- Använd beskrivande namn som säger vad variabeln innehåller, till exempel `wordToTranslate` hellre än `w`.

## Kommentarer och texter

- Skriv kommentarer på svenska. De ska förklara *varför* eller *vad som händer* på ett sätt som hjälper en elev, till exempel `// skapar lista baserat på gemensam nyckel (t.ex. stor => big, large)`.
- Skriv texter som visas för användaren på svenska.

## Arbetssätt

- Gör en sak i taget i små steg. Varje steg ska motsvara en use case och gå att köra och testa innan nästa påbörjas.
- Bygg vidare på det som finns. Undvik stora omskrivningar och nya abstraktioner som eleverna inte behöver ännu.

## Prompthistorik

- Underhåll `prompt-history.md` kontinuerligt. Lägg till varje ny prompt från användaren i slutet av filen, i samma tur som prompten besvaras.
- Varje post har en numrerad rubrik, prompten ordagrant som citat (`>`) och en kort sammanfattning på svenska av vad som gjordes.
- Ta bara med användarens egna meddelanden, inte systemprompten eller Claudes svar.
