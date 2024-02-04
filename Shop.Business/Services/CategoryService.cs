using Shop.Business.Interfaces;
using Shop.Core.Entities;
using Shop.DataAccess.DataAccess;

namespace Shop.Business.Services;

public class CategoryService:ICategoryService
{
    AppDbContext appDbContext = new AppDbContext();

    public void ShowAll()
    {
        List<Category> list = new List<Category>();
        list = appDbContext.categories.Where(x=>x.IsDeleted==false).ToList();
        foreach (var item in list)
        {
            Console.WriteLine("id: " + item.Id + " name: " + item.Name);
        }
    }
    public void CreateCategory(Category category)
    {
        Category checkCategory=appDbContext.categories.Where(x=>x.Name== category.Name && x.IsDeleted==false).FirstOrDefault();
        if (checkCategory==null) 
        {
            appDbContext.categories.Add(category);
            appDbContext.SaveChanges();
        }
        else
        {
            Console.WriteLine("bu adda kategoriya movcuddur");
        }
    }

    public void UpdateCategory(Category category)
    {
        Category checkCategory = appDbContext.categories.Where(x => x.Id == category.Id).FirstOrDefault();
        if (checkCategory == null)
        {
            Console.WriteLine("bele kategoriya yoxdur");
        }
        else
        {
            checkCategory.Name = category.Name;
            appDbContext.SaveChanges();
            Console.WriteLine("kateqoriyanin adi deyisdirildi");

        }
    }

    public void DeleteCategory(int id)
    {
        Category checkCategory = appDbContext.categories.Where(x => x.Id ==id).FirstOrDefault();
        if (checkCategory == null)
        {
            Console.WriteLine("bele kategoriya yoxdur");
        }
        else
        {
            checkCategory.IsDeleted = true;
            appDbContext.SaveChanges();
            Console.WriteLine("kateqoriya silindi");
        }
    }
}
