using Domain.Location.Countries;

namespace Domain.Location.Regions;
public class RegionBlank
{
    public Guid? Id { get; set; }
    public String? Name { get; set; }
    public String? ShortName { get; set; }
    public CountryCode[]? CountryCodes { get; set; }
}
