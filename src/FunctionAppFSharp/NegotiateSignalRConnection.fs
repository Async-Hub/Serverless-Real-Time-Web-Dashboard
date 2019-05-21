namespace FunctionAppFsharp

open Microsoft.Azure.WebJobs
open Microsoft.AspNetCore.Http
open Microsoft.Extensions.Logging
open Microsoft.Azure.WebJobs.Extensions.Http
open Microsoft.Azure.WebJobs.Extensions.SignalRService

module SignalRConnectionNegotiation = 
    [<FunctionName("negotiate")>]
    let NegotiateSignalRConnection
        ([<HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)>] req: HttpRequest,
         [<SignalRConnectionInfo(HubName = "MessageDispatcher")>] connectionInfo: SignalRConnectionInfo,
         log: ILogger) = 
            connectionInfo

