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
          "MI_ID": self.poller._mac,
          "MI_BATTERY": self.poller.parameter_value(MI_BATTERY) ,
          "MI_CONDUCTIVITY": self.poller.parameter_value(MI_CONDUCTIVITY),
          "MI_LIGHT": self.poller.parameter_value(MI_LIGHT),
          "MI_MOISTURE": self.poller.parameter_value(MI_MOISTURE),
          "MI_TEMPERATURE": self.poller.parameter_value(MI_TEMPERATURE)
        }
        # convert into JSON:
        return json.dumps(data)
