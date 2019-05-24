using System;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.SignalRService;
using Microsoft.Extensions.Logging;

namespace FunctionAppCSharp
{
    public class HttpMessageSender
    {
        [FunctionName("HttpMessageSender")]
        public void Run([HttpTrigger(AuthorizationLevel.Anonymous, "get")]HttpRequest req,
            [SignalR(HubName = "MessageDispatcher")]IAsyncCollector<SignalRMessage> signalRMessages, ILogger log)
        {
            log.LogInformation($"C# HTTP trigger function executed at: {DateTime.Now}");

            signalRMessages.AddAsync(
                new SignalRMessage
                {
                    Target = "ReceiveMessage",
                    Arguments = new object[] { DateTime.Now }
                });
        }
    }
}