using Shop.Core.Abstract;

namespace Shop.Core.Entities;

public class ProductInvoice:BaseEntities
{
    public int ProductId { get; set; }
    public Product Product { get; set; }
    public int InvoiceId { get; set; }
    public int ProductCount { get; set; }
    public decimal ProductPrice { get; set; }
    public decimal DiscountApplied {  get; set; }
    
}
