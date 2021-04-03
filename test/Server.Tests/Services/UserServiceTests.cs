using System.Collections.Generic;
using System.Linq;
using HiddenLove.DataAccess.Repositories;
using NUnit.Framework;
using Moq;
using HiddenLove.DataAccess.Entities;
using HiddenLove.Server.Services;
using HiddenLove.Server.Helpers;
using Microsoft.Extensions.Options;
using HiddenLove.Shared.Models;
using HiddenLove.DataAccess.TableAccesses;

namespace HiddenLove.Tests.Server.Services
{
    [TestFixture]
    public class UserServiceTests
    {
        private UserService TestSubject;
        private Mock<Repository> RepositoryMock;
        private List<User> UsersInMemoryDb;
        private IOptions<AppSettings> AppSettings;

        [SetUp]
        public void SetUp()
        {
            UsersInMemoryDb = new List<User>
            {
                new User { Id = 1, EmailAddress = "jean.valjean@mail.com", Passwordhash = "$2y$10$OU4W/aqtaHqt/yO302Qf6euq6gPAp7dA4ZyJCmExEVh/2MyrcFW2W" },  // marvel123
                new User { Id = 2, EmailAddress = "tommy.mclagen@orange.fr", Passwordhash = "$2y$10$ZZfuPOUpxjg6rvxQnL9HTOwy/EnkvG8vzj.WGvjMlVT0a1UruL46e" }   // Leroileo78*
            };

            RepositoryMock = new Mock<Repository>(new UsersTableAccess());
            RepositoryMock.Setup(x => x.GetById<int, User>(It.IsAny<int>()))
                .Returns((int i) => UsersInMemoryDb.FirstOrDefault(x => x.Id == i));

            RepositoryMock.Setup(x => x.GetAll<int, User>())
                .Returns(UsersInMemoryDb);

            RepositoryMock.Setup(
                    x => x.GetByColumn<int, User>(
                        It.Is<string>(x => x.ToUpper() == "EMAILADDRESS"), It.IsAny<object>()))
                .Returns(
                    (string s, object o) => UsersInMemoryDb
                    .Where(x => o != null && x.EmailAddress == o.ToString()));

            RepositoryMock.Setup(x => x.Insert<int, User>(It.IsAny<User>()))
                .Callback((User u) => { 
                    u.Id = UsersInMemoryDb.Count + 1;
                    UsersInMemoryDb.Add(u); 
                });

            AppSettings = Options.Create(new AppSettings { Secret = "secretsecretsecret" });
            TestSubject = new UserService(AppSettings, RepositoryMock.Object);
        }

        [Test]
        [TestCase(1, "jean.valjean@mail.com")]
        [TestCase(2, "tommy.mclagen@orange.fr")]
        public void GetById(int id, string expected)
        {
            string actual = TestSubject.GetById(id).EmailAddress;

            Assert.AreEqual(expected, actual);   
        }

        [Test]
        [TestCase("jean.valjean@mail.com", "marvel123", 1)]
        [TestCase("tommy.mclagen@orange.fr", "Leroileo78*", 2)]
        [TestCase("jean.valjean@fakeaddress.com", "marvel123", null)]
        [TestCase("tommy.mclagen@orange.fr", "Leroilucas93-", null)]
        [TestCase(null, "Leroileo78*", null)]
        [TestCase("tommy.mclagen@orange.fr", null, null)]
        public void Authenticate_Success(string emailAddress, string password, int? expected)
        {
            var authenticationRequest = new AuthenticationRequest { EmailAddress = emailAddress, Password = password };
            
            AuthenticationResponse actual = TestSubject.Authenticate(authenticationRequest);

            Assert.AreEqual(expected, actual?.Id);
        }

        [Test]
        [TestCase("jean.valjean@mail.com", "B", "marvel123", null)]
        [TestCase("thomas.sac@gmail.com", "D", "helloworld", 3)]
        [TestCase("jerome.passe@gmail.com", "E", "supermdp88", 3)]
        public void Register(string emailAddress, string username, string password, int? expected)
        {
            var authenticationRequest = new RegisterRequest { EmailAddress = emailAddress, UserName = username, Password = password };

            AuthenticationResponse actual = TestSubject.Register(authenticationRequest);

            Assert.AreEqual(expected, actual?.Id);
        }
    }
}