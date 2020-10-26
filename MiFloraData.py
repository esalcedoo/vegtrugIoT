from miflora.miflora_poller import MiFloraPoller
from btlewrap.bluepy import BluepyBackend
from miflora.miflora_poller import MiFloraPoller, MI_BATTERY, MI_CONDUCTIVITY, MI_LIGHT, MI_MOISTURE, MI_TEMPERATURE
import json

class MiFloraData():
    
    def __init__(self, mac):
        self.poller = MiFloraPoller(mac, BluepyBackend)
    
    def scan(self):
        print(self.poller.name())
        self.poller.fill_cache()
        
#         print('MI_BATTERY. ' + str(self.poller.parameter_value(MI_BATTERY)))
#         print('MI_CONDUCTIVITY: ' + str(self.poller.parameter_value(MI_CONDUCTIVITY)))
#         print('MI_LIGHT: ' + str(self.poller.parameter_value(MI_LIGHT)))
#         print('MI_MOISTURE: ' + str(self.poller.parameter_value(MI_MOISTURE)))
#         print('MI_TEMPERATURE: ' + str(self.poller.parameter_value(MI_TEMPERATURE)))

        data = {
          "MI_BATTERY": self.poller.parameter_value(MI_BATTERY) ,
          "MI_CONDUCTIVITY": self.poller.parameter_value(MI_CONDUCTIVITY),
          "MI_LIGHT": self.poller.parameter_value(MI_LIGHT),
          "MI_MOISTURE": self.poller.parameter_value(MI_MOISTURE),
          "MI_TEMPERATURE": self.poller.parameter_value(MI_TEMPERATURE)
        }
        # convert into JSON:
        return json.dumps(data)
