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
    public class HotelCatalogSingleton
    {
        private static HotelCatalogSingleton _instance = null; //new HotelCatalogSingleton();//eager

        public static HotelCatalogSingleton Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new HotelCatalogSingleton(); //Lazy
                return _instance;
            }
        }

        public ObservableCollection<Hotel> Hotels { get; set; }

        private HotelCatalogSingleton()
        {
            Hotels = new ObservableCollection<Hotel>();
            //Hotel h1 = new Hotel(456, "Hotel 456", "Vej 456");
            //Hotel h2 = new Hotel(678, "Hotel 678", "Vej 678");
            //Hotel h3 = new Hotel(9102, "Hotel 9102", "Vej 9102");
            //Hotels.Add(h1);
            //Hotels.Add(h2);
            //Hotels.Add(h3);

            LoadHotels();
        }

        public async void LoadHotels()
        {
            PersistenceFacade facade = new PersistenceFacade();
            List<Hotel> hotels = await facade.GetAllHotelsAsync();
            foreach (Hotel hotel in hotels)
            {
                Hotels.Add(hotel);
            }

        }
    }
}
