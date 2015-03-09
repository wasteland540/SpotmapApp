using Android.Content;
using Android.Net.Wifi;
using Cirrious.CrossCore;
using Cirrious.CrossCore.Droid;

namespace KS21.MvvmCross.Plugins.GetIpAddress.Droid
{
    public class GetIpAddressTask : IGetIpAddressTask
    {
        public string GetIpAddress()
        {
            var globals = Mvx.Resolve<IMvxAndroidGlobals>();
            var wifiManager = globals.ApplicationContext
                .GetSystemService(Context.WifiService)
                as WifiManager;

            string ipAddressFormated = string.Empty;

            if (wifiManager != null)
            {
                WifiInfo wifiInfo = wifiManager.ConnectionInfo;
                int ipAddress = wifiInfo.IpAddress;

                ipAddressFormated = string.Format(
                    "{0}.{1}.{2}.{3}",
                    (ipAddress & 0xff),
                    (ipAddress >> 8 & 0xff),
                    (ipAddress >> 16 & 0xff),
                    (ipAddress >> 24 & 0xff));
            }

            return ipAddressFormated;
        }
    }
}