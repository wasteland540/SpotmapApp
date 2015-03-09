using Android.App;
using Android.OS;
using Cirrious.MvvmCross.Droid.Views;

namespace Spotmap.Droid.Views
{
    [Activity(Label = "@string/GetSharedSpotViewLabel")]
    public class GetSharedSpotView : MvxActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.GetSharedSpotView);
        }
    }
}