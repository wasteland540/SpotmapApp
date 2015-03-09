using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Acr.MvvmCross.Plugins.UserDialogs;
using Cirrious.CrossCore;
using Cirrious.MvvmCross.Plugins.Messenger;
using Cirrious.MvvmCross.ViewModels;
using Spotmap.Core.NavigationModels;
using Spotmap.Core.Services;
using SpotmapModel.Models;

namespace Spotmap.Core.ViewModels
{
    public class SpotGpsListViewModel : BaseSpotViewModel
    {
        private readonly IWebserviceService _webserviceService;

        public SpotGpsListViewModel(IMvxMessenger messenger, IDataService dataService,
            IWebserviceService webserviceService)
            : base(messenger, dataService, true)
        {
            _webserviceService = webserviceService;
            _webserviceService.ShareSpotsResponseCompleted += _webserviceService_ShareSpotsResponseCompleted;
        }

        private void _webserviceService_ShareSpotsResponseCompleted(object sender, ShareSpotsResponseCompletedArgs args)
        {
            ShowViewModel<ShareKeyViewModel>(new SpotDetailToShareKeyParams {SpotKey = args.SpotsKey});
        }

        private bool IsAtLeastOneSpotSelected()
        {
            return Spotlist.Any(spot => spot.Selected);
        }

        #region ShareSpots command

        private MvxCommand _shareSpotsCommand;

        public ICommand ShareSpotsCommand
        {
            get
            {
                _shareSpotsCommand = _shareSpotsCommand ?? new MvxCommand(DoShareSpotsCommand);
                return _shareSpotsCommand;
            }
        }

        private void DoShareSpotsCommand()
        {
            var dialogService = Mvx.Resolve<IUserDialogService>();

            if (IsAtLeastOneSpotSelected())
            {
                List<Spot> selectedSpots = Spotlist.Where(s => s.Selected).ToList();
                _webserviceService.ShareSpotsAsync(selectedSpots);

                dialogService.Toast("Upload is running.");
                RefreshList();
            }
            else
            {
                dialogService.Alert("You have to select at least one spot!");
            }
        }

        #endregion
    }
}