using Core.Models;
using Infrastructure.Repositories;
using Infrastructure.Repositories.Data;
using Microsoft.EntityFrameworkCore;
using Xunit;
using FluentAssertions;

namespace Tests.Integration.Repositories
{
    public class UserRepositoryTests
    {
        private OrdersDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<OrdersDbContext>()
                .UseInMemoryDatabase("TestDb")
                .Options;

            var dbContext = new OrdersDbContext(options);
            dbContext.Database.EnsureCreated();
            return dbContext;
        }

        [Fact]
        public async Task Should_Save_And_Retrieve_User()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var repository = new UserRepository(context);

            var user = new User
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Test User",
                Email = "testuser@example.com",
                Password = "Test@1234"
            };

            // Act
            await repository.AddUser(user);
            var retrievedUser = await context.Users.FirstOrDefaultAsync(u => u.Id == user.Id);

            // Assert
            retrievedUser.Should().NotBeNull();
            retrievedUser!.Name.Should().Be("Test User");
        }
    }
}
