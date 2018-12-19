module WebJobExample.Functions

open System
open System.Configuration
open System.Threading.Tasks
open Microsoft.Azure.WebJobs
open Microsoft.Extensions.Logging

//[<Singleton()>]
let processQueueMessage ([<QueueTrigger("%InputQueue%"); StorageAccount("%StorageAccount%")>]message:string, log:ILogger) =
    log.LogTrace(message)

[<Disable("TimerDisable")>]
let run ([<TimerTrigger("%TimerTrigger%")>]timer:TimerInfo, log:ILogger) =
    // Async is unnecessary here but used to show how to correctly return the Task.
    async {
        log.LogInformation(
            sprintf "F# Timer trigger function executed at: %s"
                (DateTime.Now.ToString()))
        log.LogInformation(
            sprintf "The queue processor is listening to %s"
                ConfigurationManager.AppSettings.["InputQueue"])
    }
    |> Async.StartAsTask :> Task
