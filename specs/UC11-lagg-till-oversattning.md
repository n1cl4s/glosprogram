# UC11: Lägg till en översättning

**Som** användare **vill jag** kunna lägga till en ny översättning, till exempel `stor → big`, direkt i programmet, **så att** jag inte behöver öppna csv-filen och redigera den för hand.

## Vad som ska göras
Ett kommando, till exempel `:lägg`, frågar efter ordet och översättningen. Det nya ordparet ska hamna på tre ställen så att allt hänger ihop:

1. **I listan `words`**, åt båda hållen (som i UC6).
2. **I dictionaryt `translations`**, så att ordet kan slås upp direkt. Enklast är att bygga om dictionaryt efter ändringen.
3. **I rätt csv-fil på disk.** Filen skrivs om med `File.WriteAllLines`.

Det knepiga är att rätt fil och rätt ordning måste användas. Om användaren har valt `english → swedish` och lägger till `big → stor` finns det ingen `english-swedish.csv`. Raden ska då skrivas som `stor,big` i `swedish-english.csv`. Programmet måste alltså komma ihåg vilken fil varje språkpar kommer från och om paret är "vänt" (UC6).

Tips: gör en egen funktion, till exempel `SaveWordList(...)`, som skriver om en fil utifrån orden i `words`. Den återanvänds i UC12, UC13 och UC14.

## Var i koden
`Program.cs`, i den inre huvudloopen. Spara-funktionen kan ligga längst ned i `Program.cs` som en lokal funktion.

## Tänkbara tekniker
`List.Add`, `File.WriteAllLines`, `Where` + `Select` för att bygga raderna `ord,översättning`.

## Klart när
- [x] `:lägg` frågar efter ord och översättning och lägger till paret.
- [x] Det nya ordet går att översätta direkt, utan omstart.
- [x] Ordet går att översätta åt andra hållet också (`big → stor`).
- [x] Ordparet finns kvar efter omstart, alltså är det sparat i filen.
- [x] När paret läggs till från det "vända" hållet (`english → swedish`) sparas det i rätt ordning i `swedish-english.csv`.
- [x] Om exakt samma ordpar redan finns läggs det inte till igen, och användaren får veta det.
- [x] Synonymer fungerar: att lägga till `stor → huge` behåller `big` och `large`.
- [x] Tomma ord och ord med kommatecken nekas, eftersom de skulle förstöra csv-filen.
- [ ] Det går inte att lägga till i ett extrapolerat språkpar (UC8), eftersom det inte har någon egen fil. Användaren får ett tydligt meddelande. Koden kontrollerar detta redan, men det kan testas först när UC8 är byggd.

## Beroenden
UC4, UC6
