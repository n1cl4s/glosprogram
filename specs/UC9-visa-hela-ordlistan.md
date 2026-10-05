# UC9: Visa hela ordlistan

**Som** användare **vill jag** kunna se alla ord och översättningar i det valda språkparet i bokstavsordning, **så att** jag får en överblick över vad som finns att träna på.

## Vad som ska göras
Ett nytt kommando, till exempel `:lista`, skriver ut hela ordlistan för det valda språkparet. Dictionaryt `translations` innehåller redan allt som behövs: nyckeln är ordet och värdet är listan med synonymer. Sortera nycklarna med `OrderBy` och skriv ut varje ord med alla sina översättningar på samma rad.

Exempel för `swedish → english`:
```
hem   → home
hus   → house, building
stor  → big, large
```

## Var i koden
`Program.cs`, i den inre huvudloopen (samma ställe som `:byt` från UC5).

## Tänkbara tekniker
`foreach` över dictionaryt, `OrderBy`, `string.Join(", ", ...)`, formaterad utskrift med `PadRight`.

## Klart när
- [x] Kommandot `:lista` skriver ut alla ord i det valda språkparet.
- [x] Orden visas i bokstavsordning (å, ä, ö hamnar sist för svenska).
- [x] Synonymer visas på samma rad, separerade med kommatecken (`stor → big, large`).
- [x] Översättningarna står i en rak kolumn, så att listan är lätt att läsa.
- [x] Efter listan kan användaren fortsätta översätta som vanligt.
- [x] Texten som frågar efter ett ord nämner det nya kommandot.

## Beroenden
UC4
