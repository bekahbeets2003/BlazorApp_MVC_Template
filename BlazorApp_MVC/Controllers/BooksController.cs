using BlazorApp_MVC.Interfaces;
using BlazorApp_MVC.Models;
using BlazorApp_MVC.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace BlazorApp_MVC.Controllers
{
    public class BooksController : Controller
    {
        private readonly ILogger _logger;
        private readonly IDbDapper _dbDapper;
        private readonly Utility _utility;

        public BooksController(ILogger logger, IDbDapper dbDapper, Utility utility) 
        {
            _logger = logger;
            _utility = utility;
            _dbDapper = dbDapper;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                var booksTask = _dbDapper.GetAthingsAsync();

                IEnumerable<athing> books = await booksTask;

                return View(books);
            }
            catch(Exception ex)
            {
                //TODO: implement custom error handling later
               return View(ex);
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var bookTask = _dbDapper.GetAthingAsync(id);

                athing thing = await bookTask;

                return View(thing);
            }
            catch (Exception ex)
            {
                //TODO: implement custom error handling later
                return View(ex);
            }
        }
    }
}
