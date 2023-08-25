namespace StrategyPattern.WebApp.Models
{
    public class Settings
    {
        public static string ClaimDatabaseType = "databaseType";

        public EDatabaseType DatabaseType { get; set; }

        public EDatabaseType GetDefaultDatabaseType=> EDatabaseType.SqlServer;
    }


    public enum EDatabaseType
    {
        SqlServer,
        MongoDb
    }
}
