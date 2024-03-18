using EFCrud.Data;
using EFCrud.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace EFCrud.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TvShowsController : ControllerBase
    {
        private readonly TvShowContext _context;
        private readonly IDistributedCache? _cache;

        public TvShowsController(TvShowContext context, IDistributedCache cache)
        {
            _context = context;
            _cache = cache;
        }

        [HttpGet]
        public ActionResult<IEnumerable<TvShow>> GetTvShows()
        {
            string cachedData = _cache.GetString("cachedData");
            if (string.IsNullOrEmpty(cachedData))
            {
                var tvShows = _context.TvShows.ToList();
                string serializedObject = JsonConvert.SerializeObject(tvShows);
                _cache.SetString("cachedData", serializedObject, new DistributedCacheEntryOptions() { AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10) });
                return tvShows;
            }

            return JsonConvert.DeserializeObject<List<TvShow>>(cachedData);
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

