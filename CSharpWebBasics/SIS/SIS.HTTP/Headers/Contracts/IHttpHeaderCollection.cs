namespace SIS.HTTP.Headers.Contracts
{
    public interface IHttpHeaderCollection
    {
        void Addheader(HttpHeader header);

        bool ContainsHeader(string key);

        HttpHeader GetHeader(string key);
    }
}
