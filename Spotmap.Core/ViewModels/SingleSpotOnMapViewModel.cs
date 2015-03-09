using Cirrious.MvvmCross.ViewModels;
using Spotmap.Core.NavigationModels;
using Spotmap.Core.Services;
using SpotmapModel.Models;

namespace Spotmap.Core.ViewModels
{
    public class SingleSpotOnMapViewModel : MvxViewModel
    {
        private readonly IDataService _dataService;

        public SingleSpotOnMapViewModel(IDataService dataService)
        {
            _dataService = dataService;
        }

        public void Init(SpotListToSpotDetailParams detailParams)
        {
            Spot spot = _dataService.GetSpotById(detailParams.SpotId);

            //_spotId = spot.Id;

            Name = spot.Name;
            Description = spot.Description;
            LocationKown = spot.LocationKown;
            Lat = spot.Lat;
            Lng = spot.Lng;
            PictureBytes = spot.PictureBytes;
        }

        #region Name

        private string _name;

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                RaisePropertyChanged(() => Name);
            }
        }

        #endregion

        #region Description

        private string _description;

        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                RaisePropertyChanged(() => Description);
            }
        }

        #endregion

        #region LocationKown

        private bool _locationKown;

        public bool LocationKown
        {
            get { return _locationKown; }
            set
            {
                _locationKown = value;
                RaisePropertyChanged(() => LocationKown);
            }
        }

        #endregion

        #region Lat

        private double _lat;

        public double Lat
        {
            get { return _lat; }
            set
            {
                _lat = value;
                RaisePropertyChanged(() => Lat);
            }
        }

        #endregion

        #region Lng

        private double _lng;

        public double Lng
        {
            get { return _lng; }
            set
            {
                _lng = value;
                RaisePropertyChanged(() => Lng);
            }
        }

        #endregion

        #region PictureBytes

        private byte[] _pictureBytes;

        public byte[] PictureBytes
        {
            get { return _pictureBytes; }
            set
            {
                _pictureBytes = value;
                RaisePropertyChanged(() => PictureBytes);
            }
        }

        #endregion
    }
}