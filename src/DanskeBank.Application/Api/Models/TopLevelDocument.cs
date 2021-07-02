
namespace DanskeBank.Application.Api.Models
{
    public class TopLevelDocument<T>
    {
        internal TopLevelDocument(T data)
            : this(data, null)
        {
        }

        internal TopLevelDocument(T data, object meta)
        {
            Data = data;
            Meta = meta;
        }

        public T Data { get; }

        public object Meta { get; }
    }
}
