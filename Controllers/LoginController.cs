using DataAccessLibrary.Models;
using DataAccessLibrary1.DataAccess;
using Microsoft.AspNetCore.Mvc;

namespace PostsByRating.Controllers
{

    public class LoginController : Controller
    {

        private readonly ApplicationDbContext _db;

        private readonly ILogger<LoginController> _logger;

        public LoginController(ILogger<LoginController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        [HttpGet]
        [Route("/Login")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]

        public IActionResult Login(User user)
        {
            user.Id = _db.User.Where(u => u.Email == user.Email && u.Password == user.Password).Select(x => x.Id).FirstOrDefault();
            if (user.Id > 0)
            {
                return RedirectToAction("index", "Posts", user);
            }
            else
            {
                return View("LoginFailed");
            }

        }
    }
}
