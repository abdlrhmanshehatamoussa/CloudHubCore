@echo off
dotnet ef migrations remove --project "CloudHub.Infra" --startup-project "CloudHub.API"