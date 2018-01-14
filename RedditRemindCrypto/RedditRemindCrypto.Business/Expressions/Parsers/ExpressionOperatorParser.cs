using RedditRemindCrypto.Business.Expressions.Enums;
using System;
using System.Collections.Generic;

namespace RedditRemindCrypto.Business.Expressions.Parsers
{
    public class ExpressionOperatorParser
    {
        private readonly Dictionary<string, ExpressionOperator> lookup;

        public ExpressionOperatorParser()
        {
            this.lookup = CreateLookupDictionary();
        }

        public bool TryParse(string toParse, out ExpressionOperator result)
        {
            foreach (var pair in lookup)
            {
                if (!toParse.Equals(pair.Key, StringComparison.InvariantCultureIgnoreCase))
                    continue;

                result = pair.Value;
                return true;
            }

            result = ExpressionOperator.SmallerThan;
            return false;
        }

        private Dictionary<string, ExpressionOperator> CreateLookupDictionary()
        {
            return new Dictionary<string, ExpressionOperator>
            {
                { ">", ExpressionOperator.LargerThan },
                { "<", ExpressionOperator.SmallerThan }
            };
        }
    }
}
