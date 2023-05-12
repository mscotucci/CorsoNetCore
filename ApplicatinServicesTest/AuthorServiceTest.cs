using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWebApi.Application;
using TestWebApi.Application.Impl;
using TestWebApi.Entities;
using TestWebApi.Infrastructure;
using TestWebApi.Infrastructure.EntityConfiguraions;
using TestWebApi.Requests.Authors;

namespace ApplicatinServicesTest
{
    [TestClass]
    public class AuthorServiceTest
    {
        //contesto per inizializzare i nostri dati fake
        private BookStoreDbContext _initContext;

        //contesto da utilizzare per i nostri test da passare al servizio
        private BookStoreDbContext _testContext;

        //contest che servirà pe verificare i dati
        private BookStoreDbContext _assertionContext;

        private IAuthorsService _entityService;

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

            _entityService = new AuthorsService(_testContext);

        }
        [TestMethod]
        public async Task CreateAuthorAsync_Should_SaveAuthorOK()

        {
            //Act
            var createAthorRequest = new CreateAuthorRequest();
            createAthorRequest.Nome = "PIPO";
            var author = await _entityService.CreateAuthorAsync(createAthorRequest);
            //Assert
            Assert.IsTrue(author.Id>0);
           var authorFromDb= await _assertionContext.Authors.SingleOrDefaultAsync(x => x.Id == author.Id);
             Assert.IsNotNull(authorFromDb);    

        }
        [TestMethod]    
        public async Task UpdateAuthorAsync_Should_GoFine()
        {
            //Arrange
            var author = new Author { Name = "Test" };
            _initContext.Authors.Add(author);
            await _initContext.SaveChangesAsync();
            //Act
            var updateAuthorRequest= new UpdateAuthorRequest();
            updateAuthorRequest.Nome = "Pipo";
            updateAuthorRequest.Id = author.Id;
            await  _entityService.UpdateAuthorAsync(updateAuthorRequest);

            // Assert
            var authorFromDb = await _assertionContext.Authors.SingleOrDefaultAsync(x => x.Id == author.Id);
            Assert.AreEqual(updateAuthorRequest.Nome, authorFromDb.Name);

        }

    }
}
