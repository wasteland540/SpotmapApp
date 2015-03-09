using Android.Content;
using Cirrious.CrossCore;
using Cirrious.CrossCore.Platform;
using Cirrious.MvvmCross.Droid.Platform;
using Cirrious.MvvmCross.ViewModels;
using Spotmap.Core.Services;
using Spotmap.Droid.Services;

namespace Spotmap.Droid
{
    public class Setup : MvxAndroidSetup
    {
        public Setup(Context applicationContext) : base(applicationContext)
        {
        }

        protected override IMvxApplication CreateApp()
        {
            Mvx.RegisterSingleton<INullImageHelper>(new DroidNullImageHelper());

            return new Core.App();
        }
		
        protected override IMvxTrace CreateDebugTrace()
        {
            return new DebugTrace();
        }
    }
}