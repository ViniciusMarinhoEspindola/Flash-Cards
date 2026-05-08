using Domain.Entities.Generics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class GrammarSection : BaseEntity
    {
        protected GrammarSection() { }

        public static GrammarSection Create()
        {
            return new GrammarSection
            {
            };
        }
    }
}
