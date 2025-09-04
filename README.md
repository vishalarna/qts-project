To Use:

First you will need to add a new appsettings.json file to QTD2.Dev.Migrations

Copy your appsettings.Devleopment.json to QTD2.Dev.Migrations.

Next, add appsettings.Sqlite.Json and copy/paste below.

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  },
  "AllowedHosts": "*",
  "DbContexts": [
    {
      "Name": "QTDAuthenticationContext",
      "ConnectionString": "Data Source=QTD2Auth.db;",
      "Provider": "Sqlite"
    },
    {
      "Name": "QTDContext_Admin",
      "ConnectionString": "Data Source=test.qtd.db",
      "Provider": "Sqlite"
    },
    {
      "Name": "QTDContext",
      "ConnectionString": "Data Source=test.qtd.db",
      "Provider": "Sqlite"
    }
  ],
  "ApplicationSettings": {
    "Domains": {
      "QTD": "",
      "EMP": "",
      "Admin": "",
      "Angular": ""
    }
  },
  "Jwt": {
    "TokenSecretKey": "asdasdasdasdasdasdasdasd",
    "ValidIssuer": "https://localhost:44353", //issuer will be server address
    "ValidAudience": "https://localhost:44389" // audience will be single page applicationn URL
  },
  "NotificationSettings": {
    "SmsSettings": {
    },
    "MimeKitEmailSettings": {
      "DefaultFrom": "",
      "BCC": [
        ""
      ],
      "UserName": "",
      "Password": "",
      "Port": "",
      "Server": ""
    }
  }
}
```

Next run the command

./AddMigration -name {MigrationName} -context {desiredContext}


In the future we may need to add additional providers.  

We will need to update the Factories to accomadate the new provider
Add a new project for that provider's migrations
Modify AddMigration.ps1 for the new provider

If we add a new Context we need to add a new Factory for it

TODOs:

Add MySql to Factories
Refactory Factories to base class