using System.IO;
using Android.Graphics;
using Android.Graphics.Drawables;
using Cirrious.CrossCore;
using Cirrious.CrossCore.Droid;
using Spotmap.Core.Services;

namespace Spotmap.Droid.Services
{
    public class DroidNullImageHelper : INullImageHelper
    {
        public byte[] GetNullPicture()
        {
            var globals = Mvx.Resolve<IMvxAndroidGlobals>();
            Drawable drawable = globals.ApplicationContext.Resources.GetDrawable(Resource.Drawable.spotmap);

            Bitmap bitmap = ((BitmapDrawable) drawable).Bitmap;
            var stream = new MemoryStream();
            bitmap.Compress(Bitmap.CompressFormat.Jpeg, 100, stream);
            byte[] bitmapdata = stream.ToArray();

            return bitmapdata;
        }
    }
}