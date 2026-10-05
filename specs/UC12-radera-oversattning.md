# UC12: Radera en översättning

**Som** användare **vill jag** kunna ta bort en enskild översättning, till exempel `stor → big`, men behålla `stor → large`, **så att** jag kan rensa bort fel utan att tappa synonymerna.

## Vad som ska göras
Ett kommando, till exempel `:radera`, frågar efter ordet. Programmet visar ordets översättningar som en numrerad lista, och användaren väljer vilken som ska tas bort:

```
stor:
1. big
2. large
Vilken översättning vill du radera?
```

Den valda översättningen tas bort från `words` (åt båda hållen), dictionaryt byggs om och filen skrivs om med samma spara-funktion som i UC11. Om ordet bara har en översättning försvinner ordet helt från ordlistan.

## Var i koden
`Program.cs`, i den inre huvudloopen. Återanvänd spara-funktionen från UC11.

## Tänkbara tekniker
`List.RemoveAll` eller `Remove`, `File.WriteAllLines`, numrerad lista som i UC3/UC4.

## Klart när
- [x] `:radera` visar ordets översättningar som en numrerad lista.
- [x] Bara den valda översättningen tas bort, synonymerna finns kvar.
- [x] Översättningen försvinner åt båda hållen (`big` ger inte längre `stor`).
- [x] Ändringen finns kvar efter omstart.
- [x] Om ordet bara har en översättning tas ordet bort helt och går inte längre att slå upp.
- [x] Ogiltigt nummer eller ett ord som inte finns ger ett tydligt meddelande, ingen krasch.
- [x] Användaren får bekräfta (`j/n`) innan något raderas.
- [x] Det fungerar även när användaren har valt det "vända" språkparet.

## Beroenden
UC11
