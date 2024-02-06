using Shop.Core.Entities;

namespace Shop.Business.Interfaces;

public interface IProductService
{
    void ShowAll(string Role);
    void CreateProduct(Product product);

    void UpdateProduct(Product product);
    void DeleteProduct(int id);
    void Search(Product product);

}
