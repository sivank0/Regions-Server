using Domain.Location.Countries;
using Npgsql;
using Tools.Database;
using Tools.Types;

namespace Services.Location.Countries.Repository;
public class CountriesRepository
{
    public void SaveCountry(CountryBlank countryBlank)
    {
        DatabaseUtils.UseSqlCommand(command =>
        {
            command.CommandText = "Insert into countries values (@p_code, @p_name, @p_population_number, @p_currentDateTimeUtc, null, false, @p_foundationDate) " +
            "on conflict (code) do update set name = @p_name, population_number = @p_population_number," +
            " modified_datetime = @p_currentDateTimeUtc, is_removed = false, foundation_date = @p_foundationDate";

            command.Parameters.AddWithValue("p_code", countryBlank.Code!);
            command.Parameters.AddWithValue("p_name", countryBlank.Name!);
            command.Parameters.AddWithValue("p_currentDateTimeUtc", DateTime.UtcNow);
            command.Parameters.AddWithValue("p_population_number", countryBlank.PopulationNumber!);
            command.Parameters.AddWithValue("p_foundationDate", countryBlank.FoundationDate!);

            command.ExecuteNonQuery();
        });
    }

    public void RemoveCountry(CountryCode code)
    {
        DatabaseUtils.UseSqlCommand(command =>
        {
            command.CommandText = "Update countries set is_removed = true, modified_datetime = @p_modified_datetime where code = @p_code";

            command.Parameters.AddWithValue("p_code", (Int32)code);
            command.Parameters.AddWithValue("p_modified_datetime", DateTime.UtcNow);

            command.ExecuteNonQuery();
        });
    }

    public Country? GetCountry(CountryCode code)
    {
        return DatabaseUtils.UseSqlCommand(command =>
        {
            command.CommandText = "Select code, name, population_number, foundation_date from countries where code = @p_code and is_removed = false";

            command.Parameters.AddWithValue("p_code", (Int32)code);

            using (NpgsqlDataReader reader = command.ExecuteReader())
                while (reader.Read())
                    return CountryMapper.ToCountry(reader);

            return null;
        });
    }
    public Country[] GetCountriesByCodes(CountryCode[] codes)
    {
        return DatabaseUtils.UseSqlCommand(command =>
        {
            List<Country> countries = new List<Country>();
            command.CommandText = "Select code, name, population_number, foundation_date from countries where code = any(@p_codes)";

            command.Parameters.AddWithValue("p_codes", codes);
            using NpgsqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
                countries.Add(CountryMapper.ToCountry(reader));

            return countries.ToArray();
        });
       
    }
    public Country? GetCountryByName(String name)
    {
        return DatabaseUtils.UseSqlCommand(command =>
        {
            command.CommandText = "Select code, name, population_number, foundation_date from countries where name = @p_name and is_removed = false";

            command.Parameters.AddWithValue("p_code", name);

            using (NpgsqlDataReader reader = command.ExecuteReader())
                while (reader.Read())
                    return CountryMapper.ToCountry(reader);

            return null;
        });
    }

    public Page<Country> GetCountriesPage(Int32 page, Int32 countInPage, String searchText)
    {
        return DatabaseUtils.UseSqlCommand(command =>
        {
            List<Country> countries = new List<Country>();
            Int32 totalCount = 0;

            command.CommandText = "Select code, name, population_number, foundation_date, count(*) over() as count " +
                                    "from countries where (name ilike @p_name or cast(code as varchar) ilike @p_name) " +
                                    "and is_removed = false order by name limit @p_countInPage offset @p_page";
            
            command.Parameters.AddWithValue("p_name", $"%{searchText}%");
            command.Parameters.AddWithValue("p_countInPage", countInPage);
            command.Parameters.AddWithValue("p_page", (page - 1) * countInPage);

            using NpgsqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                totalCount = Convert.ToInt32(reader["count"]);
                countries.Add(CountryMapper.ToCountry(reader));
            }
            return new Page<Country>(countries.ToArray(), totalCount);
        });
    }
}
