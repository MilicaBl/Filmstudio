# Filmstudio
1: Det inlämnande git-repot ska innehålla ett Webb-API skapats med ASP.NET och som går att starta med .NET 8 eller 9.
Max poäng: 1p

Läranderesultat: 3


2: API:et ska tillhandahålla resurserna "film" och "filmstudio".
Max poäng: 2p

Läranderesultat: 3, 4
Kriterium: Krav 1 måste också uppfyllas
Klassen “Film” ska finnas på följande plats Models/Film/Film.cs
Klassen “FilmStudio” ska finnas på följande plats Models/FilmStudio/FilmStudio.cs
Klassen Film ska ha uppfyllt interfacet IFilm.
Klassen FilmStudio ska ha uppfyllt interfacet IFilmStudio.


3: En filmstudio ska kunna registrera sig via API:et
Max poäng: 6p

Läranderesultat: 3, 4, 5
Kriterium: Krav 2 måste också uppfyllas
Detta anrop ska använda följande metod: POST
Registreringen ska ske vid ett anrop mot följande endpoint: /api/filmstudio/register
BODY:n i detta anrop ska kunna innehålla ett json-stringifierat objekt som uppfyller interface:et IRegisterFilmStudio.
Detta objekt innehåller nödvändig data för att korrekt registrera filmstudion.
Ett lyckat anrop ska returnera statuskod 200.
Ett lyckat anrop ska returnera ett json-objekt i sin body som, parse:at till C#, är ett objekt som uppfyller interface:et IFilmStudio.


4: En administratör ska kunna registrera sig via API:et, en administratör är inte en filmstudio.
Max poäng: 2p

Läranderesultat: 3, 4, 5
Kriterium: Krav 2 måste också uppfyllas
Detta anrop ska använda följande metod: POST
Registreringen ska ske vid ett anrop mot följande endpoint: /api/users/register
BODY:n i detta anrop ska kunna innehålla ett json-stringifierat objekt som uppfyller interface:et IUserRegister.
Detta objekt innehåller nödvändig data för att korrekt registrera en ny användare.
Om egenskapen IsAdmin är true, och alla Username och Password innehåller passande information, så registreras användaren som admin.
Vid lyckad tegistration av en admin returneras ett json-objekt i body:n som, parse:at till C#, är ett objekt som ENDAST innehåller egenskaperna Username, Role och UserId som finns i IUser-interface:et.
Det är viktigt att gömma känslig information. Vid returnering av förbjuden data görs poängavdrag
Ett lyckat anrop ska returnera statuskod 200.
Det är viktigt att gömma känslig information. Vid returnering av förbjuden data görs poängavdrag


5: Både filmstudios och administratörer ska kunna autentisera sig via API:et.
Max poäng: 3p

Läranderesultat: 3, 4, 5
Kriterium: Krav 3, 4 måste också uppfyllas
Detta anrop ska använda följande metod: POST
Autentiseringen ska ske vid ett anrop mot följande endpoint: /api/users/authenticate
BODY:n i detta anrop ska kunna innehåll ett json-stringifierat objekt som uppfyller interface:et IUserAuthenticate.
Detta objekt innehåller nödvändig data för att korrekt autentisera en användare.
Om denna autentisering godkänns så ska ett JSON-objekt som motsvarar interface:t IUser, förutom egenskapen ‘password’, returneras.
Det är viktigt att gömma känslig information. Vid returnering av förbjuden data görs poängavdrag
Ifall användarnamn och lösenord som används vid autentiseringen tillhör en registrerad admin så ska egenskapen Role vara en sträng med ordet “admin”.
Stora eller små bokstäver på denna sträng spelar ingen roll.
Ifall användarnamn och lösenord som används vid autentiseringen tillhör en registrerad filmstudio så ska egenskapen Role i objektet vara en sträng med ordet “filmstudio”. Egenskapen filmStudioId ska också finnas och egenskapen filmStudio ska innehålla ett objekt som motsvarar interface:et IFilmStudio och innehåller åtminstone data i egenskaperna FilmStudioId, Name och City.
Vidare autentiseringen i applikationen ska ske via headern "Authentication" vid anrop till API:et.


6: Det ska gå att lägga till nya filmer via Webb-API:et om man är autentiserad som administratör.
Max poäng: 2p

Läranderesultat: 3, 4, 5
Kriterium: Krav 4, 5 måste också uppfyllas
Detta anrop ska använda följande metod: POST
Anropet ska göras mot följande endpoint: /api/films
Body:n i anropen ska vara ett JSON-objekt som parse:at motsvarar ett objekt som uppfyller interface:et ICreateFilm.
Ett anrop av en autentiserad admin ska ge statuskod 200
Ett anrop av en autentiserad admin ska returnera ett JSON-stringifierat objekt som motsvarar IFilm, inklusive listan av FilmCopies.


7: Via Webb-API:et ska alla filmstudios kunna hämtas.
Max poäng: 3p

Läranderesultat: 3, 4, 5
Kriterium: Krav 3 måste också uppfyllas
Detta anrop ska använda följande metod: GET
Anropet ska kunna göras mot följande endpoint: /api/filmstudios
Ett lyckat anrop med en oautentiserad användare eller en autentiserad filmstudio ska ge tillbaka ett json-objekt som, parse:at till C#, är en array av objekt som uppfyller interface:et IFilmStudio.cs men som INTE innehåller listan RentedFilmCopies eller egenskapen City.
Ett lyckat anrop med en autentiserad admin ska innehålla alla egenskaper i objektet, även listan med RentedFilmCopies och dess innehåll.


8: Via Webb-API:et ska informationen om en enskild filmstudio kunna hämtas
Max poäng: 2p

Läranderesultat: 3, 4, 5
Kriterium: Krav 3, 4, 5 måste också uppfyllas
Detta anrop ska använda följande metod: GET
Anropet ska kunna göras mot följande endpoint: /api/filmstudio/{id} där {id} motsvarar FilmStudioId på eftersökt filmstudio.
Ett lyckat anrop med en oautentiserad användare eller en autentiserad filmstudio men som inte är filmstudion som eftersöks ska ge tillbaka ett json-objekt som, parse:at till C#, är en array av objekt som uppfyller interface:et IFilmStudio.cs men som INTE innehåller listan RentedFilmCopies eller egenskapen City.
Ett lyckat anrop med en admin ska ge returnera ett json-objekt som, parse:at till C#, är en array av objekt som uppfyller interface:et IFilmStudio.cs.
Ett lyckat anrop med en autentiserad filmstudio som har samma FilmStudioId som den filmstudio som eftersöks ska ge returnera ett json-objekt som, parse:at till C#, är en array av objekt som uppfyller interface:et IFilmStudio.cs.
Detta innebär även data i RentedFilmCopies


9: Via Webb-API:et ska alla filmer kunna hämtas.
Max poäng: 3p

Läranderesultat: 3, 4, 5
Kriterium: Krav 4, 6 måste också uppfyllas
Detta anrop ska använda följande metod: GET
Anropet ska kunna göras mot följande endpoint: /api/films
Ett anrop av en autentiserad användare (filmstudio eller admin) ska ge tillbaka en array med json-objekt som ifall vi parse:ar det till ett objekt i C# motsvarar ett objekt som uppfyller interface:et IFilm.
Ett anrop av en icke-autentiserad användare ska ge tillbaka en array med json-objekt som ifall vi parse:ar dem till objekt i C# motsvarar objekt som har alla properties i IFilm-interface:et förutom egenskapen `FilmCopies`.


10: Via Webb-API:et ska informationen om en enskild film kunna hämtas.
Max poäng: 2p

Läranderesultat: 3, 4, 5
Kriterium: Krav 4, 6 måste också uppfyllas
Detta anrop ska använda följande metod: GET
Anropet ska kunna göras mot följande endpoint: /api/films/{id} där path-variabeln id motsvarar FilmId för filmen som ska hämtas.
Ett anrop av en autentiserad användare (filmstudio eller admin) ska ge tillbaka ett json-objekt som ifall vi parse:ar det till ett objekt i C# motsvarar ett objekt som uppfyller interface:et IFilm.
Ett anrop av en icke-autentiserad användare ska ge tillbaka ett json-objekt som ifall vi parse:ar det till ett objekt i C# motsvarar ett objekt som har alla properties i IFilm-interface:et förutom egenskapen `FilmCopies`.


11: Det ska gå att ändra informationen om en film via Webb-API:et om man är autentiserad som administratör:
Max poäng: 5p

Läranderesultat: 3, 4, 5
Kriterium: Krav 4, 6, 10 måste också uppfyllas
Detta anrop ska använda följande metod: PATCH/PUT/POST
Anropet ska göras till följande endpoint: /api/films/{id} där path-variabeln id motsvarar FilmId:t för filmen som ska uppdateras.
Ett lyckat anrop av en autentiserad admin ska ge statuskod 200.
Ett lyckat anrop av en autentiserad admin ska returnera det uppdaterade objektet som är ett json-objekt som parse:at till C# uppfyller interface:et IFilm.
Ett anrop av en icke-autentiserad användare ska ge statuskod 401.


12: En autentiserad administratör ska kunna ändra antalet tillgängliga exemplar som går att låna av varje film.
Max poäng: 4p

Läranderesultat: 3, 4, 5
Kriterium: Krav 4, 6, 10 måste också uppfyllas
Detta anrop ska använda följande metod: PATCH/PUT/POST
Anropet ska göras till följande endpoint för att fungera: /api/films/{id} där path-variabeln id motsvarar id:t för filmen som ska uppdateras.
I body:n för detta anrop ska ett JSON-objekt som, parse:at till ett C#-objekt, som uppfyller interface:et IFilm kunna skickas.
Ett lyckat anrop av en autentiserad admin ska ge statuskod 200 och returnera det uppdaterade objektet. Detta objekt är återigen ett json-objekt som parse:at till C# uppfyller interface:et IFilm.
Ett anrop av en icke-autentiserad användare ska ge statuskod 401.


13: En autentiserad filmstudio ska kunna låna ett exemplar av en film via Webb-API:et om det finns exemplar tillgängliga.
Max poäng: 4p

Läranderesultat: 3, 4, 5
Kriterium: Krav 3, 4, 5, 6, 8 måste också uppfyllas
Detta anrop ska kunna använda följande metod: POST
Anropet ska göras till följande endpoint för att fungera: /api/films/rent?id={id}&studioid={studioid} där parametern {id} motsvarar id:t för filmen som ska lånas och {studioid} id:t för studion som ska låna filmen.
Ett lyckat anrop ska returnera statuskod 200.
Om anropet görs av en icke-autentiserad admin, eller av en autentiserad filmstudio vars id inte överensstämmer med id:t som anges i anropet, ska anropet returnera statuskod 401 eller annan felkod (absolut inte 200) och lånet ska inte godkännas.
Om filmen inte finns ska statuskod 409 returneras.
Om filmen inte har några lediga kopior ska statuskod 409 returneras.
Om filmstudion redan hyr en kopia av samma film ska statuskod 403 returneras och lånet ska inte gå igenom.


14: En autentiserad filmstudio ska kunna lämna tillbaka ett lånat exemplar av en film via Webb-API:et.
Max poäng: 2p

Läranderesultat: 3, 4, 5
Kriterium: Krav 3, 4, 5, 6, 8, 13 måste också uppfyllas
Detta anrop ska kunna använda följande metod: POST
Anropet ska göras till följande endpoint för att fungera: /api/films/return?id={id}&studioid={studioid} där parametern {id} motsvarar id:t för filmen som ska lånas och {studioid} id:t för studion som ska låna filmen.
Ett lyckat anrop ska returnera statuskod 200.
Om anropet görs av en icke-autentiserad användare, eller om filmstudion som är inloggad inte överensstämmer med filmstudion vars id anges i anropet, ska anropet returnera statuskod 401.
Om filmen inte finns ska statuskod 409 returneras.


15: En autentiserad filmstudio ska via Webb-API:et kunna hämta vilka filmer som denna studio för närvarande har lånat
Max poäng: 2p

Läranderesultat: 3, 4, 5
Kriterium: Krav 3, 4, 5, 6, 8, 13 måste också uppfyllas
Detta anrop ska kunna använda följande metod: GET
Anropet ska göras till följande endpoint för att fungera: /api/mystudio/rentals
Ett lyckat anrop ska returnera statuskod 200.
Ett lyckat anrop ska returnera en array bestående av objekt som motsvarar interface:t IFilmCopy.
Dessa objekt ska överrensstämma med de filmer som den autentiserade filmstudion för tillfället hyr.
Om anropet görs av en icke-autentiserad filmstudio ska anropet returnera statuskod 401.
