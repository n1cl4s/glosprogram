# UC7: Hantera felaktiga filer och felaktig inmatning

**Som** användare **vill jag** att programmet inte kraschar om en fil eller min inmatning är fel.

## Var i koden
`Program.cs`, i inläsningen och i huvudloopen.

## Klart när
- [ ] Tomma rader och rader utan kommatecken hoppas över.
- [ ] Filer vars namn inte följer mönstret `språk-språk.csv` hoppas över med en varning.
- [ ] Om mappen `wordlists` saknas eller är tom visas ett tydligt meddelande.
- [ ] Om användaren matar in `null`, till exempel med Ctrl+D, avslutas programmet utan krasch. I dag används `!` vid uppslagningen.

## Beroenden
UC2
