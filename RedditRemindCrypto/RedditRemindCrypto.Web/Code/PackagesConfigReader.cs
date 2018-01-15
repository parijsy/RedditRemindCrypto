using NuGet;
using System.Collections.Generic;

namespace RedditRemindCrypto.Web.Code
{
    public class PackagesConfigReader
    {
        public IEnumerable<PackageReference> ReadPackageNames(string fileName)
        {
            var file = new PackageReferenceFile(fileName);
            foreach (var packageReference in file.GetPackageReferences())
                yield return packageReference;
        }
    }
}