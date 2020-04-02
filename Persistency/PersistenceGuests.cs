using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Chat;
using ModelLibrary;
using Newtonsoft.Json;

namespace HotelMVVM.Persistency
{
    public class PersistenceGuests
    {
        private const string URI = "http://localhost:51273/api/guest";
        public async Task<List<Guest>> GetAllGuestsAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                string jsonString = await client.GetStringAsync(URI);
                List<Guest> guest = JsonConvert.DeserializeObject<List<Guest>>(jsonString);
                return guest;
            }
        }
        public async Task<Guest> GetOneGuestAsync(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                string jsonString = await client.GetStringAsync(URI + "/" + id);
                Guest guest = JsonConvert.DeserializeObject<Guest>(jsonString);
                return guest;
            }
        }
        public async Task<bool> DeleteGuestAsync(int id)
        {
            bool ok = false;
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.DeleteAsync(URI + "/" + id);
                if (response.IsSuccessStatusCode)
                {
                    string jsonString = response.Content.ReadAsStringAsync().Result;
                    ok = JsonConvert.DeserializeObject<bool>(jsonString);
                }
                else
                {
                    ok = false;
                }
            }
            return ok;
        }

        public async Task<bool> PostGuestAsync(Guest guest)
        {
            bool ok = false;
            using (HttpClient client = new HttpClient())
            {
                string jsonString = JsonConvert.SerializeObject(guest);
                StringContent content = new StringContent(jsonString, Encoding.UTF8, "application/json");
                HttpResponseMessage resultMessage = await client.PostAsync(URI, content);
                if (resultMessage.IsSuccessStatusCode)
                {
                    string jsonStr = await resultMessage.Content.ReadAsStringAsync();
                    ok = JsonConvert.DeserializeObject<bool>(jsonStr);
                }
                else
                {
                    ok = false;
                }
            }

            return ok;
        }

        public async Task<bool> PutGuestAsync(int id, Guest guest)
        {
            bool ok = false;
            using (HttpClient client = new HttpClient())
            {
                string jsonString = JsonConvert.SerializeObject(guest);
                StringContent content = new StringContent(jsonString, Encoding.UTF8, "application/json");
                HttpResponseMessage resultMessage = await client.PutAsync(URI + "/" + id, content);
                if (resultMessage.IsSuccessStatusCode)
                {
                    string jsonStr = await resultMessage.Content.ReadAsStringAsync();
                    ok = JsonConvert.DeserializeObject<bool>(jsonStr);
                }
                else
                {
                    ok = false;
                }
            }

            return ok;
        }
    }
}
