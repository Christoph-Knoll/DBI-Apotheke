using System.Collections.Generic;
using System.Threading.Tasks;
using DBI_Apotheke.Core.Util;
using DBI_Apotheke.Core.Workloads.ProductInfos;
using DBI_Apotheke.Core.Workloads.Products;
using FluentAssertions;
using MongoDB.Bson;
using NSubstitute;
using Xunit;

namespace DBI_Apotheke.Test
{
    public sealed class ProductServiceTests
    {
        [Fact]
        public async Task TestGetProductById()
        {
            var id = new ObjectId();
            var repoMock = Substitute.For<IProductRepository>();
            repoMock.GetItemById(Arg.Any<ObjectId>()).Returns(ci => new Product
            {
                Id = ci.Arg<ObjectId>()
            });

            var service = new ProductService(Substitute.For<IDateTimeProvider>(), repoMock);
            var product = await service.GetItemById(id);

            await repoMock.Received(1).GetItemById(Arg.Is(id));
            product.Should().NotBeNull();
            product!.Id.Should().Be(id);
        }
        [Fact]
        public async Task TestGetAllPosts()
        {
            Product[] expectedPosts = { new Product(), new Product() };
            var repoMock = Substitute.For<IProductRepository>();
            repoMock.GetAllItems().Returns(expectedPosts);

            var service = new ProductService(Substitute.For<IDateTimeProvider>(), repoMock);
            IReadOnlyCollection<Product> posts = await service.GetAllItems();

            await repoMock.Received(1).GetAllItems();
            posts.Should().NotBeNullOrEmpty()
                .And.HaveCount(2)
                .And.Contain(expectedPosts);
        }
        [Fact]
        public async Task TestAddProduct()
        {
            var piId = new ProductInfo();
            var expectedProduct = new Product
            {
                Amount = 200,
                Id = new ObjectId(),
                Price = 10,
                ProductInfoId = piId.Id,
                PZN = 01,
                Unit = DBI_Apotheke.Core.Workloads.Modules.Unit.G
            };
            var repoMock = Substitute.For<IProductRepository>();
            repoMock.InsertItem(Arg.Any<Product>()).Returns(c =>
            {
                var p = c.ArgAt<Product>(0);
                p.Id = expectedProduct.Id;
                return p;
            });
            var repoMockPI = Substitute.For<IProductInfoRepository>();
            repoMockPI.GetItemById(expectedProduct.ProductInfoId).Returns(new ProductInfo { });

            var service = new ProductService(Substitute.For<IDateTimeProvider>(), repoMock);
            var servicePi = new ProductInfoService(Substitute.For<IDateTimeProvider>(), repoMockPI);

            var resPi = await servicePi.GetItemById(expectedProduct.ProductInfoId);

            var actualProduct = await service.InsertItem(resPi,
                expectedProduct.PZN,
                expectedProduct.Price,
                expectedProduct.Amount,
                expectedProduct.Unit);

            await repoMock.Received(1).InsertItem(Arg.Any<Product>());
            actualProduct.Should().NotBeNull();
            actualProduct.Should().BeEquivalentTo(expectedProduct);
        }
        [Fact]
        public async Task TestDeleteProduct()
        {
            var piId = new ProductInfo();
            var expectedProduct = new Product
            {
                Amount = 200,
                Id = new ObjectId(),
                Price = 10,
                ProductInfoId = piId.Id,
                PZN = 01,
                Unit = DBI_Apotheke.Core.Workloads.Modules.Unit.G
            };

            var repoMock = Substitute.For<IProductRepository>();
            repoMock.DeleteItem(Arg.Is(expectedProduct.Id)).Returns(Task.CompletedTask);

            var service = new ProductService(Substitute.For<IDateTimeProvider>(), repoMock);
            await service.DeleteItem(expectedProduct.Id);
            var r = await service.GetItemById(expectedProduct.Id);

            await repoMock.Received(1).DeleteItem(Arg.Is(expectedProduct.Id));
            r.Should().BeNull();
        }
    }
}