namespace ScentifyWebApp.Services.Contracts
{
    public interface IConfigurationService
    {
        string GetConfigValue(string key);

        T GetConfigValue<T>(string key)
            where T : class;
    }
}
