namespace ScentifyWebApp.Helper
{
    public static class DependencyInjectionHelper
    {
        public static IServiceProvider _serviceProvider;

        public static void Initialize(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public static TService ResolveService<TService>()
        {
            if (_serviceProvider == null)
            {
                throw new InvalidOperationException("DependencyInjectionHelper not initialized. Call Initialize method first.");
            }

            return _serviceProvider.GetService<TService>();
        }
    }
}
