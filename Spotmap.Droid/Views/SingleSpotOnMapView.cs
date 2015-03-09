using Android.App;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.OS;
using Cirrious.MvvmCross.Droid.Fragging;
using Spotmap.Core.ViewModels;
using Spotmap.Droid.CustomUi.InfoWindows;

namespace Spotmap.Droid.Views
{
    [Activity(Label = "@string/SingleSpotOnMapViewLabel")]
    public class SingleSpotOnMapView : MvxFragmentActivity
    {
        private byte[] _pictureBytes;

        public byte[] PictureBytes
        {
            get { return _pictureBytes; }
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.SingleSpotOnMapView);

            //Add marker on map:
            var spotmapViewModel = (SingleSpotOnMapViewModel) ViewModel;
            var mapFragment = (SupportMapFragment)SupportFragmentManager.FindFragmentById(Resource.Id.singlemap);

            _pictureBytes = spotmapViewModel.PictureBytes;
            mapFragment.Map.SetInfoWindowAdapter(new SingleSpotPictureInfoWindow(this, LayoutInflater));

            if (spotmapViewModel != null)
            {
                var options = new MarkerOptions();
                options.SetPosition(new LatLng(spotmapViewModel.Lat, spotmapViewModel.Lng));
                options.SetTitle(spotmapViewModel.Name);
                options.SetSnippet(spotmapViewModel.Description);

                mapFragment.Map.AddMarker(options);
            }
        }
    }
}