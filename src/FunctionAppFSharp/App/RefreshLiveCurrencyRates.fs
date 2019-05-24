namespace FunctionAppFsharp

open System
open Microsoft.Azure.WebJobs
open Microsoft.Azure.WebJobs.Extensions.SignalRService
open Microsoft.Extensions.Logging
open LiveCurrencyRates.Currency
open Newtonsoft.Json

module LiveCurrencyRates =
    [<FunctionName("RefreshLiveCurrencyRates")>]
    let RefreshLiveCurrencyRates
        ([<TimerTrigger("*/5 * * * * *")>] myTimer: TimerInfo,
         [<SignalRConnectionInfo(HubName = "CurrencyRates")>] connectionInfo: SignalRConnectionInfo,
         [<SignalR(HubName = "CurrencyRates")>] signalRMessages: IAsyncCollector<SignalRMessage>,
         log: ILogger) =
            let currentDate = DateTime.Now.ToString()
            let message = "Message from MessageDispatcher: " + currentDate
            
            printf "F# Timer trigger function executed at: %s" currentDate

            let newRates = GetNewRates()
            let message = JsonConvert.SerializeObject newRates

            let signalrMessage = new SignalRMessage(Target = "ReceiveNewRates", Arguments = [|message|])
            signalRMessages.AddAsync(signalrMessage) |> ignore