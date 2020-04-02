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
    public class GuestHandler
    {
        public GuestViewModel GuestViewModel { get; set; }

        public GuestHandler(GuestViewModel guestViewModel)
        {
            GuestViewModel = guestViewModel;
        }


        public async void CreateGuest()
        {
            int guestNr = GuestViewModel.NewGuest.GuestNr;
            string guestName = GuestViewModel.NewGuest.Name;
            string guestAddress = GuestViewModel.NewGuest.Address;
            Guest aGuest = new Guest(guestNr, guestName, guestAddress);
            PersistenceGuests facade = new PersistenceGuests();
            bool ok = await facade.PostGuestAsync(aGuest);
            if (!ok)
            {
                MessageDialogHelper.Show("Der skete en fejl!", "Guest blev ikke oprettet");
            }
            else
            {
                MessageDialogHelper.Show("Alt gik godt!", $"Gæster {aGuest.Name} blev oprettet");
                GuestViewModel.GuestCatalogSingleton.Guests.Clear();
                GuestViewModel.GuestCatalogSingleton.LoadGuest();
            }
        }
        public async void DeleteGuest()
        {
            int guestNr = GuestViewModel.NewGuest.GuestNr;
            PersistenceGuests facade = new PersistenceGuests();
            bool ok = await facade.DeleteGuestAsync(guestNr);
            if (!ok)
            {
                MessageDialogHelper.Show("Der skete en fejl!", "Guest blev ikke slettet");
            }
            else
            {
                MessageDialogHelper.Show("Alt gik godt!", $"Guest {guestNr} blev slettet");
                GuestViewModel.GuestCatalogSingleton.Guests.Clear();
                GuestViewModel.GuestCatalogSingleton.LoadGuest();
            }
        }
        public async void UpdateGuest()
        {
            int guestNr = GuestViewModel.NewGuest.GuestNr;
            string guestName = GuestViewModel.NewGuest.Name;
            string guestAddress = GuestViewModel.NewGuest.Address;
            Guest aGuest = new Guest(guestNr, guestName, guestAddress);
            PersistenceGuests facade = new PersistenceGuests();
            bool ok = await facade.PutGuestAsync(guestNr, aGuest);
            if (!ok)
            {
                MessageDialogHelper.Show("Der skete en fejl!", "Guest blev ikke updateret");
            }
            else
            {
                MessageDialogHelper.Show("Alt gik godt!", $"Guest {guestNr} blev updateret");
                GuestViewModel.GuestCatalogSingleton.Guests.Clear();
                GuestViewModel.GuestCatalogSingleton.LoadGuest();
            }
        }

        public void ClearGuest()
        {
            GuestViewModel.NewGuest= new Guest();
        }
    }
}
