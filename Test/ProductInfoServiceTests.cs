using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DBI_Apotheke.Core.Util;
using DBI_Apotheke.Core.Workloads.Generics;
using DBI_Apotheke.Core.Workloads.ProductInfos;
using FluentAssertions;
using MongoDB.Bson;
using NSubstitute;
using Xunit;

namespace MongoDBDemoApp.Test
{
    public sealed class ProductInfoServiceTests

    {
        [Fact]
        public async Task TestGetProductInfoById()
        {
            var id = new ObjectId();
            var repoMock = Substitute.For<IGenericRepository<ProductInfo>>();
            repoMock.GetItemById(Arg.Any<ObjectId>()).Returns(pi => new ProductInfo
            {
                Id = pi.Arg<ObjectId>()
            });

        //    var service = new ProductInfoService(Substitute.For<IDateTimeProvider>(), repoMock);
        //    var comment = await service.GetProductInfoById(id);

        }
    }
}