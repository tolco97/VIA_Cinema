using System.Configuration;

namespace DNP1.ViaCinema.Model
{
    /// <summary>
    /// 
    /// </summary>
    public static class ConnectionStringHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["PgsqlConnectionString"].ConnectionString;
        }
    }
}
