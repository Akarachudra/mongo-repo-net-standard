namespace MongoRepo
{
    public class Entity<TKey> : IEntity<TKey>
        where TKey : class
    {
        public TKey Id { get; set; }
    }
}