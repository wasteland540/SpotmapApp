using System;
using System.Windows.Input;
using Cirrious.MvvmCross.Plugins.Messenger;
using Cirrious.MvvmCross.ViewModels;
using Spotmap.Core.Messages;
using Spotmap.Core.Services;
using Spotmap.Core.Services.Events;
using SpotmapModel.Models;

namespace Spotmap.Core.ViewModels
{
    public class GetSharedSpotViewModel : MvxViewModel, IDisposable
    {
        private readonly IDataService _dataService;
        private readonly IMvxMessenger _messenger;
        private readonly IWebserviceService _webserviceService;
        private Spot _spot;

        public GetSharedSpotViewModel(IWebserviceService webserviceService, IDataService dataService,
            IMvxMessenger messenger)
        {
            _webserviceService = webserviceService;
            _dataService = dataService;
            _messenger = messenger;

            _webserviceService.GetShareSpotResponseCompleted += webserviceService_GetShareSpotResponseCompleted;
        }

        public void Dispose()
        {
            _webserviceService.GetShareSpotResponseCompleted -= webserviceService_GetShareSpotResponseCompleted;
        }

        #region ShareKey

        private string _shareKey;

        public string ShareKey
        {
            get { return _shareKey; }
            set
            {
                _shareKey = value;
                RaisePropertyChanged(() => ShareKey);
            }
        }

        #endregion

        #region ResponseCompleted

        private bool _responseCompleted;

        public bool ResponseCompleted
        {
            get { return _responseCompleted; }
            set
            {
                _responseCompleted = value;
                RaisePropertyChanged(() => ResponseCompleted);
            }
        }

        #endregion

        #region GetSharedSpot command

        private MvxCommand _getSharedSpotCommand;

        public ICommand GetSharedSpotCommand
        {
            get
            {
                _getSharedSpotCommand = _getSharedSpotCommand ?? new MvxCommand(DoGetSharedSpotCommand);
                return _getSharedSpotCommand;
            }
        }

        private void DoGetSharedSpotCommand()
        {
            _webserviceService.GetSharedSpotAsync(_shareKey);
        }

        #endregion

        #region Spotname

        private string _spotName;

        public string SpotName
        {
            get { return _spotName; }
            set
            {
                _spotName = value;
                RaisePropertyChanged(() => SpotName);
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

        #region AddSpot command

        private MvxCommand _addSpotCommand;

        public ICommand AddSpotCommand
        {
            get
            {
                _addSpotCommand = _addSpotCommand ?? new MvxCommand(DoAddSpotCommand);
                return _addSpotCommand;
            }
        }

        private void DoAddSpotCommand()
        {
            _dataService.AddSpot(_spot);
            _messenger.Publish(new SpotChangedMessage(this, SpotChangedMessage.ChangeType.Added));

            Close(this);
        }

        #endregion

        #region Cancel command

        private MvxCommand _cancelCommand;

        public ICommand CancelCommand
        {
            get
            {
                _cancelCommand = _cancelCommand ?? new MvxCommand(DoCancelCommand);
                return _cancelCommand;
            }
        }

        private void DoCancelCommand()
        {
            Close(this);
        }

        #endregion

        private void webserviceService_GetShareSpotResponseCompleted(object sender,
            GetShareSpotResponseCompletedArgs args)
        {
            ResponseCompleted = true;
            _spot = args.Spot;

            SpotName = _spot.Name;
            PictureBytes = _spot.PictureBytes;
        }
    }
}