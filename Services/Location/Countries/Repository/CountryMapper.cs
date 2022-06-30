using Domain.Location.Countries;
using Npgsql;

namespace Services.Location.Countries.Repository; 
public static class CountryMapper
{
    public static Country ToCountry(NpgsqlDataReader reader)
    {
        return new Country((Int32)reader["code"], (String)reader["name"], (Int32)reader["population_number"], (DateTime)reader["foundation_date"]);
    }
}
