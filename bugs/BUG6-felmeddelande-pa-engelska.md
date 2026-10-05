# BUG6: Felmeddelandet visas på engelska

## Steg för att återskapa
1. Starta programmet med `dotnet run`.
2. Skriv ett ord som inte finns i någon ordlista, till exempel `xyz`.

## Förväntat
Ett meddelande på svenska, som resten av programmet.

## Faktiskt
```
This word does not exist in this dictionary
```

## Orsak
Texten skrevs på engelska. Enligt kodstandarden ska texter som visas för användaren vara på svenska.

## Var i koden
`Program.cs`, i `else`-grenen i huvudloopen.

## Åtgärdas i
Ingen use case. Rättad direkt: meddelandet blev `Ordet finns inte i någon ordlista`. Sedan UC4 är det `Ordet finns inte i ordlistan för swedish → english` (med det valda språkparet).
