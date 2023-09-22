using System;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(ReviewsAzureFunctionsApi.Startup))]

namespace ReviewsAzureFunctionsApi
{
	public class Startup : FunctionsStartup
	{
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var connectionString = Environment.GetEnvironmentVariable("SqlConnectionString");
            builder.Services.AddDbContext<ReviewsDbContext>(options => options.UseSqlServer(connectionString));
        }
    }
}

