
from configparser import ConfigParser
from azure.iot.device.aio import ProvisioningDeviceClient
from azure.iot.device.aio import IoTHubDeviceClient
from azure.iot.device import  Message
from azure.iot.device import MethodResponse
from MiFloraData import MiFloraData


import asyncio
import random
import uuid
import json


class Configuration:
    def __init__(self):
        # Load configuration file
        configParser = ConfigParser() 
        configParser.read('config.ini')
        self.provisioning_host = configParser['IoTCentral']['Host']
        self.id_scope = configParser['IoTCentral']['IDScope']
        self.registration_id = configParser['IoTCentral']['DeviceId']
        self.symmetric_key = configParser['IoTCentral']['PrimaryKey']
        self.macs = configParser['macs'].items()



# PROVISION DEVICE
async def provision_device(provisioning_host, id_scope, registration_id, symmetric_key):
    provisioning_device_client = ProvisioningDeviceClient.create_from_symmetric_key(
        provisioning_host=provisioning_host,
        registration_id=registration_id,
        id_scope=id_scope,
        symmetric_key=symmetric_key,
    )
    return await provisioning_device_client.register()


async def send_telemetry(device_client, message, name):
    try:
        msg = Message(message)
        msg.custom_properties["$.sub"] = name
        msg.content_encoding = "utf-8"
        msg.content_type = "application/json"
        await device_client.send_message(msg)

    except Exception as e:
        print(e)


async def scan(name, mac):
    miFloraData = MiFloraData(mac)
    # Build the message with miFloraData telemetry values.
    message = miFloraData.scan()
    return message


async def sending_vegtrug_telemetry(device_client, vegtrugs):    
     while True:
        print("Sending every minute vegtrug telemetry... ")
        await send_vegtrug_data (device_client, vegtrugs)        
        await asyncio.sleep(60)


async def send_vegtrug_data (device_client, vegtrugs):
    for [name, mac] in vegtrugs:
            message = await scan(name, mac)    
            print(name +": " + message)
            await send_telemetry(device_client, message, name.capitalize())



# MAIN STARTS
async def main():

    # Load configuration file
    configuration = Configuration()

    registration_result = await provision_device(
                configuration.provisioning_host, 
                configuration.id_scope, 
                configuration.registration_id, 
                configuration.symmetric_key
    )

    if registration_result.status == "assigned":
            device_client = IoTHubDeviceClient.create_from_symmetric_key(
                symmetric_key=configuration.symmetric_key,
                hostname=registration_result.registration_state.assigned_hub,
                device_id=registration_result.registration_state.device_id,
            )
    else:
        raise RuntimeError(
            "Could not provision device. Aborting Plug and Play device connection."
        )

    # Connect the client.
    await device_client.connect()

    # thread that send the telemetry
    send_telemetry_task = asyncio.create_task(sending_vegtrug_telemetry(device_client, configuration.macs))

    while True:
        # command 
        #registring C2D method
        command_request = await device_client.receive_method_request("ScanNow")   
        
        print("[Scan now...!!!]")        
        await send_vegtrug_data(device_client, configuration.macs)        
        
        method_response = MethodResponse.create_from_method_request(command_request, 200)
        await device_client.send_method_response(method_response)
            

    # Close
    await device_client.disconnect()




# EXECUTE MAIN
if __name__ == "__main__":
    asyncio.run(main())