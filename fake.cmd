@echo off
cls

dotnet restore build.proj
if not "%*"=="" (
    dotnet fake run build.fsx --target %*
) else (
    dotnet fake run build.fsx
)
