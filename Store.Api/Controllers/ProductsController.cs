using MediatR;
using Microsoft.AspNetCore.Mvc;
using Store.Application.Products.Commands;
using Store.Application.Products.Queries;

namespace Store.Api.Controllers
{
  
    public class ProductsController : BaseApiController
    {


        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator) => _mediator = mediator;

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductCommand createProductCommand)
        {
            var id = await _mediator.Send(createProductCommand);

            return Ok(id);
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var result = await _mediator.Send(new GetProductsQuery());
            
            return Ok(result);
        }
    }
}