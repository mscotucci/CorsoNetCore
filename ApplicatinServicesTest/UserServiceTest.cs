using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWebApi.Application;
using TestWebApi.Application.Impl;
using TestWebApi.Entities;
using TestWebApi.Exceptions;
using TestWebApi.Infrastructure;
using TestWebApi.Requests.Books;
using TestWebApi.Requests.Users;

namespace ApplicatinServicesTest
{
    [TestClass]
    public class UserServiceTest
    {
        //contesto per inizializzare i nostri dati fake
        private BookStoreDbContext _initContext;

        //contesto da utilizzare per i nostri test da passare al servizio
        private BookStoreDbContext _testContext;

        //contest che servirà pe verificare i dati
        private BookStoreDbContext _assertionContext;

        private IUsersService _entityService;

        [TestInitialize]
        public void Initialize()
        {
            var options = new DbContextOptionsBuilder<BookStoreDbContext>()
                .UseInMemoryDatabase("BookStore")
                .Options;
            _initContext = new BookStoreDbContext(options);
            _initContext.Database.EnsureCreated();

            _testContext = new BookStoreDbContext(options);

            _assertionContext = new BookStoreDbContext(options);

            _entityService = new UsersService(_testContext);

        }
        [TestMethod]
        public async Task CreateUserAsync_Should_ThrowUserAlreadyExistException() {

            //Arrange
            User user = new User();
            user.Username= "Test";
            user.Id = Guid.NewGuid();
            _initContext.Users.Add(user);
            _initContext.SaveChanges();

            //Assert
            CreateUserRequest createUserRequest = new CreateUserRequest();
            createUserRequest.Username = user.Username;    
            
            await Assert.ThrowsExceptionAsync<UserAlreadyExistException>(async () => await _entityService
           .CreateUserAsync(createUserRequest));

        }
    }
}
