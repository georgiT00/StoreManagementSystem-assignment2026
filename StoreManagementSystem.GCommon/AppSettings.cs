namespace StoreManagementSystem.GCommon
{
    using Microsoft.Extensions.Configuration;

    public static class AppSettings
    {
        public static Database? Database { get; set; }

        public static void Initialize(IConfiguration configuration)
        {
            Database = new Database
            {
                ConnectionString = configuration["Database:ConnectionString"],
            };
        }
    }

    public class Database
    {
        public string? ConnectionString { get; set; }
    }
}
