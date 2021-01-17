using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DBI_Apotheke.Core.Util;
using DBI_Apotheke.Core.Workloads.ProductInfos;
using FluentAssertions;
using MongoDB.Bson;
using NSubstitute;
using NSubstitute.ReceivedExtensions;
using Xunit;

namespace MongoDBDemoApp.Test
{
    public sealed class ProductInfoRepositoryTests

    {
        [Fact]
        public async Task TestGetProductInfoById()
        {
            var idPI = new ObjectId();
            var ingredient = new Ingredient
            {
                Amount = 100,
                Name = "Wirkstoff A",
                Unit = DBI_Apotheke.Core.Workloads.Modules.Unit.G
            };
            var list = new List<Ingredient>();
            list.Add(ingredient);
            var repoMock = Substitute.For<IProductInfoRepository>();
            repoMock.GetItemById(Arg.Any<ObjectId>()).Returns(ci => new ProductInfo
            {
                Id = ci.Arg<ObjectId>(),
                Brand = "Aspiring",
                Ingredients = list,
                Name = "AspirinComplex"
            });
            var expected = new ProductInfo
            {
                Brand = "Aspiring",
                Id = idPI,
                Ingredients = list,
                Name = "AspirinComplex"
            };

            var service = new ProductInfoService(Substitute.For<IDateTimeProvider>(), repoMock);
            var productInfo = await service.GetItemById(idPI);

            await repoMock.Received(1).GetItemById(Arg.Is(idPI));
            productInfo.Should().NotBeNull();
            productInfo!.Id.Should().Be(expected.Id);
            productInfo!.Ingredients.Should().BeEquivalentTo(expected.Ingredients);
            productInfo!.Name.Should().Be(expected.Name);
            productInfo!.Brand.Should().Be(expected.Brand);
        }
        [Fact]
        public async Task TestAddProductInfo()
        {
            var idPi = new ObjectId();
            var ingredient = new Ingredient
            {
                Amount = 100,
                Name = "Wirkstoff A",
                Unit = DBI_Apotheke.Core.Workloads.Modules.Unit.MG
            };
            var list = new List<Ingredient>();
            list.Add(ingredient);
            var expectedPI = new ProductInfo
            {
                Brand = "Aspiring",
                Id = idPi,
                Ingredients = list,
                Name = "AspirinComplex"
            };

            var repoMock = Substitute.For<IProductInfoRepository>();
            repoMock.InsertItem(Arg.Any<ProductInfo>()).Returns(ci =>
            {
                var c = ci.ArgAt<ProductInfo>(0);
                c.Id = expectedPI.Id;
                return c;
            });
            var dtMock = Substitute.For<IDateTimeProvider>();

            var service = new ProductInfoService(dtMock, repoMock);
            var productInfo = await service.InsertItem(expectedPI.Name, expectedPI.Brand, expectedPI.Ingredients);

            await repoMock.Received(1).InsertItem(Arg.Any<ProductInfo>());
            productInfo.Should().NotBeNull();
            productInfo.Should().BeEquivalentTo(expectedPI);
        }
        [Fact]
        public async Task TestAddMultipleProductInfo()
        {
            var piId = new ObjectId();
            var expectedIngredient = new Ingredient
            {
                Amount = 100,
                Name = "Wirkstoff A",
                Unit = DBI_Apotheke.Core.Workloads.Modules.Unit.MG
            };
            var list = new List<Ingredient>();
            list.Add(new Ingredient {
                Amount = 100,
                Name = "Wirkstoff A",
                Unit = DBI_Apotheke.Core.Workloads.Modules.Unit.MG});

            ProductInfo[] expectedProdInfo =
            {
                new ProductInfo
                {
                    Brand = "Aspiring",
                    Id = new ObjectId(),
                    Ingredients = list,
                    Name = "AspirinComplex"
                },
                new ProductInfo
                {
                    Brand = "Aspiring",
                    Id = new ObjectId(),
                    Ingredients = list,
                    Name = "AspirinComplex2"
                }
            };
            var repoMock = Substitute.For<IProductInfoRepository>();
            repoMock.InsertItem(Arg.Any<ProductInfo>()).Returns(ci =>
            {
                var c = ci.ArgAt<ProductInfo>(0);
                c.Id = expectedProdInfo[0].Id;
                return c;
            });
            repoMock.InsertItem(Arg.Any<ProductInfo>()).Returns(ci =>
            {
                var c = ci.ArgAt<ProductInfo>(0);
                c.Id = expectedProdInfo[1].Id;
                return c;
            });
            repoMock.GetAllItems().Returns(expectedProdInfo);
            var service = new ProductInfoService(Substitute.For<IDateTimeProvider>(), repoMock);
            var productInfo = await service.InsertItem(expectedProdInfo[0].Name, expectedProdInfo[0].Brand, expectedProdInfo[0].Ingredients);
            var productInfo2 = await service.InsertItem(expectedProdInfo[1].Name, expectedProdInfo[1].Brand, expectedProdInfo[1].Ingredients);
            var listProductInfo = await service.GetAllItems();

            await repoMock.Received(2).InsertItem(Arg.Any<ProductInfo>());
            await repoMock.Received(1).GetAllItems();

            productInfo.Should().NotBeNull();
            productInfo.Should().BeEquivalentTo(expectedProdInfo[0]);
            productInfo2.Should().NotBeNull();
            productInfo2.Should().BeEquivalentTo(expectedProdInfo[1]);

            listProductInfo.Should().HaveCount(2);
        }
        [Fact]
        public async Task TestDeleteProductInfo()
        {
            var list = new List<Ingredient>();
            list.Add(new Ingredient
            {
                Amount = 100,
                Name = "Wirkstoff A",
                Unit = DBI_Apotheke.Core.Workloads.Modules.Unit.MG
            });
            var productInfo = new ProductInfo
            {
                Brand = "Aspiring",
                Id = new ObjectId(),
                Ingredients = list,
                Name = "AspirinComplex"
            };
            var repoMock = Substitute.For<IProductInfoRepository>();
            repoMock.DeleteItem(Arg.Is(productInfo.Id)).Returns(Task.CompletedTask);
            var service = new ProductInfoService(Substitute.For<IDateTimeProvider>(), repoMock);
            await service.DeleteItem(productInfo.Id);
            var r = await service.GetItemById(productInfo.Id);

            await repoMock.Received(1).DeleteItem(Arg.Is(productInfo.Id));
            r.Should().BeNull();
        }
        [Fact]
        public async Task TestAddProductInfoAlreadyAdded()
        {
            var idPi = new ObjectId();
            var ingredient = new Ingredient
            {
                Amount = 100,
                Name = "Wirkstoff A",
                Unit = DBI_Apotheke.Core.Workloads.Modules.Unit.MG
            };
            var list = new List<Ingredient>();
            list.Add(ingredient);
            var normalPI = new ProductInfo
            {
                Brand = "Aspiring",
                Id = idPi,
                Ingredients = list,
                Name = "AspirinComplex"
            };
            var duplicatePI = new ProductInfo
            {
                Brand = "Aspiring",
                Id = idPi,
                Ingredients = list,
                Name = "AspirinComplex"
            };
            var repoMock = Substitute.For<IProductInfoRepository>();
            repoMock.InsertItem(Arg.Any<ProductInfo>()).Returns(ci =>
            {
                var c = ci.ArgAt<ProductInfo>(0);
                c.Id = normalPI.Id;
                return c;
            });
            repoMock.InsertItem(Arg.Any<ProductInfo>()).Returns(ci =>{
                var c = ci.ArgAt<ProductInfo>(0);
                c.Id = duplicatePI.Id;
                return c;
            });
            var dtMock = Substitute.For<IDateTimeProvider>();

            var service = new ProductInfoService(dtMock, repoMock);
            var productInfo = await service.InsertItem(normalPI.Name, normalPI.Brand, normalPI.Ingredients);
            var dupResultPI = await service.InsertItem(duplicatePI.Name, duplicatePI.Brand, duplicatePI.Ingredients);
            await repoMock.Received(2).InsertItem(Arg.Any<ProductInfo>());
            productInfo.Should().NotBeNull();
            productInfo.Should().BeEquivalentTo(normalPI);
            dupResultPI.Should().BeEquivalentTo(normalPI); //
        }
    }
}