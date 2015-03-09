using System;
using System.Windows.Input;
using Acr.MvvmCross.Plugins.UserDialogs;
using Cirrious.CrossCore;
using Cirrious.MvvmCross.Plugins.Messenger;
using Cirrious.MvvmCross.ViewModels;
using Spotmap.Core.Messages;
using Spotmap.Core.NavigationModels;
using Spotmap.Core.Services;
using Spotmap.Core.Services.Events;
using SpotmapModel.Models;

namespace Spotmap.Core.ViewModels
{
    public class SpotDetailViewModel : MvxViewModel, IDisposable
    {
        private readonly IDataService _dataService;
        private readonly IMvxMessenger _messenger;
        private readonly IWebserviceService _webserviceService;
        private int _spotId;

        public SpotDetailViewModel(IDataService dataService, IMvxMessenger messenger,
            IWebserviceService webserviceService)
        {
            _dataService = dataService;
            _messenger = messenger;
            _webserviceService = webserviceService;

            _webserviceService.ShareSpotResponseCompleted += webserviceService_ShareSpotResponseCompleted;
        }

        public void Dispose()
        {
            _webserviceService.ShareSpotResponseCompleted -= webserviceService_ShareSpotResponseCompleted;
        }

        public void Init(SpotListToSpotDetailParams detailParams)
        {
            Spot spot = _dataService.GetSpotById(detailParams.SpotId);

            _spotId = spot.Id;

            Name = spot.Name;
            Description = spot.Description;
            LocationKown = spot.LocationKown;
            Lat = spot.Lat;
            Lng = spot.Lng;
            PictureBytes = spot.PictureBytes;
        }

        private void webserviceService_ShareSpotResponseCompleted(object sender, ShareSpotResponseCompletedArgs args)
        {
            ShowViewModel<ShareKeyViewModel>(new SpotDetailToShareKeyParams {SpotKey = args.SpotKey});
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

        #region Delete command

        private MvxCommand _deleteCommand;

        public ICommand DeleteCommand
        {
            get
            {
                _deleteCommand = _deleteCommand ?? new MvxCommand(DoDeleteCommand);
                return _deleteCommand;
            }
        }

        private void DoDeleteCommand()
        {
            var dialogService = Mvx.Resolve<IUserDialogService>();

            dialogService.Confirm("Are you sure that you want to delete this spot?", delegate(bool okClicked)
            {
                if (okClicked)
                {
                    _dataService.DeleteSpot(_spotId);
                    _messenger.Publish(new SpotChangedMessage(this, SpotChangedMessage.ChangeType.Deleted));

                    Close(this);
                }
            });
        }

        #endregion

        #region ShowOnMap command

        private MvxCommand _showOnMapCommand;

        public ICommand ShowOnMapCommand
        {
            get
            {
                _showOnMapCommand = _showOnMapCommand ?? new MvxCommand(DoShowOnMapCommand);
                return _showOnMapCommand;
            }
        }

        private void DoShowOnMapCommand()
        {
            ShowViewModel<SingleSpotOnMapViewModel>(new SpotListToSpotDetailParams {SpotId = _spotId});
            Close(this);
        }

        #endregion

        #region ShareSpot command

        private MvxCommand _shareSpotCommand;

        public ICommand ShareSpotCommand
        {
            get
            {
                _shareSpotCommand = _shareSpotCommand ?? new MvxCommand(DoShareSpotCommand);
                return _shareSpotCommand;
            }
        }

        private void DoShareSpotCommand()
        {
            var spot = new Spot
            {
                Name = Name,
                Description = Description,
                Lat = Lat,
                Lng = Lng,
                PictureBytes = PictureBytes,
                LocationKown = LocationKown
            };

            _webserviceService.ShareSpotAsync(spot);

            var dialogService = Mvx.Resolve<IUserDialogService>();
            dialogService.Toast("Upload running.");

            //_webserviceService.ShareSpot(spot, OnComplete, OnError); Variante  1

            //_webserviceService.ShareSport(spot); Variante 2
        }

        #endregion

        //void OnViewEvent(ViewEventArgs args) { if(args.Stop && _webserviceService.IsStarted) _webserviceService.Stop(); else _webserviceService.Start(OnSpotTransferCompleted); Variante 2
    }
}