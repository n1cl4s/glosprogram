# UC8: Extrapolera översättningar via ett gemensamt språk

**Som** användare **vill jag** kunna översätta mellan två språk som inte har en egen ordlista, till exempel engelska till spanska, **så att** programmet kan använda svenska som mellanled i stället för att någon måste skriva en ny ordlista.

Exempel: `house → hus` (från `swedish-english.csv`) och `hus → casa` (från `swedish-spanish.csv`) ger tillsammans `house → casa`.

## Var i koden
`Program.cs`, efter inläsningen (se UC2 och UC6) och innan språkparen plockas ut (se UC3).

## Klart när
- [ ] Paren `english → spanish` och `spanish → english` visas i listan från UC3, trots att det inte finns någon `english-spanish.csv`.
- [ ] `house` ger `casa` och `edificio` i paret `english → spanish`.
- [ ] Samma översättning visas bara en gång, även om den nås via flera svenska ord.
- [ ] Ett ord översätts aldrig till sitt eget språk, det vill säga inga par som `english → english` skapas.
- [ ] Bara ett mellanled används. En ny ordlista, till exempel `swedish-german.csv`, ger `english → german` och `spanish → german`, men inga längre kedjor behövs.
- [ ] Språkpar som redan har en egen ordlista påverkas inte.
- [ ] README beskriver att översättningar via ett mellanled kan bli mindre exakta, eftersom en synonym på svenska kan ha en annan betydelse än originalordet.

## Beroenden
UC6
