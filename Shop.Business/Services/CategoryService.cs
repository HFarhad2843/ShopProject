using Shop.Core.Entities;
using Shop.DataAccess.DataAccess;

namespace Shop.Business.Services;

public class CategoryService
{
    AppDbContext appDbContext = new AppDbContext();

    public void ShowAll()
    {
        List<Category> list = new List<Category>();
        list = appDbContext.categories.ToList();
        foreach (var item in list)
        {
            Console.WriteLine("id: " + item.Id + " name: " + item.Name);
        }
    }
}
