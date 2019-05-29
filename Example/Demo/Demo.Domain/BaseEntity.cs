﻿using Demo.Domain.Interfaces;
using System;

namespace Demo.Domain
{
    public class BaseEntity : IAudit, IEntity
    {
        public Guid Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
