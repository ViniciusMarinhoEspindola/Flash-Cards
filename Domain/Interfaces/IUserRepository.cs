using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<List<User>> GetByUserId(Guid userId);
    }
}
