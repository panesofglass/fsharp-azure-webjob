module WebJobExample.Program

open Microsoft.Azure.WebJobs

[<EntryPoint>]
let main argv =
    let config = JobHostConfiguration()
    config.UseTimers()
    if config.IsDevelopment then config.UseDevelopmentSettings()
    use host = new JobHost(config)
    host.RunAndBlock()
    0 // return an integer exit code
