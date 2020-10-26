import random
import time

# Using the Python Device SDK for IoT Hub:
#   https://github.com/Azure/azure-iot-sdk-python
from azure.iot.device import IoTHubDeviceClient, Message
from MiFloraScanner import MiFloraScanner
from MiFloraData import MiFloraData

# The device connection string to authenticate the device with your IoT hub.
# Using the Azure CLI:
# az iot hub device-identity show-connection-string --hub-name {YourIoTHubName} --device-id MyNodeDevice --output table
CONNECTION_STRING = "<YOUR_IOT_HUB_CONNECTION_STRING>"

def initIoTHubClient():
    # Create an IoT Hub client
    iothubClient = IoTHubDeviceClient.create_from_connection_string(CONNECTION_STRING)
    return iothubClient

def runIoTHubClient():

    try:
        iothubClient = initIoTHubClient()
        print ( "IoT Hub device sending periodic messages, press Ctrl-C to exit" )

        while True:

            miFloraScanner = MiFloraScanner(5)
            
            floraMACS = miFloraScanner.getMACS()
            
            for mac in floraMACS:
                #print(mac)
                miFloraData = MiFloraData(mac)
                
                # Build the message with miFloraData telemetry values.
                message = miFloraData.scan()
                #print(message)
                
                # Send the message.
                #print( "Sending message: {}".format(message) )
                iothubClient.send_message(message)
                #print ( "Message successfully sent" )

    except KeyboardInterrupt:
        print ( "IoTHubClient sample stopped" )

if __name__ == '__main__':
    print ( "IoT Hub - Messages from Flora devices" )
    print ( "Press Ctrl-C to exit" )
    runIoTHubClient()
