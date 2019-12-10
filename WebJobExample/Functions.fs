module WebJobExample.Functions

open System
open System.Threading.Tasks
open Microsoft.Azure.WebJobs
open Microsoft.Extensions.Logging
open FSharp.Control.Tasks.V2.ContextInsensitive

//[<Singleton()>]
let processQueueMessage ([<QueueTrigger("%AppSettings:InputQueue%"); StorageAccount("%AppSettings:StorageAccount%")>]message:string, log:ILogger) =
    log.LogTrace(message)

[<Disable("AppSettings:TimerDisable")>]
let run ([<TimerTrigger("%AppSettings:TimerTrigger%")>]timer:TimerInfo, log:ILogger) =
    // Async is unnecessary here but used to show how to correctly return the Task.
    task {
        log.LogInformation(
            sprintf "F# Timer trigger function executed at: %s"
                (DateTime.Now.ToString()))
    } :> Task
