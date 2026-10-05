# BUG2: Krasch vid tom rad eller rad utan kommatecken

## Steg för att återskapa
1. Lägg till en tom rad i slutet av en ordlista, till exempel `wordlists/swedish-english.csv`.
2. Starta programmet med `dotnet run`.

Samma sak händer med en rad som saknar kommatecken, till exempel `hund`.

## Förväntat
Raden hoppas över och resten av ordlistan läses in.

## Faktiskt
```
Unhandled exception. System.IndexOutOfRangeException: Index was outside the bounds of the array.
   at Program.<Main>$(String[] args) in .../Program.cs:line 23
```

## Orsak
`line.Split(",")` ger bara ett element när raden saknar kommatecken (`""` => `[""]`), så `wordPair[1]` finns inte.

## Var i koden
`Program.cs`, i inläsningsloopen där `new Word(...)` skapas.

## Åtgärdas i
UC7
