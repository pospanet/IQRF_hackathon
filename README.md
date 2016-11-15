# IQRF Azure Demo

## Po�adavky
* Notebook s Windows 10
* Visual Studio 2015 ([comunity edice](https://www.visualstudio.com/cs/vs/community/))
* Raspberry Pi + SD karta
* [Windows 10 IoT Core Dashboard](https://developer.microsoft.com/en-us/windows/iot/docs/iotdashboardtroubleshooting)

## Postup
### Zalo�en� Azure Subscription
Podle n�vodu na [aka.ms/cz/azurepass](https://aka.ms/cz/azurepass) si aktivujte Azure Subscription. P��padn� je mo�n� pou��t u� existuj�c� Azure Subscription.

### P��prava Azure prost�ed�
Pro demo budeme pot�ebovat n�kolik slu�eb pro zpracov�n� a vizualizaci dat.
1. Vytvo�te `Skupinu prost�edk�`
![Resource group](https://github.com/pospanet/IQRF_hackathon/tree/master/images/Azure1.PNG)
![Resource group](https://github.com/pospanet/IQRF_hackathon/tree/master/images/Azure2.PNG)
2. Vytvo�te `IoT Hub`
![IoT Hub](https://github.com/pospanet/IQRF_hackathon/tree/master/images/Azure3.PNG)
3. Vytvo�te `Streaming Analytics Job`
![Streaming Analytics Job](https://github.com/pospanet/IQRF_hackathon/tree/master/images/Azure4.PNG)
4. Nastavte Stream Analytics Job (SAJ)
   1. Jako nov� vstup z IoT hubu
![Streaming Analytics Job](https://github.com/pospanet/IQRF_hackathon/tree/master/images/Azure5.PNG)
   2. Jako nov� v�stup pou�ijte Power BI
![Streaming Analytics Job](https://github.com/pospanet/IQRF_hackathon/tree/master/images/Azure6.PNG)
   3. P�ekop�rujte n�sleduj�c� transformaci:
```SQL
SELECT
    Device,
    System.Timestamp AS Time,
    Max(Value) AS MaxValue,
    Min(Value) AS MinValue,
    Avg(Value) AS AvgValue
INTO output
FROM input TIMESTAMP BY Time
GROUP BY
    TumblingWindow( minute , 1 ),
    Device
```
![Streaming Analytics Job](https://github.com/pospanet/IQRF_hackathon/tree/master/images/Azure7.PNG)

### Instalace Raspberry Pi
1. Pomoc� [Windows 10 IoT Core Dashboard](https://developer.microsoft.com/en-us/windows/iot/docs/iotdashboardtroubleshooting) nahrajte na SD kartu Windows 10 IoT core
![Win10IoTdashboard](https://github.com/pospanet/IQRF_hackathon/tree/master/images/IoTdashboard.PNG)
   1. Vyberte z�lo�ku `Setup new device`
   2. Podle obr�zku vypl�te polol��ka. N�zev a kapacita disku u pol��ka `Drive`
   3. Voliteln� lze vybrat WiFi s�, ke kter� se m� Raspberry Pi p�ipojit
2. Vlo�te SD kartu do Raspberry Pi, p�ipojte monitor a n�sledn� nap�jen�. P�i prvn�m spu�t�n� se provede samotn� instalace.
4. St�hn�te a zkompilujte [IQRF Gateway emul�tor](https://github.com/pospanet/IQRF_hackathon/tree/master/IoT%20simulator)

### Vytvo�en� za��zen� v IoT Hubu
1. St�hn�te a zkompilujte [Device Explorer](https://github.com/Azure/azure-iot-sdks/tree/master/tools/DeviceExplorer)
2. Z Azure port�lu zkop�rujte `Connection String` pro IoT Hub
![Win10IoTdashboard](https://github.com/pospanet/IQRF_hackathon/tree/master/images/IoTHub1.PNG)
3. Vlo�te ho do Device Exploreru
![Win10IoTdashboard](https://github.com/pospanet/IQRF_hackathon/tree/master/images/IoTHub2.PNG)
4. V Device Exploreru vytvo�te nov� za��zen�
![Win10IoTdashboard](https://github.com/pospanet/IQRF_hackathon/tree/master/images/IoTHub3.PNG)
5. Zkop�rujte Connection String pro nov� vytvo�en� za��zen�.

### Spus�te IQRF Gateway emul�tor
1. Z Visual Studia nastavte deploy na za��zen� a nastavte IP adressu Raspberry Pi
![VisualStudio](https://github.com/pospanet/IQRF_hackathon/tree/master/images/IDE.PNG)
2. Vlo�te do elul�toru Connection String za��zen� a spus�te jej.
![VisualStudio](https://github.com/pospanet/IQRF_hackathon/tree/master/images/Emulator.PNG)

### Power BI
V r�mci Power BI zkuste r�zn� vizualizace dat.
![PowerBI](https://github.com/pospanet/IQRF_hackathon/tree/master/images/PowerBI.PNG)
