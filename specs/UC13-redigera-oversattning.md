# UC13: Redigera en översättning

**Som** användare **vill jag** kunna rätta ett felstavat ord eller en felaktig översättning, **så att** ordlistan blir korrekt utan att jag behöver radera och lägga till på nytt.

## Vad som ska göras
Ett kommando, till exempel `:ändra`, frågar efter ordet och visar dess översättningar numrerade (som i UC12). Användaren väljer en rad och om det är **ordet** eller **översättningen** som ska ändras, och skriver sedan den nya texten.

Eftersom `Word` är oföränderlig (get-only properties) går det inte att ändra ett befintligt objekt. Det gamla objektet tas i stället bort och ett nytt läggs till, åt båda hållen. Sedan byggs dictionaryt om och filen sparas med spara-funktionen från UC11. Poängen är att fil och minne alltid ska vara synkroniserade.

Om själva ordet ändras (till exempel `stro` → `stor`) ska alla rader med det felstavade ordet rättas, inte bara en.

## Var i koden
`Program.cs`, i den inre huvudloopen. Återanvänd spara-funktionen från UC11.

## Tänkbara tekniker
Hitta med `Where`/`FirstOrDefault`, ersätt genom att ta bort + lägga till, `File.WriteAllLines`.

## Klart när
- [x] `:ändra` låter användaren välja vilken översättning som ska ändras.
- [x] Det går att ändra översättningen (`stor → bigg` blir `stor → big`).
- [x] Det går att ändra ordet, och då rättas alla dess synonymrader (`stro → big, large` blir `stor → big, large`).
- [x] Ändringen syns direkt och åt båda hållen.
- [x] Ändringen finns kvar efter omstart.
- [x] Om ändringen skulle skapa en dubblett av ett befintligt ordpar får användaren veta det.
- [x] Samma regler för tomma ord och kommatecken som i UC11.

## Beroenden
UC11, UC12
