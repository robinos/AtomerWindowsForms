AtomerWindowsForms

AtomerWindowsForm �r en kodbas f�r studenter att koppla fr�n databasen till uppvisning p�
AtomerForm. 

Klasser som ges f�rdigt:
Databas - databas kopplingen
Atom - datarad beh�llare
AtomerProgram - kopplar databasen till formen

Vad man m�ste g�r:
I AtomerForm borde man koppla rader i Dictionary<int,Atom> Atomer till:
En combobox som inneh�ller alla atom namn
Labels f�r f�rkortning, atomnummer, atomvikt, kokpunkt och sm�ltpunkt som �ndrar f�r att
visa information f�r atomen som �r vald i comboboxen.

Vad man kan g�ra f�r lite extra utmanning (om man vill):
Koppla mot en MySQL databas som inneh�ller tabell tblAtomer ist�llet och l�gg till inloggning.
Man f�r �ndra det f�rdiga koden s� mycket man vill.