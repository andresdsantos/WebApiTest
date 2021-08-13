using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiJwt.Domain
{
    public interface IUserRepository
    {
        bool AddUser(User user);
        bool UpdateUser(User user);
        bool RemoveUser(string username);
        bool IsValid(string username, string password);
        User GetByUsername(string username);
        Address GetAddressByUsername(string username);
        bool AddAddress(Address address);
        bool UpdateAddress(Address address);
    }
}
