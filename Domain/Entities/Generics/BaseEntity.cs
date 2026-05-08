using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.Generics
{
    public class BaseEntity
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public DateTime CreatedAt { get; private set; } = DateTime.Now;
        public DateTime UpdatedAt { get; private set; } = DateTime.Now;
    }
}
