using Domain.Location.Regions;
using Domain.Services.Location.Regions;
using Tools.Types;

public class RegionService : IRegionService
{
    private readonly RegionRepository _repository = new RegionRepository();
    public Result SaveRegion(RegionBlank regionBlank)
    {
        if (regionBlank.Id is null && regionBlank.Name is null && regionBlank.ShortName is null
            && (regionBlank.CountryCodes is null || regionBlank.CountryCodes.Length == 0))
            return Result.Failed("Данные не введены");

        if (regionBlank.Id is null)
            return Result.Failed("ID имеет неверный формат или отсутствует");

        if (string.IsNullOrWhiteSpace(regionBlank.Name))
            return Result.Failed("Название страны записано некорректно");

        if (string.IsNullOrWhiteSpace(regionBlank.ShortName))
            return Result.Failed("Короткое название страны записано некорректно");

        if (regionBlank.CountryCodes is null || regionBlank.CountryCodes.Length == 0)
            return Result.Failed($"В регионе {regionBlank.Name} страны записаны некорректно");

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

    public Page<Region> GetRegionsPage(Int32 page, Int32 countInPage) => _repository.GetRegionsPage(page, countInPage);
}
