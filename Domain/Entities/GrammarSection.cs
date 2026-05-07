using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class GrammarSection
    {
        public Guid Id { get; private set; }

        public static GrammarSection Create()
        {
            return new GrammarSection
            {
                Id = Guid.NewGuid()
            };
        }
    }
}
