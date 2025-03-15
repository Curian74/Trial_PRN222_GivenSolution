using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Project2.Models;
using System.Threading.Tasks;

namespace Project2.Pages.Movies
{
    [BindProperties]
    public class IndexModel : PageModel
    {
        private readonly PePrn222TrialContext _context;
        public List<Movie> Movies = new List<Movie>();
        public List<Director> Directors = new List<Director>();

        public IndexModel(PePrn222TrialContext context)
        {
            _context = context;
        }

        public async Task OnGet(int? id)
        {
            Directors = await _context.Directors.ToListAsync();

            if(id != null)
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
    }
}
