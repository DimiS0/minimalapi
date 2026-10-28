# MinimalAPI – Projecten en Blogs

Backend voor mijn portfoliosite, Via de API kun je projecten aanmaken, opvragen, wijzigen en verwijderen en blog/s opvragen.

## Belangerijke instellingen

Controleer je versie met `dotnet --version`

- Zorg dat je versie .NET 10.0 SDK hebt gedownload: https://dotnet.microsoft.com/download
- SQL Server download: https://www.microsoft.com/sql-server/sql-server-downloads
- EF Core-tools: `dotnet tool install --global dotnet-ef`

## Database aanmaken
1. Open `appsettings.json` en pas de connection string `ProjectsDatabase` aan naar jouw SQL Server connection string.

2. Voer in de projectmap: `dotnet ef database update`

   De migraties maken de database en de tabellen aan.

## Applicatie starten en sluiten
1. Voer in de projectmap uit:

       `dotnet run`

2. Als je klaar bent kun je de aplicatie stoppen door in de terminal `ctrl c`

## Endpoints testen

de endpoints kan je via de file minimalapi.http testen dit is handmatig en dan moet je op send request drukken om een response te krijgen, maar je kan ook met swagger testen 

- Open in je browser de URL die in de terminal staat, met `/swagger` erachter, bijvoorbeeld voor mij stond er in de terminal http://localhost:5041 dus wordt het `http://localhost:5041/swagger`. Daar kun je alle endpoints makkelijk testen.