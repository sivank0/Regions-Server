using Domain.Location.Countries;
using Domain.Location.Regions;
using Domain.Services.Location.Regions;
using Services.Location.Countries;
using Services.Location.Regions.Repository;
using Tools.Types;

namespace Services.Location.Regions;
public class RegionService : IRegionService
{
    private CountryService _countryService;
    public RegionService(CountryService countryService)
    {
        _countryService = countryService;
    }
    private readonly RegionRepository _repository = new RegionRepository();
    public Result SaveRegion(RegionBlank regionBlank)
    {
        if (string.IsNullOrWhiteSpace(regionBlank.Name))
            return Result.Failed("Название региона записано некорректно");

        if (string.IsNullOrWhiteSpace(regionBlank.ShortName))
            return Result.Failed("Сокращенное название региона записано некорректно");

        if (regionBlank.CountryCodes is null || regionBlank.CountryCodes.Length == 0)
            return Result.Failed($"Не введены страны, входящие в {regionBlank.Name}");

        if (_countryService.GetCountriesByCodes(regionBlank.CountryCodes).Length != regionBlank.CountryCodes.Length)
            return Result.Failed($"В регионе {regionBlank.Name} отсутствуют страны");

        _repository.SaveRegion(regionBlank);

        return Result.Succes();
    }
    public Result RemoveRegion(Guid id)
    {
        Region? region = GetRegion(id);

        if (region is null)
            return Result.Failed("Региона не существует");

        _repository.RemoveRegion(id);

        return Result.Succes();
    }
    public Region? GetRegion(Guid id) => _repository.GetRegion(id);

    public Page<Region> GetRegionsPage(Int32 page, Int32 countInPage, String searchingText) => 
        _repository.GetRegionsPage(page, countInPage, searchingText);
}
