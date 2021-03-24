using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using HiddenLove.DataAccess.Repositories;
using NUnit.Framework;
using Moq;
using HiddenLove.DataAccess.Entities;
using HiddenLove.Server.Services;
using HiddenLove.Server.Helpers;
using Microsoft.Extensions.Options;
using FluentAssertions;

namespace HiddenLove.Tests.Server.Services
{
    [TestFixture]
    public class UserServiceTests
    {
        private UserService TestSubject;
        private Mock<UserRepository> RepositoryMock;
        private IEnumerable<User> UsersInMemoryDb;

        [SetUp]
        public void SetUp()
        {
            UsersInMemoryDb = new List<User>
            {
                new User { Id = 1, EmailAddress = "jean.valjean@mail.com" },
                new User { Id = 2, EmailAddress = "tommy.mclagen@orange.fr" }
            };

            RepositoryMock = new Mock<UserRepository>();
            RepositoryMock.Setup(x => x.GetById(It.IsAny<int>()))
                .Returns((int i) => UsersInMemoryDb.Single(x => x.Id == i));

            RepositoryMock.Setup(x => x.GetAll())
                .Returns(UsersInMemoryDb);

            var someOptions = Options.Create(new AppSettings());
            TestSubject = new UserService(RepositoryMock.Object, someOptions);
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


    }
}