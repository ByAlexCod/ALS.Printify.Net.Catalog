using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using ALS.Printify.Net.Get;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ALS.Printify.Net
{
    public class PrintifyClient
    {
        private readonly string _token = null;
        public PrintifyClient(string authToken)
        {
            _token = authToken;
        }

        public async Task<IEnumerable<Shop>> GetShops()
        {
            using (var client = CreateClient())
            {
                var response = await client.GetAsync("shops.json");
                var responseString = await response.Content.ReadAsStringAsync();
                var shops = JsonConvert.DeserializeObject<List<Shop>>(responseString);
                shops = FillClients(shops);
                return shops;
            }
        }

        public async Task<IEnumerable<Blueprint>> GetBlueprints()
        {
            using (var client = CreateClient())
            {
                var response = await client.GetAsync("catalog/blueprints.json");
                var responseString = await response.Content.ReadAsStringAsync();
                var blueprints = JsonConvert.DeserializeObject<List<Blueprint>>(responseString);
                blueprints = FillClients(blueprints);
                return blueprints;
            }
        }

        public async Task<Blueprint> GetBlueprint(int id)
        {
            using (var client = CreateClient())
            {
                var response = await client.GetAsync($"catalog/blueprints/{id}.json");
                var responseString = await response.Content.ReadAsStringAsync();
                var blueprint = JsonConvert.DeserializeObject<Blueprint>(responseString);
                blueprint.PrintifyClient = this;
                return blueprint;
            }
        }

        /// <summary>
        /// Get a list of Providers for the blueprint
        /// </summary>
        /// <param name="blueprint">Blueprint</param>
        /// <param name="fullResponse">Complete the result with location of the provider (default false). => +1 HTTPS requests</param>
        /// <returns>The list of the Blueprint providers with or without address.</returns>
        public async Task<IEnumerable<Provider>> GetProviders(Blueprint blueprint, bool fullResponse = false)
        {
            using (var client = CreateClient())
            {
                var response = await client.GetAsync($"catalog/blueprints/{blueprint.Id}/print_providers.json");
                var responseString = await response.Content.ReadAsStringAsync();
                var providers = JsonConvert.DeserializeObject<List<Provider>>(responseString);
                providers = FillClients(providers);
                foreach (var provider in providers)
                {
                    provider.Blueprint = blueprint;
                }

                if (!fullResponse) return providers;
                
                var allProviders = await GetAllProviders();
                foreach (var provider in providers)
                {
                    provider.Location = allProviders.FirstOrDefault(e => e.Id == provider.Id)?.Location;
                }
                return providers;
            }
        }

        public async Task<List<Provider>> GetAllProviders()
        {
            using (var client = CreateClient())
            {
                var response = await client.GetAsync($"catalog/print_providers.json");
                var responseString = await response.Content.ReadAsStringAsync();
                var providers = JsonConvert.DeserializeObject<List<Provider>>(responseString);
                providers = FillClients(providers);
                return providers;
            }
        }

        public async Task<IEnumerable<Variant>> GetVariants(long blueprintId, long providerId)
        {
            using (var client = CreateClient())
            {
                var response = await client.GetAsync($"catalog/blueprints/{blueprintId}/print_providers/{providerId}/variants.json");
                var responseString = await response.Content.ReadAsStringAsync();
                var variants = JsonConvert.DeserializeObject<List<Variant>>(JObject.Parse(responseString)["variants"].ToString());
                return variants;
            }
        }

        public async Task<Shipping> GetShippingInfo(long blueprintId, long providerId)
        {
            using (var client = CreateClient())
            {
                var response = await client.GetAsync($"catalog/blueprints/{blueprintId}/print_providers/{providerId}/shipping.json");
                var responseString = await response.Content.ReadAsStringAsync();
                var variants = JsonConvert.DeserializeObject<Shipping>(responseString);
                return variants;
            }
        }

        private List<T> FillClients<T>(List<T> objs)
        {
            foreach (var obj in objs)
            {
                ((dynamic)obj).PrintifyClient = this;
            }
            return objs;
        }


        private HttpClient CreateClient()
        {
            var client = new HttpClient(new HttpClientHandler(), false)
            {
                BaseAddress = new Uri("https://api.printify.com/v1/")
            };
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + _token);
            return client;
        }
    }
}
