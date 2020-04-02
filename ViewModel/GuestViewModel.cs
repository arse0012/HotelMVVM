using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using HotelMVVM.Annotations;
using HotelMVVM.Common;
using HotelMVVM.Handler;
using HotelMVVM.Model;
using ModelLibrary;

namespace HotelMVVM.ViewModel
{
    public class GuestViewModel : INotifyPropertyChanged
    {
        public GuestCatalogSingleton GuestCatalogSingleton { get; set; }
        public GuestHandler GuestHandler { get; set; }
        public GuestViewModel()
        {
            GuestCatalogSingleton = GuestCatalogSingleton.Instance;
            GuestHandler = new GuestHandler(this);
            _newGuest = new Guest();
            CreateGuestCommand = new RelayCommand(GuestHandler.CreateGuest);
            UpdateGuestCommand = new RelayCommand(GuestHandler.UpdateGuest, GuestSelected);
            DeleteGuestCommand = new RelayCommand(GuestHandler.DeleteGuest, GuestSelected);
            ClearGuestCommand = new RelayCommand(GuestHandler.ClearGuest, GuestSelected);

        }

        private Guest _newGuest;

        public Guest NewGuest
        {
            get { return _newGuest; }
            set
            {
                _newGuest = value;
                OnPropertyChanged();
                ((RelayCommand)CreateGuestCommand).RaiseCanExecuteChanged();
                ((RelayCommand)UpdateGuestCommand).RaiseCanExecuteChanged();
                ((RelayCommand)DeleteGuestCommand).RaiseCanExecuteChanged();
                ((RelayCommand)ClearGuestCommand).RaiseCanExecuteChanged();

            }
        }


        public ICommand CreateGuestCommand { get; set; }
        public ICommand UpdateGuestCommand { get; set; }
        public ICommand DeleteGuestCommand { get; set; }
        public ICommand ClearGuestCommand { get; set; }


        public bool GuestSelected()
        {
            if (NewGuest != null && NewGuest.GuestNr != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
