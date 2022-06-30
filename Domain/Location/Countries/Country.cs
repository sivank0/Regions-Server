namespace Domain.Location.Countries;
public class Country
{
    public CountryCode Code { get; }
    public String Name { get; }
    public Int32 PopulationNumber { get; }
    public DateTime FoundationDate { get; }
    public Country(Int32 code, String name, Int32 populationNumber, DateTime foundationDate)
    {
        Code = (CountryCode)code;
        Name = name;
        PopulationNumber = populationNumber;
        FoundationDate = foundationDate;
    }
}
