using Android.App;
using Android.OS;
using Android.Views;
using Cirrious.MvvmCross.Droid.Views;
using Spotmap.Core.ViewModels;

namespace Spotmap.Droid.Views
{
    [Activity(Label = "@string/SpotDetailViewLabel")]
    public class SpotDetailView : MvxActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.SpotDetailView);
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.SpotDetail_Menu, menu);

            //Enable/Disable showOnMap-menuItem, based on locationKown
            var viewModel = ((SpotDetailViewModel)ViewModel);
            var menuItem = menu.FindItem(Resource.Id.showOnMap);
            menuItem.SetVisible(viewModel.LocationKown);

            var menuItem2 = menu.FindItem(Resource.Id.shareSpot);
            menuItem2.SetVisible(viewModel.LocationKown);

            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.deleteSpot:
                    DeleteSpot();
                    return true;
                
                case Resource.Id.showOnMap:
                    ShowOnMap();
                    return true;

                case Resource.Id.shareSpot:
                    ShareSpot();
                    return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        private void DeleteSpot()
        {
            ((SpotDetailViewModel) ViewModel).DeleteCommand.Execute(null);
        }

        private void ShowOnMap()
        {
            ((SpotDetailViewModel)ViewModel).ShowOnMapCommand.Execute(null);
        }

        private void ShareSpot()
        {
            ((SpotDetailViewModel)ViewModel).ShareSpotCommand.Execute(null);
        }
    }
}