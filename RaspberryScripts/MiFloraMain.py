import random
import time

# Using the Python Device SDK for IoT Hub:
#   https://github.com/Azure/azure-iot-sdk-python
from azure.iot.device import IoTHubDeviceClient, Message
from MiFloraData import MiFloraData

# The device connection string to authenticate the device with your IoT hub.
# Using the Azure CLI:
# az iot hub device-identity show-connection-string --hub-name {YourIoTHubName} --device-id MyNodeDevice --output table
CONNECTION_STRING = "HostName=W4TTIoTHub.azure-devices.net;DeviceId=raspi4W4TT;SharedAccessKey=/Pd/TTkTRZLw/2vMxD5YMm/TlazTxyiyZHhnNvHddVc="

def initIoTHubClient():
    # Create an IoT Hub client
    iothubClient = IoTHubDeviceClient.create_from_connection_string(CONNECTION_STRING)
    return iothubClient

def runIoTHubClient():

    try:
        iothubClient = initIoTHubClient()
        print ( "IoT Hub device sending periodic messages, press Ctrl-C to exit" )

        while True:
            floraMACS = ["C4:7C:8D:6B:3B:DC","80:EA:CA:88:F5:5D"]
            
            for mac in floraMACS:
                miFloraData = MiFloraData(mac)

                # Build the message with miFloraData telemetry values.
                message = miFloraData.scan()
                
                # Send the message.
                print(message)
                iothubClient.send_message(message)
            time.sleep(1) # once per day
                    
    except KeyboardInterrupt:
        print ( "IoTHubClient sample stopped" )

if __name__ == '__main__':
    print ( "Flora devices" )
    print ( "Press Ctrl-C to exit" )
    runIoTHubClient()
