using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Product.Api.Contracts;
using Product.Application.Abstractions.Messaging;
using Product.Application.Application.DTO;
using Product.Application.CommandHandler.CreateProduct;
using Product.Application.CommandHandler.UpdateProduct;
using Product.Application.QueryHandler;
using Product.Domain.Core;

namespace Product.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IQueryDispatcher _queryDispatcher;
    private readonly ICommandDispatcher _commandDispatcher;
    // private readonly IQueryHandler<GetProductByIdQuery, ProductDto?> _queryHandler;
    // private readonly ICommandHandler<CreateProductCommand, Result> _commandHandler;

    public ProductsController(IQueryDispatcher queryDispatcher,
        ICommandDispatcher commandDispatcher)
    {
        _queryDispatcher = queryDispatcher;
        _commandDispatcher = commandDispatcher;
    }

    [HttpGet]
    public async Task<IActionResult> GetProducts()
    {
        var query = new GetProductsQuery();
        var products = await _queryDispatcher.Dispatch<GetProductsQuery, List<ProductDto>>(query);
        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProductById(Guid id)
    {
        var query = new GetProductByIdQuery(id);
        ProductDto? product = await _queryDispatcher.Dispatch<GetProductByIdQuery, ProductDto?>(query);
        if (product is null)
        {
            return NotFound();
        }
        return Ok(product);
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct(CreateProductRequest request)
    {
        var command = new CreateProductCommand(
            request.ProductName,
            request.Qty
        );
        // Implementation for creating a new product
        Result result = await _commandDispatcher.Dispatch<CreateProductCommand, Result>(command);
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return StatusCode(201);
    }

    [HttpPut("{id}/stock")]
    public async Task<IActionResult> UpdateProductQuantity ([FromBody] UpdateProductQuantityRequest request, Guid id)
    {
        var command = new UpdateStockQuantityCommand(
            id,
            request.Quantity
        );

        Result result = await _commandDispatcher.Dispatch<UpdateStockQuantityCommand, Result>(command);
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return NoContent();
    }

    }
    
