using PayPal.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayPal.Data.Interfaces
{
    public interface IUserRepository : IRepository<Users>
    {
        Task<Users> GetByEmailAsync(string email);
        Task<Users> GetByUsernameAsync(string username);
        Task<IEnumerable<Orders>> GetUserOrdersAsync(int userId);
    }
}
