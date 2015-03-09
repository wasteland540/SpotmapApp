using System;
using System.IO;
using System.Windows.Input;
using Acr.MvvmCross.Plugins.UserDialogs;
using Cirrious.CrossCore;
using Cirrious.MvvmCross.Plugins.Messenger;
using Cirrious.MvvmCross.ViewModels;
using KS21.MvvmCross.Plugins.ExifPictureChooser;
using Spotmap.Core.Messages;
using Spotmap.Core.Services;
using SpotmapModel.Models;

namespace Spotmap.Core.ViewModels
{
    public class AddSpotViewModel : MvxViewModel
    {
        private readonly IMvxMessenger _messenger;
        private readonly INullImageHelper _nullImageHelper;

        public AddSpotViewModel(IMvxMessenger messenger, INullImageHelper nullImageHelper)
        {
            _messenger = messenger;
            _nullImageHelper = nullImageHelper;

            PictureBytes = _nullImageHelper.GetNullPicture();
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

        #region TakePicture command

        private MvxCommand _takePictureCommand;

        public ICommand TakePictureCommand
        {
            get
            {
                _takePictureCommand = _takePictureCommand ?? new MvxCommand(DoTakePictureCommand);
                return _takePictureCommand;
            }
        }

        private void DoTakePictureCommand()
        {
            var pictureChooser = Mvx.Resolve<IExifPictureChooserTask>();
            pictureChooser.TakePicture(400, 95, PictureAvailable, () => { });
        }

        #endregion

        #region ChoosePicture command

        private MvxCommand _choosePictureCommand;

        public ICommand ChoosePictureCommand
        {
            get
            {
                _choosePictureCommand = _choosePictureCommand ?? new MvxCommand(DoChoosePictureCommand);
                return _choosePictureCommand;
            }
        }

        private void DoChoosePictureCommand()
        {
            var pictureChooser = Mvx.Resolve<IExifPictureChooserTask>();
            pictureChooser.ChoosePictureFromLibrary(400, 95, PictureAvailable, () => { });
        }

        #endregion

        #region Add command

        private MvxCommand _addCommand;

        public ICommand AddCommand
        {
            get
            {
                _addCommand = _addCommand ?? new MvxCommand(DoAddCommand);
                return _addCommand;
            }
        }

        private void DoAddCommand()
        {
            var dialogService = Mvx.Resolve<IUserDialogService>();

            if (IsVaild())
            {
                var dataService = Mvx.Resolve<IDataService>();

                var spot = new Spot
                {
                    Name = Name,
                    Description = Description,
                    LocationKown = LocationKown,
                    Lat = Lat,
                    Lng = Lng,
                    PictureBytes = PictureBytes,
                    CreatedAt = DateTime.Now,
                };

                dataService.AddSpot(spot);

                _messenger.Publish(new SpotChangedMessage(this, SpotChangedMessage.ChangeType.Added));

                dialogService.Toast("Spot saved.");
                ClearInputs();
            }
            else
            {
                dialogService.Alert("Name and picture have to be set!");
            }
        }

        #endregion

        #region cancel command

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

        private void PictureAvailable(Stream stream, float[] latLat)
        {
            var memStream = new MemoryStream();
            stream.CopyTo(memStream);

            PictureBytes = memStream.ToArray();

            if (latLat != null && latLat.Length == 2)
            {
                Lat = latLat[0];
                Lng = latLat[1];

                if (Lat != 0.0 && Lng != 0.0)
                {
                    LocationKown = true;
                }
            }
        }

        private bool IsVaild()
        {
            if (string.IsNullOrEmpty(Name))
                return false;

            if (PictureBytes == null)
                return false;

            if (PictureBytes.Length == 0)
                return false;

            return true;
        }

        private void ClearInputs()
        {
            Name = string.Empty;
            Description = string.Empty;
            LocationKown = false;
            Lat = 0.0;
            Lng = 0.0;
            PictureBytes = _nullImageHelper.GetNullPicture();
        }
    }
}