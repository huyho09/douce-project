using ScentifyWebApp.Services.Contracts;

namespace ScentifyWebApp.Services.Implementations
{
    public class ConfigurationService : IConfigurationService
    {
        private readonly IConfiguration _configuration;

        public ConfigurationService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetConfigValue(string key)
        {
            if (!string.IsNullOrEmpty(key))
            {
                var configValue = _configuration.GetSection(key).Get<string>();
                return configValue;
            }

            return string.Empty;
        }

        public T GetConfigValue<T>(string key)
            where T : class
        {
            if (!string.IsNullOrEmpty(key))
            {
                var configValue = _configuration.GetSection(key).Get<T>();
                return configValue;
            }

            return default;
        }
    }
}