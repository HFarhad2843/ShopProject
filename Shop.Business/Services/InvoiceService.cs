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
        basketProducts=appDbContext.basketproducts.Include(x=>x.Product).ThenInclude(x=>x.Discount).Where(x=>x.Basket.UserId== invoice.UserId && x.IsDeleted==false).ToList();
        decimal TotalPrice = 0; //toplam qiymet
        decimal initialPrice = 0; //ilkin qiymet
        decimal calculatedDiscount = 0; //hesablanmis endrim
        decimal calculatedPrice = 0; //hesablanmis son qiymet

        foreach (BasketProduct basketProductDetail in basketProducts)
        {
            initialPrice = basketProductDetail.Product.Price;

            if (basketProductDetail.Product.Discount!=null) //endrim yoxlayir
            {
                calculatedDiscount =  initialPrice * basketProductDetail.Product.Discount.DiscountPercent / 100; //faize uygun endrim miqdari hesablayir
                calculatedPrice = (initialPrice - calculatedDiscount)*basketProductDetail.ProductQuantity; //ilkin qiymetden endrimli qiymeti cixir sayina vurur son qiymeti hesablayir 
                TotalPrice += calculatedPrice; 
            }
            else
            {
                calculatedPrice = initialPrice * basketProductDetail.ProductQuantity;
                TotalPrice += calculatedPrice;
            }
        }

        invoice.TotalPrice = TotalPrice; //umumi qiymeti invoice deyerine yazir
        invoice.InvoiceStatus = 1; //yeni yaradilmis invoice statusu
        appDbContext.invoices.Add(invoice);
        appDbContext.SaveChanges();
        foreach (BasketProduct basketProduct in basketProducts) //zenbildeki mehsullari product invoice tablena yazir
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

       
    }

  
    public void PayInvoice(int InvoiceId)
    {
        Invoice invoice = appDbContext.invoices.FirstOrDefault(x => x.Id == InvoiceId);
        if (invoice != null)
        {
            bool paymentStatus = PaymentCheckout(invoice.WalletId,invoice.TotalPrice); //kartdaki balansi yoxlayir
            if (paymentStatus) //balansda kifayet qeder pul varsa novbeti emeliyyatlara kecir
            {
                Console.WriteLine("invoice odendi");
                ClearBasket(invoice.UserId); //cari userin zenbilini temizleyir
                ChangeProductStockQuantity(invoice.Id); //mehsulun stok sayini azaldit
                invoice.InvoiceStatus = 2;//odendi statusu
            }
            else
            {
                Console.WriteLine("odenisde xeta bas verdi balansda kifayet qeder pul yoxdur");
            }
        }
        
    }
    public void ClearBasket(int userId) //zenbili temizleyir
    {
        Basket basket = appDbContext.baskets.Where(x => x.IsDeleted == false).FirstOrDefault(x => x.UserId == userId);
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

    public bool PaymentCheckout(int WalletId,decimal TotalPrice)  //balansi yoxlayir
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
    public void ChangeProductStockQuantity(int invoiceId) //alinmis mehsullarin sayini azaldir
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
    public void GetInovicesByUserId(int userId) //userin invoicelarini gosterir
    {
        List<Invoice>invoices = new List<Invoice>();
        invoices=appDbContext.invoices.Where(x=>x.UserId== userId).ToList();   
        foreach (Invoice invoice in invoices)
        {
            Console.WriteLine("invoice id: "+invoice.Id+ " invoice date: "+invoice.CreatedDate+" invoice total price "+invoice.TotalPrice);
        }
    }
}
