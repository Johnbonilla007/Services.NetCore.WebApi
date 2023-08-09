namespace Services.NetCore.WebApi.Infraestructure.Core.RestClient
{
    public static class RestClientFactory
    {
        private static IRestClientFactory _currentRestClientFactory;

        /// <summary>
        /// Set the  rest client factory to use.
        /// </summary>
        /// <param name="restClientFactory">Rest client factory to use</param>
        public static void SetCurrent(IRestClientFactory restClientFactory)
        {
            _currentRestClientFactory = restClientFactory;
        }

        /// <summary>
        /// Create a new 
        /// <paramref>
        ///     <name>Services.NetCore.WebApi.IRestClient</name>
        /// </paramref>
        /// </summary>
        /// <param name="baseAddress">The API base address to connect to.</param>
        /// <returns>Created IRestClient</returns>        
        public static IRestClient CreateClient(string baseAddress)
        {
            return (_currentRestClientFactory != null) ? _currentRestClientFactory.Create(baseAddress) : null;
        }
    }
}
