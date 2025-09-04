To manually run migrations, do the following on US and Canada App servers:

cd C:\inetpub\wwwroot\QTD2\API\QTD

set ASPNETCORE_ENVIRONMENT=Production

set DOTNET_ENVIRONMENT=Production

QTD2.Migrator.exe RunAuth

<start Auth project, for PROD / UAT do this by recycling app pool>

QTD2.Migrator.exe RunQTD 

<start API project, for PROD / UAT do this by recycling app pool>