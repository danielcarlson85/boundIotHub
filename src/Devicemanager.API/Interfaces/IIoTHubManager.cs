﻿// -------------------------------------------------------------------------------------------------
// Copyright (c) Bound Technologies AB. All rights reserved.
// -------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Devicemanager.API.Dtos.IoTHubDevice;
using Microsoft.Azure.Devices.Shared;

namespace Devicemanager.API.Interfaces
{
    public interface IIoTHubManager
    {
        Task<IoTHubDeviceResponse> Create(string deviceName);

        Task<IoTHubDeviceResponse> Get(string deviceId);

        Task<List<IoTHubDeviceResponse>> GetAll();

        Task<List<Twin>> GetAllIoTDevices();

        Task SendDataToIoTHubDevice(IoTHubDeviceResponse ioTHubDevice, string messageToSend);

        Task<HttpStatusCode> SendStartRequestToDevice(IoTHubDeviceResponse deviceName);
        Task<HttpStatusCode> SendStopRequestToDevice(IoTHubDeviceResponse deviceName);
    }
}