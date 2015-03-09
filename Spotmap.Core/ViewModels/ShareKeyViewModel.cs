using System.Windows.Input;
using Cirrious.MvvmCross.ViewModels;
using Spotmap.Core.NavigationModels;

namespace Spotmap.Core.ViewModels
{
    public class ShareKeyViewModel : MvxViewModel
    {
        public void Init(SpotDetailToShareKeyParams shareKeyParams)
        {
            ShareKey = shareKeyParams.SpotKey;
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

        #region Ok command

        private MvxCommand _okCommand;

        public ICommand OkCommand
        {
            get
            {
                _okCommand = _okCommand ?? new MvxCommand(DoOkCommand);
                return _okCommand;
            }
        }

        private void DoOkCommand()
        {
            Close(this);
        }

        #endregion
    }
}