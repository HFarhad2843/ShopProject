using Shop.Core.Entities;

namespace Shop.Business.Interfaces;

public interface IProductService
{
    void ShowAll();
    void CreateProduct(Product product);

    void UpdateProduct(Product product);
    void DeleteProduct(int id);
}
