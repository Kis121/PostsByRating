using DataAccessLibrary.Models;
using DataAccessLibrary1.DataAccess;
using Microsoft.AspNetCore.Mvc;

namespace PostsByRating.Controllers
{
    public class PostsController : Controller
    {
        private readonly ApplicationDbContext _db;
        private static int _userId;

        public PostsController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        [Route("/posts")]
        public IActionResult Index(User user)
        {
            _userId = user.Id;
            List<Post> posts = _db.Posts.ToList()
                .OrderByDescending(p => p.Diference)
                .ThenByDescending(p => p.Percentage)
                .ThenByDescending(p => p.UpVotes)
                .Select(p => new Post(p.Id, p.UpVotes, p.DownVotes, p.Diference, p.Percentage))
                .ToList();


            return View(posts);
        }

        [HttpGet]
        [Route("/DownVote/{postId}")]
        public IActionResult DownVote(int postId)
        {
            Voting voting = _db.Voting.Where(p => p.PostId == postId && p.UserId == _userId).FirstOrDefault();
            Post post = _db.Posts.Where(v => v.Id == postId).FirstOrDefault();

            if (voting == null || voting.VoteValue == 0) //only get new entries
            {
                voting = new Voting(_userId, postId, -1);

                if (post != null)
                {
                    post = UpdatePostVotingData(post, -1);
                    _db.Posts.Update(post);
                    _db.Voting.Add(voting);

                    _db.SaveChanges();
                }
                else
                {
                    throw new Exception("Post value is null");
                }


            }
            else
            {
                if (voting.VoteValue == 1) //edit existing entries only if voting changes
                {

                    voting.VoteValue = -1;

                    if (post != null)
                    {

                        post.UpVotes--;
                        post.Diference--;

                        post = UpdatePostVotingData(post, -1);
                        _db.Posts.Update(post);

                        _db.Voting.Update(voting);

                        _db.SaveChanges();
                    }
                    else
                    {
                        throw new Exception("Post value is null");
                    }

                }
            }

            return RedirectToAction("index");
        }

        [HttpGet]
        [Route("UpVote/{PostId}")]
        public IActionResult UpVote(int postId)
        {
            Voting voting = _db.Voting.Where(p => p.PostId == postId && p.UserId == _userId).FirstOrDefault();
            Post post = _db.Posts.Where(v => v.Id == postId).FirstOrDefault();

            if (voting == null || voting.VoteValue == 0) //only get new entries
            {
                voting = new Voting(_userId, postId, 1);

                if (post != null)
                {
                    post = UpdatePostVotingData(post, 1);

                    _db.Posts.Update(post);
                    _db.Voting.Add(voting);

                    _db.SaveChanges();
                }
                else
                {
                    throw new Exception("Post value is null");
                }


            }
            else
            {
                if (voting.VoteValue == -1) //edit existing entries only if voting changes
                {

                    voting.VoteValue = 1;

                    if (post != null)
                    {

                        post.DownVotes--;
                        post.Diference++;

                        post = UpdatePostVotingData(post, 1);

                        _db.Posts.Update(post);
                        _db.Voting.Update(voting);

                        _db.SaveChanges();
                    }
                    else
                    {
                        throw new Exception("Post value is null");
                    }

                }
            }
            return RedirectToAction("index");
        }

        public Post UpdatePostVotingData(Post post, int vote)
        {
            if (vote > 0)
            {
                post.UpVotes++;
            }
            else if (vote < 0)
            {
                post.DownVotes++;
            }

            post.Diference += vote;

            post.Percentage = post.UpVotes * 100 / (post.UpVotes + post.DownVotes); //3 simple rule to get percentage

            return post;
        }

    }
}
