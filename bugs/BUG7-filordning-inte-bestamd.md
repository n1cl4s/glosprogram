# BUG7: Filerna läses inte i en bestämd ordning

## Steg för att återskapa
Buggen syns inte alltid. Den beror på operativsystem och filsystem.

1. Lägg flera ordlistor i `wordlists`.
2. Starta programmet på olika datorer, till exempel macOS och Windows, och översätt `stor`.

## Förväntat
Översättningarna visas i samma ordning varje gång.

## Faktiskt
Ordningen på filerna kan skilja sig mellan datorer, och då kommer också översättningarna i olika ordning.

## Orsak
`Directory.GetFiles(...)` lovar inte någon särskild ordning på filerna.

## Varför det spelar roll
I UC3 ska språkparen visas som en numrerad lista, och i UC4 väljer användaren ett nummer. Om ordningen kan ändras kan samma nummer betyda olika språkpar på olika datorer.

## Var i koden
`Program.cs`, vid `Directory.GetFiles(...)`.

## Åtgärdas i
UC3. Åtgärdad: språkparen sorteras i bokstavsordning med `Order()`, så numren är desamma oavsett i vilken ordning filerna läses. Eftersom man översätter i ett språkpar i taget (UC4) kommer översättningarna i samma ordning som i filen.
