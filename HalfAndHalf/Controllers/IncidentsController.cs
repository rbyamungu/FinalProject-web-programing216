using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization; // Import authorization attributes
using Microsoft.EntityFrameworkCore;
using HalfAndHalf.Data;
using HalfAndHalf.Models;
using HalfAndHalf.ViewModels;

namespace HalfAndHalf.Controllers
{
    // Require authentication for all actions in this controller
    [Authorize]
    public class IncidentsController : Controller
    {
        // Private field to interact with the database context
        private readonly ApplicationDbContext _context;

        // Constructor to inject the database context
        public IncidentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Action method to display paginated list of incidents
        // Requires authentication to access
        public async Task<IActionResult> Index(string? searchString, int pageNumber = 1)
        {
            // Start with a queryable collection of incidents, including related entities
            var query = _context.Incidents
                .Include(i => i.Company)
                .Include(i => i.Railroad)
                .AsQueryable();

            // Apply search filtering if a search string is provided
            if (!string.IsNullOrEmpty(searchString))
            {
                // Search across multiple incident properties
                query = query.Where(i => 
                    (i.DescriptionOfIncident ?? "").Contains(searchString) || 
                    (i.ResponsibleCity ?? "").Contains(searchString) ||
                    (i.TypeOfIncident ?? "").Contains(searchString));
            }

            // Set the number of items per page
            int pageSize = 10;

            // Retrieve paginated incidents, ordered by most recent first
            var items = await query.OrderByDescending(i => i.DateTimeReceived)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            // Count total number of matching incidents
            var totalCount = await query.CountAsync();
            
            // Return view with paginated list of incidents
            return View(new PaginatedList<Incident>(
                items, 
                totalCount, 
                pageNumber, 
                pageSize));
        }

        // Action method to show details of a specific incident
        // Requires authentication to access
        public async Task<IActionResult> Details(int id)
        {
            // Retrieve incident with all related entities
            var incident = await _context.Incidents
                .Include(i => i.Company)
                .Include(i => i.Railroad)
                .Include(i => i.IncidentTrain)
                    .ThenInclude(t => t.TrainCars)
                .FirstOrDefaultAsync(i => i.Seqnos == id);

            // Return 404 if incident not found
            if (incident == null)
            {
                return NotFound();
            }

            // Display incident details
            return View(incident);
        }

        // API endpoint to retrieve incident statistics by date
        // Requires authentication to access
        [HttpGet]
        public async Task<IActionResult> GetIncidentStats()
        {
            // Group incidents by date and calculate statistics
            var stats = await _context.Incidents
                .GroupBy(i => i.DateTimeReceived.Date)
                .Select(g => new 
                {
                    Date = g.Key,
                    Count = g.Count(),
                    Injuries = g.Sum(i => i.InjuryCount ?? 0),
                    Fatalities = g.Sum(i => i.FatalityCount ?? 0)
                })
                .OrderBy(x => x.Date)
                .ToListAsync();

            // Return statistics as JSON
            return Json(stats);
        }

        // API endpoint to retrieve incident statistics by incident type
        // Requires authentication to access
        [HttpGet]
        public async Task<IActionResult> GetIncidentsByType()
        {
            // Group incidents by type and calculate statistics
            var stats = await _context.Incidents
                .GroupBy(i => i.TypeOfIncident)
                .Select(g => new
                {
                    Type = g.Key,
                    Count = g.Count(),
                    Injuries = g.Sum(i => i.InjuryCount ?? 0),
                    Fatalities = g.Sum(i => i.FatalityCount ?? 0)
                })
                .OrderByDescending(x => x.Count)
                .ToListAsync();

            // Return statistics as JSON
            return Json(stats);
        }

        // API endpoint to retrieve incident statistics by state
        // Requires authentication to access
        [HttpGet]
        public async Task<IActionResult> GetIncidentsByState()
        {
            // Group incidents by state and count occurrences
            var stats = await _context.Incidents
                .Where(i => !string.IsNullOrEmpty(i.ResponsibleState))
                .GroupBy(i => i.ResponsibleState)
                .Select(g => new
                {
                    State = g.Key,
                    Count = g.Count()
                })
                .OrderByDescending(x => x.Count)
                .ToListAsync();

            // Return statistics as JSON
            return Json(stats);
        }

        // Optional: Provide a custom access denied page
        // This action can be accessed without authentication
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            // Return a view explaining access is denied
            return View();
        }
    }
}