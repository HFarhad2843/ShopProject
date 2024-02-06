using Shop.Core.Abstract;

namespace Shop.Core.Entities;

public class Invoice : BaseEntities
{
    public int UserId { get; set; }
    public int WalletId { get; set; }
    public Wallet Wallet { get; set; }
    public DateTime InvoiceDate { get; set; }
    public decimal TotalPrice { get; set; }
    public string PaymentMethod { get; set; }
    public ICollection<ProductInvoice> ProductInvoices { get; set; }
    public int InvoiceStatus { get; set; }
}
