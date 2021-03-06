﻿using CoreApiTemplate.Domain.Interfaces;
using System;

namespace CoreApiTemplate.Domain
{
    public class BaseEntity : IAudit, IEntity
    {
        public string Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
