# UC10: Sök på början av ett ord

**Som** användare **vill jag** kunna skriva början av ett ord följt av `*`, till exempel `st*`, och få alla ord som börjar så (`stor`, `stark`, `stol`), **så att** jag hittar ord jag inte minns exakt hur de stavas.

## Vad som ska göras
Innan uppslagningen i huvudloopen kontrollerar programmet om inmatningen slutar med `*`. I så fall tas stjärnan bort (`st*` → `st`) och alla nycklar i `translations` som börjar med den texten plockas ut. Varje träff skrivs ut med sina översättningar, i bokstavsordning. Om inmatningen inte slutar med `*` fungerar uppslagningen som i dag.

## Var i koden
`Program.cs`, i den inre huvudloopen, före den vanliga uppslagningen.

## Tänkbara tekniker
`EndsWith("*")`, `TrimEnd('*')` eller `Substring`, `Where` + `StartsWith(..., StringComparison.OrdinalIgnoreCase)`, `OrderBy`.

## Klart när
- [x] `st*` visar alla ord som börjar på `st`, med sina översättningar.
- [x] Sökningen är skiftlägesokänslig: `ST*` ger samma resultat som `st*`.
- [x] Träffarna visas i bokstavsordning.
- [x] Om inget ord matchar visas ett tydligt meddelande, till exempel `Inga ord börjar på "xq" i swedish → english`.
- [x] Bara `*` (utan bokstäver framför) visar ett meddelande om att man måste skriva minst en bokstav, i stället för hela listan.
- [x] Vanlig uppslagning utan `*` fungerar som tidigare.

## Beroenden
UC4. Utskriften av ett ord med synonymer kan delas med UC9.
