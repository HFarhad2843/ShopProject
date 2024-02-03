using Shop.Business.Interfaces;
using Shop.Core.Entities;
using Shop.DataAccess.DataAccess;

namespace Shop.Business.Services;

public class ProductInvoiceService : IProductInvoiceService
{
    AppDbContext AppDbContext = new AppDbContext();
    public void CreateProductInvoice(ProductInvoice productInvoice)
    {
        //create invoice for user
        
    }
}
