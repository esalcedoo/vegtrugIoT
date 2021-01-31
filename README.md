```bash
python3 -m venv /path/to/new/virtual/environment
```
## Execute
```bash

git clone https://github.com/esalcedoo/vegtrugIoTHub.git
cd vegtrugIoTHub
cd RaspberryScripts


python3 -m venv floravenv
source floravenv/bin/activate
python -m pip install -r requirements.txt
```

! rename config.ini.template to config.ini and add your own settings.


check your vegtrug MAC address with:
```bash
sudo hcitool lescan | grep Flower
```
And run:

```
python MiFloraMain.py
```





