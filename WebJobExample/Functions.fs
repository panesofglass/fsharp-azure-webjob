module WebJobExample.Functions

open System.IO
open Microsoft.Azure.WebJobs

type Functions =
    static member ProcessQueueMessage ([<QueueTrigger("queue")>] message : string) (log : TextWriter) =
        message |> log.WriteLine