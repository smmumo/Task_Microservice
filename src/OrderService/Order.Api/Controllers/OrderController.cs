using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Abstractions.Messaging;
using Microsoft.AspNetCore.Mvc;
using Order.Api.Contracts;
using Order.Application.CommandHandler.CreateOrder;
using Order.Application.DTO;
using Order.Application.QueryHandler;
using Order.Domain.Core.Results;

namespace Order.Api.Controllers;

    [ApiController]
    [Route("[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IQueryDispatcher _queryDispatcher;
        private readonly ICommandDispatcher _commandDispatcher;
        // private readonly IQueryHandler<GetOrderByIdQuery, OrderDto?> _queryHandler;
        // private readonly ICommandHandler<CreateOrderCommand, Result> _commandHandler;

        public OrdersController(IQueryDispatcher queryDispatcher,
            ICommandDispatcher commandDispatcher)
        {
            _queryDispatcher = queryDispatcher;
            _commandDispatcher = commandDispatcher;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrders()        
        {
            var query = new GetOrdersQuery();
            var orders = await _queryDispatcher.Dispatch<GetOrdersQuery, List<OrderDto>>(query);
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(Guid id)
        {
        
            var query = new GetOrderByIdQuery(id);
            OrderDto? order = await _queryDispatcher.Dispatch<GetOrderByIdQuery, OrderDto?>(query);
            if (order is null)
            {
                return NotFound();
            }
            return Ok(order);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(CreateOrderRequest request)
        {
            var command = new CreateOrderCommand(
                request.ProductId,             
                request.TotalAmount,
                request.Qty
            );

            // Implementation for creating a new order
            Result result = await _commandDispatcher.Dispatch<CreateOrderCommand, Result>(command);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return CreatedAtAction(nameof(GetOrderById), new { id = command.ProductId }, command);
        }
    }
    
