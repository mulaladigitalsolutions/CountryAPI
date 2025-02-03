namespace Country.Domain.Entities
{
    public class Country
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Flag{ get; set; }
        public long Population { get; set; }
        public string? Capital { get; set; }
    }
}
