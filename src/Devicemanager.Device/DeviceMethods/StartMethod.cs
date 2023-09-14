using Bound;
using Device;
using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using WorkoutData.Managers;

namespace DeviceManager.Device.NewFolder
{
    public static class StartMethod
    {
        static string BlobsConnectionString = "DefaultEndpointsProtocol=https;AccountName=boundalgorithmapi;AccountKey=hYgYq3IOQw+FSYxnl+1pXoYsSOlskLVLZQ11GUmDSmOBwzioLg4OvUL4P4pG/PQ7/lbRiNarZ1/42WfYEJ3AMQ==;EndpointSuffix=core.windows.net";
        public static BlobsManager blobsManager = new BlobsManager(BlobsConnectionString);
        public static async Task<MethodResponse> OnStart(MethodRequest methodRequest, object userContext)
        {
            Program.IsRunning = true;
            Program.DeviceData = JsonConvert.DeserializeObject<DeviceData>(methodRequest.DataAsJson);
            Program.DeviceData.TrainingData = new List<TrainingData>();

            var filePath = CreateBlobPath(Program.DeviceData);


            while (Program.IsRunning)
            {
                Thread.Sleep(100);
                var x = new Random().Next(0, 256);
                var y = new Random().Next(0, 256);
                var z = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();


                Console.WriteLine($"{x},{y},{z}");

                Program.DeviceData.TrainingData.Add(new TrainingData() { X = x, Y = y, Z = z });

                if (Program.DeviceData.TrainingData.Count == 5000)
                {
                    Stopwatch stopwatch = new Stopwatch();
                    stopwatch.Start();

                    string trainingData = JsonConvert.SerializeObject(Program.DeviceData.TrainingData);
                    _ = await blobsManager.AppendDataInBlob(filePath, trainingData);
                    Program.DeviceData.TrainingData.Clear();

                    Debug.WriteLine(stopwatch.ElapsedMilliseconds + " Tog det att ladda upp datan");
                }
            }

            return null;
        }

        static string CreateBlobPath(DeviceData deviceData)
        {
            string blobPath = deviceData.ObjectId + "/";
            blobPath += deviceData.MachineName.ToLower() + "/";
            blobPath += DateTime.Now.ToString("yyyyMMdd") + ".txt";

            return blobPath;
        }
    }
}
