@echo off
set migration_name=%~1
set migration_dir=%~2
if "%migration_name%" == "" (
set /P migration_name=Migration Name:
)
if "%migration_dir%" == "" (
set /P migration_dir=Migration Output Directory:
)
dotnet ef migrations add %migration_name% --project "CloudHub.ServiceProvider" --startup-project "CloudHub.API" -o %migration_dir%