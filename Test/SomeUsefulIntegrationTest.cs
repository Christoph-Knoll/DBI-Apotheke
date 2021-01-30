using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DBI_Apotheke.Core.Util;
using DBI_Apotheke.Core.Workloads.ProductInfos;
using DBI_Apotheke.Core.Workloads.Products;
using DBI_Apotheke.Core.Workloads.Recipes;
using DBI_Apotheke.Core.Workloads.Storages;
using FluentAssertions;
using LeoMongo;
using MongoDB.Bson;
using Xunit;
using Xunit.Abstractions;

namespace MongoDBDemoApp.Test
{
    public sealed class SomeUsefulIntegrationTest
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public SomeUsefulIntegrationTest(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public async Task TestWholeInfrastructureBeingHere()
        {
            //PLEASE TAKE CARE IF THE CONFIG MATCHES YOUR OWN MONGODB SETTINGS!
            var mongoConfig = new MongoConfig("mongodb://localhost", "test-blog");
            var databaseProvider = Factory.CreateDatabaseProvider(mongoConfig);
            var transactionProvider = Factory.CreateTransactionProvider(databaseProvider);

            var productRepository = new ProductRepository(transactionProvider, databaseProvider);
            var productInfoRepository = new ProductInfoRepository(transactionProvider, databaseProvider);
            var recipeRepository = new RecipeRepository(transactionProvider, databaseProvider);
            var storageRepository = new StorageRepository(transactionProvider, databaseProvider);
            // Clear database :)
            await deleteAllItemsFromDatabase(productRepository, productInfoRepository, recipeRepository, storageRepository);
            var ingredient = new Ingredient
            {
                Amount = 100,
                Name = "Wirkstoff A",
                Unit = DBI_Apotheke.Core.Workloads.Modules.Unit.MG
            };
            var idPi = new ObjectId();
            var list = new List<Ingredient>();
            list.Add(ingredient);
            var productInfo = new ProductInfo
            {
                Brand = "Aspiring",
                Id = idPi,
                Ingredients = list,
                Name = "AspirinComplex"
            };
            productInfo = await productInfoRepository.InsertItem(productInfo);
            var product1 = new Product
            {
                Amount = 200,
                Id = new ObjectId(),
                Price = 20,
                ProductInfoId = productInfo.Id,
                PZN = 01,
                Unit = DBI_Apotheke.Core.Workloads.Modules.Unit.G
            };
            var product2 = new Product
            {
                Amount = 200,
                Id = new ObjectId(),
                Price = 10,
                ProductInfoId = productInfo.Id,
                PZN = 02,
                Unit = DBI_Apotheke.Core.Workloads.Modules.Unit.G
            };
            var recipe = new Recipe
            {
                Address = "Burger Staﬂe",
                Id = new ObjectId(),
                Issuer = "Kirchi",
                Name = "Elias",
                PZNs = new List<int>() { 1, 2 }
            };
            product1 = await productRepository.InsertItem(product1);
            product2 = await productRepository.InsertItem(product2);
            recipe = await recipeRepository.InsertItem(recipe);
            var storage1 = new Storage
            {
                Id = new ObjectId(),
                Amount = 100,
                PZN = 1,
                StorageSite = "Hamburg"
            };
            var storage2 = new Storage
            {
                Id = new ObjectId(),
                Amount = 100,
                PZN = 2,
                StorageSite = "Hamburg"
            };
            storage1 = await storageRepository.InsertItem(storage1);
            storage2 = await storageRepository.InsertItem(storage2);

            _testOutputHelper.WriteLine("Test if everything was received!");

            _testOutputHelper.WriteLine("Id: " + productInfo.Id.ToString() +" Object: "+ productInfo.ToString());
            _testOutputHelper.WriteLine("Id: " + product1.Id.ToString() + " Object: " + product1.ToString());
            _testOutputHelper.WriteLine("Id: " + product2.Id.ToString() + " Object: " + product2.ToString());
            _testOutputHelper.WriteLine("Id: " + product1.Id.ToString() + " Object: " + product1.ToString());
            _testOutputHelper.WriteLine("Id: " + recipe.Id.ToString() + " Object: " + recipe.ToString());
            _testOutputHelper.WriteLine("Id: " + storage1.Id.ToString() + " Object: " + storage1.ToString());
            _testOutputHelper.WriteLine("Id: " + storage2.Id.ToString() + " Object: " + storage2.ToString());

            var productsInfoByIngreadients = await productInfoRepository.GetByIngredient("Wirkstoff A");
            var productsInfo = await productInfoRepository.GetAllItems();


            productsInfoByIngreadients.Should().HaveCount(1);
            productsInfo.Should().HaveCount(1);
            productsInfo.Should().BeEquivalentTo(productsInfoByIngreadients);


            product1 = await productRepository.GetByPzn(recipe.PZNs.IndexOf(0));
            product2 = await productRepository.GetByPzn(recipe.PZNs.IndexOf(1));
            var products = await productRepository.GetAllItems();

            products.Should().HaveCount(2);
            products.Should().BeEquivalentTo(new List<Product> {product1,product2});


            var actualRecipe = await recipeRepository.GetItemById(recipe.Id);
            actualRecipe.Should().BeEquivalentTo(recipe);

            var totalPrice = recipeRepository.GetTotalPrice(new List<Product> { product1, product2 });
            totalPrice.Should().BeApproximately(30,0.1);

            var actualStorage = await storageRepository.GetItemById(storage1.Id);
            actualStorage.Should().BeEquivalentTo(storage1);

            var actualStorage2 = await storageRepository.GetItemById(storage2.Id);
            actualStorage2.Should().BeEquivalentTo(storage2);

            actualStorage = await storageRepository.GetByPzn(product1.PZN);
            actualStorage.Should().BeEquivalentTo(storage1);

            actualStorage2 = await storageRepository.GetByPzn(product2.PZN);
            actualStorage2.Should().BeEquivalentTo(storage2);
        }

        private static async Task deleteAllItemsFromDatabase(ProductRepository productRepository, ProductInfoRepository productInfoRepository, RecipeRepository recipeRepository, StorageRepository storageRepository)
        {
            foreach (var p in await productRepository.GetAllItems())
            {
                if (p != null)
                {
                    await productRepository.DeleteItem(p.Id);
                }
            }
            foreach (var p in await productInfoRepository.GetAllItems())
            {
                if (p != null)
                {
                    await productInfoRepository.DeleteItem(p.Id);
                }
            }
            foreach (var p in await recipeRepository.GetAllItems())
            {
                if (p != null)
                {
                    await recipeRepository.DeleteItem(p.Id);
                }
            }
            foreach (var p in await storageRepository.GetAllItems())
            {
                if (p != null)
                {
                    await storageRepository.DeleteItem(p.Id);
                }
            }
        }
    }
}