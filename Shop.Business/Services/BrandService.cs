using Shop.Core.Entities;
using Shop.DataAccess.DataAccess;

namespace Shop.Business.Services;

public class BrandService
{
    AppDbContext appDbContext = new AppDbContext();

    public void ShowAll()
    {
        List<Brand> list = new List<Brand>();
        list = appDbContext.brands.ToList();
        foreach (var item in list)
        {
            Console.WriteLine("id: " + item.Id + " name: " + item.Name);
        }
    }
}
