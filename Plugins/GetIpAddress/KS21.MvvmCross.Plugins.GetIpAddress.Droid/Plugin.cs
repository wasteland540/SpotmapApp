using Cirrious.CrossCore;
using Cirrious.CrossCore.Plugins;

namespace KS21.MvvmCross.Plugins.GetIpAddress.Droid
{
    public class Plugin : IMvxPlugin
    {
        public void Load()
        {
            //Mvx.RegisterSingleton<Interface>(new ImplementierendeKlasse());
            //oder
            //Mvx.RegisterType<Interface, ImplementierendeKlasseTyp>();

            Mvx.RegisterSingleton<IGetIpAddressTask>(new GetIpAddressTask());
        }
    }
}