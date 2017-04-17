module WebJobExample.Functions

open System.IO
open Microsoft.Azure.WebJobs

let processQueueMessage ([<QueueTrigger("queue")>] message : string) (log : TextWriter) =
    message |> log.WriteLine