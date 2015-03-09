using Cirrious.MvvmCross.Plugins.Messenger;
using Spotmap.Core.Services;

namespace Spotmap.Core.ViewModels
{
    public class SpotListViewModel : BaseSpotViewModel
    {
        public SpotListViewModel(IMvxMessenger messenger, IDataService dataService)
            : base(messenger, dataService, false)
        {
        }
    }
}