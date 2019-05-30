using Demo.Domain.Interfaces;

namespace Demo.Domain
{
    public class Category : BaseEntity, IEntity
    {
        public string Name { get; set; }
    }
}
