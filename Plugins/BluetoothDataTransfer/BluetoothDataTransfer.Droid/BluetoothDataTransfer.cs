using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Android.Bluetooth;
using Android.Content;
using Cirrious.CrossCore;
using Cirrious.CrossCore.Droid;
using Java.Util;

namespace KS21.MvvmCross.Plugins.BluetoothDataTransfer.Droid
{
    public class BluetoothDataTransfer : IBluetoothDataTransfer
    {
        private readonly BluetoothAdapter _bluetoothAdapter;
        private ICollection<BluetoothDevice> _bondedDevices;

        public BluetoothDataTransfer()
        {
            _bluetoothAdapter = BluetoothAdapter.DefaultAdapter;
        }

        public void TurnOn()
        {
            if (!_bluetoothAdapter.IsEnabled)
            {
                var globals = Mvx.Resolve<IMvxAndroidGlobals>();
                var turnOn = new Intent(BluetoothAdapter.ActionRequestEnable);
                turnOn.AddFlags(ActivityFlags.NewTask);
                globals.ApplicationContext.StartActivity(turnOn);
            }
        }

        public void TurnOff()
        {
            _bluetoothAdapter.Disable();
        }

        public void Discoverable()
        {
            var globals = Mvx.Resolve<IMvxAndroidGlobals>();

            var getVisible = new Intent(BluetoothAdapter.ActionRequestDiscoverable);
            getVisible.AddFlags(ActivityFlags.NewTask);
            globals.ApplicationContext.StartActivity(getVisible);
        }

        public List<string> GetPairedDevices()
        {
            _bondedDevices = _bluetoothAdapter.BondedDevices;

            return _bondedDevices.Select(bt => bt.Name).ToList();
        }

        public void ConnectToDevice()
        {
            throw new NotImplementedException();
        }

        public void SendData(string deviceName)
        {
            //var device = (BluetoothDevice)_bluetoothAdapter.BondedDevices.Select(bt => bt.Name == deviceName).FirstOrDefault();

            BluetoothDevice device = null;
            foreach (BluetoothDevice bondedDevice in _bluetoothAdapter.BondedDevices)
            {
                if (bondedDevice.Name == deviceName)
                {
                    device = bondedDevice;
                }
            }

            //TODO:
            if (device != null)
            {
                UUID uuid = UUID.RandomUUID();
                var socket = device.CreateRfcommSocketToServiceRecord(uuid);

                var thread = new Thread(() => BtConnect(socket));
                thread.Start();
            }
        }

        private void BtConnect(BluetoothSocket socket)
        {
            try
            {
                socket.Connect();
                //var stream = socket.InputStream;
                var thread = new Thread(() => BtWrite(socket));
                thread.Start();
            }
            catch(Exception e)
            {

            }
            finally
            {
                socket.Close();
            }
        }

        private void BtWrite(BluetoothSocket socket)
        {
            
        }
    }
}