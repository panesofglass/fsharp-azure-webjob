module WebJobExample.Functions

open System.Configuration
open System.IO
open Microsoft.Azure.WebJobs
open Microsoft.Azure.WebJobs.Host

let processQueueMessage ([<QueueTrigger("%InputQueue%"); StorageAccount("%StorageAccount%")>] message : string) (log : TextWriter) =
    message |> log.WriteLine

[<Disable("TimerDisable")>]
let run ([<TimerTrigger("%TimerTrigger%")>] timer, log: TraceWriter) =
    let setting = ConfigurationManager.AppSettings.["InputQueue"]
    log.Info(sprintf "The queue processor is listening to %s" setting)
