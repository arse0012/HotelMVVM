using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelMVVM.Common;
using HotelMVVM.Persistency;
using HotelMVVM.ViewModel;
using ModelLibrary;

namespace HotelMVVM.Handler
{
    public class HotelHandler
    {
        public HotelViewModel HotelViewModel { get; set; }

        public HotelHandler(HotelViewModel hotelViewModel)
        {
            HotelViewModel = hotelViewModel;
        }


        public async void CreateHotel()
        {
            int hotelNr = HotelViewModel.NewHotel.HotelNr;
            string hotelName = HotelViewModel.NewHotel.Name;
            string hotelAddress = HotelViewModel.NewHotel.Address;
            Hotel aHotel = new Hotel(hotelNr, hotelName, hotelAddress);
            PersistenceFacade facade = new PersistenceFacade();
            bool ok = await facade.PostHotelAsync(aHotel);
            if (!ok)
            {
                MessageDialogHelper.Show("Der skete en fejl!", "Hotel blev ikke oprettet");
            }
            else
            {
                MessageDialogHelper.Show("Alt gik godt!", $"Hotellet {aHotel.Name} blev oprettet");
                HotelViewModel.HotelCatalogSingleton.Hotels.Clear();
                HotelViewModel.HotelCatalogSingleton.LoadHotels();
            }
        }

        public async void DeleteHotel()
        {
            int hotelNr = HotelViewModel.NewHotel.HotelNr;
            PersistenceFacade facade = new PersistenceFacade();
            bool ok = await facade.DeleteHotelAsync(hotelNr);
            if (!ok)
            {
                MessageDialogHelper.Show("Der skete en fejl!", "Hotel blev ikke slettet");
            }
            else
            {
                MessageDialogHelper.Show("Alt gik godt!", $"Hotellet {hotelNr} blev slettet");
                HotelViewModel.HotelCatalogSingleton.Hotels.Clear();
                HotelViewModel.HotelCatalogSingleton.LoadHotels();
            }
        }

        public async void UpdateHotel()
        {
            int hotelNr = HotelViewModel.NewHotel.HotelNr;
            string hotelName = HotelViewModel.NewHotel.Name;
            string hotelAddress = HotelViewModel.NewHotel.Address;
            Hotel aHotel = new Hotel(hotelNr, hotelName, hotelAddress);
            PersistenceFacade facade = new PersistenceFacade();
            bool ok = await facade.PutHotelAsync(hotelNr, aHotel);
            if (!ok)
            {
                MessageDialogHelper.Show("Der skete en fejl!", "Hotel blev ikke updateret");
            }
            else
            {
                MessageDialogHelper.Show("Alt gik godt!", $"Hotellet {hotelNr} blev updateret");
                HotelViewModel.HotelCatalogSingleton.Hotels.Clear();
                HotelViewModel.HotelCatalogSingleton.LoadHotels();
            }
        }

        public void ClearHotel()
        {
            HotelViewModel.NewHotel = new Hotel();
        }

    }
}
