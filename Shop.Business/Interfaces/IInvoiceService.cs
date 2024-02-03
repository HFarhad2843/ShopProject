using Shop.Core.Entities;

namespace Shop.Business.Interfaces;
public interface IInvoiceService
{
    void CreateInvoice(Invoice invoice);
    void GetInovicesByUserId(int userId);

}
