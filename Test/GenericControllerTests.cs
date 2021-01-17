using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DBI_Apotheke.Core.Util;
using DBI_Apotheke.Core.Workloads.Generics;
using DBI_Apotheke.Core.Workloads.ProductInfos;
using FluentAssertions;
using MongoDB.Bson;
using NSubstitute;
using NSubstitute.ReceivedExtensions;
using Xunit;

namespace MongoDBDemoApp.Test
{
    public sealed class GenericControllerTests

    {
        [Fact]
        public async Task TestGetProductInfoById()
        {
            var idPI = new ObjectId();
            var idI = new ObjectId();
            var ingredient = new Ingredient
            {
                Id = idI,
                Amount = 100,
                Name = "Wirkstoff A",
                Unit = DBI_Apotheke.Core.Workloads.Modules.Unit.G
            };
            var list = new List<Ingredient>();
            list.Add(ingredient);
            var productInfo1 = new ProductInfo
            {
                Brand = "Aspiring",
                Id = idPI,
                Ingredients = list,
                Name = "AspirinComplex"
            };
            var repoMock = Substitute.For<IProductInfoRepository>();
            repoMock.GetItemById(Arg.Any<ObjectId>()).Returns(ci => new ProductInfo
            {
                Id = ci.Arg<ObjectId>()
            });
            var rs = await repoMock.InsertItem(productInfo1);
            
            await repoMock.Received(1).InsertItem(Arg.Any<ProductInfo>());
            
            var result = await repoMock.GetItemById(productInfo1.Id);
            var result2 = await repoMock.GetAllItems();
            //await repoMock.Received(1).GetItemById(productInfo1.Id);

            result.Should().NotBeNull();
        }
    }
}