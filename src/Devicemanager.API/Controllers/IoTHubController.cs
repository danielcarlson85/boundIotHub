// -------------------------------------------------------------------------------------------------
// Copyright (c) Bound Technologies AB. All rights reserved.
// -------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Devicemanager.API.Dtos.IoTHubDevice;
using Devicemanager.API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Devicemanager.API.Controllers
{
    /// <summary>
    /// Main controller for IoTHubDevices.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class IoTHubController : Controller
    {
        public IoTHubController(IIoTHubManager iIoTHubManager)
        {
            this.IIoTHubManager = iIoTHubManager;
        }

        public IIoTHubManager IIoTHubManager { get; }

        /// <summary>
        /// Returns all registered IoTHubDevices.
        /// </summary>
        /// <returns>Returns all iothub devices.</returns>
        [Produces(typeof(List<IoTHubDeviceRequest>))]
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var iotHubDevices = await this.IIoTHubManager.GetAllIoTDevices();
            return this.Ok(iotHubDevices);
        }

        /// <summary>
        /// Returns a specific IoTHubDevice.
        /// </summary>
        /// <returns>Returns the iothubdevice.</returns>
        /// <param name="ioTHubDeviceName">The name of the IoTHubDevice to send text to.</param>
        [Produces(typeof(IoTHubDeviceResponse))]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [HttpGet("{iothubdevicensame}")]
        public async Task<IActionResult> GetAsync(string ioTHubDeviceName)
        {
            if (ioTHubDeviceName is null)
            {
                throw new System.ArgumentNullException(nameof(ioTHubDeviceName));
            }

            return this.Ok(await this.IIoTHubManager.Get(ioTHubDeviceName));
        }

        /// <summary>
        /// Sends a text message to a specific IoTHubDevice.
        /// </summary>
        /// <param name="ioTHubDeviceName">The name of the IoTHubDevice to send text to.</param>
        /// <param name="textToSend">The text to send to the IoTHubDevice.</param>
        /// <returns>Returns the status of sending the message.</returns>
        [Produces(typeof(IoTHubDeviceResponse))]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [HttpGet("device/send")]
        public async Task<IActionResult> SendDataToIoTHubDeviceAsync(string ioTHubDeviceName, string textToSend)
        {
            var iotdevice = await this.IIoTHubManager.Get(ioTHubDeviceName);

            await this.IIoTHubManager.SendDataToIoTHubDevice(iotdevice, textToSend);

            return this.Ok($"Message {textToSend} sent to device {ioTHubDeviceName}");
        }

        /// <summary>
        /// Sends a request to start the specific device.
        /// </summary>
        /// <param name="ioTHubDeviceName">The name of the IoTHubDevice to start.</param>
        /// <returns>Returns the status of sending the message.</returns>
        [Produces(typeof(IoTHubDeviceResponse))]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [HttpGet("device/start")]
        public async Task<IActionResult> SendStartRequestOnDevice(string ioTHubDeviceName)
        {
            var iotdevice = await this.IIoTHubManager.Get(ioTHubDeviceName);

            var result = await this.IIoTHubManager.SendStartRequestToDevice(iotdevice);

            return this.Ok($"Result: {result}");
        }

        /// <summary>
        /// Sends a request to start the specific device.
        /// </summary>
        /// <param name="ioTHubDeviceName">The name of the IoTHubDevice to start.</param>
        /// <returns>Returns the status of sending the message.</returns>
        [Produces(typeof(IoTHubDeviceResponse))]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [HttpGet("device/stop")]
        public async Task<IActionResult> SendStopRequestOnDevice(string ioTHubDeviceName)
        {
            var iotdevice = await this.IIoTHubManager.Get(ioTHubDeviceName);

            var result = await this.IIoTHubManager.SendStopRequestToDevice(iotdevice);

            return this.Ok($"Result: {result}");
        }

        /// <summary>
        /// Creates a new IoTHubDevice.
        /// </summary>
        /// <param name="request">The IoTHubRequest model to create.</param>
        /// <returns>Returns the status of the creation of the iothub device.</returns>
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] IoTHubDeviceRequest request)
        {
            var id = await this.IIoTHubManager.Create(request.IoTHubName);
            return this.Ok(id);
        }

        /// <summary>
        /// Update the current IoTHubDevice.
        /// </summary>
        /// <param name="ioTHubDeviceRequest">The update IoTHubDevice model.</param>
        /// <param name="iotHubDeviceName">The id of the IoTHubDevice to update.</param>
        /// <returns>Returns the status of the updated iothub device.</returns>
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [Produces(typeof(IoTHubDeviceResponse))]
        [HttpPut("device")]
        public async Task<IActionResult> PutAsync([FromBody] IoTHubDeviceRequest ioTHubDeviceRequest, string iotHubDeviceName)
        {
            return await Task.FromResult(this.NoContent());
        }

        /// <summary>
        /// Deletes the specified IoTHubDevice.
        /// </summary>
        /// <param name = "iotHubDeviceName" > The id of the IoTHubDevice to delete.</param>
        /// <returns>Returns the status of deleted iothub device.</returns>
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [HttpDelete("device")]
        public async Task<IActionResult> DeleteAsync(int iotHubDeviceName)
        {
            return await Task.FromResult(this.NoContent());
        }
    }
}