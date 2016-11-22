module WebJobExample

open Microsoft.Azure.WebJobs
open WebJobExample.Functions

[<EntryPoint>]
let main argv =
    let config = new JobHostConfiguration()
    if config.IsDevelopment then config.UseDevelopmentSettings()
    let host = new JobHost()
    host.RunAndBlock()
    0 // return an integer exit code
