using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using HiddenLove.DataAccess.Repositories;
using NUnit.Framework;
using Moq;
using HiddenLove.Shared.Entities;
using HiddenLove.Server.Services;
using HiddenLove.Server.Helpers;
using Microsoft.Extensions.Options;
using FluentAssertions;
using HiddenLove.Shared.Models.Authentication;

namespace HiddenLove.Tests.Server.Services
{
    [TestFixture]
    public class UserServiceTests
    {
        private UserService TestSubject;
        private Mock<UserRepository> RepositoryMock;
        private IEnumerable<User> UsersInMemoryDb;
        private IOptions<AppSettings> AppSettings;

        [SetUp]
        public void SetUp()
        {
            UsersInMemoryDb = new List<User>
            {
                new User { Id = 1, EmailAddress = "jean.valjean@mail.com", PasswordHash = "$2y$10$OU4W/aqtaHqt/yO302Qf6euq6gPAp7dA4ZyJCmExEVh/2MyrcFW2W" },  // marvel123
                new User { Id = 2, EmailAddress = "tommy.mclagen@orange.fr", PasswordHash = "$2y$10$ZZfuPOUpxjg6rvxQnL9HTOwy/EnkvG8vzj.WGvjMlVT0a1UruL46e" }   // Leroileo78*
            };

            RepositoryMock = new Mock<UserRepository>();
            RepositoryMock.Setup(x => x.GetById(It.IsAny<int>()))
                .Returns((int i) => UsersInMemoryDb.FirstOrDefault(x => x.Id == i));

            RepositoryMock.Setup(x => x.GetAll())
                .Returns(UsersInMemoryDb);

            RepositoryMock.Setup(x => x.GetByEmailAddress(It.IsAny<string>()))
                .Returns((string s) => UsersInMemoryDb.FirstOrDefault(x => x.EmailAddress == s));

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
        public void GetAll()
        {
            IEnumerable<User> expected = UsersInMemoryDb;

            IEnumerable<User> actual = TestSubject.GetAll();

            actual.Should().Equal(expected);
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
    }
}