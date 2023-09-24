using ApiTranslateASP.NETWEB.Models; // Ensure you have the appropriate using directive for your model
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace ApiTranslateASP.NETWEB.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMongoCollection<DatabaseSettings> _collection; // Use IMongoCollection<T> for strong typing

        public HomeController(IMongoDatabase database)
        {
            // Specify the collection name "TRANSLATEAPP" here
            _collection = database.GetCollection<DatabaseSettings>("TRANSLATEAPP");
        }

        public IActionResult Index()
        {
            try
            {
                // Example: Query data from MongoDB
                var data = _collection.Find(FilterDefinition<DatabaseSettings>.Empty)
                                      .Limit(100) // Limit the result to 100 documents
                                      .ToList();

                // Pass the data to the view
                return View(data);
            }
            catch (Exception ex)
            {
                // Handle any exceptions here (e.g., log or return an error view)
                return View("Error", ex); // You can create an Error.cshtml view to display errors
            }
        }
    }
}
