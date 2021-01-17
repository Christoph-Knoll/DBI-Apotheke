using AutoMapper;
using DBI_Apotheke.Core.Workloads.Recipes;
using DBI_Apotheke.Model.Recipe;
using LeoMongo.Transaction;
using Microsoft.AspNetCore.Mvc;
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

    }
}
