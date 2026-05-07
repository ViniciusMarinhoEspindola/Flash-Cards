using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class CardProgress
    {
        public Guid Id { get; private set; }

        public static CardProgress Create()
        {
            return new CardProgress
            {
                Id = Guid.NewGuid()
            };
        }
    }
}
