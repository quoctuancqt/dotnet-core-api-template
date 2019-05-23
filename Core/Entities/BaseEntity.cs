using System;

namespace Core.Entities
{
    public class BaseEntity : Domain.Interfaces.IAudit, Domain.Interfaces.IEntity
    {
        public string Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
