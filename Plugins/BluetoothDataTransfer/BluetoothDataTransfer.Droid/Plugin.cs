using Cirrious.CrossCore;
using Cirrious.CrossCore.Plugins;

namespace KS21.MvvmCross.Plugins.BluetoothDataTransfer.Droid
{
    public class Plugin : IMvxPlugin
    {
        public void Load()
        {
            //Mvx.RegisterSingleton<Interface>(new ImplementierendeKlasse());
            //oder
            //Mvx.RegisterType<Interface, ImplementierendeKlasseTyp>();

            Mvx.RegisterSingleton<IBluetoothDataTransfer>(new BluetoothDataTransfer());
        }
    }
}