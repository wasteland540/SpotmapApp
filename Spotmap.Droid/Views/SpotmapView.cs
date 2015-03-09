using System.Collections.Generic;
using Android.App;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.OS;
using Android.Views;
using Cirrious.CrossCore;
using Cirrious.CrossCore.Core;
using Cirrious.MvvmCross.Droid.Fragging;
using Cirrious.MvvmCross.Plugins.Messenger;
using Spotmap.Core.Messages;
using Spotmap.Core.ViewModels;
using Spotmap.Droid.CustomUi.InfoWindows;
using SpotmapModel.Models;

namespace Spotmap.Droid.Views
{
    [Activity(Label = "@string/SpotmapViewLabel")]
    public class SpotmapView : MvxFragmentActivity
    {
        protected MvxSubscriptionToken MessagerToken;
        private Dictionary<string, int> _markerSpotDictionary;

        private IMvxMessenger _messenger;

        public Dictionary<string, int> MarkerSpotDictionary
        {
            get { return _markerSpotDictionary; }
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.SpotmapView);

            _messenger = Mvx.Resolve<IMvxMessenger>();
            MessagerToken = _messenger.Subscribe<SpotChangedMessage>(DeliverySpotChangedMessage);

            RefreshMapMarkers();
        }

        private void RefreshMapMarkers()
        {
            RunOnUiThread(() =>
            {
                _markerSpotDictionary = new Dictionary<string, int>();

                //Add markers on map:
                var spotmapViewModel = (SpotmapViewModel) ViewModel;
                var mapFragment = (SupportMapFragment) SupportFragmentManager.FindFragmentById(Resource.Id.map);

                if (mapFragment != null && mapFragment.Map != null)
                {
                    mapFragment.Map.Clear();

                    mapFragment.Map.SetInfoWindowAdapter(new PictureInfoWindow(this, LayoutInflater));

                    if (spotmapViewModel.Spotlist != null)
                    {
                        foreach (Spot spot in spotmapViewModel.Spotlist)
                        {
                            var options = new MarkerOptions();
                            options.SetPosition(new LatLng(spot.Lat, spot.Lng));
                            options.SetTitle(spot.Name);
                            options.SetSnippet(spot.Description);

                            Marker marker = mapFragment.Map.AddMarker(options);

                            _markerSpotDictionary.Add(marker.Id, spot.Id);
                        }
                    }
                }
            });
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.Spotmap_Menu, menu);

            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.getSharedSpot:
                    GetSharedSpot();
                    return true;

                case Resource.Id.getSharedSpots:
                    GetSharedSpots();
                    return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        private void GetSharedSpot()
        {
            ((SpotmapViewModel) ViewModel).GetSharedSpotCommand.Execute(null);
        }

        private void GetSharedSpots()
        {
            ((SpotmapViewModel)ViewModel).GetSharedSpotsCommand.Execute(null);
        }

        private void DeliverySpotChangedMessage(SpotChangedMessage spotChangedMessage)
        {
            MvxAsyncDispatcher.BeginAsync(RefreshMapMarkers);
        }
    }
}