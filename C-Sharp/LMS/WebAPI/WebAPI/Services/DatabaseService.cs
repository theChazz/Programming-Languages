using System.Data.SqlClient;

namespace WebAPI.Services
{
    public interface IDatabaseService
    {
        string GetConnectionString();
    }

    public class DatabaseService : IDatabaseService
    {
        private readonly IConfiguration _configuration;

        public DatabaseService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetConnectionString()
        {
            return _configuration.GetConnectionString("DefaultConnection");
        }
    }
} 