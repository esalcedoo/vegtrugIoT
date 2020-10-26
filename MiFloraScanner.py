from bluepy.btle import Scanner, DefaultDelegate
    
class MiFloraScanner():
    def __init__(self, scanTimeInterval):  
        self.scanner = Scanner()
        self.devices = self.scanner.scan(scanTimeInterval)
        
    def getMACS(self):
        macs = []
        for dev in self.devices:
                
            for (adtype, desc, value) in dev.getScanData():
                if desc == 'Complete Local Name' and value == 'Flower care':
                    macs.append(dev.addr)
        return macs
        
        
        
        
        
        
        