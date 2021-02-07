import random
import time
import logging

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

        while True:  
            for [name, mac] in configParser['macs'].items():
                miFloraData = MiFloraData(mac)

                # Build the message with miFloraData telemetry values.
                message = miFloraData.scan()
                
                # Send the message.
                logging.info(name +": " + message)
                iothubClient.send_message(message)
            time.sleep(10) # once per hour
    except Exception as e:
        logging.exception(e)

if __name__ == '__main__':
    runMain()