using Domain.Location.Countries;
using Domain.Location.Regions;
using Microsoft.AspNetCore.Mvc;
using Services.Location.Countries;
using System.Diagnostics;
using Tools.Types;
using WebApplication1.Models;

namespace WebApplication1.Controllers;
public class HomeController : Controller
{
    private readonly CountryService _countryService = new CountryService();
    private readonly RegionService _regionService = new RegionService();

    private readonly ILogger<HomeController> _logger;

    [HttpPost("SaveCountry")]
    public Result SaveCountry([FromBody] CountryBlank countryBlank) => _countryService.SaveCountry(countryBlank);

    [HttpGet("RemoveCountry")]
    public Result RemoveCountry([FromQuery] CountryCode code) => _countryService.RemoveCountry(code);

    [HttpGet("GetCountry")]
    public Country? GetCountry(CountryCode code) => _countryService.GetCountry(code);

    [HttpGet("GetCountriesPage")]
    public Page<Country> GetCountriesPage(Int32 page, Int32 countInPage, String searchText) => _countryService.GetCountriesPage(page, countInPage, searchText);
    
    [HttpPost("SaveRegion")]
    public Result SaveRegion([FromBody] RegionBlank regionBlank) => _regionService.SaveRegion(regionBlank);

    [HttpGet("RemoveRegion")]
    public Result RemoveRegion([FromQuery] Guid id) => _regionService.RemoveRegion(id);

    [HttpGet("GetRegion")]
    public Region? GetRegion(Guid id) => _regionService.GetRegion(id);

    [HttpGet("GetRegionsPage")]
    public Page<Region> GetRegionsPage(Int32 page, Int32 countInPage) => _regionService.GetRegionsPage(page, countInPage);


    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
