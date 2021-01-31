using AutoMapper;
using DBI_Apotheke.Core.Workloads.ProductInfos;
using DBI_Apotheke.Model.ProductInfos;
using LeoMongo.Transaction;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DBI_Apotheke.Core.Workloads.Products;

namespace DBI_Apotheke.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public sealed class ProductInfoController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IProductInfoService _service;
        private readonly ITransactionProvider _transactionProvider;

        public ProductInfoController(ITransactionProvider transactionProvider, IMapper mapper, IProductInfoService service)
        {
            this._transactionProvider = transactionProvider;
            this._mapper = mapper;
            this._service = service;
        }

        /// <summary>
        ///     Returns the ProductInfo identified by its id.
        /// </summary>
        /// <param name="id">id of ProductInfo</param>
        /// <returns>a ProductInfo</returns>
        [HttpGet]
        public async Task<ActionResult<ProductInfoDTO>> GetById(string id)
        {
            ProductInfo? productInfo;
            if (string.IsNullOrWhiteSpace(id) ||
                (productInfo = await this._service.GetItemById(new ObjectId(id))) == null)
            {
                return BadRequest();
            }

            return Ok(this._mapper.Map<ProductInfo>(productInfo));
        }
        
        /// <summary>
        ///     Returns the ProductInfo identified by its name.
        /// </summary>
        /// <param name="name">name of ProductInfo</param>
        /// <returns>a ProductInfo</returns>
        [HttpGet]
        [Route("name")]
        public async Task<ActionResult<IReadOnlyCollection<ProductInfoDTO>>> GetByName(string name)
        {
            IReadOnlyCollection<ProductInfo> productInfos;
            if (string.IsNullOrWhiteSpace(name) ||
                (productInfos = await this._service.GetByName(name)) == null)
            {
                return BadRequest();
            }

            return Ok(this._mapper.Map<List<ProductInfo>>(productInfos));
        }
        
        /// <summary>
        ///     Returns the ProductInfo identified by a Ingredient it contains.
        /// </summary>
        /// <param name="ingredientName">Name of ingredient</param>
        /// <returns>a ProductInfo</returns>
        [HttpGet]
        [Route("ingredient")]
        public async Task<ActionResult<IReadOnlyCollection<ProductInfo>>> GetByIngredientName(string ingredientName)
        {
            if (string.IsNullOrWhiteSpace(ingredientName))
            {
                return BadRequest();
            }

            IReadOnlyCollection<ProductInfo> productInfos = await this._service.GetByIngredient(ingredientName);

            return Ok(this._mapper.Map<List<ProductInfoDTO>>(productInfos));
        }

        /// <summary>
        ///     Returns all ProductInfos.
        /// </summary>
        /// <returns>All existing ProductInfo</returns>
        [HttpGet]
        [Route("all")]
        public async Task<ActionResult<IReadOnlyCollection<ProductInfoDTO>>> GetAll()
        {
            IReadOnlyCollection<ProductInfo> productInfos = await this._service.GetAllItems();
            return Ok(this._mapper.Map<List<ProductInfoDTO>>(productInfos));
        }

        /// <summary>
        ///     Removes a ProductInfo by its id
        /// </summary>
        /// <param name="id">id of an existing ProductInfo</param>
        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return BadRequest();
            }

            using var transaction = await this._transactionProvider.BeginTransaction();
            await this._service.DeleteItem(new ObjectId(id));
            await transaction.CommitAsync();
            return Ok();
        }

        /// <summary>
        ///     Creates a new ProductInfo.
        /// </summary>
        /// <param name="request">Data for the new ProductInfo</param>
        /// <returns>Created ProductInfo if successful</returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProductInfoRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Brand)
                || string.IsNullOrWhiteSpace(request.Name))
            {
                return BadRequest();
            }

            using var transaction = await this._transactionProvider.BeginTransaction();
            var productInfo = await this._service.InsertItem(request.Name, request.Brand, request.Ingredients);
            await transaction.CommitAsync();
            return CreatedAtAction(nameof(GetById), new { id = productInfo.Id.ToString() }, productInfo);
        }
    }
}
