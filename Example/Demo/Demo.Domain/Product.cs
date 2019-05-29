using Demo.Domain.Interfaces;

namespace Demo.Domain
{
    public class Product : BaseEntity, IEntity
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
