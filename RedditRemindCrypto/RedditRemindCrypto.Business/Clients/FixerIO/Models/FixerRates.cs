using System;
using System.Collections.Generic;

namespace RedditRemindCrypto.Business.Clients.FixerIO.Models
{
    public class FixerRates
    {
        public string Base { get; set; }
        public DateTime Date { get; set; }
        public Dictionary<string, decimal> Rates { get; set; }
    }
}
