using Bound;
using Device;
using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using WorkoutData.Managers;

namespace DeviceManager.Device.DeviceMethods
{
    public class StopMethod
    {
       

        public static async Task<MethodResponse> OnStop(MethodRequest methodRequest, object userContext)
        {
            Program.IsRunning = false;
            Console.WriteLine("Device stopped");

            return null;
        }

    }
}
