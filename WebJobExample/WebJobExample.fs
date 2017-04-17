module WebJobExample.Program

open Microsoft.Azure.WebJobs
open WebJobExample.Functions

[<EntryPoint>]
let main argv =
    let config = JobHostConfiguration()
    if config.IsDevelopment then config.UseDevelopmentSettings()
    use host = new JobHost()
    host.RunAndBlock()
    0 // return an integer exit code
