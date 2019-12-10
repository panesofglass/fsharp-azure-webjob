#!/usr/bin/env bash

dotnet tool restore
dotnet paket restore
if [ ! -z "$@" ]; then
    dotnet fake run build.fsx --target $@
else
    dotnet fake build
fi
