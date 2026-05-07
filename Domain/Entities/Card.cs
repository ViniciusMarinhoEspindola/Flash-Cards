using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Card
    {
        public Guid Id { get; private set; }

        public static Card Create()
        {
            return new Card
            {
                Id = Guid.NewGuid()
            };
        }
    }
}
