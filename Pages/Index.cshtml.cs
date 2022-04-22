using DataAccessLibrary.Models;
using DataAccessLibrary1.DataAccess;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PostsByRating.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ApplicationDbContext _db;

        public IndexModel(ILogger<IndexModel> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public void OnGet()
        {
            //populate DB

            /*
            List<User> users = new List<User> { new User { Email = "admin", Password = "admin" }, new User { Email = "admin1", Password = "admin" }, new User { Email = "admin2", Password = "admin" }, new User { Email = "admin3", Password = "admin" }, new User { Email = "admin4", Password = "admin" }, new User { Email = "admin5", Password = "admin" } };
            _db.User.AddRange(users);

            List<Post> posts = new List<Post>();

            for (int i = 0; i < 30; i++)
            {
                Post post = new Post { };
                posts.Add(post);
            }

            _db.Posts.AddRange(posts);

            _db.SaveChanges();*/
        }


    }
}