# UC17: Säkerhetskopiera ordlistor

**Som** användare **vill jag** att programmet sparar en kopia av ordlistan innan den ändras, **så att** jag kan få tillbaka mina ord om något blir fel vid redigering eller radering.

## Vad som ska göras
Innan spara-funktionen från UC11 skriver över en fil kopieras den befintliga filen till en mapp, till exempel `backups/`. Kopian får en tidsstämpel i namnet så att gamla kopior inte skrivs över:

```
backups/swedish-english_2026-10-05_14-32-10.csv
```

Mappen skapas automatiskt om den inte finns. Eftersom kopieringen ligger i spara-funktionen gäller den automatiskt för lägg till, radera och redigera.

Lägg `backups/` i `.gitignore`, så att kopiorna inte checkas in.

## Var i koden
`Program.cs`, i spara-funktionen från UC11, precis före `File.WriteAllLines`.

## Tänkbara tekniker
`Directory.CreateDirectory`, `File.Copy`, `DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss")`, `Path.Combine`, `Path.GetFileNameWithoutExtension`.

## Klart när
- [x] En kopia skapas i `backups/` innan en ordlista skrivs om.
- [x] Kopians namn innehåller originalnamnet och en tidsstämpel.
- [x] Mappen `backups/` skapas automatiskt om den saknas.
- [x] Flera ändringar i rad ger flera kopior, ingen skrivs över.
- [x] Tidsstämpeln innehåller inga tecken som är ogiltiga i filnamn (till exempel `:`).
- [x] Ingen kopia skapas för en helt ny, tom fil (UC16), eftersom det inte finns något att säkra.
- [x] Filer i `backups/` läses inte in som ordlistor.
- [x] `backups/` finns i `.gitignore`.

## Beroenden
UC11
