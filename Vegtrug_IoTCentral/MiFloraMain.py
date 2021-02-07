from MiFloraData import MiFloraData
from configparser import ConfigParser
from azure.iot.device.aio import ProvisioningDeviceClient
from azure.iot.device.aio import IoTHubDeviceClient
from azure.iot.device import  Message
from azure.iot.device import MethodResponse


import asyncio
import random
import uuid
import json
import logging
import sys

# writing to stdout                                                     
handler = logging.StreamHandler(sys.stdout)                             
handler.setLevel(logging.INFO)
log_format = logging.Formatter('[%(asctime)s] [%(levelname)s] - %(message)s')
handler.setFormatter(log_format)
log = logging.getLogger(__name__)                                  
log.addHandler(handler)

# PROVISION DEVICE
async def provision_device(provisioning_host, id_scope, registration_id, symmetric_key):
    provisioning_device_client = ProvisioningDeviceClient.create_from_symmetric_key(
        provisioning_host=provisioning_host,
        registration_id=registration_id,
        id_scope=id_scope,
        symmetric_key=symmetric_key
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
        log.exception(e)


async def scan(name, mac):
    miFloraData = MiFloraData(mac)
    # Build the message with miFloraData telemetry values.
    message = miFloraData.scan()
    return message
    


# MAIN STARTS
async def main():

    # Load configuration file
    configParser = ConfigParser() 
    configParser.read('config.ini')
    
    provisioning_host = configParser['IoTCentral']['Host']
    id_scope = configParser['IoTCentral']['IDScope']
    registration_id = configParser['IoTCentral']['DeviceId']
    symmetric_key = configParser['IoTCentral']['PrimaryKey']
    
    log.debug(provisioning_host)
    print(provisioning_host)
    
    registration_result = await provision_device(
                provisioning_host, id_scope, registration_id, symmetric_key
    )

    if registration_result.status == "assigned":
            device_client = IoTHubDeviceClient.create_from_symmetric_key(
                symmetric_key=symmetric_key,
                hostname=registration_result.registration_state.assigned_hub,
                device_id=registration_result.registration_state.device_id,
            )
            
            log.debug(device_client.connected)
            print(device_client.connected)
    else:
        raise RuntimeError(
            "Could not provision device. Aborting Plug and Play device connection."
        )

    # Connect the client.
    await device_client.connect()
    
    while True:
        # command 
        command_request = await device_client.receive_method_request("ScanNow")    
        
        for [name, mac] in configParser['macs'].items():
            message = await scan(name, mac)    
            log.debug(name +": " + message)
            print(name +": " + message)
            await send_telemetry(device_client, message, name.capitalize())
        
        method_response = MethodResponse.create_from_method_request(command_request, 200)
        await device_client.send_method_response(method_response)
            

    # Close
    await device_client.disconnect()




# EXECUTE MAIN
if __name__ == "__main__":
    asyncio.run(main())