# UC15: Träna på felaktiga svar igen

**Som** användare **vill jag** kunna köra en extra omgång med bara de glosor jag svarade fel på, **så att** jag övar mest på det jag inte kan.

## Vad som ska göras
Under förhöret i UC14 sparas varje ord man svarar fel på i en separat lista, till exempel `List<string> wrongWords`. När förhöret är klart och listan inte är tom frågar programmet: `Vill du träna på de 3 ord du svarade fel på? (j/n)`.

Svarar användaren `j` körs samma förhörsfunktion igen, men med bara de felaktiga orden. Det går att upprepa tills alla ord är rätt eller användaren säger nej. Det viktiga här är att **återanvända** förhörsfunktionen i stället för att kopiera koden: funktionen tar emot vilka ord som ska förhöras och returnerar vilka som blev fel.

## Var i koden
`Program.cs`, i förhörsfunktionen från UC14 och där den anropas.

## Tänkbara tekniker
En separat lista, metod med parameter och returvärde, `while`-loop för upprepade omgångar.

## Klart när
- [x] Orden man svarar fel på sparas under förhöret.
- [x] Efter förhöret erbjuds en extra omgång om det fanns fel.
- [x] Extra omgången innehåller exakt de ord man svarade fel på, i ny slumpad ordning.
- [x] Efter extra omgången erbjuds en ny omgång med de ord som fortfarande är fel.
- [x] Om allt var rätt visas ett meddelande, till exempel `Alla rätt!`, och ingen extra omgång erbjuds.
- [x] Förhörslogiken finns på ett ställe och återanvänds, ingen kopierad kod.

## Beroenden
UC14
