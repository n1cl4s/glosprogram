# UC14: Glosförhör

**Som** användare **vill jag** få ett förhör med slumpade ord från det valda språkparet, skriva mina svar och se hur många rätt jag fick, **så att** jag kan träna på glosorna och inte bara slå upp dem.

## Vad som ska göras
Ett kommando, till exempel `:förhör`, startar ett förhör. Programmet frågar hur många ord förhöret ska ha och slumpar sedan så många olika ord (nycklar) från `translations`. För varje ord skrivs frågan ut, användaren svarar och programmet säger om det var rätt. Om fel visas rätt svar.

Ett svar räknas som rätt om det matchar **någon** av synonymerna: på `stor` är både `big` och `large` rätt. Jämförelsen ska vara skiftlägesokänslig och ignorera mellanslag före och efter.

Efter sista frågan visas resultatet, till exempel `Du fick 7 av 10 rätt`.

## Var i koden
`Program.cs`, i den inre huvudloopen. Lägg gärna själva förhöret i en egen lokal funktion, eftersom den återanvänds i UC15.

## Tänkbara tekniker
`Random.Shared` eller `OrderBy(x => Random.Shared.Next())`, `Take`, `for`/`foreach`, `Any(...)` för att jämföra mot synonymerna, `Trim`, en räknare för poäng.

## Klart när
- [x] `:förhör` frågar hur många ord och startar ett förhör.
- [x] Orden kommer i slumpad ordning och samma ord frågas inte två gånger i ett förhör.
- [x] Om användaren vill ha fler ord än som finns används alla ord.
- [x] Alla synonymer räknas som rätt svar.
- [x] `Big`, `big` och ` big ` räknas alla som rätt.
- [x] Vid fel svar visas de rätta svaren.
- [x] Antal rätt av totalt visas när förhöret är klart.
- [x] Användaren kan avbryta förhöret, till exempel med `:avbryt`, och kommer då tillbaka till huvudloopen.
- [x] Ogiltigt antal (bokstäver, 0, negativa tal) ger en ny fråga, ingen krasch.

## Beroenden
UC4
