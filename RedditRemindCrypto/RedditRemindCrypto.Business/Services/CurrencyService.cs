using RedditRemindCrypto.Business.Factories;
using RedditRemindCrypto.Business.Services.Extensions;
using RedditRemindCrypto.Business.Services.Models;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace RedditRemindCrypto.Business.Services
{
    public class CurrencyService : ICurrencyService
    {
        private readonly string connectionString;

        public CurrencyService(IConnectionStringFactory connectionStringFactory)
        {
            this.connectionString = connectionStringFactory.Create();
        }

        public void Add(string ticker, string coinMarketCapId)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = connection.CreateCommand())
            {
                connection.Open();

                command.CommandText = $"INSERT INTO Currencies (CurrencyType, Ticker, CoinMarketCapId) VALUES (2, '{ticker}', '{coinMarketCapId}')";
                command.ExecuteNonQuery();
            }
        }

        public void AddAlternativeName(string ticker, string alternativeName)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = connection.CreateCommand())
            {
                connection.Open();

                command.CommandText = $"INSERT INTO CurrencyAlternativeNames (Name, CurrencyTicker) VALUES ('{alternativeName}', (SELECT Ticker FROM Currencies WHERE Ticker = '{ticker}'))";
                command.ExecuteNonQuery();
            }
        }

        public CurrencyModel GetByTicker(string ticker)
        {
            return GetAll().Single(x => x.Ticker == ticker);
        }

        public IEnumerable<CurrencyModel> GetAll()
        {
            var currencies = GetAllCurrencies().ToList();
            var currencyAlternativeNames = GetAllCurrencyAlternativeNames().ToList();

            foreach (var currency in currencies)
            {
                currency.AlternativeNames = currencyAlternativeNames.Where(x => x.CurrencyTicker == currency.Ticker).Select(x => x.Name).ToList();
            }

            return currencies;
        }

        private IEnumerable<CurrencyModel> GetAllCurrencies()
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = connection.CreateCommand())
            {
                connection.Open();
                command.CommandText = "SELECT * FROM Currencies";

                using (var reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            yield return new CurrencyModel
                            {
                                Ticker = (string)reader[nameof(CurrencyModel.Ticker)],
                                CurrencyType = (CurrencyType)((int)reader[nameof(CurrencyModel.CurrencyType)]),
                                FixerIOName = reader.IsDBNull(nameof(CurrencyModel.FixerIOName)) ? null : (string)reader[nameof(CurrencyModel.FixerIOName)],
                                CoinMarketCapId = reader.IsDBNull(nameof(CurrencyModel.CoinMarketCapId)) ? null : (string)reader[nameof(CurrencyModel.CoinMarketCapId)],
                            };
                        }
                    }
                }
            }
        }

        private IEnumerable<CurrencyAlternativeName> GetAllCurrencyAlternativeNames()
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = connection.CreateCommand())
            {
                connection.Open();
                command.CommandText = "SELECT * FROM CurrencyAlternativeNames";

                using (var reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            yield return new CurrencyAlternativeName
                            {
                                CurrencyTicker = (string)reader[nameof(CurrencyAlternativeName.CurrencyTicker)],
                                Name = (string)reader[nameof(CurrencyAlternativeName.Name)],
                            };
                        }
                    }
                }
            }
        }

        private class CurrencyAlternativeName
        {
            public string CurrencyTicker { get; set; }
            public string Name { get; set; }
        }
    }
}
