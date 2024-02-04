using Shop.Core.Entities;

namespace Shop.Business.Interfaces;

public interface IBrandService
{
    void ShowAll();
    void CreateBrand(Brand category);

    void UpdateBrand(Brand category);
    void DeleteBrand(int id);
}
