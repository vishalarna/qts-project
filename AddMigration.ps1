param (
   [string]$context,
   [string]$name
)

if($name -eq "")
{
   throw "Your must supply a -name argument"
}

if($context -eq "")
{
   throw "You must supply a -context argument with either QTDContext or QTDAuthenticationContext"
}

if ( $context -eq "QTDContext")
{
   $Env:ASPNETCORE_ENVIRONMENT="Sqlite"
   Add-Migration $name -context QTDContext -Project QTD2.Data.Migrations.Sqlite -OutputDir QTD -Args "--provider Sqlite" -S QTD2.Dev.Migrations
   $Env:ASPNETCORE_ENVIRONMENT="Development"
   Add-Migration $name -context QTDContext -Project QTD2.Data -S QTD2.Dev.Migrations
   $Env:ASPNETCORE_ENVIRONMENT=""
}

elseif ($context -eq "QTDAuthenticationContext")
{
   $Env:ASPNETCORE_ENVIRONMENT="Sqlite"
   Add-Migration $name -context QTDAuthenticationContext -Project QTD2.Data.Migrations.Sqlite -OutputDir QTDAuthentication -Args "--provider Sqlite" -S QTD2.Dev.Migrations
   $Env:ASPNETCORE_ENVIRONMENT="Development"
   Add-Migration $name -context QTDAuthenticationContext -Project QTD2.Data -S QTD2.Dev.Migrations
   $Env:ASPNETCORE_ENVIRONMENT=""
}

else {
   Write-Host "You must select either QTDContext or QTDAuthenticationContext"
}