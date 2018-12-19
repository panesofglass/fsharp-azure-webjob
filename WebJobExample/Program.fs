module WebJobExample.Program

open Microsoft.Extensions.Hosting

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
            .UseConsoleLifetime()

    use host = builder.Build()
    host.Run()
    0 // return an integer exit code
