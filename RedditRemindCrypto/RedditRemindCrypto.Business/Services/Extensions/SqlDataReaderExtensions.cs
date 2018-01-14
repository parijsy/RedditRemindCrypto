using System.Data.SqlClient;

namespace RedditRemindCrypto.Business.Services.Extensions
{
    public static class SqlDataReaderExtensions
    {
        public static bool IsDBNull(this SqlDataReader reader, string columnName)
        {
            var ordinal = reader.GetOrdinal(columnName);
            return reader.IsDBNull(ordinal);
        }
    }
}
