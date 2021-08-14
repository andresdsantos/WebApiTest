using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiJwt.Domain
{
    public class UserRepository : IUserRepository
    {
        private IDataContext _customerDbContext;

        public UserRepository(IDataContext dataContext)
        {
            this._customerDbContext = dataContext;
        }
        
        public bool AddUser(User user)
        {
            try
            {
                _customerDbContext.Set<User>().Add(user);
                return _customerDbContext.SaveChanges() > 0;
            }
            catch(Exception ex) 
            {
                return false;
            }
        }
        public bool IsValid(string username,string password)
        {
            try
            {
                var passwordHashed = MD5Hash.Hash.Content(password);
                bool IsValid = _customerDbContext.Set<User>().Any(u => u.Username == username && u.PasswordHash ==passwordHashed);
                return IsValid;
            }
            catch(Exception ex) 
            {
                return false;
            }
        }

        
        public User GetByUsername(string username)
        {
            try
            {
                var user = _customerDbContext.Set<User>().SingleOrDefault(u => u.Username == username);
                return user;
            }
            catch (Exception ex)
            {

                return null;
            }
            
        }
        public Address GetAddressByUsername(string username)
        {
            var address = _customerDbContext.Set<Address>().SingleOrDefault(a => a.Username == username);
            return address;
        }
        public bool UpdateUser(User user)
        {
            try
            {
                var existingUser = GetByUsername(user.Username);
                if (existingUser == null) return false;

                existingUser.Email = user.Email;
                existingUser.FirstName = user.FirstName;
                existingUser.LastName = user.LastName;
                existingUser.PasswordHash = user.PasswordHash;
                existingUser.Phone = user.Phone;
                return _customerDbContext.SaveChanges() > 0;
            }
            catch
            {
                return false;
            }
        }

        public bool RemoveUser(string username)
        {
            var existingUser = GetByUsername(username);
            bool result = false;
            try
            {
                if (existingUser != null)
                {
                    _customerDbContext.Set<User>().Remove(existingUser);
                    result = _customerDbContext.SaveChanges() > 0;
                }
            }
            catch (Exception ex)
            {

                result = false;
            }
            return result;
        }
        public bool AddAddress(Address address)
        {
            try
            {
                _customerDbContext.Set<Address>().Add(address);
                return _customerDbContext.SaveChanges() > 0;
            }
            catch 
            {
                return false;
            }
        }
        public bool UpdateAddress(Address address)
        {
            try
            {
                var existingAddress = GetAddressByUsername(address.Username);
                if (existingAddress == null) return false;

                existingAddress.City = address.City;
                existingAddress.State = address.State;
                existingAddress.Street = address.Street;
                existingAddress.ZipCode = address.ZipCode;
                return _customerDbContext.SaveChanges() > 0;
            }
            catch 
            {
                return false;
            }
        }
    }
}