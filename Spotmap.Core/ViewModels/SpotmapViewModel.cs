using System.Windows.Input;
using Cirrious.MvvmCross.Plugins.Messenger;
using Cirrious.MvvmCross.ViewModels;
using Spotmap.Core.Services;

namespace Spotmap.Core.ViewModels
{
    public class SpotmapViewModel : BaseSpotViewModel
    {
        public SpotmapViewModel(IMvxMessenger messenger, IDataService dataService) : base(messenger, dataService, true)
        {
        }

        #region SharedSpot command

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
            ShowViewModel<GetSharedSpotViewModel>();
        }

        #endregion

        #region SharedSpots command

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
            ShowViewModel<GetSharedSpotsBundleViewModel>();
        }

        #endregion
    }
}