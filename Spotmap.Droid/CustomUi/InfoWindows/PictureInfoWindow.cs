using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.Graphics;
using Android.Views;
using Android.Widget;
using Cirrious.CrossCore;
using Java.Lang;
using Spotmap.Core.Services;
using Spotmap.Droid.Views;
using SpotmapModel.Models;

namespace Spotmap.Droid.CustomUi.InfoWindows
{
    public class PictureInfoWindow : Object, GoogleMap.IInfoWindowAdapter
    {
        private readonly View _myContentsView;
        private readonly SpotmapView _parent;

        public PictureInfoWindow(SpotmapView parent, LayoutInflater layoutInflater)
        {
            _parent = parent;
            _myContentsView = layoutInflater.Inflate(Resource.Layout.Custom_info_contents, null);
        }

        public View GetInfoContents(Marker marker)
        {
            var tvTitle = (TextView) _myContentsView.FindViewById(Resource.Id.pictureInfowindowTitle);
            tvTitle.SetText(marker.Title, TextView.BufferType.Normal);

            var tvSippet = (TextView) _myContentsView.FindViewById(Resource.Id.pictureInfowindowSnippet);
            tvSippet.SetText(marker.Snippet, TextView.BufferType.Normal);

            var ivPicture = (ImageView) _myContentsView.FindViewById(Resource.Id.pictureInfowindowPicture);

            if (_parent.MarkerSpotDictionary.ContainsKey(marker.Id))
            {
                int spotId = _parent.MarkerSpotDictionary[marker.Id];

                var dataService = Mvx.Resolve<IDataService>();
                Spot spot = dataService.GetSpotById(spotId);

                Bitmap bitmap = BitmapFactory.DecodeByteArray(spot.PictureBytes, 0, spot.PictureBytes.Length);
                ivPicture.SetImageBitmap(bitmap);
            }

            return _myContentsView;
        }

        public View GetInfoWindow(Marker marker)
        {
            return null;
        }
    }
}