@echo off
set PRODUCTION_MODE=false
dotnet ef migrations remove --project "CloudHub.Infra" --startup-project "CloudHub.API"