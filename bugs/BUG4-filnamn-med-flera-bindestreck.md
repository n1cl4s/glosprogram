# BUG4: Filnamn med flera bindestreck läses in utan varning

## Steg för att återskapa
1. Gör en kopia av en ordlista med ett extra bindestreck i namnet, till exempel `swedish-english-gammal.csv`.
2. Starta programmet med `dotnet run` och översätt `hund`.

## Förväntat
Filen hoppas över med en varning, eftersom namnet inte följer mönstret `språk-språk.csv`.

## Faktiskt
Filen läses in som `swedish → english`, och orden kommer med två gånger:
```
dog
dog
```

## Orsak
Koden använder bara `languages[0]` och `languages[1]` och bryr sig inte om att det finns fler delar. En säkerhetskopia av en ordlista ger därför dubbletter utan att man märker det.

## Var i koden
`Program.cs`, där språken plockas ut ur filnamnet.

## Åtgärdas i
UC7 (filer som inte följer mönstret `språk-språk.csv` ska hoppas över med en varning)
