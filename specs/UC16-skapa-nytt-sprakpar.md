# UC16: Skapa ett nytt språkpar

**Som** användare **vill jag** kunna ange två språk och börja lägga till ord direkt, **så att** jag inte behöver skapa csv-filen manuellt.

## Vad som ska göras
I menyn där språkpar väljs (UC3/UC4) läggs ett val till, till exempel `0. Skapa nytt språkpar` eller kommandot `:nytt`. Programmet frågar efter källspråk och målspråk, bygger filnamnet och skapar en tom fil i `wordlists/`. Därefter ska det nya paret (och det vända paret, UC6) finnas med i listan över språkpar, och användaren kan lägga till ord med `:lägg` från UC11.

Språknamnen ska följa reglerna i `CLAUDE.md`: engelska, gemener, och inget bindestreck i namnet (eftersom bindestrecket skiljer språken åt i filnamnet, se BUG3/BUG4).

Obs: listan `languagePairs` byggs i dag bara från ord som finns. En tom fil har inga ord och ger därför inget språkpar. Det behöver lösas, till exempel genom att bygga språkparen från filnamnen i stället.

## Var i koden
`Program.cs`, i valet av språkpar och i uppbyggnaden av `languagePairs`.

## Tänkbara tekniker
`Path.Combine("./wordlists", $"{languageIn}-{languageOut}.csv")`, `File.Exists`, `File.Create` eller `File.WriteAllText(path, "")`, `ToLower`, `Trim`.

## Klart när
- [x] Det finns ett val i menyn för att skapa ett nytt språkpar.
- [x] Programmet frågar efter två språk och skapar filen `språk-språk.csv` i `wordlists/`.
- [x] Språknamnen sparas med gemener och utan mellanslag före och efter (`German ` → `german`).
- [x] Det nya paret och det vända paret syns i listan direkt, utan omstart.
- [x] Ett nytt, tomt språkpar kan väljas, och `:lägg` fungerar i det.
- [x] Om filen redan finns, även åt andra hållet (`english-swedish` när `swedish-english.csv` finns), skapas ingen ny fil och användaren får veta det.
- [x] Samma språk två gånger, tomma namn och namn med bindestreck eller andra ogiltiga tecken nekas.

## Beroenden
UC3, UC4, UC11
