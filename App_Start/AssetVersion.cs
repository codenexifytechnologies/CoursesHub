using System;
using System.IO;
using System.Web;
using System.Web.Caching;
using System.Web.Hosting;

namespace CoursesHub
{
    /// <summary>
    /// Static files are cached by browsers for a long time (see staticContent/clientCache in Web.config).
    /// Url() appends a stamp of the file's last change (?v=...) so a changed CSS/JS/image is always
    /// downloaded again, while unchanged files keep coming from the browser cache.
    /// </summary>
    public static class AssetVersion
    {
        private const string CacheKeyPrefix = "AssetVersion:";

        public static string Url(string virtualPath)
        {
            string key = CacheKeyPrefix + virtualPath;
            string cached = HttpRuntime.Cache[key] as string;
            if (cached != null)
            {
                return cached;
            }

            string url = VirtualPathUtility.ToAbsolute(virtualPath);
            string physicalPath = HostingEnvironment.MapPath(virtualPath);
            if (physicalPath != null && File.Exists(physicalPath))
            {
                url += "?v=" + File.GetLastWriteTimeUtc(physicalPath).Ticks.ToString("x");
                // drop the cached stamp as soon as the file changes on disk
                HttpRuntime.Cache.Insert(key, url, new CacheDependency(physicalPath));
            }
            return url;
        }
    }
}
