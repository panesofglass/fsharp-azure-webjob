@echo off
cls

dotnet tool restore
dotnet paket restore
if not "%*"=="" (
    dotnet fake run build.fsx --target %*
) else (
    dotnet fake build
)
