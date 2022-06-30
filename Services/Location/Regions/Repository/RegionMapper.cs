using Domain.Location.Countries;
using Domain.Location.Regions;
using Npgsql;

public class RegionMapper
{
    public static Region ToRegion(NpgsqlDataReader reader)
    {
        return new Region((Guid)reader["id"], (String)reader["name"], (String)reader["short_name"], (CountryCode[])reader["country_code"]);
    }
}
