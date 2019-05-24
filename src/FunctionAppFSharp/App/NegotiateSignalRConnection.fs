namespace FunctionAppFsharp

open Microsoft.Azure.WebJobs
open Microsoft.AspNetCore.Http
open Microsoft.Extensions.Logging
open Microsoft.Azure.WebJobs.Extensions.Http
open Microsoft.Azure.WebJobs.Extensions.SignalRService

module SignalRConnectionNegotiation =
    [<FunctionName("NegotiateSignalRConnectionToCurrencyRatesHub")>]
    //MessageDispatcher
    let NegotiateSignalRConnectionToCurrencyRatesHub
        ([<HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "currency-rates/negotiate")>] req: HttpRequest,
         [<SignalRConnectionInfo(HubName = "CurrencyRates")>] connectionInfo: SignalRConnectionInfo,
         log: ILogger) = 
            connectionInfo

    [<FunctionName("NegotiateSignalRConnectionToMessageDispatcherHub")>]
    //MessageDispatcher
    let NegotiateSignalRConnectionToMessageDispatcher
        ([<HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "message-dispatcher/negotiate")>] req: HttpRequest,
         [<SignalRConnectionInfo(HubName = "MessageDispatcher")>] connectionInfo: SignalRConnectionInfo,
         log: ILogger) = 
            connectionInfo

