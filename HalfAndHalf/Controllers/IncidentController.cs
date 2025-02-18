using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HalfAndHalf.Data;
using HalfAndHalf.Models;

namespace HalfAndHalf.Controllers
{
    public class IncidentController : Controller
    {
        private readonly ApplicationDbContext _context;
        private const int PageSize = 10;
        public IncidentController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Incident
        public async Task<IActionResult> Index(int page = 1)
        {
            var incidents = await _context.Incidents
                .Include(i => i.Company)
                .Include(i => i.Railroad)
                .Include(i => i.IncidentTrain)
                .AsNoTracking()
                .ToListAsync();

            return View(incidents);
        }

        // GET: Incident/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var incident = await _context.Incidents
                .Include(i => i.Company)
                .Include(i => i.Railroad)
                .Include(i => i.IncidentTrain)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Seqnos == id);

            if (incident == null)
            {
                return NotFound();
            }

            return View(incident);
        }

        public async Task<IActionResult> TypeDistribution()
        {
            var incidentTypes = await _context.Incidents
                .GroupBy(i => i.TypeOfIncident)
                .Select(g => new { Type = g.Key, Count = g.Count() })
                .ToListAsync();

            return View(incidentTypes);
        }
    }
}