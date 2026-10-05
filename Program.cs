using System.Globalization; // UC9: behövs för CultureInfo, som ger svensk bokstavsordning

Console.WriteLine("Glosprogram");

List<Word> words = [];

// UC11: kommer ihåg vilken fil varje språkpar kommer från, så att nya ord kan sparas i rätt fil
Dictionary<string, string> filePathForLanguagePair = [];

// UC2: hämtar sökvägarna till alla .csv-filer i mappen, så att en ny ordlista kommer med utan att koden ändras
string[] filePaths = Directory.GetFiles("./wordlists", "*.csv");

// UC2: läser in en fil i taget, och orden från alla filer hamnar i samma lista
foreach (string filePath in filePaths)
{
  // UC1: plockar ut filnamnet utan mapp och ändelse (./wordlists/swedish-english.csv => swedish-english)
  string fileName = Path.GetFileNameWithoutExtension(filePath);

  // UC1: delar filnamnet vid bindestrecket (swedish-english => ["swedish", "english"])
  string[] languages = fileName.Split("-");
  string languageIn = languages[0];
  string languageOut = languages[1];

  // UC11: båda riktningarna sparas i samma fil (UC6), så båda språkparen pekar på filen
  filePathForLanguagePair[$"{languageIn} → {languageOut}"] = filePath;
  filePathForLanguagePair[$"{languageOut} → {languageIn}"] = filePath;

  // fyll listan med ord från disk (wordlists)
  foreach (string line in File.ReadAllLines(filePath)) // UC1: använder variabeln filePath
  {
    string[] wordPair = line.Split(",");
    words.Add(new Word(wordPair[0], wordPair[1], languageIn, languageOut)); // UC1: språken kommer från filnamnet i stället för hårdkodad text

    // UC6: lägger också till ordet åt andra hållet, så att samma fil räcker för båda riktningarna (house => hus)
    words.Add(new Word(wordPair[1], wordPair[0], languageOut, languageIn));
  }
}

// UC2: visar att orden från alla filer har lästs in (t.ex. 197 ord från 2 filer)
Console.WriteLine($"{words.Count} ord inlästa från {filePaths.Length} filer");

// UC3: plockar ut alla språkpar som finns bland orden, utan dubbletter
// Order sorterar i bokstavsordning, så att samma nummer alltid betyder samma språkpar
// UC16: tar också med språkparen från filerna, så att en ny, tom ordlista syns i listan fast den saknar ord
List<string> languagePairs = words
  .Select(word => word.LanguagePair)
  .Concat(filePathForLanguagePair.Keys)
  .Distinct()
  .Order()
  .ToList();

// UC5: yttre loop, så att användaren kan komma tillbaka hit och välja ett nytt språkpar med :byt
while (true)
{
  // UC3: skriver ut språkparen som en numrerad lista som börjar på 1
  Console.WriteLine("Språkpar:");
  for (int i = 0; i < languagePairs.Count; i++)
  {
    Console.WriteLine($"{i + 1}. {languagePairs[i]}");
  }


  // UC4: frågar efter ett nummer tills användaren har valt ett språkpar som finns
  string chosenLanguagePair = "";
  while (true)
  {
    Console.WriteLine("Välj språkpar genom att skriva dess nummer (eller :nytt för att skapa ett nytt språkpar)"); // UC16: berättar om kommandot
    string? choice = Console.ReadLine();

    // UC4: inmatningen har tagit slut (t.ex. Ctrl+D), annars skulle frågan upprepas i all oändlighet
    if (choice == null)
    {
      return;
    }

    // UC16: skapar ett nytt språkpar med en tom ordlista, så att filen inte behöver skapas för hand
    if (choice == ":nytt")
    {
      Console.WriteLine("Skriv källspråket på engelska (t.ex. swedish):");
      string? languageInInput = Console.ReadLine();
      Console.WriteLine("Skriv målspråket på engelska (t.ex. german):");
      string? languageOutInput = Console.ReadLine();

      if (languageInInput == null || languageOutInput == null)
      {
        return;
      }

      // UC16: språknamnen skrivs med gemener och utan mellanslag, som i CLAUDE.md (German  => german)
      string newLanguageIn = languageInInput.Trim().ToLower();
      string newLanguageOut = languageOutInput.Trim().ToLower();

      // UC16: bara bokstäver är tillåtna, eftersom t.ex. ett bindestreck skulle förstöra filnamnet (se BUG3 och BUG4)
      // All ger true för en tom text, så tomma namn kontrolleras för sig
      if (newLanguageIn == "" || newLanguageOut == "" || !newLanguageIn.All(char.IsLetter) || !newLanguageOut.All(char.IsLetter))
      {
        Console.WriteLine("Språknamnen får bara innehålla bokstäver och får inte vara tomma");
        continue;
      }

      if (newLanguageIn == newLanguageOut)
      {
        Console.WriteLine("Välj två olika språk");
        continue;
      }

      // UC16: Path.Combine sätter ihop mappen och filnamnet till en sökväg (./wordlists/swedish-german.csv)
      string newFilePath = Path.Combine("./wordlists", $"{newLanguageIn}-{newLanguageOut}.csv");
      string reversedFilePath = Path.Combine("./wordlists", $"{newLanguageOut}-{newLanguageIn}.csv");

      // UC16: finns filen redan, åt något håll, skulle den nya filen ge dubbletter (se UC6)
      if (File.Exists(newFilePath) || File.Exists(reversedFilePath))
      {
        Console.WriteLine($"Det finns redan en ordlista för {newLanguageIn} och {newLanguageOut}");
        continue;
      }

      // UC16: skapar en tom fil, orden läggs sedan till med :lägg (UC11)
      File.WriteAllText(newFilePath, "");

      // UC16: samma sak som vid inläsningen, båda riktningarna pekar på den nya filen
      string newLanguagePair = $"{newLanguageIn} → {newLanguageOut}";
      string reversedLanguagePair = $"{newLanguageOut} → {newLanguageIn}";
      filePathForLanguagePair[newLanguagePair] = newFilePath;
      filePathForLanguagePair[reversedLanguagePair] = newFilePath;

      // UC16: lägger till paren i listan och sorterar om, så att de syns nästa gång listan visas
      languagePairs.Add(newLanguagePair);
      languagePairs.Add(reversedLanguagePair);
      languagePairs.Sort();

      Console.WriteLine($"Skapade {newFilePath}. Lägg till ord med :lägg");
      chosenLanguagePair = newLanguagePair; // UC16: användaren hamnar direkt i det nya språkparet
      break;
    }

    // UC4: TryParse försöker göra om texten till ett tal och ger false om det inte går (t.ex. "abc")
    if (int.TryParse(choice, out int chosenNumber) && chosenNumber >= 1 && chosenNumber <= languagePairs.Count)
    {
      chosenLanguagePair = languagePairs[chosenNumber - 1]; // listan börjar på 0, men numren börjar på 1
      break;
    }

    Console.WriteLine($"Ogiltigt val, skriv ett nummer mellan 1 och {languagePairs.Count}");
  }

  Console.WriteLine($"Du översätter nu {chosenLanguagePair}");


  // Dictionary

  // UC4: ersätter swedishToEnglish och fungerar för alla språkpar, eftersom bara orden i det valda paret tas med
  Dictionary<string, List<Word>> translations = words
    .Where(word => word.LanguagePair == chosenLanguagePair)
    .GroupBy(word => word.WordIn, StringComparer.OrdinalIgnoreCase) // skapar lista baserat på gemensam nyckel (i e stor => big, large)
    .ToDictionary(
      group => group.Key, // nyckeln
      group => group.ToList(),          // värdet, typiskt hela objektet (referensen)
      StringComparer.OrdinalIgnoreCase
    );


  while (true)
  {

    // UC5 och UC9–UC18: berättar om kommandona, på två rader eftersom de har blivit många
    Console.WriteLine("Ange vilket ord du vill översätta, eller ett kommando:");
    Console.WriteLine("  st* söker, :lista visar alla ord, :lägg lägger till, :radera raderar, :ändra redigerar, :förhör startar ett förhör, :historik visar resultat, :byt byter språkpar");
    string? wordToTranslate = Console.ReadLine();

    // UC5: break lämnar bara den inre loopen, så programmet hoppar tillbaka till valet av språkpar
    if (wordToTranslate == ":byt")
    {
      break;
    }

    // UC9: visar hela ordlistan för det valda språkparet
    if (wordToTranslate == ":lista")
    {
      PrintWordList(translations, translations.Keys.ToList());
      continue; // UC9: hoppar över uppslagningen nedanför och frågar efter nästa ord
    }

    // UC10: en stjärna sist betyder att användaren söker på början av ett ord (st* => stor, stark, stol)
    if (wordToTranslate != null && wordToTranslate.EndsWith("*"))
    {
      // UC10: tar bort stjärnan, så att bara bokstäverna att söka på blir kvar (st* => st)
      string searchText = wordToTranslate.TrimEnd('*');

      // UC10: bara * skulle matcha alla ord, så användaren får skriva minst en bokstav
      if (searchText == "")
      {
        Console.WriteLine("Skriv minst en bokstav före *, till exempel st*");
        continue;
      }

      // UC10: plockar ut alla ord som börjar med söktexten, oavsett stora eller små bokstäver
      List<string> matchingWords = translations.Keys
        .Where(key => key.StartsWith(searchText, StringComparison.OrdinalIgnoreCase))
        .ToList();

      if (matchingWords.Count == 0)
      {
        Console.WriteLine($"Inga ord börjar på \"{searchText}\" i {chosenLanguagePair}");
      }
      else
      {
        PrintWordList(translations, matchingWords);
      }

      continue; // UC10: hoppar över den vanliga uppslagningen nedanför
    }

    // UC11: lägger till en ny översättning i det valda språkparet, både i minnet och i filen
    if (wordToTranslate == ":lägg")
    {
      // UC11: ett språkpar utan egen fil (t.ex. via ett mellanled, UC8) har ingenstans att spara
      if (!filePathForLanguagePair.ContainsKey(chosenLanguagePair))
      {
        Console.WriteLine($"Det går inte att lägga till ord i {chosenLanguagePair}, eftersom paret inte har någon egen fil");
        continue;
      }

      // UC11: delar upp språkparet i två språk (swedish → english => ["swedish", "english"])
      string[] chosenLanguages = chosenLanguagePair.Split(" → ");
      string chosenLanguageIn = chosenLanguages[0];
      string chosenLanguageOut = chosenLanguages[1];

      Console.WriteLine($"Skriv ordet på {chosenLanguageIn}:");
      string? newWordInput = Console.ReadLine();
      Console.WriteLine($"Skriv översättningen på {chosenLanguageOut}:");
      string? newTranslationInput = Console.ReadLine();

      // UC11: inmatningen har tagit slut (t.ex. Ctrl+D), samma lösning som i UC4
      if (newWordInput == null || newTranslationInput == null)
      {
        return;
      }

      // UC11: tar bort mellanslag före och efter, så att " big " sparas som "big"
      string newWord = newWordInput.Trim();
      string newTranslation = newTranslationInput.Trim();

      // UC11: ett tomt ord eller ett kommatecken skulle förstöra raden ord,översättning i csv-filen
      if (newWord == "" || newTranslation == "" || newWord.Contains(',') || newTranslation.Contains(','))
      {
        Console.WriteLine("Ordet och översättningen får inte vara tomma eller innehålla kommatecken");
        continue;
      }

      // UC11: kollar om exakt samma ordpar redan finns, oavsett stora eller små bokstäver
      bool alreadyExists = words.Any(word =>
        word.LanguagePair == chosenLanguagePair &&
        string.Equals(word.WordIn, newWord, StringComparison.OrdinalIgnoreCase) &&
        string.Equals(word.WordOut, newTranslation, StringComparison.OrdinalIgnoreCase));

      if (alreadyExists)
      {
        Console.WriteLine($"{newWord} → {newTranslation} finns redan i ordlistan");
        continue;
      }

      // UC11: lägger till ordet åt båda hållen, precis som vid inläsningen (UC6)
      Word newWordObject = new Word(newWord, newTranslation, chosenLanguageIn, chosenLanguageOut);
      words.Add(newWordObject);
      words.Add(new Word(newTranslation, newWord, chosenLanguageOut, chosenLanguageIn));

      // UC11: lägger till ordet i dictionaryt, så att det går att översätta direkt utan omstart
      // finns ordet redan blir den nya översättningen en synonym (stor => big, large, huge)
      if (translations.ContainsKey(newWord))
      {
        translations[newWord].Add(newWordObject);
      }
      else
      {
        translations[newWord] = [newWordObject];
      }

      // UC11: skriver om filen, så att ordet finns kvar nästa gång programmet startar
      SaveWordList(filePathForLanguagePair[chosenLanguagePair], words);

      Console.WriteLine($"Lade till {newWord} → {newTranslation}");
      continue;
    }

    // UC12: raderar en översättning i det valda språkparet, men behåller ordets andra synonymer
    if (wordToTranslate == ":radera")
    {
      // UC12: samma kontroll som i UC11, ett språkpar utan egen fil kan inte ändras
      if (!filePathForLanguagePair.ContainsKey(chosenLanguagePair))
      {
        Console.WriteLine($"Det går inte att radera ord i {chosenLanguagePair}, eftersom paret inte har någon egen fil");
        continue;
      }

      Console.WriteLine("Vilket ord vill du radera en översättning för?");
      string? wordInput = Console.ReadLine();

      // UC12: inmatningen har tagit slut (t.ex. Ctrl+D), samma lösning som i UC4
      if (wordInput == null)
      {
        return;
      }

      string wordToDeleteFrom = wordInput.Trim();

      if (!translations.ContainsKey(wordToDeleteFrom))
      {
        Console.WriteLine($"Ordet finns inte i ordlistan för {chosenLanguagePair}");
        continue;
      }

      // UC12: visar ordets översättningar som en numrerad lista som börjar på 1 (som i UC3)
      List<Word> wordTranslations = translations[wordToDeleteFrom];
      Console.WriteLine($"{wordTranslations[0].WordIn}:"); // UC12: skriver ordet som det står i ordlistan, även om användaren skrev "Stor"
      for (int i = 0; i < wordTranslations.Count; i++)
      {
        Console.WriteLine($"{i + 1}. {wordTranslations[i].WordOut}");
      }

      Console.WriteLine("Vilken översättning vill du radera? Skriv numret");
      string? numberInput = Console.ReadLine();

      if (numberInput == null)
      {
        return;
      }

      // UC12: samma kontroll av numret som i UC4
      if (!int.TryParse(numberInput, out int chosenNumber) || chosenNumber < 1 || chosenNumber > wordTranslations.Count)
      {
        Console.WriteLine("Ogiltigt val, inget raderades");
        continue;
      }

      Word wordToDelete = wordTranslations[chosenNumber - 1]; // listan börjar på 0, men numren börjar på 1

      // UC12: frågar en gång till, eftersom en radering inte går att ångra
      Console.WriteLine($"Vill du radera {wordToDelete.WordIn} → {wordToDelete.WordOut}? (j/n)");
      string? confirmation = Console.ReadLine();

      if (confirmation == null)
      {
        return;
      }

      if (confirmation.Trim().ToLower() != "j")
      {
        Console.WriteLine("Inget raderades");
        continue;
      }

      // UC12: tar bort ordet åt båda hållen ur listan, eftersom det lades till åt båda hållen vid inläsningen (UC6)
      words.Remove(wordToDelete);
      words.RemoveAll(word =>
        word.LanguageIn == wordToDelete.LanguageOut &&
        word.LanguageOut == wordToDelete.LanguageIn &&
        word.WordIn == wordToDelete.WordOut &&
        word.WordOut == wordToDelete.WordIn);

      // UC12: tar bort översättningen ur dictionaryt, så att ändringen syns direkt
      wordTranslations.Remove(wordToDelete);

      // UC12: var det ordets sista översättning tas hela ordet bort, annars skulle det finnas kvar utan översättningar
      if (wordTranslations.Count == 0)
      {
        translations.Remove(wordToDeleteFrom);
      }

      // UC12: samma spara-funktion som i UC11
      SaveWordList(filePathForLanguagePair[chosenLanguagePair], words);

      Console.WriteLine($"Raderade {wordToDelete.WordIn} → {wordToDelete.WordOut}");
      continue;
    }

    // UC13: redigerar ett ord eller en översättning i det valda språkparet
    if (wordToTranslate == ":ändra")
    {
      // UC13: samma kontroll som i UC11 och UC12, ett språkpar utan egen fil kan inte ändras
      if (!filePathForLanguagePair.ContainsKey(chosenLanguagePair))
      {
        Console.WriteLine($"Det går inte att ändra ord i {chosenLanguagePair}, eftersom paret inte har någon egen fil");
        continue;
      }

      Console.WriteLine("Vilket ord vill du ändra?");
      string? wordInput = Console.ReadLine();

      if (wordInput == null)
      {
        return;
      }

      string wordToEdit = wordInput.Trim();

      if (!translations.ContainsKey(wordToEdit))
      {
        Console.WriteLine($"Ordet finns inte i ordlistan för {chosenLanguagePair}");
        continue;
      }

      // UC13: visar ordets översättningar numrerade, som i UC12
      List<Word> wordTranslations = translations[wordToEdit];
      string existingWord = wordTranslations[0].WordIn; // ordet som det står i ordlistan, även om användaren skrev "Stor"
      Console.WriteLine($"{existingWord}:");
      for (int i = 0; i < wordTranslations.Count; i++)
      {
        Console.WriteLine($"{i + 1}. {wordTranslations[i].WordOut}");
      }

      Console.WriteLine($"Skriv 0 för att ändra själva ordet ({existingWord}), eller numret på översättningen du vill ändra");
      string? numberInput = Console.ReadLine();

      if (numberInput == null)
      {
        return;
      }

      // UC13: 0 betyder ordet, 1 och uppåt betyder en översättning
      if (!int.TryParse(numberInput, out int chosenNumber) || chosenNumber < 0 || chosenNumber > wordTranslations.Count)
      {
        Console.WriteLine("Ogiltigt val, inget ändrades");
        continue;
      }

      Console.WriteLine("Skriv den nya texten:");
      string? newTextInput = Console.ReadLine();

      if (newTextInput == null)
      {
        return;
      }

      string newText = newTextInput.Trim();

      // UC13: samma regler som i UC11, annars förstörs raden i csv-filen
      if (newText == "" || newText.Contains(','))
      {
        Console.WriteLine("Texten får inte vara tom eller innehålla kommatecken");
        continue;
      }

      if (chosenNumber == 0)
      {
        // UC13: finns det nya ordet redan som ett annat ord skulle två ord slås ihop och kunna ge dubbletter
        // samma ord med andra bokstavsstorlekar (Stor => stor) är tillåtet, eftersom det är samma nyckel
        if (translations.ContainsKey(newText) && !string.Equals(newText, existingWord, StringComparison.OrdinalIgnoreCase))
        {
          Console.WriteLine($"Ordet {newText} finns redan. Lägg till synonymer med :lägg i stället");
          continue;
        }

        // UC13: byter ordet på alla rader med det gamla ordet, så att alla synonymer följer med (stro => stor)
        List<Word> renamedWords = [];
        foreach (Word oldWord in wordTranslations)
        {
          Word renamedWord = new Word(newText, oldWord.WordOut, oldWord.LanguageIn, oldWord.LanguageOut);
          ReplaceWord(words, oldWord, renamedWord);
          renamedWords.Add(renamedWord);
        }

        // UC13: nyckeln i dictionaryt är ordet, så den gamla nyckeln tas bort och den nya läggs till
        translations.Remove(existingWord);
        translations[newText] = renamedWords;

        Console.WriteLine($"Ändrade {existingWord} till {newText}");
      }
      else
      {
        Word oldWord = wordTranslations[chosenNumber - 1]; // listan börjar på 0, men numren börjar på 1

        // UC13: samma kontroll av dubbletter som i UC11
        bool alreadyExists = wordTranslations.Any(word => string.Equals(word.WordOut, newText, StringComparison.OrdinalIgnoreCase) && word != oldWord);
        if (alreadyExists)
        {
          Console.WriteLine($"{existingWord} → {newText} finns redan i ordlistan");
          continue;
        }

        // UC13: Word går inte att ändra (get-only properties), så ett nytt objekt ersätter det gamla
        Word changedWord = new Word(oldWord.WordIn, newText, oldWord.LanguageIn, oldWord.LanguageOut);
        ReplaceWord(words, oldWord, changedWord);

        // UC13: byter också ut objektet i dictionaryt, på samma plats i listan
        wordTranslations[chosenNumber - 1] = changedWord;

        Console.WriteLine($"Ändrade {existingWord} → {oldWord.WordOut} till {existingWord} → {newText}");
      }

      // UC13: samma spara-funktion som i UC11, så att fil och minne är synkroniserade
      SaveWordList(filePathForLanguagePair[chosenLanguagePair], words);
      continue;
    }

    // UC18: visar tidigare förhörsresultat från results.csv
    if (wordToTranslate == ":historik")
    {
      PrintQuizHistory();
      continue;
    }

    // UC14: startar ett glosförhör med slumpade ord från det valda språkparet
    if (wordToTranslate == ":förhör")
    {
      // UC14: utan ord finns inget att förhöra, t.ex. i ett nytt språkpar (UC16)
      if (translations.Count == 0)
      {
        Console.WriteLine("Det finns inga ord att förhöras på. Lägg till ord med :lägg först");
        continue;
      }

      // UC14: frågar efter antal tills användaren har skrivit ett giltigt tal
      int numberOfWords = 0;
      while (true)
      {
        Console.WriteLine($"Hur många ord vill du förhöras på? (det finns {translations.Count} ord)");
        string? numberInput = Console.ReadLine();

        // UC14: inmatningen har tagit slut (t.ex. Ctrl+D), samma lösning som i UC4
        if (numberInput == null)
        {
          return;
        }

        if (int.TryParse(numberInput, out numberOfWords) && numberOfWords >= 1)
        {
          break;
        }

        Console.WriteLine("Ogiltigt antal, skriv ett tal som är 1 eller större");
      }

      // UC14: finns det färre ord än användaren bad om förhörs alla ord
      if (numberOfWords > translations.Count)
      {
        Console.WriteLine($"Det finns bara {translations.Count} ord, så du förhörs på alla");
      }

      // UC14: blandar orden genom att sortera på ett slumptal, och Take plockar de första
      // eftersom varje nyckel bara finns en gång i dictionaryt frågas inget ord två gånger
      List<string> quizWords = translations.Keys
        .OrderBy(key => Random.Shared.Next())
        .Take(numberOfWords)
        .ToList();

      // UC14 och UC18: förhöret sparas i historiken, eftersom det är det riktiga förhöret
      List<string>? wrongWords = RunQuiz(translations, quizWords, chosenLanguagePair, saveResult: true);

      // UC15: erbjuder extra omgångar så länge det finns ord som blev fel
      // wrongWords är null om förhöret avbröts, och då erbjuds ingen extra omgång
      while (wrongWords != null && wrongWords.Count > 0)
      {
        Console.WriteLine($"Vill du träna på de {wrongWords.Count} ord du svarade fel på? (j/n)");
        string? trainAgain = Console.ReadLine();

        if (trainAgain == null)
        {
          return;
        }

        if (trainAgain.Trim().ToLower() != "j")
        {
          break;
        }

        // UC15: samma funktion som förhöret, men bara med de felaktiga orden i ny slumpad ordning
        // extra omgångar sparas inte i historiken (UC18), eftersom de skulle ge en missvisande bild
        List<string> wordsToPractice = wrongWords
          .OrderBy(word => Random.Shared.Next())
          .ToList();
        wrongWords = RunQuiz(translations, wordsToPractice, chosenLanguagePair, saveResult: false);
      }

      continue; // UC14: tillbaka till huvudloopen när förhöret är klart
    }

    // om ordet finns som nyckel i dictionaryt
    if (translations.ContainsKey(wordToTranslate!))
    {
      // loopa ut synonymer
      foreach (Word word in translations[wordToTranslate!])
      {
        Console.WriteLine(word.WordOut);
      }
    }
    else
    {
      Console.WriteLine($"Ordet finns inte i ordlistan för {chosenLanguagePair}"); // UC4: visar vilket par som söktes i
    }

  }
}


// UC9 och UC10: skriver ut orden i svensk bokstavsordning, med synonymerna på samma rad (stor → big, large)
// ligger i en egen funktion eftersom både :lista och sökningen med * använder den
void PrintWordList(Dictionary<string, List<Word>> translations, List<string> wordsToPrint)
{
  // UC12: om alla ord har raderats finns inget att visa, och Max nedanför skulle krascha på en tom lista
  if (wordsToPrint.Count == 0)
  {
    Console.WriteLine("Ordlistan är tom");
    return;
  }

  // det längsta ordet bestämmer hur bred första kolumnen blir, så att pilarna hamnar i en rak kolumn
  int longestWordLength = wordsToPrint.Max(word => word.Length);

  // svensk sortering, så att å, ä och ö hamnar sist i stället för bland a och o
  StringComparer swedishOrder = StringComparer.Create(new CultureInfo("sv-SE"), ignoreCase: true);

  foreach (string word in wordsToPrint.Order(swedishOrder))
  {
    // slår ihop synonymerna till en text (big, large)
    string translationsText = string.Join(", ", translations[word].Select(translation => translation.WordOut));

    // PadRight fyller ut ordet med mellanslag, så att alla rader blir lika breda före pilen
    Console.WriteLine($"{word.PadRight(longestWordLength)} → {translationsText}");
  }
}


// UC14: förhör användaren på orden i quizWords och visar antal rätt
// UC15: returnerar orden som blev fel, så att de kan tränas på igen, eller null om förhöret avbröts
// UC18: saveResult bestämmer om resultatet sparas i historiken (bara det första förhöret, inte extra omgångarna)
List<string>? RunQuiz(Dictionary<string, List<Word>> translations, List<string> quizWords, string languagePair, bool saveResult)
{
  List<string> wrongWords = [];
  int correctAnswers = 0;

  Console.WriteLine("Förhöret börjar! Skriv :avbryt för att avsluta.");

  for (int i = 0; i < quizWords.Count; i++)
  {
    string quizWord = quizWords[i];
    Console.WriteLine($"{i + 1}/{quizWords.Count}: Vad betyder \"{quizWord}\"?");
    string? answer = Console.ReadLine();

    // användaren vill sluta, eller inmatningen har tagit slut (t.ex. Ctrl+D)
    if (answer == null || answer == ":avbryt")
    {
      Console.WriteLine("Förhöret avbröts");
      return null; // UC15 och UC18: null betyder avbrutet, så ingen extra omgång erbjuds och inget sparas
    }

    // Trim tar bort mellanslag före och efter, så att " big " räknas som rätt
    string cleanAnswer = answer.Trim();

    // Any ger true om svaret matchar någon av synonymerna (stor => big eller large)
    bool isCorrect = translations[quizWord]
      .Any(word => string.Equals(word.WordOut, cleanAnswer, StringComparison.OrdinalIgnoreCase));

    if (isCorrect)
    {
      correctAnswers++;
      Console.WriteLine("Rätt!");
    }
    else
    {
      wrongWords.Add(quizWord);
      string correctAnswersText = string.Join(", ", translations[quizWord].Select(word => word.WordOut));
      Console.WriteLine($"Fel, rätt svar är: {correctAnswersText}");
    }
  }

  Console.WriteLine($"Du fick {correctAnswers} av {quizWords.Count} rätt");

  // UC15: alla rätt betyder att det inte finns något att träna på igen
  if (wrongWords.Count == 0)
  {
    Console.WriteLine("Alla rätt!");
  }

  // UC18: sparar resultatet sist i historikfilen
  if (saveResult)
  {
    SaveQuizResult(languagePair, correctAnswers, quizWords.Count);
  }

  return wrongWords;
}


// UC11: skriver om en ordlista med orden som hör till den, en rad per ordpar (stor,big)
// återanvänds när ord raderas (UC12) och redigeras (UC13)
void SaveWordList(string filePath, List<Word> words)
{
  // språken i filnamnet säger vilket håll raderna ska skrivas åt (swedish-english => stor,big och inte big,stor)
  string[] languages = Path.GetFileNameWithoutExtension(filePath).Split("-");
  string languageIn = languages[0];
  string languageOut = languages[1];

  // tar bara med orden åt filens håll, eftersom words innehåller varje ord åt båda hållen (UC6)
  List<string> lines = words
    .Where(word => word.LanguageIn == languageIn && word.LanguageOut == languageOut)
    .Select(word => $"{word.WordIn},{word.WordOut}")
    .ToList();

  // UC17: sparar en kopia av filen innan den skrivs över, så att orden går att få tillbaka om något blir fel
  // en tom fil (t.ex. ett nytt språkpar, UC16) har inget att säkra, så då görs ingen kopia
  if (File.Exists(filePath) && new FileInfo(filePath).Length > 0)
  {
    // UC17: skapar mappen om den saknas, och gör ingenting om den redan finns
    Directory.CreateDirectory("./backups");

    // UC17: tidsstämpeln gör varje kopia unik, och fff (millisekunder) gör att två ändringar samma sekund inte krockar
    // bindestreck i stället för kolon, eftersom kolon inte är tillåtet i filnamn på Windows
    string timestamp = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss-fff");
    string backupFileName = $"{Path.GetFileNameWithoutExtension(filePath)}_{timestamp}.csv";
    File.Copy(filePath, Path.Combine("./backups", backupFileName));
  }

  File.WriteAllLines(filePath, lines);
}


// UC13: byter ut ett ord mot ett nytt, åt båda hållen, på samma plats i listan
// samma plats gör att raden hamnar på samma ställe i filen när den sparas
void ReplaceWord(List<Word> words, Word oldWord, Word newWord)
{
  int index = words.IndexOf(oldWord);
  words[index] = newWord;

  // ordet finns också åt andra hållet, eftersom det lades till åt båda hållen vid inläsningen (UC6)
  int reversedIndex = words.FindIndex(word =>
    word.LanguageIn == oldWord.LanguageOut &&
    word.LanguageOut == oldWord.LanguageIn &&
    word.WordIn == oldWord.WordOut &&
    word.WordOut == oldWord.WordIn);

  if (reversedIndex >= 0)
  {
    words[reversedIndex] = new Word(newWord.WordOut, newWord.WordIn, newWord.LanguageOut, newWord.LanguageIn);
  }
}


// UC18: lägger till ett förhörsresultat sist i results.csv (datum,språkpar,antal rätt,antal frågor)
void SaveQuizResult(string languagePair, int correctAnswers, int numberOfQuestions)
{
  string date = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
  string line = $"{date},{languagePair},{correctAnswers},{numberOfQuestions}";

  // AppendAllText lägger till raden sist utan att skriva om resten, och skapar filen första gången
  File.AppendAllText("./results.csv", line + Environment.NewLine);
}


// UC18: visar alla sparade förhörsresultat i en tabell, de senaste först
void PrintQuizHistory()
{
  if (!File.Exists("./results.csv"))
  {
    Console.WriteLine("Inga sparade resultat än");
    return;
  }

  // läser in raderna och hoppar över felaktiga rader, så att en trasig rad inte kraschar programmet
  List<string[]> results = [];
  foreach (string line in File.ReadAllLines("./results.csv"))
  {
    string[] parts = line.Split(",");
    if (parts.Length == 4 && int.TryParse(parts[2], out _) && int.TryParse(parts[3], out _))
    {
      results.Add(parts);
    }
  }

  if (results.Count == 0)
  {
    Console.WriteLine("Inga sparade resultat än");
    return;
  }

  // nya resultat läggs sist i filen, så Reverse gör att de senaste visas först
  results.Reverse();

  // det längsta språkparet bestämmer hur bred kolumnen blir, så att resultaten hamnar i en rak kolumn
  int longestLanguagePairLength = results.Max(result => result[1].Length);

  Console.WriteLine($"{"Datum".PadRight(18)}{"Språkpar".PadRight(longestLanguagePairLength + 2)}Resultat");
  foreach (string[] result in results)
  {
    Console.WriteLine($"{result[0].PadRight(18)}{result[1].PadRight(longestLanguagePairLength + 2)}{result[2]}/{result[3]}");
  }
}
