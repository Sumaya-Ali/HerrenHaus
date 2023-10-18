using FakeItEasy;
using HerrenHaus_API.Logging;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

namespace HerrenHaus_API_IntegrationTests
{
    public class CustomWebApplicationFactory: WebApplicationFactory<Program>
    {
        public ILogging logging;

        public CustomWebApplicationFactory()
        {
            logging = A.Fake<ILogging>();
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            base.ConfigureWebHost(builder);
            builder.ConfigureTestServices(services =>
                services.AddSingleton(logging)
            );
        }
    }
}
