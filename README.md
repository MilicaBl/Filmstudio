# Filmstudio
Detta skolprojekt är en webbapplikation för filmföreningar anslutna till Sveriges Förenade Filmstudios (SFF), där filmstudios kan låna filmer via ett API och ett klientgränssnitt. Projektet inkluderar både ett API för att hantera filmer och filmstudios samt ett frontendgränssnitt som är enbart för filmstudios.

[Ladda ner PDF](docs/reflections.pdf)

## För att testa funktionalitet 
- På frontend behöver du först registrera dig som filmstudio och sedan logga in.
- I test.rest filen behöver du registrera dig som admin eller filmstudio. När du autentiserar dig kommer du att få en token som du ska byta ut i variabeln högst upp i filen. En filmstudio får även ett id, vilket också ska användas i variabeln filmStudioId.

## Hur man startar programmet

Förkrav
 -.NET SDK installerad på din dator.

  Kör programmet

1.**Klona eller ladda ner projektet**
   ```bash
   git clone https://github.com/MilicaBl/Filmstudio.git
   ````

2.**Öppna en terminal i projektmappen** 

3.**Navigera till backend mappen**
```bash
cd API
````

4.**Kör följande komando för att starta projektet**
```bash
dotnet watch --urls=http://localhost:5001/
````

5.**Navigera till frontend mappen**
```bash
cd Frontend
````

6.**Öppna index.html filen med LiveServer**

Nu har du både backend och frontend igång!

## Funktioner
- Autentisering och användarhantering: Användare loggar in som antingen admin eller filmstudio.
- Rollbaserad åtkomst: Det finns tre typer av användare - autentiserad admin, autentiserad filmstudio och oautentiserad användare.
- CRUD-funktionalitet: Admin kan skapa, läsa och uppdatera filmer.
- Filmuthyrning: Filmstudios kan låna filmer baserat på tillgänglighet och kan inte låna mer en en kopia av samma film åt gången.

## Teknologi  
- **Backend:** ASP.NET Core Web API  
- **Frontend:** HTML, CSS, JavaScript
- **Databas:** EF InMemory databas.
- **AutoMapper:** för att mappa mellan objekt
- **Autentisering:** ASP.NET Identity för användarhantering och JWT - JSON Web Token för autentisering