# Prompthistorik

Prompterna som användes med Claude Code (Claude Opus 5.5) i den här sessionen, i den ordning de skrevs, med en kort sammanfattning av vad som gjordes.

## 1. Spansk ordlista

> kan du skapa en version av wordlist som är mellan svenska och spanska (med samma ord)

Skapade `wordlists/swedish-spanish.csv` med samma svenska ord som `swedish-english.csv`, och spanska synonymer på egna rader.

## 2. UC2

> Kan du bygga UC2?

Programmet läser nu alla `.csv`-filer i `wordlists/` med `Directory.GetFiles` och skriver ut hur många ord som lästs in från hur många filer.

## 3. Buggrapporter

> Kan du lägga buggrapporter i bugs?

Skapade `bugs/` med en översikt och sju buggrapporter (BUG1–BUG7). Varje rapport innehåller stegen för att återskapa buggen och vilken use case som löser den.

## 4. Krasch vid start

> Nu när jag körde koden fick jag en crash direkt:
>
> ```
> Unhandled exception. System.IndexOutOfRangeException: Index was outside the bounds of the array.
>    at Program.<Main>$(String[] args) in /Users/b/Desktop/Projects/MAI26MA/glosprogram_malmo/Program.cs:line 23
> ```

Kraschen kom troligen från en tillfällig testfil med en tom rad (BUG2), som låg i `wordlists/` medan buggarna återskapades. Testfilen var redan borttagen, och programmet fungerade när det kördes igen.

## 5. README

> Har du uppdaterat manualen i README för att spegla förändringarna?

Det var inte gjort. README uppdaterades med flera ordlistor, ny exempelutskrift, hur man lägger till en ordlista och nya begränsningar.

## 6. Paritet mellan specs, README och programmet

> Utifrån en jämförelse mellan specs, README och programmet, är det något du inte har paritet i?

Gick igenom skillnaderna, bland annat fel siffra i en kommentar, rutor som inte var ibockade i UC1 och UC2, att BUG6 och BUG7 inte hörde till någon use case, och oprecisa formuleringar i README.

## 7. Åtgärda paritetsskillnaderna

> Kan du åtgärda dessa paritetsdifferenser

Rättade kommentaren, bockade av UC1 och UC2, lade till krav på bestämd ordning i UC3 (BUG7), bytte felmeddelandet till svenska (BUG6), ändrade `var` till `Word` och uppdaterade README och bugs.

## 8. Prompthistorik

> Kan du spara din prompt history i prompt-history.md?

Skapade den här filen.

## 9. Underhåll prompthistoriken

> Från och med nu, underhåll denna kontinuerligt. (Lägg in detta i din system prompt)

Systemprompten går inte att ändra. Regeln lades i stället i `CLAUDE.md` under rubriken *Prompthistorik*, eftersom den filen läses in i början av varje session.

## 10. UC3, UC4 och UC5

> Kan du bygga UC3, 4 och 5?

Byggdes ett steg i taget, och varje steg kördes och testades innan nästa påbörjades. Testerna med extra ordlistor kördes i en kopia av projektet.

- **UC3:** `Word` fick propertyn `LanguagePair` (t.ex. `swedish → english`). Språkparen plockas ut ur orden, sorteras i bokstavsordning och visas som en numrerad lista. Sorteringen åtgärdar BUG7.
- **UC4:** användaren väljer ett nummer, och ett ogiltigt val ger ett felmeddelande. `swedishToEnglish` ersattes av `translations`, som bara innehåller orden i det valda paret. Ctrl+D vid valet avslutar programmet, annars skulle frågan upprepas i all oändlighet.
- **UC5:** valet och översättningsloopen ligger i en yttre loop, och `:byt` går tillbaka till valet av språkpar.

Specs, README och bugs uppdaterades så att de stämmer med koden.

## 11. Kontroll av README

> Är README uppdaterad?

Exemplet i README kördes och utskriften jämfördes rad för rad. Den stämde. Meningen om stora och små bokstäver hade hamnat under *Byta språkpar* och flyttades tillbaka till avsnittet om att översätta ord. README säger nu också att `:byt` måste skrivas med små bokstäver.


## 12. Förslag på UC6

> Om du skulle göra UC6 för att översätta åt båda håll, hur skulle du göra den ändringen?

Ingen kod ändrades. Förslaget är att skapa ett omvänt `Word` för varje rad i inläsningsloopen, så att `english → swedish` kommer med automatiskt i språkparen och i `translations`. En risk togs upp: om det även finns en `english-swedish.csv` blir orden dubbletter.

## 13. UC6 genom att vända nyckeln

> Du skapar då dubbelt så många instanser av Word. Om du skulle vända nyckeln i GroupBy Dictionary istället, vad skulle du mer behöva ändra då?

Ingen kod ändrades. Om nyckeln vänds i stället för att orden dubbleras måste fyra ställen ändras: `Word` behöver ett omvänt språkpar, och språkparen behöver ta med det. Dessutom måste filtret med `Where`, nyckeln i `GroupBy` och utskriften av översättningen alla veta vilket håll som gäller. Svaret jämförde de två lösningarna.

## 14. Bygg UC6

> Okej då, kör dubbla Word

UC6 byggdes med dubbla `Word`. I inläsningsloopen skapas nu ett omvänt `Word` för varje rad, så att `english → swedish` och `spanish → swedish` kommer med automatiskt. Programmet kördes och testades: `big` och `LARGE` ger `stor`, och både `house` och `building` ger `hus`. Riktningen `swedish → spanish` fungerar som innan. Rutorna i UC6-specen bockades av. README uppdaterades med de nya språkparen, de nya numren, antalet ord (394), ett nytt exempel och den nya begränsningen: filer åt båda hållen ger dubbletter.

## 15. Spec för UC8

> Kan du skapa en UC8 som innebär att vi vill kunna extrapolera översättningar?

Ingen kod ändrades. En ny spec, `specs/UC8-extrapolera-oversattningar.md`, skapades. Den beskriver hur programmet ska kunna översätta mellan två språk utan egen ordlista genom att gå via ett gemensamt språk, till exempel `house → hus → casa`. Specen bygger på UC6 och kräver bara ett mellanled. UC8 lades till i tabellen i `specs/README.md`.

## 16. Spansk-italiensk ordlista

> Kan du generera en spansk-italiensk ordlista också?

`wordlists/spanish-italian.csv` skapades med 100 rader. Den innehåller alla 97 spanska ord från `swedish-spanish.csv`, så att UC8 senare kan översätta till exempel `swedish → italian` via spanska. Några ord har synonymer, till exempel `coche,macchina` och `coche,auto`. Programmet kördes: `594 ord inlästa från 3 filer`, och paren `spanish → italian` och `italian → spanish` visas och fungerar (`perro` ger `cane`, `bello` ger `bonito` och `hermoso`). README uppdaterades med den nya filen, antalet ord och de nya numren på språkparen.

## 17. Tio funktioner som use cases

> Tjena. Kolla här och på funktion finns där 10 saker som ska in i koden. gör UC under specs och förklara vad som behövs göras. vi tar en i taget

Ingen kod ändrades. Listan med de tio funktionerna hittades inte i projektet, varken i filerna eller i meddelandet. Claude bad användaren klistra in listan eller ange var den finns.

## 18. Länk till uppgiften

> https://mai26ma-ha.lms.nodehill.se/article/ovning-bygg-ut-glosprogrammet-med-ai-stod-malmo
>
> glömde klistra in den här

Ingen kod ändrades. Sidan ligger på kursplattformen Node Hill, som kräver inloggning och laddar innehållet med JavaScript, så texten gick inte att hämta. Claude bad användaren klistra in texten från sidan.

## 19. Use cases för de tio funktionerna

> https://mai26ma-ha.lms.nodehill.se/article/ovning-bygg-ut-glosprogrammet-med-ai-stod-malmo
>
>  där är 10 funktioner som ska in i min kod som jag har öppet i vs code.
>
> gör UC för varje funktion och förklara vad som ska göras

Ingen kod ändrades. Uppgiften hämtades från kursplattformen. Specarna UC9–UC18 skapades, en för var och en av de tio funktionerna i uppgiften: visa hela ordlistan, sök på början av ett ord, lägg till, radera och redigera en översättning, glosförhör, träna på felaktiga svar, skapa ett nytt språkpar, säkerhetskopiera ordlistor och spara förhörsresultat. `specs/README.md` fick de nya raderna i tabellen och ett förslag på ordning.

## 20. Bygg UC9

> ok bra vi börjar med UC9 gör nu detta steg i koden

UC9 byggdes. Kommandot `:lista` i huvudloopen skriver ut alla ord i det valda språkparet i svensk bokstavsordning (`CultureInfo("sv-SE")`), så att å, ä och ö hamnar sist. Synonymerna slås ihop med `string.Join` (`stor → big, large`), och `PadRight` ställer pilarna i en rak kolumn. Frågan efter ett ord nämner nu också `:lista`. Programmet kördes och testades i `swedish → english`, `english → swedish` och `spanish → italian`, och vanlig översättning fungerar efter listan. Rutorna i UC9-specen bockades av och README fick ett avsnitt om `:lista`.

## 21. Bygg UC10

> ok bra fixat. och nästa?

UC10 byggdes. Om inmatningen slutar med `*` tas stjärnan bort och alla ord som börjar med texten visas (`StartsWith` med `StringComparison.OrdinalIgnoreCase`), så `st*` och `ST*` ger `stad`, `stjärna`, `stol` och `stor`. Bara `*` ger en uppmaning att skriva minst en bokstav, och en sökning utan träffar ger ett tydligt meddelande. Utskriften från UC9 flyttades till en lokal funktion, `PrintWordList`, som både `:lista` och sökningen använder. Programmet kördes och testades, även med `ä*` och `Ä*`. Frågan efter ett ord, README och UC10-specen uppdaterades.

## 22. Bygg UC14

> ok kör nästa

UC14 byggdes. Kommandot `:förhör` frågar hur många ord förhöret ska ha, tills användaren skriver ett tal som är 1 eller större. Orden blandas med `OrderBy(key => Random.Shared.Next())` och plockas med `Take`, så inget ord frågas två gånger. Finns det färre ord än användaren bad om förhörs alla. Själva förhöret ligger i en lokal funktion, `RunQuiz`, som returnerar orden som blev fel, så att UC15 kan återanvända den. Alla synonymer räknas som rätt, och svaret jämförs med `Trim` och `StringComparison.OrdinalIgnoreCase`. `:avbryt` avslutar förhöret. Programmet kördes och testades: ogiltigt antal, för stort antal, `BIG` och ` large ` räknas som rätt, fel svar visar rätt svar, ett förhör med alla 59 ord har inga dubbletter, och översättning fungerar efter `:avbryt`. Frågan efter ett ord, README och UC14-specen uppdaterades.

## 23. Bygg UC11

> va nej. nästa punkt var lägg till en översättning

UC11 byggdes i stället för UC15. Ett nytt dictionary, `filePathForLanguagePair`, kommer ihåg vilken fil varje språkpar kommer från, och båda riktningarna pekar på samma fil. Kommandot `:lägg` frågar efter ord och översättning, tar bort mellanslag och nekar tomma ord, kommatecken och ordpar som redan finns. Det nya ordet läggs till i `words` åt båda hållen och i `translations`, så att det går att översätta direkt. En ny lokal funktion, `SaveWordList`, skriver om filen med `File.WriteAllLines` och tar bara med orden åt filens håll, så att `massive → stor` från `english → swedish` sparas som `stor,massive` i `swedish-english.csv`. Funktionen återanvänds i UC12 och UC13. Programmet testades på en kopia av ordlistorna: synonym, nytt ord, vänt håll, dubblett, tomt ord, kommatecken och att orden finns kvar efter omstart. Frågan efter ett ord, README och UC11-specen uppdaterades. Rutan för extrapolerade språkpar (UC8) lämnades obockad, eftersom UC8 inte är byggd än.

## 24. Bygg UC12

> ok bra kör nästa

UC12 byggdes. Kommandot `:radera` frågar efter ett ord, visar dess översättningar som en numrerad lista och låter användaren välja en och bekräfta med `j`. Översättningen tas bort ur `words` åt båda hållen (`Remove` och `RemoveAll`) och ur `translations`. Var det ordets sista översättning tas hela ordet bort ur dictionaryt. Filen skrivs om med `SaveWordList` från UC11. `PrintWordList` visar nu `Ordlistan är tom` i stället för att krascha om alla ord har raderats. Programmet testades på en kopia av ordlistorna: radera en synonym, att synonymerna finns kvar, att översättningen försvinner åt båda hållen, att ändringen finns kvar efter omstart, `n` vid bekräftelsen, ogiltigt nummer, ord som saknas, och att radera sista översättningen från det vända hållet. Frågan efter ett ord, README och UC12-specen uppdaterades.

## 25. Bygg resten på en gång

> detta är ganska tråkigt.  gör bara allt på en gång. lägg till kommentarer som du gjort innan med UC i koden. uppdatera UC och Readme. behåll kodstrukturen.

UC13, UC15, UC16, UC17 och UC18 byggdes i samma steg, med UC-kommentarer i koden och samma struktur som tidigare: kommandon i huvudloopen och hjälpfunktioner längst ned i `Program.cs`.

- UC13: `:ändra` låter användaren välja `0` för att ändra själva ordet (alla synonymer följer med) eller numret på en översättning. En ny funktion, `ReplaceWord`, byter ut ordet åt båda hållen på samma plats i `words`, så att raden hamnar på samma ställe i filen.
- UC15: `RunQuiz` returnerar `null` när förhöret avbryts. Efter förhöret erbjuds extra omgångar med de felaktiga orden så länge det finns fel. `Alla rätt!` visas när allt är rätt.
- UC16: `:nytt` i valet av språkpar skapar en tom fil med `Path.Combine` och `File.WriteAllText`, efter kontroll av tomma namn, andra tecken än bokstäver, samma språk två gånger och om filen redan finns åt något håll. Språkparen byggs nu också från filerna, så att en tom ordlista syns i listan. Förhöret säger till om det inte finns några ord.
- UC17: `SaveWordList` kopierar filen till `backups/` med en tidsstämpel (med millisekunder) innan den skrivs över, men inte om filen är tom.
- UC18: Varje avslutat förhör sparas i `results.csv` med `File.AppendAllText` (inte avbrutna förhör eller extra omgångar). `:historik` visar resultaten i en tabell, senaste först, och hoppar över felaktiga rader.

Frågan efter ett ord delades upp på två rader eftersom kommandona blev många. Programmet testades på kopior av ordlistorna. README fick en kommandotabell och avsnitt för de nya funktionerna, `.gitignore` fick `/backups` och `results.csv`, och rutorna i specarna bockades av.
