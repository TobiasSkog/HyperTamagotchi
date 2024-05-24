# Introduktion
En E-commerce app som är byggt med ASP.NET Core och består av två delar.
Frontend ASP.NET Core Webb App (MVC)
Backend ASP.NET Core Webb API

Webbshoppen är baserad på den nostalgiska hitten Tamagotchi från 90-talet, där vi säljer Tamagotchi's som om de vore riktiga djur från våran värld. Självklart säljer vi också tillhörande produkter för att hjälpa dig att ta hand om din Tamagotchi.

# Azure Link
Hemsida: https://hypertamagotchimvc.azurewebsites.net/
<br>
API: https://hypertamagotchiapi.azurewebsites.net/
## Registrera en ny användare / Logga in
Klicka på ![image](https://github.com/TobiasSkog/HyperTamagotchi/assets/11568812/6f627849-4bde-427d-9063-099d37d0d50a) ikonen
Sedan på Login för att logga in med ditt konto 
<br>
Eller klicka på Register för att skapa en ny användare
<br>
| :exclamation:  Notera!   |
|-----------------------------------------|
| När du skapar en ny användare måste du ange en giltig address |
| (City, Street, Zipcode) för leverans tiden ska räknas ut korrekt |
| (Google Distance Matrix API) |

# Struktur av källkoden
## Backend:
Sköter all kommunikation med Databasen. <br>
Skapar JWT Tokens. <br>
Sköter Authentication och Authorization. <br>
Kommunicerar med utstående API (Google Distance Matrix API). <br>
## Frontend:
Tillåter användaren att navigera runt på hemsidan och pratar med databasen via vårt API. <br>
Controllers för hjälpa navigera användaren. <br>
Baserat på role från API så begränsas vissa views om användaren inte har en tillåten role. <br>
Custom CSS. <br>

# Färdiga användare för att testa appen
| Email           | Lösenord | Role |
| --------------- | -------- | -------- |
|  tobias@gmail.com | Abc123! | Customer |
|  daniel@gmail.com | Abc123!| Customer |
|  carro@gmail.com | Abc123!| Customer |
|  wille@gmail.com | Abc123!| Customer |
