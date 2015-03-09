using System;
using System.Collections.Generic;
using System.Windows.Input;
using Cirrious.MvvmCross.Plugins.Messenger;
using Cirrious.MvvmCross.ViewModels;
using Spotmap.Core.Messages;
using Spotmap.Core.Services;
using SpotmapModel.Models;

namespace Spotmap.Core.ViewModels
{
    public class GetSharedSpotsBundleViewModel : MvxViewModel, IDisposable
    {
        private readonly IDataService _dataService;
        private readonly IMvxMessenger _messenger;
        private readonly IWebserviceService _webserviceService;

        public GetSharedSpotsBundleViewModel(IWebserviceService webserviceService, IDataService dataService,
            IMvxMessenger messenger)
        {
            _webserviceService = webserviceService;
            _dataService = dataService;
            _messenger = messenger;

            _webserviceService.GetShareSpotsResponseCompleted += webserviceService_GetShareSpotsResponseCompleted;
        }

        public void Dispose()
        {
            _webserviceService.GetShareSpotsResponseCompleted -= webserviceService_GetShareSpotsResponseCompleted;
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

        #region GetSharedsSpot command

        private MvxCommand _getSharedSpotsCommand;

        public ICommand GetSharedSpotsCommand
        {
            get
            {
                _getSharedSpotsCommand = _getSharedSpotsCommand ?? new MvxCommand(DoGetSharedSpotsCommand);
                return _getSharedSpotsCommand;
            }
        }

        private void DoGetSharedSpotsCommand()
        {
            _webserviceService.GetSharedSpotsAsync(_shareKey);
        }

        #endregion

        #region Spotlist

        private List<Spot> _spots;

        public List<Spot> Spots
        {
            get { return _spots; }
            set
            {
                _spots = value;
                RaisePropertyChanged(() => Spots);
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
            foreach (Spot spot in Spots)
            {
                if (spot.Selected)
                {
                    spot.Selected = false; //Cause its selected in the list view...
                    _dataService.AddSpot(spot);    
                }
            }

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

        private void webserviceService_GetShareSpotsResponseCompleted(object sender,
            GetShareSpotsResponseCompletedArgs args)
        {
            ResponseCompleted = true;

            var buffList = args.Spots;
            foreach (Spot spot in buffList)
            {
                spot.Selected = true;
            }

            Spots = buffList;
        }
    }
}