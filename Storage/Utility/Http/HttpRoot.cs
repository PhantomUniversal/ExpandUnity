namespace PhantomEngine
{
    public interface IHttpOption
    {
        string ContentType { get; }
        T Deserialize<T>(string text);
    }
}