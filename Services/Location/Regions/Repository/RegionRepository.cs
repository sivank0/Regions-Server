using Domain.Location.Regions;
using Npgsql;
using Tools.Database;
using Tools.Types;

public class RegionRepository
{
    public void SaveRegion(RegionBlank regionBlank)
    {
        DatabaseUtils.UseSqlCommand(command =>
        {
            command.CommandText = "Insert into regions values (@p_id, @p_name, @p_short_name, @p_country_code," +
            " @p_currentDateTimeUtc, null, false) " +
            "on conflict (id) do update set name = @p_name, short_name = @p_short_name, country_code = @p_country_code" +
            " modified_datetime = @p_currentDateTimeUtc, is_removed = false";

            command.Parameters.AddWithValue("p_id", regionBlank.Id!);
            command.Parameters.AddWithValue("p_name", regionBlank.Name!);
            command.Parameters.AddWithValue("p_short_name", regionBlank.ShortName!);
            command.Parameters.AddWithValue("p_country_code", regionBlank.CountryCodes!);
            command.Parameters.AddWithValue("p_currentDateTimeUtc", DateTime.UtcNow);

            command.ExecuteNonQuery();
        });
    }
    public void RemoveRegion(Guid id)
    {
        DatabaseUtils.UseSqlCommand(command =>
        {
            command.CommandText = "Update regions set is_removed = true, modified_datetime = @p_modified_datetime where id = @p_id";

            command.Parameters.AddWithValue("p_id", id);
            command.Parameters.AddWithValue("p_modified_datetime", DateTime.UtcNow);

            command.ExecuteNonQuery();
        });
    }
    public Region? GetRegion(Guid id)
    {
        return DatabaseUtils.UseSqlCommand(command =>
        {
            command.CommandText = "Select id, name, short_name, country_code from countries where id = @p_id and is_removed = false";

            command.Parameters.AddWithValue("p_id", id);

            using (NpgsqlDataReader reader = command.ExecuteReader())
                if (reader.Read())
                    return RegionMapper.ToRegion(reader);

            return null;
        });
    }

    public Page<Region> GetRegionsPage(Int32 page, Int32 countInPage)
    {
        List<Region> regions = new List<Region>();

        Int32 totalCount = 0;

        DatabaseUtils.UseSqlCommand(command =>
        {
            command.CommandText = "Select id, name, short_name, country_code, count(*) over() as count " +
                                    "from regions where is_removed = false order by name limit @p_countInPage offset @p_page";

            command.Parameters.AddWithValue("p_countInPage", countInPage);
            command.Parameters.AddWithValue("p_page", (page - 1) * countInPage);

            using NpgsqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                totalCount = Convert.ToInt32(reader["count"]);
                regions.Add(RegionMapper.ToRegion(reader));
            }
        });
        return new Page<Region>(regions.ToArray(), totalCount);
    }
}
