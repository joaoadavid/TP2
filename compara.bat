@echo off
setlocal
set SCRIPT_DIR=%~dp0
dotnet run --project "%SCRIPT_DIR%TP2.csproj" -- %*
endlocal
