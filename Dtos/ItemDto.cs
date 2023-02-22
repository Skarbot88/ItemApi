namespace ItemAPI.Dtos
{
    //Data transfer object exposable to the clients
    public record ItemDto
    {
        //init accessor is to initialise it once on item creation. 
        public Guid Id { get; init; }
        public string Name { get; init; }
        public decimal Price { get; init; }
        public DateTimeOffset CreatedDate { get; init; }
    }
}