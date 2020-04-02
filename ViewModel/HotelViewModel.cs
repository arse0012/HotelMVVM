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
    public class HotelViewModel : INotifyPropertyChanged
    {
        public HotelCatalogSingleton HotelCatalogSingleton { get; set; }

        public HotelHandler HotelHandler { get; set; }
        public HotelViewModel()
        {
            HotelCatalogSingleton = HotelCatalogSingleton.Instance;
            HotelHandler = new HotelHandler(this);
            _newHotel = new Hotel();
            CreateHotelCommand = new RelayCommand(HotelHandler.CreateHotel);
            UpdateHotelCommand = new RelayCommand(HotelHandler.UpdateHotel, HotelSelected);
            DeleteHotelCommand = new RelayCommand(HotelHandler.DeleteHotel, HotelSelected);
            ClearHotelCommand = new RelayCommand(HotelHandler.ClearHotel, HotelSelected);
        }

        private Hotel _newHotel;

        public Hotel NewHotel
        {
            get { return _newHotel; }
            set
            {
                _newHotel = value;
                OnPropertyChanged();
                ((RelayCommand)CreateHotelCommand).RaiseCanExecuteChanged();
                ((RelayCommand)UpdateHotelCommand).RaiseCanExecuteChanged();
                ((RelayCommand)DeleteHotelCommand).RaiseCanExecuteChanged();
                ((RelayCommand)ClearHotelCommand).RaiseCanExecuteChanged();
            }
        }

        public ICommand CreateHotelCommand { get; set; }
        public ICommand DeleteHotelCommand { get; set; }
        public ICommand UpdateHotelCommand { get; set; }
        public ICommand ClearHotelCommand { get; set; }

        public bool HotelSelected()
        {
            if (NewHotel != null && NewHotel.HotelNr != 0)
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
