using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Project2.Hubs;
using Project2.Models;

namespace Project2.Pages.Movies
{
    public class Director_MovieModel : PageModel
    {
        private readonly IHubContext<MovieHub> _hubContext;
        private readonly PePrn222TrialContext _context;
        public List<Movie> Movies = new List<Movie>();
        public List<Director> Directors = new List<Director>();

        public Director_MovieModel(PePrn222TrialContext context, IHubContext<MovieHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }

        public async Task OnGet(int? id)
        {
            Directors = await _context.Directors.ToListAsync();

            if (id != null)
            {
                Movies = await _context.Movies
                    .Include(c => c.Director)
                    .Include(c => c.Producer)
                    .Include(c => c.Stars)
                    .Where(m => m.DirectorId == id)
                    .ToListAsync();
            }

            else
            {
                Movies = await _context.Movies.Include(c => c.Director).Include(c => c.Producer).Include(c => c.Stars).ToListAsync();
            }
        }

        public async Task OnGetDelete(int? id)
        {
            try
            {
                var movie = await _context.Movies
                    .Include(m => m.Genres)
                    .Include(m => m.Stars)
                    .FirstOrDefaultAsync(m => m.Id == id);

                if (movie != null)
                {
                    movie.Genres.Clear();

                    movie.Stars.Clear();

                    _context.Movies.Remove(movie);
                    await _context.SaveChangesAsync();
                    await _hubContext.Clients.All.SendAsync("ReceiveMessage");
                }

            }
            catch (Exception)
            {
                Console.WriteLine("cc");
            }

        }
    }
}
