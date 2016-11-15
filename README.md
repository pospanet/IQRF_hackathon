# IQRF Azure Demo

## Požadavky
* Notebook s Windows 10
* Visual Studio 2015 ([comunity edice](https://www.visualstudio.com/cs/vs/community/))
* Raspberry Pi + SD karta
* [Windows 10 IoT Core Dashboard](https://developer.microsoft.com/en-us/windows/iot/docs/iotdashboardtroubleshooting)

## Postup
### Založení Azure Subscription
Podle návodu na [aka.ms/cz/azurepass](https://aka.ms/cz/azurepass) si aktivujte Azure Subscription. Případně je možné použít už existující Azure Subscription.

### Příprava Azure prostředí
Pro demo budeme potřebovat několik služeb pro zpracování a vizualizaci dat.
1. Vytvořte `Skupinu prostředků`
![Resource group](https://github.com/pospanet/IQRF_hackathon/blob/master/images/Azure1.PNG)
![Resource group](https://github.com/pospanet/IQRF_hackathon/blob/master/images/Azure2.PNG)
2. Vytvořte `IoT Hub`
![IoT Hub](https://github.com/pospanet/IQRF_hackathon/blob/master/images/Azure3.PNG)
3. Vytvořte `Streaming Analytics Job`
![Streaming Analytics Job](https://github.com/pospanet/IQRF_hackathon/blob/master/images/Azure4.PNG)
4. Nastavte Stream Analytics Job (SAJ)
   1. Jako nový vstup z IoT hubu
![Streaming Analytics Job](https://github.com/pospanet/IQRF_hackathon/blob/master/images/Azure5.PNG)
   2. Jako nový výstup použijte Power BI
![Streaming Analytics Job](https://github.com/pospanet/IQRF_hackathon/blob/master/images/Azure6.PNG)
   3. Překopírujte následující transformaci:
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
![Streaming Analytics Job](https://github.com/pospanet/IQRF_hackathon/blob/master/images/Azure7.PNG)

### Instalace Raspberry Pi
1. Pomocí [Windows 10 IoT Core Dashboard](https://developer.microsoft.com/en-us/windows/iot/docs/iotdashboardtroubleshooting) nahrajte na SD kartu Windows 10 IoT core
![Win10IoTdashboard](https://github.com/pospanet/IQRF_hackathon/blob/master/images/IoTdashboard.PNG)
   1. Vyberte záložku `Setup new device`
   2. Podle obrázku vyplňte pololíčka. Název a kapacita disku u políčka `Drive`
   3. Volitelně lze vybrat WiFi síť, ke které se má Raspberry Pi připojit
2. Vložte SD kartu do Raspberry Pi, připojte monitor a následně napájení. Při prvním spuštění se provede samotná instalace.
4. Stáhněte a zkompilujte [IQRF Gateway emulátor](https://github.com/pospanet/IQRF_hackathon/tree/master/IoT%20simulator)

### Vytvoření zařízení v IoT Hubu
1. Stáhněte a zkompilujte [Device Explorer](https://github.com/Azure/azure-iot-sdks/tree/master/tools/DeviceExplorer)
2. Z Azure portálu zkopírujte `Connection String` pro IoT Hub
![Win10IoTdashboard](https://github.com/pospanet/IQRF_hackathon/blob/master/images/IoTHub1.PNG)
3. Vložte ho do Device Exploreru
![Win10IoTdashboard](https://github.com/pospanet/IQRF_hackathon/blob/master/images/IoTHub2.PNG)
4. V Device Exploreru vytvořte nové zařízení
![Win10IoTdashboard](https://github.com/pospanet/IQRF_hackathon/blob/master/images/IoTHub3.PNG)
5. Zkopírujte Connection String pro nově vytvořené zařízení.

### Spusťte IQRF Gateway emulátor
1. Z Visual Studia nastavte deploy na zařízení a nastavte IP adressu Raspberry Pi
![VisualStudio](https://github.com/pospanet/IQRF_hackathon/blob/master/images/IDE.PNG)
2. Vložte do elulátoru Connection String zařízení a spusťte jej.
![VisualStudio](https://github.com/pospanet/IQRF_hackathon/blob/master/images/Emulator.PNG)

### Power BI
V rámci Power BI zkuste různé vizualizace dat.
![PowerBI](https://github.com/pospanet/IQRF_hackathon/blob/master/images/PowerBI.PNG)
