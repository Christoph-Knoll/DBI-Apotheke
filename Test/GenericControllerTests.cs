using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DBI_Apotheke.Core.Util;
using DBI_Apotheke.Core.Workloads.Generics;
using DBI_Apotheke.Core.Workloads.ProductInfo;
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
            var productInfo1 = new ProductInfo
            {
                Brand = "Aspiring",
                Id = idPI,
                Ingredients = list,
                Name = "AspirinComplex"
            };
            var repoMock = Substitute.For<IGenericRepository<ProductInfo>>();

            await repoMock.InsertItem(productInfo1);

            var result = repoMock.GetItemById(Arg.Any<ObjectId>()).Returns(pi => new ProductInfo
            {
                Id = pi.Arg<ObjectId>()
            });
            result.Should().BeSameAs(productInfo1);
        }
    }
}