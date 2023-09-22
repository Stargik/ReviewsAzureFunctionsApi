using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;

namespace ReviewsAzureFunctionsApi
{
    public class ReviewsGetAllCreate
    {
        private readonly ReviewsDbContext context;

        public ReviewsGetAllCreate(ReviewsDbContext context)
        {
            this.context = context;
        }

        [FunctionName("ReviewsGetAllCreate")]
        public async Task<IActionResult> Run( [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "reviews")] HttpRequest req)
        {
            if (req.Method == HttpMethods.Post)
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                var review = JsonConvert.DeserializeObject<Review>(requestBody);
                await context.Reviews.AddAsync(review);
                await context.SaveChangesAsync();
                return new CreatedResult("/reviews", review);
            }

            var reviews = await context.Reviews.ToListAsync();

            return new OkObjectResult(reviews);
        }
    }
}

