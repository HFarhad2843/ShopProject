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
        List<int> productIds = new List<int>();
        basketProducts=appDbContext.basketproducts.Include(x=>x.Product).Where(x=>x.Basket.UserId== invoice.UserId && x.IsDeleted==false).ToList();
        decimal TotalPrice = 0;
        decimal initialPrice = 0;
        decimal calculatedDiscount = 0;
        decimal calculatedPrice = 0;

        foreach (BasketProduct basketProductDetail in basketProducts)
        {
            initialPrice = basketProductDetail.Product.Price;

            if (basketProductDetail.Product.Discount!=null)
            {
                calculatedDiscount =  initialPrice * basketProductDetail.Product.Discount.DiscountPercent / 100;
                calculatedPrice = (initialPrice - calculatedDiscount)*basketProductDetail.ProductQuantity;
                TotalPrice += calculatedPrice; 
            }
            else
            {
                calculatedPrice = initialPrice * basketProductDetail.ProductQuantity;
                TotalPrice += calculatedPrice;
            }
        }

        invoice.TotalPrice = TotalPrice;
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
            productIds.Add(productInvoice.ProductId);
        }
        appDbContext.SaveChanges();
        Console.WriteLine("Invoice yaradildi");

        bool paymentStatus = PaymentCheckout(invoice.WalletId, TotalPrice);
        if (paymentStatus)
        {
            Console.WriteLine("invoice odendi");
            ClearBasket(invoice.UserId);
            ChangeProductStockQuantity(invoice.Id);

        }
        else 
        {
            Console.WriteLine("odenisde xeta bas verdi");
        }
    }

    public void ClearBasket(int userId)
    {
       Basket basket=appDbContext.baskets.FirstOrDefault(x=>x.UserId==userId);
        if (basket != null)
        {
            basket.IsDeleted = true;
            List<BasketProduct> basketProducts = new List<BasketProduct>();
            foreach (BasketProduct basketProduct in basketProducts)
            {
                basketProduct.IsDeleted = true;
            }
        }
        appDbContext.SaveChanges();

        Console.WriteLine("Zenbil bosaldildi");
    }


    public bool PaymentCheckout(int WalletId,decimal TotalPrice)
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
                appDbContext.SaveChanges();
                return true;
            }
        }
    }
    public void ChangeProductStockQuantity(int invoiceId)
    {
        List<ProductInvoice> productInvoices = appDbContext.productsInvoices.Where(x => x.InvoiceId == invoiceId).ToList();
        foreach (var productInvoice in productInvoices)
        {
            Product product = appDbContext.products.FirstOrDefault(x => x.Id == productInvoice.ProductId);
            product.Quantity -= productInvoice.ProductCount;
        }
        appDbContext.SaveChanges();
        Console.WriteLine("mehsullarin stok sayi azaldildi");
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
