// -------------------------------------------------------------------------------------------------
// Copyright (c) Bound Technologies AB. All rights reserved.
// -------------------------------------------------------------------------------------------------

using Bound;
using DeviceManager.Device.DeviceMethods;
using DeviceManager.Device.NewFolder;
using Microsoft.Azure.Devices.Client;
using System;

namespace Device
{
    public class Program
    {
        public static bool IsRunning = false;

        public static DeviceData DeviceData { get; set; }
        static string ChestLayUp = "HostName=boundiothub.azure-devices.net;DeviceId=ChestLayUp;SharedAccessKey=hRHHqxy+QTKmwQzHgvvmOmAFQbMNcaBKpki1nlmWD28=";
        static string ShouldersStandingUp = "HostName=boundiothub.azure-devices.net;DeviceId=ShouldersStandingUp;SharedAccessKey=9qzyR0oiikanTFLV5f0iI0UL+lNqkekeI9DO/REhHZw=";
        static string BackStandingDown = "HostName=boundiothub.azure-devices.net;DeviceId=BackStandingDown;SharedAccessKey=o5z46b5qqsrM3Di+oJ724JCSZLJvX7sF3dcbtAldx9k=";
        static string ShouldersOverHead = "HostName=boundiothub.azure-devices.net;DeviceId=ShouldersOverHead;SharedAccessKey=/qWjUfO1hqVxW+moQsQqRUH69mXIiaMzOYm7jBemRyw=";

        public static void Main(string[] args)
        {
            Console.WriteLine("Device started");
            DeviceClient Client = DeviceClient.CreateFromConnectionString(ShouldersOverHead, TransportType.Mqtt);

            Client.SetMethodHandlerAsync("start", StartMethod.OnStart, null).Wait();
            Client.SetMethodHandlerAsync("stop", StopMethod.OnStop, null).Wait();
            Console.ReadLine();

            Client.SetMethodHandlerAsync("start", null, null).Wait();
            Client.SetMethodHandlerAsync("stop", null, null).Wait();
            Client.CloseAsync().Wait();
        }
    }
}
