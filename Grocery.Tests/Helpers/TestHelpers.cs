using Grocery.Domain.Entities;
using Grocery.Domain.ValueObjects;

namespace Grocery.Tests.Helpers
{
    public class TestHelpers
    {
        //Happy flow
        [Fact]
        public void TestClientCreation()
        {
            var client = new Client(1, "Test User", "test@example.com", "password");
            Assert.Equal("Test User", client.Name);
            Assert.Equal("test@example.com", client.EmailAddress);
            Assert.Equal(Role.None, client.Role);
        }

        [Fact]
        public void TestClientWithAdminRole()
        {
            var client = new Client(1, "Admin User", "admin@example.com", "password");
            client.Role = Role.Admin;
            Assert.Equal(Role.Admin, client.Role);
        }

        [Fact]
        public void TestProductCreation()
        {
            var product = new Product(1, "Test Product", 10);
            Assert.Equal("Test Product", product.Name);
            Assert.Equal(10, product.Stock);
        }

        [Fact]
        public void TestGroceryListCreation()
        {
            var groceryList = new GroceryList(1, "Test List", DateOnly.FromDateTime(DateTime.Now), "#FF0000", 1);
            Assert.Equal("Test List", groceryList.Name);
            Assert.Equal(1, groceryList.ClientId);
        }
    }
}