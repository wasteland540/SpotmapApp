using Android.App;
using Android.OS;
using Android.Widget;
using Cirrious.MvvmCross.Droid.Views;
using Spotmap.Core.ViewModels;

namespace Spotmap.Droid.Views
{
    [Activity(Label = "@string/MainViewLabel")]
    public class MainView : MvxTabActivity
    {
        protected MainViewModel MainViewModel
        {
            get { return ViewModel as MainViewModel; }
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.MainView);

            TabHost.TabSpec spec = TabHost.NewTabSpec("Spotmap");
            spec.SetIndicator("", Resources.GetDrawable(Resource.Drawable.Tab_Spotmap));
            spec.SetContent(this.CreateIntentFor(MainViewModel.SpotmapViewModel));
            TabHost.AddTab(spec);

            spec = TabHost.NewTabSpec("AddSpot");
            spec.SetIndicator("", Resources.GetDrawable(Resource.Drawable.Tab_Addspot));
            spec.SetContent(this.CreateIntentFor(MainViewModel.AddSpotViewModel));
            TabHost.AddTab(spec);

            spec = TabHost.NewTabSpec("SpotGpsList");
            spec.SetIndicator("", Resources.GetDrawable(Resource.Drawable.Tab_Spotgpslist));
            spec.SetContent(this.CreateIntentFor(MainViewModel.SpotGpsListViewModel));
            TabHost.AddTab(spec);

            spec = TabHost.NewTabSpec("SpotList");
            spec.SetIndicator("", Resources.GetDrawable(Resource.Drawable.Tab_Spotlistwithoutgps));
            spec.SetContent(this.CreateIntentFor(MainViewModel.SpotListViewModel));
            TabHost.AddTab(spec);
        }
    }
}