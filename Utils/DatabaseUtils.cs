namespace SmartIrrigatorAPI.Utils;

public class DatabaseUtils
{
    private readonly IConfiguration _configuration;

    public DatabaseUtils(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GetConnectionString()
    {
        string? env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        if (env == "Production")
        {
            string? dbHost = Environment.GetEnvironmentVariable("DATABASE_HOST");
            string? dbPort = Environment.GetEnvironmentVariable("DATABASE_PORT");
            string? dbName = Environment.GetEnvironmentVariable("DATABASE_NAME");
            string? dbUser = Environment.GetEnvironmentVariable("DATABASE_USER");
            string? dbPassword = Environment.GetEnvironmentVariable("DATABASE_PASSWORD");
            return
                $"Host={dbHost};Port={dbPort};Pooling=true;Database={dbName};User Id={dbUser};Password={dbPassword};";
        }
        else
        {
            return _configuration.GetValue<string>("ConnectionStrings:PostgreSQL")!;
        }
    }

}