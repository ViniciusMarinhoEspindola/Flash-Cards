using Domain.Entities.Generics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class GrammarBook : BaseEntity
    {
        protected GrammarBook() { }

        public static GrammarBook Create()
        {
            return new GrammarBook
            {
            };
        }
    }
}
