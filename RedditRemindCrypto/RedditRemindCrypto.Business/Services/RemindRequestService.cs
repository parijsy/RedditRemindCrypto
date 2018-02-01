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

                command.CommandText = "INSERT INTO RemindRequests(Expression, [User], Permalink) VALUES (@expression, @userid, @permalink)";
                command.Parameters.AddWithValue("@expression", request.Expression);
                command.Parameters.AddWithValue("@userid", request.User);
                command.Parameters.AddWithValue("@permalink", request.Permalink);
                command.ExecuteNonQuery();
            }
        }

        public void Delete(RemindRequest request)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = connection.CreateCommand())
            {
                connection.Open();

                command.CommandText = "DELETE FROM RemindRequests WHERE Id = @requestid";
                command.Parameters.AddWithValue("@requestid", request.Id);
                command.ExecuteNonQuery();
            }
        }

        public void DeleteByUserAndId(string user, Guid id)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = connection.CreateCommand())
            {
                connection.Open();

                command.CommandText = "DELETE FROM RemindRequests WHERE Id = @requestid AND [User] = @user";
                command.Parameters.AddWithValue("@requestid", id);
                command.Parameters.AddWithValue("@user", user);
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

                return ExecuteRemindRequestQuery(command);
            }
        }

        public IEnumerable<RemindRequest> GetByUser(string user)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = connection.CreateCommand())
            {
                connection.Open();
                command.CommandText = "SELECT * FROM RemindRequests WHERE [User] = @user";
                command.Parameters.AddWithValue("@user", user);

                return ExecuteRemindRequestQuery(command);
            }
        }

        private IEnumerable<RemindRequest> ExecuteRemindRequestQuery(SqlCommand command)
        {
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
