using System.Configuration;

namespace DNP1.ViaCinema.Model
{
    public static class ConnectionStringHelper
    {
        public static string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["PgsqlConnectionString"].ConnectionString;
        }
    }
}
