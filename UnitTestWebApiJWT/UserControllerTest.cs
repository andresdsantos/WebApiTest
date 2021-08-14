using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WebApiJwt.Domain;
namespace UnitTestWebApiJWT
{
    [TestClass]
    public class UserControllerTest
    {
        private Mock<IDataContext> _mockCustomerDbContext;
        private Mock<DbSet<User>> _mockUsers;
        private UserRepository _userRepository;

     
        [TestInitialize]
        public void TestInitialize()
        {
            _mockCustomerDbContext = new Mock<IDataContext>();
            _mockUsers = new Mock<DbSet<User>>();
            _mockCustomerDbContext.Setup(x => x.Set<User>()).Returns(_mockUsers.Object);
           
            _userRepository = new UserRepository(_mockCustomerDbContext.Object);
        }
        [TestCleanup]
        public void TestCleanup()
        {
            _mockCustomerDbContext.VerifyAll();
        }

        [TestMethod]
        public void GetUserGivenUserName_ExpectedUserReturned()
        {
            var username = "juan";

            var stubData = (new List<User>
            { new User
                {
                    Email = "juan@gmail.com",
                    FirstName = "Juan",
                    LastName = "Perez",
                    Phone = "8094556767",
                    Username = "juan"
                },
                new User
                {
                    Email = "maria@gmail.com",
                    FirstName = "Maria",
                    LastName = "Perez",
                    Phone = "8094556767",
                    Username = "maria"
                } 
            }).AsQueryable();
            SetupTestData<User>(stubData, _mockUsers);
            var actual = _userRepository.GetByUsername(username);
            Assert.AreEqual(stubData.ToList()[0], actual);
        }
        public void GetUserAndPassword_ExpectedBoleeanReturned()
        {
            var username = "juan";
            var password = "password";
            var stubData = (new List<User>
            { new User
                {
                    Email = "juan@gmail.com",
                    FirstName = "Juan",
                    LastName = "Perez",
                    Phone = "8094556767",
                    Username = "juan",
                    PasswordHash = "password"
                },
                new User
                {
                    Email = "maria@gmail.com",
                    FirstName = "Maria",
                    LastName = "Perez",
                    Phone = "8094556767",
                    Username = "maria",
                    PasswordHash = "password1"
                }
            }).AsQueryable();
            SetupTestData<User>(stubData, _mockUsers);
            var actual = _userRepository.IsValid(username,password);
            Assert.IsTrue(actual);
        }


        private void SetupTestData<T>(IQueryable<T> testData,Mock<DbSet<T>> mockDbSet) where T : class
        {
            mockDbSet.As<IQueryable<User>>().Setup(m => m.Provider).Returns(testData.Provider);
            mockDbSet.As<IQueryable<User>>().Setup(m => m.Expression).Returns(testData.Expression);
            mockDbSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator())
                .Returns((IEnumerator<User>)testData.GetEnumerator());
        }
    }
}
