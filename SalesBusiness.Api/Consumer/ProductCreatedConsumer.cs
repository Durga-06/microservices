using MassTransit;
using Microsoft.IdentityModel.Tokens;
using SalesBusiness.Api.Data;
using SalesBusiness.Api.Data.Entities;
using Shared.Models.RabbitMQModels;

namespace SalesBusiness.Api.Consumer;

public class ProductCreatedConsumer : IConsumer<ProductCreated>
{
    private readonly SalesBusinessContext _salesBusinessContext;
    public ProductCreatedConsumer(SalesBusinessContext salesBusinessContext)
    {
        _salesBusinessContext = salesBusinessContext;
    }
    public async Task Consume(ConsumeContext<ProductCreated> context)
    {
        var product = new Products
        {
            Id = context.Message.Id,
            Name = context.Message.Name
        };
        _salesBusinessContext.Products.Add(product);
        await _salesBusinessContext.SaveChangesAsync();
    }
}