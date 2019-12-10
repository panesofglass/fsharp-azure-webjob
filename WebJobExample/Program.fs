module WebJobExample.Program

open System
open System.Configuration
open System.IO
open System.Reflection
open Microsoft.Azure.WebJobs
open Microsoft.Extensions.Configuration
open Microsoft.Extensions.DependencyInjection
open Microsoft.Extensions.Hosting
open Microsoft.Extensions.Logging

let configNameResolver =
    { new INameResolver with
        member __.Resolve(name) =
            ConfigurationManager.AppSettings.[name] }

[<EntryPoint>]
let main argv =
    let builder =
        HostBuilder()
            .UseEnvironment("Development")
            .ConfigureWebJobs(fun b ->
                b.AddAzureStorageCoreServices()
                 .AddAzureStorage()
                 .AddTimers() |> ignore
            )
            .ConfigureAppConfiguration(fun b ->
                b.SetBasePath(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))
                 .AddJsonFile("appsettings.json")
                 .AddEnvironmentVariables() |> ignore
            )
            .ConfigureLogging(fun b ->
                b.SetMinimumLevel(LogLevel.Debug) |> ignore
                b.AddConsole() |> ignore
                let appInsightsKey = ConfigurationManager.AppSettings.["APPLICATION_INSIGHTS_KEY"]
                if not (String.IsNullOrEmpty appInsightsKey) then
                    b.AddApplicationInsights(appInsightsKey) |> ignore
            )
            .ConfigureServices(fun services ->
                // This seems to correctly add the name resolver for the QueueTrigger
                services.AddSingleton(configNameResolver) |> ignore
                // Next error is that the ExampleStorage connection string cannot be found
                //services.AddSingleton(
                //    StorageAccountProvider( ???
                //            ConfigurationManager.ConnectionStrings.["ExampleStorage"].ConnectionString)) |> ignore
            )
            .UseConsoleLifetime()

    use host = builder.Build()
    host.Run()
    0 // return an integer exit code
