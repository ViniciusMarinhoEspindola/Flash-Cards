using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class GrammarChapter
    {
        public Guid Id { get; private set; }

        public static GrammarChapter Create()
        {
            return new GrammarChapter
            {
                Id = Guid.NewGuid()
            };
        }
    }
}
