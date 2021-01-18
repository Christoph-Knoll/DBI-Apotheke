using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DBI_Apotheke.Core.Util;
using DBI_Apotheke.Core.Workloads.Generics;
using DBI_Apotheke.Core.Workloads.ProductInfos;
using DBI_Apotheke.Core.Workloads.Products;
using DBI_Apotheke.Core.Workloads.Recipes;
using DBI_Apotheke.Core.Workloads.Storages;
using FluentAssertions;
using MongoDB.Bson;
using NSubstitute;
using NSubstitute.ReceivedExtensions;
using Xunit;

namespace MongoDBDemoApp.Test
{
    public sealed class StorageServiceTests

    {
        [Fact]
        public async Task TestGetStorageById()
        {
            var id = new ObjectId();
            var repoMock = Substitute.For<IStorageRepository>();
            repoMock.GetItemById(Arg.Any<ObjectId>()).Returns(ci => new Storage
            {
                Id = ci.Arg<ObjectId>()
            });

            var service = new StorageService(Substitute.For<IDateTimeProvider>(), repoMock);
            var storage = await service.GetItemById(id);

            await repoMock.Received(1).GetItemById(Arg.Is(id));
            storage.Should().NotBeNull();
            storage!.Id.Should().Be(id);
        }
        [Fact]
        public async Task TestGetAllStorage()
        {
            Storage[] expectedStorage = { new Storage(), new Storage() };
            var repoMock = Substitute.For<IStorageRepository>();
            repoMock.GetAllItems().Returns(expectedStorage);

            var service = new StorageService(Substitute.For<IDateTimeProvider>(), repoMock);
            IReadOnlyCollection<Storage> posts = await service.GetAllItems();

            await repoMock.Received(1).GetAllItems();
            posts.Should().NotBeNullOrEmpty()
                .And.HaveCount(2)
                .And.Contain(expectedStorage);
        }
        [Fact]
        public async Task TestAddStorage()
        {
            var piId = new Product();
            var expectedStorage = new Storage
            {
                Id = new ObjectId(),
                Amount = 100,
                PZN = 1,
                StorageSite = "Hamburg"

            };
            var repoMock = Substitute.For<IStorageRepository>();
            repoMock.InsertItem(Arg.Any<Storage>()).Returns(c =>
            {
                var p = c.ArgAt<Storage>(0);
                p.Id = expectedStorage.Id;
                return p;
            });
            var repoMockP = Substitute.For<IProductRepository>();
            repoMockP.GetByPzn(expectedStorage.PZN).Returns(new Product { });

            var service = new StorageService(Substitute.For<IDateTimeProvider>(), repoMock);
            var serviceP = new ProductService(Substitute.For<IDateTimeProvider>(), repoMockP);

            var resP = await serviceP.GetByPzn(expectedStorage.PZN);

            var actualStorage = await service.InsertItem(
                expectedStorage.PZN,
                expectedStorage.Amount,
                expectedStorage.StorageSite);

            await repoMock.Received(1).InsertItem(Arg.Any<Storage>());
            await repoMockP.Received(1).GetByPzn(Arg.Any<int>());
            actualStorage.Should().NotBeNull();
            actualStorage.Should().BeEquivalentTo(expectedStorage);
            resP.Should().NotBeNull();
        }
        [Fact]
        public async Task TestDeleteRecipe()
        {
            var expectedStorage= new Storage
            {
                Id = new ObjectId(),
                Amount = 100,
                PZN = 1,
                StorageSite = "Hamburg"

            };

            var repoMock = Substitute.For<IStorageRepository>();
            repoMock.DeleteItem(Arg.Is(expectedStorage.Id)).Returns(Task.CompletedTask);

            var service = new StorageService(Substitute.For<IDateTimeProvider>(), repoMock);
            await service.DeleteItem(expectedStorage.Id);
            var r = await service.GetItemById(expectedStorage.Id);

            await repoMock.Received(1).DeleteItem(Arg.Is(expectedStorage.Id));
            r.Should().BeNull();
        }
    }
}