using Npgsql;

namespace HealthApplication.Utilities
{
    public static class Utility
    {

        /// <summary>
        /// Create connection string for DB connection
        /// </summary>
        /// <param name="databaseUrl"></param>
        /// <returns>Connection string</returns>
        public static string GetBuilder(string databaseUrl)
        {
            var databaseUri = new Uri(databaseUrl, true);
            var userInfo = databaseUri.UserInfo.Split(':');

            var builder = new NpgsqlConnectionStringBuilder
            {
                Host = databaseUri.Host,
                Port = databaseUri.Port,
                Username = userInfo[0],
                Password = userInfo[1],
                Database = databaseUri.LocalPath.TrimStart('/')
            };

            return builder.ToString();
        }

    }
}
