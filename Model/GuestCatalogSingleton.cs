using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelMVVM.Persistency;
using ModelLibrary;

namespace HotelMVVM.Model
{
    public class GuestCatalogSingleton
    {
        private static GuestCatalogSingleton _instance = null; //new GuestCatalogSingleton(); //eager

        public static GuestCatalogSingleton Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new GuestCatalogSingleton(); //Lazy
                return _instance;
            }
        }

        public ObservableCollection<Guest> Guests { get; set; }

        private GuestCatalogSingleton()
        {
            Guests = new ObservableCollection<Guest>();

            LoadGuest();
        }
        public async void LoadGuest()
        {
            PersistenceGuests facade = new PersistenceGuests();
            List<Guest> guests = await facade.GetAllGuestsAsync();
            foreach (Guest guest in guests)
            {
                Guests.Add(guest);
            }
        }

    }
}
