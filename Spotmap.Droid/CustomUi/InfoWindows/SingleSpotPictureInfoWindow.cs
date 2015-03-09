using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.Graphics;
using Android.Views;
using Android.Widget;
using Java.Lang;
using Spotmap.Droid.Views;

namespace Spotmap.Droid.CustomUi.InfoWindows
{
    public class SingleSpotPictureInfoWindow : Object, GoogleMap.IInfoWindowAdapter
    {
        private readonly View _myContentsView;
        private readonly SingleSpotOnMapView _parent;

        public SingleSpotPictureInfoWindow(SingleSpotOnMapView parent, LayoutInflater layoutInflater)
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

            Bitmap bitmap = BitmapFactory.DecodeByteArray(_parent.PictureBytes, 0, _parent.PictureBytes.Length);
            ivPicture.SetImageBitmap(bitmap);

            return _myContentsView;
        }

        public View GetInfoWindow(Marker marker)
        {
            return null;
        }
    }
}