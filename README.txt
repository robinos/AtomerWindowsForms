AtomerWindowsForms

AtomerWindowsForm är en kodbas för studenter att koppla från databasen till uppvisning på
AtomerForm. 

Klasser som ges färdigt:
Databas - databas kopplingen
Atom - datarad behållare
AtomerProgram - kopplar databasen till formen

Vad man måste gör:
I AtomerForm borde man koppla rader i Dictionary<int,Atom> Atomer till:
En combobox som innehåller alla atom namn
Labels för förkortning, atomnummer, atomvikt, kokpunkt och smältpunkt som ändrar för att
visa information för atomen som är vald i comboboxen.

Vad man kan göra för lite extra utmanning (om man vill):
Koppla mot en MySQL databas som innehåller tabell tblAtomer istället och lägg till inloggning.
Man får ändra det färdiga koden så mycket man vill.