using System;
using System.Linq;
using System.Threading.Tasks;
using ALS.Printify.Net;
using NUnit.Framework;

namespace ALS.Printify.Tests
{
    public class Tests
    {
        private PrintifyClient _client = null;
        [SetUp]
        public void Setup()
        {
            _client = new PrintifyClient(Environment.GetEnvironmentVariable("PRINTIFY_TOKEN"));
        }

        [Test]
        public async Task TestGetShopsRequest()
        {
            var shops = await _client.GetShops();
            Assert.Greater(shops.Count(), 0);
        }

        [Test]
        public async Task TestGetBlueprintsRequest()
        {
            var blueprints = await _client.GetBlueprints();
            Assert.Greater(blueprints.Count(), 10);
        }

        [Test]
        public async Task TestGetBlueprintRequest()
        {
            var blueprint = await _client.GetBlueprint(3);
            Assert.AreEqual(blueprint.Title, "Kids Regular Fit Tee");
        }


        [Test]
        public async Task TestGetProvidersRequest()
        {
            var providers = await (await _client.GetBlueprint(3)).GetProviders();
            Assert.Greater(providers.Count, 0);
        }

        [Test]
        public async Task TestGetFullProvidersRequest()
        {
            var providers = await (await _client.GetBlueprint(3)).GetProviders(true);
            Assert.Greater(providers.Count, 0);
            foreach (var provider in providers)
            {
                Assert.NotNull(provider.Location);
            }
        }

        [Test]
        public async Task TestGetVariantsRequest()
        {
            var variants = await (await (await _client.GetBlueprint(3)).GetProviders())[0].GetVariants();
            Assert.Greater(variants.Count, 0);
        }

        [Test]
        public async Task TestGetShippingInfo()
        {
            var bluePrintId = 3;
            var provider = (await (await _client.GetBlueprint(bluePrintId)).GetProviders())[0];
            var info = await _client.GetShippingInfo(bluePrintId, provider.Id);
            Assert.Greater(info.Profiles[0].Countries.Length, 0);
            Assert.Greater(info.HandlingTime.Value, 0);
            Assert.Greater(info.Profiles[0].VariantIds.Length, 0);
        }
    }
}