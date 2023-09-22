using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ReviewsAzureFunctionsApi
{
    public class ReviewsGetByIdUpdateDelete
    {
        private readonly ReviewsDbContext context;

        public ReviewsGetByIdUpdateDelete(ReviewsDbContext context)
        {
            this.context = context;
        }

        [FunctionName("ReviewsGetByIdUpdateDelete")]
        public async Task<IActionResult> Run( [HttpTrigger(AuthorizationLevel.Anonymous, "get", "put", "delete", Route = "reviews/{id}")] HttpRequest req, int id)
        {

            if (req.Method == HttpMethods.Get)
            {
                var review = await context.Reviews.FirstOrDefaultAsync(review => review.Id == id);
                if(review is null)
                {
                    return new NotFoundResult();
                }
                return new OkObjectResult(review);
            }
            else if (req.Method == HttpMethods.Put)
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                var review = JsonConvert.DeserializeObject<Review>(requestBody);
                review.Id = id;
                context.Reviews.Update(review);
                await context.SaveChangesAsync();
                return new OkObjectResult(review);
            }
            else
            {
                var review = await context.Reviews.FirstOrDefaultAsync(review => review.Id == id);
                if (review is null)
                {
                    return new NotFoundResult();
                }
                context.Reviews.Remove(review);
                await context.SaveChangesAsync();
                return new NoContentResult();
            }
        }
    }
}

