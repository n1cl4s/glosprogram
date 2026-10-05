# BUG5: Krasch när mappen wordlists inte hittas

## Steg för att återskapa
1. Gå till mappen ovanför projektet.
2. Starta programmet med `dotnet run --project glosprogram_malmo`.

## Förväntat
Ett tydligt meddelande om att mappen `wordlists` inte hittas.

## Faktiskt
```
Unhandled exception. System.IO.DirectoryNotFoundException: Could not find a part of the path '.../MAI26MA/wordlists'.
```

## Orsak
`./wordlists` är en relativ sökväg och letas upp från mappen man startar programmet i, inte från projektmappen. Samma krasch händer om mappen har tagits bort.

## Var i koden
`Program.cs`, vid `Directory.GetFiles(...)`.

## Åtgärdas i
UC7
