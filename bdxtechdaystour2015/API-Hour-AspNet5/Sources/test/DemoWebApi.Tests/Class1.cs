using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoWebApi.Tests
{
    using DemoWebApi.Models;
    using Microsoft.AspNet.Builder;
    using Microsoft.AspNet.TestHost;
    using Microsoft.Framework.DependencyInjection;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using Xunit;

    public class BiereTests
    {
        private readonly Action<IApplicationBuilder> _app;
        private readonly Action<IServiceCollection> _services;

        public BiereTests()
        {
            var startup = new Startup();
            _app = startup.Configure;
            _services = startup.ConfigureServices;
        }

        [Fact]
        public async Task GetAllHoublon_OK()
        {
            // Arrange
            var server = TestServer.Create(_app, _services);
            var client = server.CreateClient();

            // Act
            var response = await client.GetAsync("http://localhost/api/houblons");
            var deserialized = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var obj = JsonConvert.DeserializeObject<List<Houblon>>(deserialized);
            Assert.True(obj.Any());
        }
    }

}
