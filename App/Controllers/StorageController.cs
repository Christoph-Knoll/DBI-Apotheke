using AutoMapper;
using DBI_Apotheke.Core.Workloads.Storages;
using LeoMongo.Transaction;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DBI_Apotheke.Model.Storage;

namespace DBI_Apotheke.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public sealed class StorageController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IStorageService _service;
        private readonly ITransactionProvider _transactionProvider;

        public StorageController(ITransactionProvider transactionProvider, IMapper mapper, IStorageService service)
        {
            this._transactionProvider = transactionProvider;
            this._mapper = mapper;
            this._service = service;
        }

        /// <summary>
        ///     Returns the Storage identified by its id.
        /// </summary>
        /// <param name="id">id of Storage</param>
        /// <returns>a Storage</returns>
        [HttpGet]
        public async Task<ActionResult<StorageDTO>> GetById(string id)
        {
            Storage? storage;
            if (string.IsNullOrWhiteSpace(id) ||
                (storage = await this._service.GetItemById(new ObjectId(id))) == null)
            {
                return BadRequest();
            }

            return Ok(this._mapper.Map<Storage>(storage));
        }
        
        /// <summary>
        ///     Returns the Storage identified by its PZN.
        /// </summary>
        /// <param name="pzn">PZN of Storage</param>
        /// <returns>a Storage</returns>
        [HttpGet]
        [Route("pzn")]
        public async Task<ActionResult<StorageDTO>> GetByPZN(int pzn)
        {
            var storage = await this._service.GetByPzn(pzn);
            return Ok(this._mapper.Map<Storage>(storage));
        }

        /// <summary>
        ///     Returns all Storages.
        /// </summary>
        /// <returns>All existing Storage</returns>
        [HttpGet]
        [Route("all")]
        public async Task<ActionResult<IReadOnlyCollection<StorageDTO>>> GetAll()
        {
            IReadOnlyCollection<Storage> storages = await this._service.GetAllItems();
            return Ok(this._mapper.Map<List<StorageDTO>>(storages));
        }

        /// <summary>
        ///     Removes a Storage by its id
        /// </summary>
        /// <param name="id">id of an existing Storage</param>
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
        ///     Creates a new Storage.
        /// </summary>
        /// <param name="request">Data for the new Storage</param>
        /// <returns>Created Storage if successful</returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStorageRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.StorageSite))
            {
                return BadRequest();
            }

            using var transaction = await this._transactionProvider.BeginTransaction();
            var storage = await this._service.InsertItem(request.PZN, request.Amount, request.StorageSite);
            await transaction.CommitAsync();
            return Ok(storage);
        }
    }
}
