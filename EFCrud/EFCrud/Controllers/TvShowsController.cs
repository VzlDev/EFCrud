using EFCrud.Data;
using EFCrud.Model;
using Microsoft.AspNetCore.Mvc;

namespace EFCrud.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TvShowsController : ControllerBase
    {
        private readonly TvShowContext _context;

        public TvShowsController(TvShowContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<TvShow>> GetTvShows()
        {
            return _context.TvShows.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<TvShow> GetTvShow(int id)
        {
            var tvshow = _context.TvShows.Find(id);
            if (tvshow == null)
            {
                return NotFound();
            }
            return tvshow;
        }

        [HttpPost]
        public ActionResult<TvShow> CreateTvShow(TvShow tvShow)
        {
            if (tvShow == null)
            {
                return BadRequest();
            }
            _context.TvShows.Add(tvShow);
            _context.SaveChanges();
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteTvShow(int id)
        {
            var tvshow = _context.TvShows.Find(id);
            if (tvshow == null)
            {
                return NotFound();
            }
            _context.TvShows.Remove(tvshow);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpPut("{id}")]
        public ActionResult<TvShow> UpdateTvShow(int id, TvShow tvShow)
        {
            var existingTvShow = _context.TvShows.Find(id);
            if (existingTvShow == null)
            {
                return NotFound();
            }

            existingTvShow.Name = tvShow.Name;
            existingTvShow.ImdbRating = tvShow.ImdbRating;

            _context.SaveChanges();
            return NoContent();
        }
    }
}

