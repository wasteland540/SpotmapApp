using System;
using System.IO;

namespace KS21.MvvmCross.Plugins.ExifPictureChooser
{
    public interface IExifPictureChooserTask
    {
        void ChoosePictureFromLibrary(int maxPixelDimension, int percentQuality, Action<Stream, float[]> pictureAvailable,
                                  Action assumeCancelled);

        void TakePicture(int maxPixelDimension, int percentQuality, Action<Stream, float[]> pictureAvailable,
                         Action assumeCancelled);
    }
}