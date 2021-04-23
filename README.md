
You can watch the talk about this demo here: https://www.youtube.com/watch?v=h2M8Nc6d1og (in Spanish)

## Import template:

- Go to your IoT Central Portal > Device templates -> +New
- Create a custom device template > IoT device 
- Device template name -> FloraComponentsTemplate (or any name)
- Create a model -> Import a model. 

- Add views (they are not automatically imported :(  )


- Publish

## Create a new device:

- Device > +New
- Device template > FloraComponentsTemplate
- Devide name -> the default or whatever you want (raspisa4)
- Device ID -> this will be added to the config.ini file

## Config.ini file: 

- Devices > select the device >  "Connect" (icon in the top right)
- Device connection
- Fill ID Scope, Device ID, Primary Key.

<br >


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

