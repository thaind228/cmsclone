using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;

namespace CMS.Utility
{
    public static class NLogExtensions
    {
        // Call early in each app's Program.cs
        public static WebApplicationBuilder UseCmsNLog(this WebApplicationBuilder builder, string nlogConfig = "nlog.config")
        {
            // Resolve config path relative to host content root
            var configPath = Path.Combine(builder.Environment.ContentRootPath, nlogConfig);
            if (!File.Exists(configPath))
            {
                // fallback to provided value (absolute or relative)
                configPath = nlogConfig;
            }

            // configure NLog from resolved file
            NLogBuilder.ConfigureNLog(configPath);
            builder.Logging.ClearProviders();
            builder.Host.UseNLog();
            return builder;
        }
    }
}
