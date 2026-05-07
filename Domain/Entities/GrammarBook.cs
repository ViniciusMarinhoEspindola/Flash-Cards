using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class GrammarBook
    {
        public Guid Id { get; private set; }

        public static GrammarBook Create()
        {
            return new GrammarBook
            {
                Id = Guid.NewGuid()
            };
        }
    }
}
