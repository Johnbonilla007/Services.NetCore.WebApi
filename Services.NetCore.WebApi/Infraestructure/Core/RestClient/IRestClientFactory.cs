namespace Services.NetCore.WebApi.Infraestructure.Core.RestClient
{
    public interface IRestClientFactory
    {
        IRestClient Create(string baseAddress);
    }
}
