using Microsoft.AspNetCore.Mvc;

namespace mvc;

public class HomeController : Controller
{
    private readonly IDataContext _context;

    public HomeController(IDataContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        List<Producto> products= await _context.ObtenProductosAsync();
        return View(products);
    }

    public IActionResult Error()
    {
        return View();
    }
}