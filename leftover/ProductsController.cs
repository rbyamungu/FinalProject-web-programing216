using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductManagement.Data;
using ProductManagement.Models;
using ProductManagement.ViewModels;

namespace ProductManagement.Controllers;
public class ProductsController : Controller
{
    private readonly ApplicationDbContext _context;

    public ProductsController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index(string searchString, int? pageNumber)
    {
        var query = _context.Products.AsQueryable();

        if (!string.IsNullOrEmpty(searchString))
        {
            query = query.Where(p => p.Name.Contains(searchString) || 
                                   p.Description.Contains(searchString));
        }

        int pageSize = 10;
        return View(await PaginatedList<Product>.CreateAsync(
            query.OrderByDescending(p => p.CreatedDate), 
            pageNumber ?? 1, 
            pageSize));
    }

    [HttpGet]
    public async Task<IActionResult> GetProductStats()
    {
        var stats = await _context.Products
            .GroupBy(p => p.CreatedDate.Date)
            .Select(g => new 
            {
                Date = g.Key,
                Count = g.Count(),
                Revenue = g.Sum(p => p.Price)
            })
            .OrderBy(x => x.Date)
            .ToListAsync();

        return Json(stats);
    }
}