using Npgsql;

namespace Tools.Database;
public static class DatabaseUtils
{
    const String connectionString = "Server=localhost;Username=postgres;Password=1111;Database=Universe";

    public static void UseSqlCommand(Action<NpgsqlCommand> getCommand)
    {
        using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
        {
            connection.Open();

            using (NpgsqlCommand command = new NpgsqlCommand())
            {
                command.Connection = connection;
                getCommand(command);
            }
        }
    }


    public static T UseSqlCommand<T>(Func<NpgsqlCommand, T> getCommand)
    {
        using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
        {
            connection.Open();

            using (NpgsqlCommand command = new NpgsqlCommand())
            {
                command.Connection = connection;
                return getCommand(command);
            }
        }
    }
}
