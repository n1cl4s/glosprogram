# BUG3: Krasch när filnamnet saknar bindestreck

## Steg för att återskapa
1. Lägg en fil i `wordlists` vars namn saknar bindestreck, till exempel `swedish.csv` med raden `hund,dog`.
2. Starta programmet med `dotnet run`.

## Förväntat
Filen hoppas över med en varning och de andra ordlistorna läses in.

## Faktiskt
```
Unhandled exception. System.IndexOutOfRangeException: Index was outside the bounds of the array.
   at Program.<Main>$(String[] args) in .../Program.cs:line 17
```

## Orsak
`fileName.Split("-")` ger bara ett element (`swedish` => `["swedish"]`), så `languages[1]` finns inte.

## Var i koden
`Program.cs`, där språken plockas ut ur filnamnet.

## Åtgärdas i
UC7
