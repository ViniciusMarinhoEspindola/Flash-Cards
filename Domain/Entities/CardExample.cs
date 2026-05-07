using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class CardExample
    {
        public Guid Id { get; private set; }

        public static CardExample Create()
        {
            return new CardExample
            {
                Id = Guid.NewGuid()
            };
        }
    }
}
