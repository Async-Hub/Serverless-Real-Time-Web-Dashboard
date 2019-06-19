using System;
using System.Globalization;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.SignalRService;
using Microsoft.Build.Utilities;
using Microsoft.Extensions.Logging;

namespace FunctionAppCSharp
{
    public class LiveCurrencyRates
    {
        [FunctionName("RefreshLiveCurrencyRates")]
        public void RefreshLiveCurrencyRates(
            [TimerTrigger("*/5 * * * * *")]TimerInfo timerInfo,
            [SignalR(HubName = "CurrencyRates")] IAsyncCollector<SignalRMessage> signalrMessages,
            ILogger log)
        {
            var currentDate = DateTime.Now.ToString(CultureInfo.InvariantCulture);
            var message = "Message from MessageDispatcher: " + currentDate;


            log.LogInformation($"C# Timer trigger function executed at: {currentDate}");
        }
    }
}
