using System.Collections.Generic;
using System.Threading.Tasks;
using DBI_Apotheke.Core.Util;
using DBI_Apotheke.Core.Workloads.Products;
using DBI_Apotheke.Core.Workloads.Recipes;
using FluentAssertions;
using MongoDB.Bson;
using NSubstitute;
using Xunit;

namespace DBI_Apotheke.Test
{
    public sealed class RecipeServiceTests

    {
        [Fact]
        public async Task TestGetRecipeById()
        {
            var id = new ObjectId();
            var repoMock = Substitute.For<IRecipeRepository>();
            repoMock.GetItemById(Arg.Any<ObjectId>()).Returns(ci => new Recipe
            {
                Id = ci.Arg<ObjectId>()
            });

            var service = new RecipeService(Substitute.For<IDateTimeProvider>(), repoMock);
            var product = await service.GetItemById(id);

            await repoMock.Received(1).GetItemById(Arg.Is(id));
            product.Should().NotBeNull();
            product!.Id.Should().Be(id);
        }
        [Fact]
        public async Task TestGetAllRecipe()
        {
            Recipe[] expectedPosts = { new Recipe(), new Recipe() };
            var repoMock = Substitute.For<IRecipeRepository>();
            repoMock.GetAllItems().Returns(expectedPosts);

            var service = new RecipeService(Substitute.For<IDateTimeProvider>(), repoMock);
            IReadOnlyCollection<Recipe> posts = await service.GetAllItems();

            await repoMock.Received(1).GetAllItems();
            posts.Should().NotBeNullOrEmpty()
                .And.HaveCount(2)
                .And.Contain(expectedPosts);
        }
        [Fact]
        public async Task TestAddRecipe()
        {
            var piId = new Product();
            var expectedRecipe = new Recipe
            {
                Address = "Burger Staße",
                Id = new ObjectId(),
                Issuer = "Kirchi",
                Name = "Elias",
                PZNs = new List<int>() {1,2,3}
            };
            var repoMock = Substitute.For<IRecipeRepository>();
            repoMock.InsertItem(Arg.Any<Recipe>()).Returns(c =>
            {
                var p = c.ArgAt<Recipe>(0);
                p.Id = expectedRecipe.Id;
                return p;
            });
            var repoMockP = Substitute.For<IProductRepository>();
            repoMockP.GetByPzn(expectedRecipe.PZNs[0]).Returns(new Product { });
            repoMockP.GetByPzn(expectedRecipe.PZNs[1]).Returns(new Product { });
            repoMockP.GetByPzn(expectedRecipe.PZNs[2]).Returns(new Product { });

            var service = new RecipeService(Substitute.For<IDateTimeProvider>(), repoMock);
            var serviceP = new ProductService(Substitute.For<IDateTimeProvider>(), repoMockP);

            var resP = await serviceP.GetByPzn(expectedRecipe.PZNs[0]);
            var resP1 = await serviceP.GetByPzn(expectedRecipe.PZNs[1]);
            var resP2 = await serviceP.GetByPzn(expectedRecipe.PZNs[2]);

            List<Product> list = new List<Product>();
            list.Add(resP);
            list.Add(resP1);
            list.Add(resP2);

            var actualRecipe = await service.InsertItem(
                expectedRecipe.Name,
                expectedRecipe.Address,
                expectedRecipe.Issuer,
                expectedRecipe.PZNs);

            await repoMock.Received(1).InsertItem(Arg.Any<Recipe>());
            await repoMockP.Received(3).GetByPzn(Arg.Any<int>());
            actualRecipe.Should().NotBeNull();
            actualRecipe.Should().BeEquivalentTo(expectedRecipe);
            list.Should().HaveCount(3);
            resP.Should().NotBeNull();
            resP1.Should().NotBeNull();
            resP2.Should().NotBeNull();
        }
        [Fact]
        public async Task TestDeleteRecipe()
        {
            var expectedRecipe = new Recipe
            {
                Address = "Burger Staße",
                Id = new ObjectId(),
                Issuer = "Kirchi",
                Name = "Elias",
                PZNs = new List<int>() { 1, 2, 3 }
            };

            var repoMock = Substitute.For<IRecipeRepository>();
            repoMock.DeleteItem(Arg.Is(expectedRecipe.Id)).Returns(Task.CompletedTask);

            var service = new RecipeService(Substitute.For<IDateTimeProvider>(), repoMock);
            await service.DeleteItem(expectedRecipe.Id);
            var r = await service.GetItemById(expectedRecipe.Id);

            await repoMock.Received(1).DeleteItem(Arg.Is(expectedRecipe.Id));
            r.Should().BeNull();
        }
    }
}