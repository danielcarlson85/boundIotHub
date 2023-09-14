using Bound.EventBus;
using Microsoft.Azure.EventHubs;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using IoTHubTrigger = Microsoft.Azure.WebJobs.EventHubTriggerAttribute;

namespace DeviceManager.IoTHubFunctions
{
    public static class BoundDeviceFunctions
    {
        private static UserEventBusHandler _userEventBusHandler;

        public static bool isRunning = false;

        [FunctionName("BoundDeviceFunctions")]
        public static async System.Threading.Tasks.Task RunAsync([IoTHubTrigger("messages/events", Connection = "IoTHubConnectionString")] EventData message, ILogger log)
        {
            var messageText = Encoding.UTF8.GetString(message.Body.Array);
            var serviceBusConnectionString = Environment.GetEnvironmentVariable("ServiceBusConnectionString");

            _userEventBusHandler = new UserEventBusHandler(serviceBusConnectionString, "usercreatedqueue");
            await _userEventBusHandler.SendMessageAsync(messageText);

            log.LogInformation($"{messageText}");
        }
    }
}