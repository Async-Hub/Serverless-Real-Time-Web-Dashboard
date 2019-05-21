namespace FunctionAppFsharp

open System
open Microsoft.Azure.WebJobs
open Microsoft.Azure.WebJobs.Extensions.SignalRService
open Microsoft.Extensions.Logging

module RecurringMessageSender =
    [<FunctionName("SendMessageByInterval")>]
    let SendMessageByInterval
        ([<TimerTrigger("*/5 * * * * *")>] myTimer: TimerInfo,
         [<SignalRConnectionInfo(HubName = "MessageDispatcher")>] connectionInfo: SignalRConnectionInfo,
         [<SignalR(HubName = "MessageDispatcher")>] signalRMessages: IAsyncCollector<SignalRMessage>,
         log: ILogger) =
            let currentDate = DateTime.Now.ToString()
            let message = "Message from MessageDispatcher: " + currentDate
            printf "F# Timer trigger function executed at: %s" currentDate
            let signalrMessage = new SignalRMessage(Target = "ReceiveMessage", Arguments = [|message|])
            signalRMessages.AddAsync(signalrMessage) |> ignore