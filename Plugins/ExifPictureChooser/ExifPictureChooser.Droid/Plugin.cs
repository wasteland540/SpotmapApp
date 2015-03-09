using Cirrious.CrossCore;
using Cirrious.CrossCore.Plugins;

namespace KS21.MvvmCross.Plugins.ExifPictureChooser.Droid
{
    public class Plugin : IMvxPlugin
    {
        public void Load()
        {
            //Mvx.RegisterSingleton<Interface>(new ImplementierendeKlasse());
            //oder
            //Mvx.RegisterType<Interface, ImplementierendeKlasseTyp>();

            Mvx.RegisterSingleton<IExifPictureChooserTask>(new ExifPictureChooserTask());
        }
    }
}