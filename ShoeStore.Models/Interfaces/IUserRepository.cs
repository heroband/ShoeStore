using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoeStore.Models.Entities;

namespace ShoeStore.Models.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetUserByEmailAsync(string email);
        Task AddUserAsync(User user, string password);
        Task<bool> IsEmailTakenAsync(string email);
        Task AddUserToRoleAsync(User user, string role);
    }
}
