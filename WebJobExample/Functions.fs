module WebJobExample.Functions

open System
open System.Configuration
open System.IO
open System.Threading.Tasks
open Microsoft.Azure.WebJobs
open Microsoft.Azure.WebJobs.Host

let processQueueMessage ([<QueueTrigger("%InputQueue%"); StorageAccount("%StorageAccount%")>]message:string, log:TextWriter) =
    message |> log.WriteLine

[<Disable("TimerDisable")>]
let run ([<TimerTrigger("%TimerTrigger%")>]timer:TimerInfo, log:TraceWriter) =
    // Async is unnecessary here but used to show how to correctly return the Task.
    async {
        log.Info(
            sprintf "F# Timer trigger function executed at: %s" 
                (DateTime.Now.ToString()))
        log.Info(
            sprintf "The queue processor is listening to %s"
                (ConfigurationManager.AppSettings.["InputQueue"]))
    }
    |> Async.StartAsTask :> Task
