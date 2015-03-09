using Android.App;
using Android.OS;
using Android.Views;
using Cirrious.MvvmCross.Droid.Views;
using Spotmap.Core.ViewModels;

namespace Spotmap.Droid.Views
{
    [Activity(Label = "@string/SpotGpsListViewLabel")]
    public class SpotGpsListView : MvxActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.SpotGpsListView);
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.SpotGpsList_Menu, menu);

            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.shareSpots:
                    ShareSpots();
                    return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        private void ShareSpots()
        {
            ((SpotGpsListViewModel) ViewModel).ShareSpotsCommand.Execute(null);
        }
    }
}