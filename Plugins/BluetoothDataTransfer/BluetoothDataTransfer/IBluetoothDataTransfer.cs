using System.Collections.Generic;

namespace KS21.MvvmCross.Plugins.BluetoothDataTransfer
{
    public interface IBluetoothDataTransfer
    {
        void TurnOn();

        void TurnOff();

        void Discoverable();

        List<string> GetPairedDevices();

        void ConnectToDevice();

        void SendData(string deviceName);
    }
}