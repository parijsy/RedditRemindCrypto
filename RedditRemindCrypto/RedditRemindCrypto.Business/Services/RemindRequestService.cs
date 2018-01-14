using RedditRemindCrypto.Business.Factories;
using RedditRemindCrypto.Business.Services.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace RedditRemindCrypto.Business.Services
{
    public class RemindRequestService : IRemindRequestService
    {
        private readonly string connectionString;

        public RemindRequestService(IConnectionStringFactory connectionStringFactory)
        {
            this.connectionString = connectionStringFactory.Create();
        }

        public void Save(RemindRequest request)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = connection.CreateCommand())
            {
                connection.Open();

                command.CommandText = $"INSERT INTO RemindRequests(Expression, [User], Permalink) VALUES ('{request.Expression}', '{request.User}', '{request.Permalink}')";
                command.ExecuteNonQuery();
            }
        }

        public void Delete(RemindRequest request)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = connection.CreateCommand())
            {
                connection.Open();

                command.CommandText = $"DELETE FROM RemindRequests WHERE Id = '{request.Id}'";
                command.ExecuteNonQuery();
            }
        }

        public IEnumerable<RemindRequest> GetAll()
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = connection.CreateCommand())
            {
                connection.Open();
                command.CommandText = "SELECT * FROM RemindRequests";

                using (var reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            yield return new RemindRequest
                            {
                                Id = (Guid)reader[nameof(RemindRequest.Id)],
                                Expression = (string)reader[nameof(RemindRequest.Expression)],
                                User = (string)reader[nameof(RemindRequest.User)],
                                Permalink = (string)reader[nameof(RemindRequest.Permalink)],
                            };
                        }
                    }
                }
            }
        }
    }
}
