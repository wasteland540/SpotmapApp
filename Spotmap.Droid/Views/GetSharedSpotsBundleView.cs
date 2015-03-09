using Android.App;
using Android.OS;
using Android.Views;
using Cirrious.MvvmCross.Binding.Droid.Views;
using Cirrious.MvvmCross.Droid.Views;

namespace Spotmap.Droid.Views
{
    [Activity(Label = "@string/GetSharedSpotsBundleViewLabel")]
    public class GetSharedSpotsBundleView : MvxActivity
    {
        private MvxListView _mvxlv;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.GetSharedSpotsBundleView);

            _mvxlv = (MvxListView) FindViewById(Resource.Id.bundlesharelistview);
            _mvxlv.Touch += lv_Touch;
        }

        private void lv_Touch(object sender, View.TouchEventArgs e)
        {
            //Disable scrolling for parent scrollview, when listview is touching, so we can scroll our listview
            ((MvxListView) sender).Parent.RequestDisallowInterceptTouchEvent(true);
            e.Handled = false;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (disposing)
            {
                if (_mvxlv != null)
                {
                    _mvxlv.Touch -= lv_Touch;
                }
            }
        }
    }
}