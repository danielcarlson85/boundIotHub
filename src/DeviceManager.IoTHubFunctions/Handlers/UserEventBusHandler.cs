using Azure.Messaging.ServiceBus;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Bound.EventBus
{
    public class UserEventBusHandler
    {
        private static ServiceBusClient _serviceBusClient;
        private readonly ServiceBusSender _sender;
        private readonly ServiceBusProcessor _processor;

        public UserEventBusHandler(string connectionString, string queueName)
        {
            _serviceBusClient = new ServiceBusClient(connectionString);
            _sender = _serviceBusClient.CreateSender(queueName);
            _processor = _serviceBusClient.CreateProcessor(queueName, new ServiceBusProcessorOptions());
        }

        public async Task StartRecieveMessageAsync()
        {
            await _processor.StartProcessingAsync();
        }

        public async Task SendMessageAsync(string payload)
        {
            Debug.WriteLine($"Message {payload} is sent to User queue");
            ServiceBusMessage message = new ServiceBusMessage(payload);
            await _sender.SendMessageAsync(message);
        }
    }
}
