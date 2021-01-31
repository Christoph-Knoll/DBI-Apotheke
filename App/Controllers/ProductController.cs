using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using DBI_Apotheke.Core.Workloads.ProductInfos;
using DBI_Apotheke.Core.Workloads.Products;
using DBI_Apotheke.Model.Product;
using LeoMongo.Transaction;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace DBI_Apotheke.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public sealed class ProductController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IProductService _service;
        private readonly IProductInfoService _productInfoService;

        private readonly ITransactionProvider _transactionProvider;

        public ProductController(ITransactionProvider transactionProvider, IMapper mapper, IProductService service, IProductInfoService productInfoService)
        {
            this._transactionProvider = transactionProvider;
            this._mapper = mapper;
            this._service = service;
            this._productInfoService = productInfoService;
        }

        /// <summary>
        ///     Returns the Product identified by its id.
        /// </summary>
        /// <param name="id">id of Product</param>
        /// <returns>a Product</returns>
        [HttpGet]
        public async Task<ActionResult<ProductDTO>> GetById(string id)
        {
            Product? product;
            if (string.IsNullOrWhiteSpace(id) ||
                (product = await this._service.GetItemById(new ObjectId(id))) == null)
            {
                return BadRequest();
            }

            return Ok(this._mapper.Map<Product>(product));
        }
        
        /// <summary>
        ///     Returns the Product identified by its PZN.
        /// </summary>
        /// <param name="pzn">PZN of Product</param>
        /// <returns>a Product</returns>
        [HttpGet]
        [Route("pzn")]
        public async Task<ActionResult<ProductDTO>> GetByPZN(int pzn)
        {
            var storage = await this._service.GetByPzn(pzn);
            return Ok(this._mapper.Map<Product>(storage));
        }

        /// <summary>
        ///     Returns all Products.
        /// </summary>
        /// <returns>All existing Product</returns>
        [HttpGet]
        [Route("all")]
        public async Task<ActionResult<IReadOnlyCollection<ProductDTO>>> GetAll()
        {
            IReadOnlyCollection<Product> products = await this._service.GetAllItems();
            return Ok(this._mapper.Map<List<ProductDTO>>(products));
        }

        /// <summary>
        ///     Removes a Product by its id
        /// </summary>
        /// <param name="id">id of an existing Product</param>
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
        ///     Creates a new Product.
        /// </summary>
        /// <param name="request">Data for the new Product</param>
        /// <returns>Created Product if successful</returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProductRequest request)
        {
            using var transaction = await this._transactionProvider.BeginTransaction();

            var productInfo = await _productInfoService.GetItemById(new ObjectId(request.ProductInfoId));

            if (productInfo == null)
            {
                return BadRequest();
            }
            
            var product = await this._service.InsertItem(productInfo, request.PZN, request.Price, request.Amount, request.Unit);
            await transaction.CommitAsync();
            return Ok(this._mapper.Map<ProductDTO>(product));
        }
    }
}
