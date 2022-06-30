using Domain.Location.Countries;
using Domain.Services.Location.Countries;
using Services.Location.Countries.Repository;
using Tools.Types;

namespace Services.Location.Countries;
public class CountryService : ICountriesService
{
    private readonly CountriesRepository _repository = new CountriesRepository();
    public Result SaveCountry(CountryBlank countryBlank)
    {
        if (countryBlank.Code is null && countryBlank.Name is null && countryBlank.PopulationNumber is null)
            return Result.Failed("Данные не введены");

        if (countryBlank.Code is null)
            return Result.Failed("Код имеет неверный формат или отсутствует");

        if (!Enum.IsDefined(typeof(CountryCode), (CountryCode)countryBlank.Code))
            return Result.Failed("Вашей страны нет в нашей системе");

        if (string.IsNullOrWhiteSpace(countryBlank.Name))
            return Result.Failed("Название страны записано некорректно");

        if (countryBlank.PopulationNumber is null || countryBlank.PopulationNumber <= 0)
            return Result.Failed("Значения численности населения записано некорректно");

        _repository.SaveCountry(countryBlank);

        return Result.Succes();
    }
    public Result RemoveCountry(CountryCode countryCode)
    {
        Country? country = GetCountry(countryCode);

        if (country is null)
            return Result.Failed("Страны не существует");

        _repository.RemoveCountry(countryCode);

        return Result.Succes();
    }
    public Country[] GetCountriesByCodes(CountryCode[] codes) => _repository.GetCountriesByCodes(codes);
    
    public Country? GetCountryByName(String name) => _repository.GetCountryByName(name);

    public Country? GetCountry(CountryCode countryCode) => _repository.GetCountry(countryCode);

    public Page<Country> GetCountriesPage(Int32 page, Int32 countInPage, String searchText) =>
        _repository.GetCountriesPage(page, countInPage, searchText);
}
