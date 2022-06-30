using Domain.Location.Countries;

namespace Domain.Location.Regions;
public class Region
{
    Guid Id { get; }
    String Name { get; }
    String ShortName { get; }
    CountryCode[] CountryCodes { get; }
    public Region(Guid id, String name, String shortName, CountryCode[] countryCodes)
    {
        Id = id;
        Name = name;
        ShortName = name;
        CountryCodes = countryCodes;
    }
}
