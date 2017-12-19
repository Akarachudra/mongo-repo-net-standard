namespace MongoRepo
{
    public interface IEntity<TKey> where TKey : class
    {
        TKey Id { get; set; }
    }
}