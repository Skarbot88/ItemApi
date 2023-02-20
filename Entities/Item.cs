namespace ItemAPI.Entities
{
    public record Item
    {
        //init accessor is to initialise it once on item creation. 
        public Guid Id { get; init; }
        public string Name { get; init; }
        public decimal Price { get; init; }
        public DateTimeOffset CreatedDate { get; init; }
    }
}