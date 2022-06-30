using Domain.Location.Countries;
using Tools.Types;

namespace Domain.Services.Location.Countries;
public interface ICountriesService
{
    public Result SaveCountry(CountryBlank countryBlank);
    public Result RemoveCountry(CountryCode countryCode);
    public Country? GetCountry(CountryCode countryCode);
    public Page<Country> GetCountriesPage(Int32 page, Int32 countInPage, String searchText);

}
