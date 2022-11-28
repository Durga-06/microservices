using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesBusiness.Api.Data;
using SalesBusiness.Api.Data.Entities;
using SalesBusiness.Api.DTOs;

namespace SalesBusiness.Api.Controllers;
[ApiController]
[Route("[controller]")]
public class OrdersController : ControllerBase
{
    private readonly SalesBusinessContext _salesBusinessContext;

    public OrdersController(SalesBusinessContext salesBusinessContext)
    {
        _salesBusinessContext = salesBusinessContext;
    }
    [HttpGet]
    [Route("{id}")]
    public async  Task<IActionResult> GetAsync(int id)
    {
        var orders = await _salesBusinessContext.Orders.FindAsync(id);
        return Ok(orders);
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync(Orders newOrder)
    {
         _salesBusinessContext.Orders.Add(newOrder);
         await _salesBusinessContext.SaveChangesAsync();
         return CreatedAtAction("Get", new { id = newOrder.Id }, newOrder);
    }

    [HttpGet]
    public async Task<IActionResult> GetAsync()
    {
        var orders = await _salesBusinessContext.Orders.Join(
                _salesBusinessContext.Products, orders => orders.ProductId, product => product.Id,
                ((orders, product) => new { Order = orders, Product = product }))
            .Select(_ => new OrdersDTO
            {
                Id = _.Order.Id,
                UserId = _.Order.UserId,
                OrderDate = _.Order.OrderDate,
                Product = new ProductsDTO
                {
                    Id = _.Product.Id,
                    Name = _.Product.Name
                }
            }).ToListAsync();
        return Ok(orders);
    }
}