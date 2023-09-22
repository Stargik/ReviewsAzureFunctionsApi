using System;
namespace ReviewsAzureFunctionsApi
{
	public class Review
	{
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string TextContent { get; set; }
	}
}

