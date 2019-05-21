using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.SignalRService;
using Microsoft.Extensions.Logging;

namespace FunctionAppCSharp
{
    public class RecurringMessageSender
    {
        private readonly DiSampleClass _diSampleClass;

        private readonly string _instanceId;

        public RecurringMessageSender(DiSampleClass diSampleClass)
        {
            _diSampleClass = diSampleClass;
            _instanceId = Guid.NewGuid().ToString();
        }

        [FunctionName("SendMessageByInterval")]
        public void Run([TimerTrigger("*/5 * * * * *")]TimerInfo myTimer,
            [SignalRConnectionInfo(HubName = "MessageDispatcher")] SignalRConnectionInfo connectionInfo,
            [SignalR(HubName = "MessageDispatcher")]IAsyncCollector<SignalRMessage> signalRMessages,
            ILogger log)
        {
            log.LogInformation($"Instance Id: {_instanceId}");
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
            log.LogInformation(_diSampleClass.HelloDi);

            signalRMessages.AddAsync(
                new SignalRMessage
                {
                    Target = "ReceiveMessage",
                    Arguments = new object[] { DateTime.Now }
                });
        }
    }
}
