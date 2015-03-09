using System.Collections.Generic;
using System.Windows.Input;
using Cirrious.CrossCore.Core;
using Cirrious.MvvmCross.Plugins.Messenger;
using Cirrious.MvvmCross.ViewModels;
using Spotmap.Core.Messages;
using Spotmap.Core.NavigationModels;
using Spotmap.Core.Services;
using SpotmapModel.Models;

namespace Spotmap.Core.ViewModels
{
    public abstract class BaseSpotViewModel : MvxViewModel
    {
        protected readonly IDataService DataService;
        protected readonly MvxSubscriptionToken MessagerToken;
        private readonly bool _isLocationKown;

        protected BaseSpotViewModel(IMvxMessenger messenger, IDataService dataService, bool isLocationKown)
        {
            MessagerToken = messenger.Subscribe<SpotChangedMessage>(DeliverySpotChangedMessage);
            DataService = dataService;
            _isLocationKown = isLocationKown;

            //MvxAsyncDispatcher.BeginAsync(RefreshList);
            RefreshList();
        }

        #region Spotlist

        private List<Spot> _spotlist;

        public List<Spot> Spotlist
        {
            get { return _spotlist; }
            set
            {
                _spotlist = value;
                RaisePropertyChanged(() => Spotlist);
            }
        }

        #endregion

        #region ShowSpotDetail command

        private MvxCommand<Spot> _showSpotDetailCommand;

        public ICommand ShowSpotDetailCommand
        {
            get
            {
                _showSpotDetailCommand = _showSpotDetailCommand ?? new MvxCommand<Spot>(DoShowSpotDetailCommand);
                return _showSpotDetailCommand;
            }
        }

        private void DoShowSpotDetailCommand(Spot spot)
        {
            ShowViewModel<SpotDetailViewModel>(new SpotListToSpotDetailParams{SpotId = spot.Id});
        }

        #endregion

        protected void RefreshList()
        {
            Spotlist = _isLocationKown ? DataService.GetAllSpotsWithLocation() : DataService.GetAllSpotsWithoutLocation();
        }

        private void DeliverySpotChangedMessage(SpotChangedMessage spotChangedMessage)
        {
            MvxAsyncDispatcher.BeginAsync(RefreshList);
        }
    }
}