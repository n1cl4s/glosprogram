# Use cases

Use cases för att göra glosprogrammet generiskt, så att språken bestäms av vilka ordlistor som finns i `wordlists/`. Ta dem i ordning, en i taget, och kör och testa varje steg innan nästa påbörjas.

| # | Use case | Beroenden |
|---|----------|-----------|
| 1 | [Läs språken från filnamnet](UC1-sprak-fran-filnamn.md) | – |
| 2 | [Läs in alla ordlistor i mappen](UC2-las-alla-ordlistor.md) | UC1 |
| 3 | [Visa vilka språkpar som finns](UC3-visa-sprakpar.md) | UC2 |
| 4 | [Välj språkpar](UC4-valj-sprakpar.md) | UC3 |
| 5 | [Byt språkpar under körning](UC5-byt-sprakpar.md) | UC4 |
| 6 | [Översätt åt båda hållen](UC6-oversatt-at-bada-hallen.md) | UC4 |
| 7 | [Hantera felaktiga filer och felaktig inmatning](UC7-felhantering.md) | UC2 |
| 8 | [Extrapolera översättningar via ett gemensamt språk](UC8-extrapolera-oversattningar.md) | UC6 |
| 9 | [Visa hela ordlistan](UC9-visa-hela-ordlistan.md) | UC4 |
| 10 | [Sök på början av ett ord](UC10-sok-pa-borjan-av-ord.md) | UC4 |
| 11 | [Lägg till en översättning](UC11-lagg-till-oversattning.md) | UC4, UC6 |
| 12 | [Radera en översättning](UC12-radera-oversattning.md) | UC11 |
| 13 | [Redigera en översättning](UC13-redigera-oversattning.md) | UC11, UC12 |
| 14 | [Glosförhör](UC14-glosforhor.md) | UC4 |
| 15 | [Träna på felaktiga svar igen](UC15-trana-pa-felaktiga-svar.md) | UC14 |
| 16 | [Skapa ett nytt språkpar](UC16-skapa-nytt-sprakpar.md) | UC3, UC4, UC11 |
| 17 | [Säkerhetskopiera ordlistor](UC17-sakerhetskopiera-ordlistor.md) | UC11 |
| 18 | [Spara förhörsresultat](UC18-spara-forhorsresultat.md) | UC14 |

UC1–UC4 gör programmet generiskt. UC5–UC8 är förbättringar ovanpå det. UC9–UC18 bygger ut programmet med nya funktioner:

- **Läsa** (UC9–UC10): visar och söker i ordlistan, ändrar ingenting.
- **Ändra** (UC11–UC13, UC17): lägger till, raderar och redigerar ord. UC11 skapar spara-funktionen som de andra återanvänder, och UC17 säkrar filen innan den skrivs över.
- **Träna** (UC14–UC15, UC18): förhör, extra omgång med felen och historik.
- **Nytt språkpar** (UC16): skapar en ny ordlista från programmet.

Förslag på ordning: UC9 → UC10 → UC14 → UC15 → UC18 → UC11 → UC17 → UC12 → UC13 → UC16.
