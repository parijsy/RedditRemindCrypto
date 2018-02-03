using System;
using System.Collections.Generic;
using System.Linq;

namespace RedditRemindCrypto.Web.Code
{
    public static class IEnumerableExtensions
    {
        public static IEnumerable<IEnumerable<T>> Split<T>(this IEnumerable<T> collection, int parts)
        {
            return collection.Section(collection.Count() / parts + 1);
        }

        public static IEnumerable<IEnumerable<T>> Section<T>(this IEnumerable<T> collection, int length)
        {
            // Credit goes to: https://stackoverflow.com/a/3382769
            if (length <= 0)
                throw new ArgumentOutOfRangeException("length");

            var section = new List<T>(length);
            foreach (var item in collection)
            {
                section.Add(item);

                if (section.Count == length)
                {
                    yield return section.AsReadOnly();
                    section = new List<T>(length);
                }
            }

            if (section.Count > 0)
                yield return section.AsReadOnly();
        }
    }
}