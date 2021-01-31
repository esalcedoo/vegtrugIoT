from miflora.miflora_poller import MiFloraPoller
from btlewrap.bluepy import BluepyBackend
from miflora.miflora_poller import MiFloraPoller, MI_BATTERY, MI_CONDUCTIVITY, MI_LIGHT, MI_MOISTURE, MI_TEMPERATURE
import json

class MiFloraData():
    
    def __init__(self, mac):
        self.poller = MiFloraPoller(mac, BluepyBackend)
    
    def scan(self):
        self.poller.fill_cache()
        
        data = {
          "MAC": self.poller._mac,
          "Battery": self.poller.parameter_value(MI_BATTERY) ,
          "Fertility": self.poller.parameter_value(MI_CONDUCTIVITY),
          "Light": self.poller.parameter_value(MI_LIGHT),
          "Humidity": self.poller.parameter_value(MI_MOISTURE),
          "Temperature": self.poller.parameter_value(MI_TEMPERATURE)
        } 


        # convert into JSON:
        return json.dumps(data)




