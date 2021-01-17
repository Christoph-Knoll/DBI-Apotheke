using AutoMapper;
using DBI_Apotheke.Core.Workloads.Recipes;
using DBI_Apotheke.Model.Recipe;
using LeoMongo.Transaction;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DBI_Apotheke.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public sealed class RecipeController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IRecipeService _service;
        private readonly ITransactionProvider _transactionProvider;

        public RecipeController(ITransactionProvider transactionProvider, IMapper mapper, IRecipeService service)
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
        public async Task<ActionResult<RecipeDTO>> GetById(string id)
        {
            Recipe? recipe;
            if (string.IsNullOrWhiteSpace(id) ||
                (recipe = await this._service.GetItemById(new ObjectId(id))) == null)
            {
                return BadRequest();
            }

            return Ok(this._mapper.Map<Recipe>(recipe));
        }

        /// <summary>
        ///     Returns all ProductInfos.
        /// </summary>
        /// <returns>All existing ProductInfo</returns>
        [HttpGet]
        [Route("all")]
        public async Task<ActionResult<IReadOnlyCollection<RecipeDTO>>> GetAll()
        {
            IReadOnlyCollection<Recipe> recipes = await this._service.GetAllItems();
            return Ok(this._mapper.Map<List<RecipeDTO>>(recipes));
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
        public async Task<IActionResult> Create([FromBody] CreateRecipeRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Name)
                || string.IsNullOrWhiteSpace(request.Issuer))
            {
                return BadRequest();
            }

            using var transaction = await this._transactionProvider.BeginTransaction();
            var productInfo = await this._service.InsertItem(request.Name, request.Address, request.Issuer, request.PZNs);
            await transaction.CommitAsync();
            return CreatedAtAction(nameof(GetById), new { id = productInfo.Id.ToString() }, productInfo);
        }

    }
}
