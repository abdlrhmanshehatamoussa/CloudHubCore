@echo off
dotnet ef database update --project "CloudHub.Infra" --startup-project "CloudHub.API"