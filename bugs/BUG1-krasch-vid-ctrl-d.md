# BUG1: Krasch när inmatningen tar slut (Ctrl+D)

## Steg för att återskapa
1. Starta programmet med `dotnet run`.
2. Välj ett språkpar, till exempel `1`.
3. Tryck Ctrl+D (macOS/Linux) eller Ctrl+Z och Enter (Windows) när programmet frågar efter ett ord.

När programmet frågar efter språkpar avslutas det däremot utan krasch om man trycker Ctrl+D (sedan UC4).

## Förväntat
Programmet avslutas lugnt.

## Faktiskt
```
Unhandled exception. System.ArgumentNullException: Value cannot be null. (Parameter 'key')
   at System.Collections.Generic.Dictionary`2.FindValue(TKey key)
```

## Orsak
`Console.ReadLine()` returnerar `null` när det inte finns mer att läsa. `!` i `ContainsKey(wordToTranslate!)` tystar bara kompilatorns varning, och `null` skickas ändå vidare till dictionaryn.

## Var i koden
`Program.cs`, i den inre loopen där orden översätts.

## Åtgärdas i
UC7
