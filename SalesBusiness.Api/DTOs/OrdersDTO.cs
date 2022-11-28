namespace SalesBusiness.Api.DTOs;

public class OrdersDTO
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public DateTime? OrderDate { get; set; }
    public ProductsDTO Product { get; set; }
}

public class ProductsDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
}