using Cirrious.MvvmCross.Plugins.Messenger;
using Cirrious.MvvmCross.ViewModels;
using Spotmap.Core.Services;

namespace Spotmap.Core.ViewModels
{
    public class MainViewModel
        : MvxViewModel
    {
        //#region OpenSpotmap command

        //private MvxCommand _openSpotmapCommand;

        //public ICommand OpenSpotmapCommand
        //{
        //    get
        //    {
        //        _openSpotmapCommand = _openSpotmapCommand ?? new MvxCommand(DoOpenSpotmapCommand);
        //        return _openSpotmapCommand;
        //    }
        //}

        //private void DoOpenSpotmapCommand()
        //{
        //    ShowViewModel<SpotmapViewModel>();
        //}

        //#endregion

        //#region SpotList command

        //private MvxCommand _spotListCommand;

        //public ICommand SpotListCommand
        //{
        //    get
        //    {
        //        _spotListCommand = _spotListCommand ?? new MvxCommand(DoSpotListCommand);
        //        return _spotListCommand;
        //    }
        //}

        //private void DoSpotListCommand()
        //{
        //    ShowViewModel<SpotListViewModel>();
        //}

        //#endregion

        //#region AddSpot command

        //private MvxCommand _addSpotCommand;

        //public ICommand AddSpotCommand
        //{
        //    get
        //    {
        //        _addSpotCommand = _addSpotCommand ?? new MvxCommand(DoAddSpotCommand);
        //        return _addSpotCommand;
        //    }
        //}

        //private void DoAddSpotCommand()
        //{
        //    ShowViewModel<AddSpotViewModel>();
        //}

        //#endregion

        //#region SpotGpsList command

        //private MvxCommand _spotGpsListCommand;

        //public ICommand SpotGpsListCommand
        //{
        //    get
        //    {
        //        _spotGpsListCommand = _spotGpsListCommand ?? new MvxCommand(DoSpotGpsListCommand);
        //        return _spotGpsListCommand;
        //    }
        //}

        //private void DoSpotGpsListCommand()
        //{
        //    ShowViewModel<SpotGpsListViewModel>();
        //}

        //#endregion      

        private AddSpotViewModel _addSpotViewModel;
        private SpotGpsListViewModel _spotGpsListViewModel;
        private SpotListViewModel _spotListViewModel;
        private SpotmapViewModel _spotmapViewModel;

        public MainViewModel(IMvxMessenger messenger, IDataService dataService, INullImageHelper nullImageHelper, IWebserviceService webserviceService)
        {
            SpotmapViewModel = new SpotmapViewModel(messenger, dataService);
            AddSpotViewModel = new AddSpotViewModel(messenger, nullImageHelper);
            SpotGpsListViewModel = new SpotGpsListViewModel(messenger, dataService, webserviceService);
            SpotListViewModel = new SpotListViewModel(messenger, dataService);
        }

        public SpotmapViewModel SpotmapViewModel
        {
            get { return _spotmapViewModel; }

            set
            {
                _spotmapViewModel = value;
                RaisePropertyChanged(() => SpotmapViewModel);
            }
        }

        public AddSpotViewModel AddSpotViewModel
        {
            get { return _addSpotViewModel; }

            set
            {
                _addSpotViewModel = value;
                RaisePropertyChanged(() => AddSpotViewModel);
            }
        }

        public SpotGpsListViewModel SpotGpsListViewModel
        {
            get { return _spotGpsListViewModel; }

            set
            {
                _spotGpsListViewModel = value;
                RaisePropertyChanged(() => SpotGpsListViewModel);
            }
        }

        public SpotListViewModel SpotListViewModel
        {
            get { return _spotListViewModel; }

            set
            {
                _spotListViewModel = value;
                RaisePropertyChanged(() => SpotListViewModel);
            }
        }
    }
}