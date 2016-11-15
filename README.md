# IQRF Azure Demo

## Poadavky
* Notebook s Windows 10
* Visual Studio 2015 ([comunity edice](https://www.visualstudio.com/cs/vs/community/))
* Raspberry Pi + SD karta
* [Windows 10 IoT Core Dashboard](https://developer.microsoft.com/en-us/windows/iot/docs/iotdashboardtroubleshooting)

## Postup
### Zaloení Azure Subscription
Podle návodu na [aka.ms/cz/azurepass](https://aka.ms/cz/azurepass) si aktivujte Azure Subscription. Pøípadnì je moné pouít u existující Azure Subscription.

### Pøíprava Azure prostøedí
Pro demo budeme potøebovat nìkolik slueb pro zpracování a vizualizaci dat.
1. Vytvoøte `Skupinu prostøedkù`
![Resource group](https://github.com/pospanet/IQRF_hackathon/tree/master/images/Azure1.PNG)
![Resource group](https://github.com/pospanet/IQRF_hackathon/tree/master/images/Azure2.PNG)
2. Vytvoøte `IoT Hub`
![IoT Hub](https://github.com/pospanet/IQRF_hackathon/tree/master/images/Azure3.PNG)
3. Vytvoøte `Streaming Analytics Job`
![Streaming Analytics Job](https://github.com/pospanet/IQRF_hackathon/tree/master/images/Azure4.PNG)
4. Nastavte Stream Analytics Job (SAJ)
   1. Jako novı vstup z IoT hubu
![Streaming Analytics Job](https://github.com/pospanet/IQRF_hackathon/tree/master/images/Azure5.PNG)
   2. Jako novı vıstup pouijte Power BI
![Streaming Analytics Job](https://github.com/pospanet/IQRF_hackathon/tree/master/images/Azure6.PNG)
   3. Pøekopírujte následující transformaci:
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
1. Pomocí [Windows 10 IoT Core Dashboard](https://developer.microsoft.com/en-us/windows/iot/docs/iotdashboardtroubleshooting) nahrajte na SD kartu Windows 10 IoT core
![Win10IoTdashboard](https://github.com/pospanet/IQRF_hackathon/tree/master/images/IoTdashboard.PNG)
   1. Vyberte záloku `Setup new device`
   2. Podle obrázku vyplòte pololíèka. Název a kapacita disku u políèka `Drive`
   3. Volitelnì lze vybrat WiFi sí, ke které se má Raspberry Pi pøipojit
2. Vlote SD kartu do Raspberry Pi, pøipojte monitor a následnì napájení. Pøi prvním spuštìní se provede samotná instalace.
4. Stáhnìte a zkompilujte [IQRF Gateway emulátor](https://github.com/pospanet/IQRF_hackathon/tree/master/IoT%20simulator)

### Vytvoøení zaøízení v IoT Hubu
1. Stáhnìte a zkompilujte [Device Explorer](https://github.com/Azure/azure-iot-sdks/tree/master/tools/DeviceExplorer)
2. Z Azure portálu zkopírujte `Connection String` pro IoT Hub
![Win10IoTdashboard](https://github.com/pospanet/IQRF_hackathon/tree/master/images/IoTHub1.PNG)
3. Vlote ho do Device Exploreru
![Win10IoTdashboard](https://github.com/pospanet/IQRF_hackathon/tree/master/images/IoTHub2.PNG)
4. V Device Exploreru vytvoøte nové zaøízení
![Win10IoTdashboard](https://github.com/pospanet/IQRF_hackathon/tree/master/images/IoTHub3.PNG)
5. Zkopírujte Connection String pro novì vytvoøené zaøízení.

### Spuste IQRF Gateway emulátor
1. Z Visual Studia nastavte deploy na zaøízení a nastavte IP adressu Raspberry Pi
![VisualStudio](https://github.com/pospanet/IQRF_hackathon/tree/master/images/IDE.PNG)
2. Vlote do elulátoru Connection String zaøízení a spuste jej.
![VisualStudio](https://github.com/pospanet/IQRF_hackathon/tree/master/images/Emulator.PNG)

### Power BI
V rámci Power BI zkuste rùzné vizualizace dat.
![PowerBI](https://github.com/pospanet/IQRF_hackathon/tree/master/images/PowerBI.PNG)
