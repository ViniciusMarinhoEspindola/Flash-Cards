using Domain.Entities.Generics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class GrammarChapter : BaseEntity
    {
        protected GrammarChapter() { }

        public static GrammarChapter Create()
        {
            return new GrammarChapter
            {
            };
        }
    }
}
