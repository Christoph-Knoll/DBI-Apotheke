using AutoMapper;
using DBI_Apotheke.Core.Workloads.ProductInfos;
using DBI_Apotheke.Core.Workloads.Products;
using DBI_Apotheke.Core.Workloads.Recipes;
using DBI_Apotheke.Core.Workloads.Storages;
using DBI_Apotheke.Model.Product;
using DBI_Apotheke.Model.ProductInfos;
using DBI_Apotheke.Model.Recipe;
using DBI_Apotheke.Model.Storage;

namespace DBI_Apotheke.Core.Util
{
    public sealed class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<ProductInfo, ProductInfoDTO>()
                .ForMember(p => p.Id, c => c.MapFrom(p => p.Id.ToString()));
            CreateMap<Product, ProductDTO>()
                .ForMember(p => p.Id, c => c.MapFrom(p => p.Id.ToString()))
                .ForMember(p => p.ProductInfoId, c => c.MapFrom(p => p.ProductInfoId.ToString()));
            CreateMap<Recipe, RecipeDTO>()
                .ForMember(p => p.Id, c => c.MapFrom(p => p.Id.ToString()));

            CreateMap<Storage, StorageDTO>()
                .ForMember(p => p.Id, c => c.MapFrom(p => p.Id.ToString()));
        }
    }
}