@echo off
set migration_name=%~1
set migration_dir=%~2
if "%migration_name%" == "" (
set /P migration_name=Migration Name:
)
if "%migration_dir%" == "" (
set /P migration_dir=Migration Output Directory:
)
set GOOGLE_TOKEN_INFO_API_URL=0
set ASPNETCORE_ENVIRONMENT=L
set BUILD_ID=0
set API_DATABASE=Host=127.0.0.1;Database=cloudhub-api-core-local;Username=postgres;Password=123456
set PRODUCTION_MODE=false
dotnet ef migrations add %migration_name% --project "CloudHub.Infra" --startup-project "CloudHub.API" -o %migration_dir%