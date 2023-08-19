using System;
using System.Configuration;

namespace Framework.Configuration
{
    /// <summary>
    /// Get config from .config file dynamic key
    /// </summary>
    public class Config
    {
        /// <summary>
        /// Gets the config by key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public static string GetConfigByKey(string key)
        {
            try
            {
                var sconfig = ConfigurationManager.AppSettings[key];
                return sconfig;
            }
            catch(Exception ex)
            {
                return string.Empty;
            }
        }
    }
}
