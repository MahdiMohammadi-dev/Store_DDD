using MediatR;
using Microsoft.AspNetCore.Mvc;
using Store.Api.Models;
using Store.Application.Products.Commands;
using Store.Application.Products.Dtos;
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

            //return Ok(id);
            return Ok(new ApiResponse<Guid>
            {
                Success = true,
                Data = id,
                Message = $"Product created",
            });
        }


        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var result = await _mediator.Send(new GetProductsQuery());
            
            return Ok(new ApiResponse<List<ProductDto>>
            {
                Success = true,
                Data = result,
                ItemsCount = result.Count,
                Message = "Data List fetched",
            });
        }
    }
}

