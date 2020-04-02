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
    public class PersistenceFacade
    {
        private const string URI = "http://localhost:51273/api/hotels";


        public async Task<List<Hotel>> GetAllHotelsAsync()
        {
            using (HttpClient client = new HttpClient())
            { 
                string jsonString  = await client.GetStringAsync(URI);
                List<Hotel> hoteller = JsonConvert.DeserializeObject<List<Hotel>>(jsonString);
                return hoteller;
            }
        }
        public async Task<List<Hotel>> GetOneHotelAsync(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                string jsonString = await client.GetStringAsync(URI + "/" + id);
                List<Hotel> hotel = JsonConvert.DeserializeObject<List<Hotel>>(jsonString);
                return hotel;
            }
        }
        public async Task<bool> DeleteHotelAsync(int id)
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

        public async Task<bool> PostHotelAsync(Hotel hotel)
        {
            using (HttpClient client = new HttpClient())
            {
                bool ok = false;
                string jsonString = JsonConvert.SerializeObject(hotel);
                StringContent content = new StringContent(jsonString, Encoding.UTF8, "application/json");
                HttpResponseMessage resultMessage = await client.PostAsync(URI, content);
                if (resultMessage.IsSuccessStatusCode)
                {
                    string jsonStr = resultMessage.Content.ReadAsStringAsync().Result;
                    ok = JsonConvert.DeserializeObject<bool>(jsonStr);
                }
                else
                {
                    ok = false;
                }

                return ok;
            }
        }

        public async Task<bool> PutHotelAsync(int id, Hotel hotel)
        {
            bool ok = false;
            using (HttpClient client = new HttpClient())
            {
                string jsonString = JsonConvert.SerializeObject(hotel);
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
