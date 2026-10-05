# UC18: Spara förhörsresultat

**Som** användare **vill jag** kunna se mina tidigare förhörsresultat med datum, språkpar och poäng, **så att** jag kan följa hur jag blir bättre.

## Vad som ska göras
När ett förhör i UC14 är klart läggs en rad till i en historikfil, till exempel `results.csv`, med datum, språkpar, antal rätt och antal frågor:

```
2026-10-05 14:32,swedish → english,7,10
```

`File.AppendAllText` lägger till raden sist utan att läsa in eller skriva om resten av filen. Filen skapas automatiskt första gången.

Ett nytt kommando, till exempel `:historik`, läser in filen och visar resultaten i en tabell, de senaste först:

```
Datum             Språkpar             Resultat
2026-10-05 14:32  swedish → english    7/10
2026-10-04 19:05  spanish → italian    9/10
```

Lägg `results.csv` i `.gitignore`.

## Var i koden
`Program.cs`, i slutet av förhörsfunktionen (UC14) och som ett nytt kommando i den inre huvudloopen.

## Tänkbara tekniker
`DateTime.Now.ToString("yyyy-MM-dd HH:mm")`, `File.AppendAllText` med `Environment.NewLine`, `File.Exists`, `File.ReadAllLines`, `Split(",")`, `Reverse` eller `OrderByDescending`, `PadRight`.

## Klart när
- [x] Varje avslutat förhör sparas med datum, språkpar och poäng.
- [x] Avbrutna förhör (`:avbryt`) sparas inte.
- [x] Extra omgångar från UC15 sparas inte som egna resultat (eller markeras tydligt), så att historiken inte blir missvisande.
- [x] `:historik` visar alla resultat i en tydlig tabell, senaste först.
- [x] Om historikfilen saknas visas `Inga sparade resultat än`, ingen krasch.
- [x] Felaktiga rader i historikfilen hoppas över.
- [x] Resultaten finns kvar efter omstart.
- [x] `results.csv` finns i `.gitignore`.

## Beroenden
UC14
