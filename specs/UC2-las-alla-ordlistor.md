# UC2: Läs in alla ordlistor i mappen

**Som** användare **vill jag** att alla filer i `wordlists` läses in, **så att** en ny ordlista bara behöver läggas i mappen för att komma med.

## Var i koden
`Program.cs`, i inläsningen med `File.ReadAllLines(...)`.

## Klart när
- [x] Programmet hittar en ny fil, till exempel `swedish-german.csv`, utan att koden ändras.
- [x] Orden från båda filerna finns i `words`.

## Beroenden
UC1
