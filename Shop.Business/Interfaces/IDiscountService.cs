using Shop.Core.Entities;

namespace Shop.Business.Interfaces;

public interface IDiscountService
{
    void ShowAll();
    void CreateDiscount(Discount discount);

}
