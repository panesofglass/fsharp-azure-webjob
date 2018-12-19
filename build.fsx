#r "paket:
source release/dotnetcore
source https://api.nuget.org/v3/index.json
nuget BlackFox.Fake.BuildTask
nuget Fake.Core.Target
nuget Fake.DotNet.Cli
nuget Fake.IO.FileSystem
nuget Fake.IO.Zip //"
#load "./.fake/build.fsx/intellisense.fsx"
#if !FAKE
#r "Facades/netstandard"
#r "netstandard"
#endif

open BlackFox.Fake
open Fake.DotNet
open Fake.IO
open Fake.IO.Globbing.Operators
open System.IO
open System.Xml.Linq

// Directories
let buildDir  = Path.Combine(__SOURCE_DIRECTORY__, "./build/")
let deployDir = Path.Combine(__SOURCE_DIRECTORY__, "./deploy/")
let versionFile = Path.Combine(__SOURCE_DIRECTORY__, "Directory.Build.props")

let getVersion (versionFile:string) =
    let doc = XElement.Load versionFile
    let version =
        doc.Elements().Elements()
        |> Seq.filter (fun e -> e.Name.LocalName = "VersionPrefix")
        |> Seq.head
    version.Value

// Targets
let cleanTask =
    BuildTask.create "Clean" [] {
        Shell.cleanDirs [buildDir; deployDir]
    }

let buildTask =
    BuildTask.create "Build" [cleanTask] {
        "WebJobExample/WebJobExample.fsproj"
        |> DotNet.build (fun p ->
            { p with
                Configuration = DotNet.BuildConfiguration.Release
                OutputPath = Some buildDir
            }) 
    }

BuildTask.create "Deploy" [buildTask] {
    let version = getVersion versionFile
    !! (buildDir + "/**/*.*")
    -- "*.zip"
    |> Zip.zip buildDir (deployDir + "ApplicationName." + version + ".zip")
}

// start build
BuildTask.runOrDefault buildTask
