
import random
import time

# Using the Python Device SDK for IoT Hub:
#   https://github.com/Azure/azure-iot-sdk-python
from azure.iot.device import IoTHubDeviceClient, Message
from MiFloraData import MiFloraData


from configparser import ConfigParser

def initIoTHubClient(connectionString):
    # Create an IoT Hub client
    iothubClient = IoTHubDeviceClient.create_from_connection_string(connectionString)
    return iothubClient

def runMain():

    try:
        # Load configuration file
        configParser = ConfigParser() 
        configParser.read('config.ini')

        iothubClient = initIoTHubClient(configParser['ConnectionStrings']['IoTHub'])
        print (iothubClient)
        print ( "IoT Hub device sending periodic messages" )
        while True:  
            for [n, mac] in configParser['macs'].items():
                miFloraData = MiFloraData(mac)

                # Build the message with miFloraData telemetry values.
                message = miFloraData.scan()
                
                # Send the message.
                print(message)
                iothubClient.send_message(message)
                
            time.sleep(10) # once per hour
    except KeyboardInterrupt:
        print ( "IoTHubClient sample stopped" )

if __name__ == '__main__':
    runMain()