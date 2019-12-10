@echo off
cls

dotnet tool restore
dotnet paket restore
if not "%*"=="" (
    dotnet fake build --target %*
) else (
    dotnet fake build
)
