@echo off
set PRODUCTION_MODE=false
dotnet ef database update --project "CloudHub.Infra" --startup-project "CloudHub.API"