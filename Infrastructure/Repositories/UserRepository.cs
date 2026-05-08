using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infraestructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        public async Task<List<User>> GetByUserId(Guid userId)
        {
            await Task.Delay(100); 
            return new List<User>
            {
                User.Create("Sample@email.com", "SampleUser", "teste")
            };
        }
    }
}
