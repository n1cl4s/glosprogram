# Glosprogram

Ett konsolprogram som översätter glosor mellan olika språk, åt båda hållen. Just nu finns ordlistor för svenska–engelska, svenska–spanska och spanska–italienska.

## Starta programmet

Programmet kräver .NET 10. Starta det från projektmappen:

```
dotnet run
```

Kör det från projektmappen, eftersom programmet letar efter ordlistorna i `./wordlists/`.

När programmet startar skriver det ut hur många ord som har lästs in och från hur många filer, till exempel `594 ord inlästa från 3 filer`. Varje rad i en ordlista räknas två gånger, en gång för varje håll. Sedan visas en numrerad lista över språkparen som finns.

## Använda programmet

1. Välj språkpar genom att skriva numret från listan och trycka Enter. Skriver du något annat än ett nummer i listan visas `Ogiltigt val` och du får välja igen.
2. Programmet skriver vilket språkpar du har valt, till exempel `Du översätter nu swedish → spanish`, och frågar efter ett ord.
3. Skriv ett ord på det första språket i paret och tryck Enter. Har du valt `english → swedish` skriver du alltså ett engelskt ord.
4. Programmet skriver ut översättningarna i det valda språkparet, en per rad. Om ordet har flera översättningar (synonymer) visas alla.
5. Om ordet saknas visas till exempel `Ordet finns inte i ordlistan för swedish → spanish`.
6. Programmet frågar sedan efter nästa ord.

Det spelar ingen roll om du skriver med stora eller små bokstäver. `hus`, `Hus` och `HUS` ger samma svar.

### Kommandon

I stället för ett ord kan du skriva ett kommando. Skriv kommandona med små bokstäver.

| Kommando | Vad det gör |
|----------|-------------|
| `st*` | Visar alla ord som börjar på `st` |
| `:lista` | Visar hela ordlistan |
| `:lägg` | Lägger till en översättning |
| `:radera` | Raderar en översättning |
| `:ändra` | Redigerar ett ord eller en översättning |
| `:förhör` | Startar ett glosförhör |
| `:historik` | Visar tidigare förhörsresultat |
| `:byt` | Byter språkpar |

När programmet frågar efter språkpar kan du skriva `:nytt` för att skapa ett nytt språkpar.

### Visa hela ordlistan

Skriv `:lista` i stället för ett ord. Då visas alla ord i det valda språkparet i bokstavsordning, med synonymerna på samma rad. Å, ä och ö hamnar sist, som i svenska alfabetet. Efter listan kan du fortsätta översätta som vanligt.

Exempel för `swedish → english`:

```
arg     → angry, mad
barn    → child, kid
berg    → mountain
bil     → car, automobile
...
```

### Sök på början av ett ord

Skriv början av ett ord följt av `*`, till exempel `st*`. Då visas alla ord i det valda språkparet som börjar så, med sina översättningar. Stora och små bokstäver spelar ingen roll. Om inget ord matchar visas till exempel `Inga ord börjar på "xq" i swedish → english`.

Exempel för `swedish → english`:

```
st*
stad    → city, town
stjärna → star
stol    → chair
stor    → big, large, huge, voluminous
```

### Glosförhör

Skriv `:förhör` för att starta ett förhör i det valda språkparet. Programmet frågar hur många ord du vill ha, och slumpar sedan så många olika ord. Skriv översättningen och tryck Enter. Alla synonymer räknas som rätt, och stora och små bokstäver spelar ingen roll. Vid fel svar visas de rätta svaren. När förhöret är klart visas hur många rätt du fick. Skriv `:avbryt` för att sluta i förtid.

Om du svarade fel på något ord får du frågan om du vill träna på de orden igen. Svarar du `j` körs en extra omgång med bara de felaktiga orden, i ny ordning. Det upprepas tills allt är rätt eller du svarar `n`.

Resultatet av varje avslutat förhör sparas i `results.csv`, med datum, språkpar och poäng. Avbrutna förhör och extra omgångar sparas inte.

Exempel för `swedish → english`:

```
:förhör
Hur många ord vill du förhöras på? (det finns 59 ord)
2
Förhöret börjar! Skriv :avbryt för att avsluta.
1/2: Vad betyder "stor"?
large
Rätt!
2/2: Vad betyder "titta"?
see
Fel, rätt svar är: look, watch
Du fick 1 av 2 rätt
```

### Lägga till en översättning

Skriv `:lägg` för att lägga till ett nytt ordpar i det valda språkparet. Programmet frågar efter ordet och översättningen. Ordparet går att översätta direkt, åt båda hållen, och sparas i ordlistan så att det finns kvar nästa gång programmet startar. Finns ordet redan blir den nya översättningen en synonym.

Du kan lägga till från vilket håll som helst. Lägger du till `massive → stor` i `english → swedish` sparas det som `stor,massive` i `swedish-english.csv`.

Ett ordpar som redan finns läggs inte till igen. Ord som är tomma eller innehåller kommatecken nekas, eftersom de skulle förstöra csv-filen.

Exempel för `swedish → english`:

```
:lägg
Skriv ordet på swedish:
stor
Skriv översättningen på english:
enormous
Lade till stor → enormous
```

### Radera en översättning

Skriv `:radera` för att ta bort en översättning. Programmet frågar efter ordet och visar dess översättningar som en numrerad lista. Välj numret och bekräfta med `j`. Bara den valda översättningen tas bort, synonymerna finns kvar. Översättningen försvinner åt båda hållen och tas bort ur ordlistan på disk. Om ordet bara hade en översättning försvinner hela ordet.

Exempel för `swedish → english`:

```
:radera
Vilket ord vill du radera en översättning för?
stor
stor:
1. big
2. large
3. huge
4. voluminous
Vilken översättning vill du radera? Skriv numret
1
Vill du radera stor → big? (j/n)
j
Raderade stor → big
```

### Redigera en översättning

Skriv `:ändra` för att rätta ett felstavat ord eller en felaktig översättning. Programmet frågar efter ordet och visar dess översättningar numrerade. Skriv `0` för att ändra själva ordet, eller numret på översättningen du vill ändra, och skriv sedan den nya texten. Ändrar du själva ordet följer alla synonymer med. Ändringen syns direkt, åt båda hållen, och sparas i ordlistan.

Exempel för `swedish → english`:

```
:ändra
Vilket ord vill du ändra?
stor
stor:
1. bigg
2. large
Skriv 0 för att ändra själva ordet (stor), eller numret på översättningen du vill ändra
1
Skriv den nya texten:
big
Ändrade stor → bigg till stor → big
```

### Visa tidigare förhörsresultat

Skriv `:historik` för att se alla sparade förhörsresultat, de senaste först:

```
:historik
Datum             Språkpar           Resultat
2026-10-05 14:32  swedish → english  7/10
2026-10-04 19:05  spanish → italian  9/10
```

### Säkerhetskopior

Innan en ordlista ändras med `:lägg`, `:radera` eller `:ändra` sparas en kopia i mappen `backups/`, med datum och tid i namnet, till exempel `swedish-english_2026-10-05_14-32-10-123.csv`. Mappen skapas automatiskt. Vill du ångra en ändring kan du kopiera tillbaka en fil därifrån till `wordlists/` och ta bort tidsstämpeln ur namnet.

### Byta språkpar

Skriv `:byt` i stället för ett ord. Då visas listan med språkpar igen och du kan välja ett nytt, utan att starta om programmet. Skriv kommandot med små bokstäver, `:BYT` fungerar inte.

Exempel:

```
Glosprogram
594 ord inlästa från 3 filer
Språkpar:
1. english → swedish
2. italian → spanish
3. spanish → italian
4. spanish → swedish
5. swedish → english
6. swedish → spanish
Välj språkpar genom att skriva dess nummer (eller :nytt för att skapa ett nytt språkpar)
6
Du översätter nu swedish → spanish
Ange vilket ord du vill översätta, eller ett kommando:
  st* söker, :lista visar alla ord, :lägg lägger till, :radera raderar, :ändra redigerar, :förhör startar ett förhör, :historik visar resultat, :byt byter språkpar
stor
grande
enorme
voluminoso
Ange vilket ord du vill översätta, eller ett kommando:
  st* söker, :lista visar alla ord, :lägg lägger till, :radera raderar, :ändra redigerar, :förhör startar ett förhör, :historik visar resultat, :byt byter språkpar
:byt
Språkpar:
1. english → swedish
2. italian → spanish
3. spanish → italian
4. spanish → swedish
5. swedish → english
6. swedish → spanish
Välj språkpar genom att skriva dess nummer (eller :nytt för att skapa ett nytt språkpar)
1
Du översätter nu english → swedish
Ange vilket ord du vill översätta, eller ett kommando:
  st* söker, :lista visar alla ord, :lägg lägger till, :radera raderar, :ändra redigerar, :förhör startar ett förhör, :historik visar resultat, :byt byter språkpar
big
stor
```

Språkparen visas i bokstavsordning, så samma nummer betyder alltid samma språkpar.

## Avsluta programmet

Programmet har inget kommando för att avsluta. Tryck **Ctrl+C** för att stänga det.

## Ordlistorna

Programmet läser in alla `.csv`-filer i mappen `wordlists/`. Just nu finns:

- `swedish-english.csv` – svenska och engelska
- `swedish-spanish.csv` – svenska och spanska
- `spanish-italian.csv` – spanska och italienska

Varje fil ger två språkpar, ett för varje håll. `swedish-english.csv` ger både `swedish → english` och `english → swedish`.

Varje rad är ett ordpar, med ordet först och översättningen efter ett kommatecken:

```
hus,house
hus,building
stor,big
```

Om ett ord har flera översättningar skriver du en rad för varje översättning, med samma ord först.

Språken hämtas från filnamnet: `swedish-english.csv` betyder från svenska till engelska. Filnamnet ska ha formen `källspråk-målspråk.csv`, med språknamnen på engelska och med gemener.

### Lägga till en ny ordlista

Enklast är att skriva `:nytt` när programmet frågar efter språkpar. Programmet frågar efter de två språken, skapar en tom fil i `wordlists/` och tar dig direkt till det nya språkparet, där du kan lägga till ord med `:lägg`. Språknamnen sparas med gemener och får bara innehålla bokstäver.

```
Välj språkpar genom att skriva dess nummer (eller :nytt för att skapa ett nytt språkpar)
:nytt
Skriv källspråket på engelska (t.ex. swedish):
german
Skriv målspråket på engelska (t.ex. german):
swedish
Skapade ./wordlists/german-swedish.csv. Lägg till ord med :lägg
Du översätter nu german → swedish
```

Du kan också lägga en ny fil i `wordlists/` för hand, till exempel `swedish-german.csv`, och starta om programmet. Det nya språkparet visas då i listan. Koden behöver inte ändras.

Det räcker med en fil per språk, eftersom programmet översätter åt båda hållen. Lägg inte till en `english-swedish.csv` om det redan finns en `swedish-english.csv`, för då visas varje översättning två gånger.

## Begränsningar

- Om det finns filer för samma språk åt båda hållen, till exempel både `swedish-english.csv` och `english-swedish.csv`, visas varje översättning två gånger.
- Varje rad i ordlistan måste innehålla ett kommatecken. Tomma rader eller rader utan kommatecken får programmet att krascha vid start.
- Filnamnet måste innehålla ett bindestreck, annars kraschar programmet vid start. Ett filnamn med flera bindestreck, till exempel `swedish-english-gammal.csv`, läses in utan varning och ger dubbletter. Lägg därför inte säkerhetskopior av ordlistor i `wordlists/`.
- Säkerhetskopiorna i `backups/` tas aldrig bort automatiskt, så mappen växer för varje ändring.
- Om man trycker **Ctrl+D** när programmet frågar efter ett ord kraschar det i stället för att avslutas. När programmet frågar efter språkpar avslutas det som det ska.

Mer om kända fel finns i [bugs/](bugs/README.md).
