using Microsoft.EntityFrameworkCore;
using Shop.Business.Interfaces;
using Shop.Core.Entities;
using Shop.DataAccess.DataAccess;

namespace Shop.Business.Services;

public class InvoiceService : IInvoiceService
{
    AppDbContext appDbContext = new AppDbContext();
    public void CreateInvoice(Invoice invoice)
    {
        List<BasketProduct> basketProducts = new List<BasketProduct>();
        basketProducts=appDbContext.basketproducts.Include(x=>x.Product).Where(x=>x.Basket.UserId== invoice.UserId && x.IsDeleted==false).ToList();
        invoice.TotalPrice = basketProducts.Sum(x => x.ProductQuantity * x.Product.Price);

        appDbContext.invoices.Add(invoice);
        appDbContext.SaveChanges();
        foreach (BasketProduct basketProduct in basketProducts)
        {
            ProductInvoice productInvoice = new ();
            productInvoice.ProductId = basketProduct.ProductId;
            productInvoice.ProductPrice=basketProduct.Product.Price;
            productInvoice.ProductCount = basketProduct.ProductQuantity;
            productInvoice.InvoiceId = invoice.Id;
            appDbContext.productsInvoices.Add(productInvoice);
        }
        appDbContext.SaveChanges();

        Console.WriteLine("Invoice yaradildi");
    }


    public bool CheckBalance(int WalletId,decimal TotalPrice)
    {
        Wallet wallet= new Wallet();
        wallet = appDbContext.wallets.FirstOrDefault(x => x.Id == WalletId);
        if (wallet == null)
        {
            Console.WriteLine("kart tapilmadi");
            return false;
        }
        else
        {
            if (wallet.Balance < TotalPrice)
            {
                Console.WriteLine("balansda kifayet qeder pul yoxdur");
                return false;

            }
            else
            {
                wallet.Balance -= TotalPrice;
                return true;
            }
        }
    }

    public void GetInovicesByUserId(int userId)
    {
        List<Invoice>invoices = new List<Invoice>();
        invoices=appDbContext.invoices.Where(x=>x.UserId== userId).ToList();   
        foreach (Invoice invoice in invoices)
        {
            Console.WriteLine("invoice id: "+invoice.Id+ " invoice date: "+invoice.CreatedDate+" invoice total price "+invoice.TotalPrice);
        }
    }
}
