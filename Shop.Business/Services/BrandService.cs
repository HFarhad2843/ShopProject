using Shop.Business.Interfaces;
using Shop.Core.Entities;
using Shop.DataAccess.DataAccess;

namespace Shop.Business.Services;

public class BrandService:IBrandService
{
    AppDbContext appDbContext = new AppDbContext();

    public void CreateBrand(Brand brand)
    {
        Brand checkBrand = appDbContext.brands.Where(x => x.Name == brand.Name && x.IsDeleted==false).FirstOrDefault();
        if (checkBrand == null)
        {
            appDbContext.brands.Add(brand);
            appDbContext.SaveChanges();
        }
        else
        {
            Console.WriteLine("bu adda brend movcuddur");
        }
    }

    public void DeleteBrand(int id)
    {
        Brand checkBrand = appDbContext.brands.Where(x => x.Id == id).FirstOrDefault();
        if (checkBrand == null)
        {
            Console.WriteLine("bele brend yoxdur");
        }
        else
        {
            checkBrand.IsDeleted = true;
            appDbContext.SaveChanges();
            Console.WriteLine("brend silindi");
        }
    }

    public void ShowAll()
    {
        List<Brand> list = new List<Brand>();
        list = appDbContext.brands.Where(x=>x.IsDeleted==false).ToList();
        foreach (var item in list)
        {
            Console.WriteLine("id: " + item.Id + " name: " + item.Name);
        }
    }

    public void UpdateBrand(Brand brand)
    {
        Brand checkBrand = appDbContext.brands.Where(x => x.Id == brand.Id).FirstOrDefault();
        if (checkBrand == null)
        {
            Console.WriteLine("bele brend yoxdur");
        }
        else
        {
            checkBrand.Name = brand.Name;
            appDbContext.SaveChanges();
            Console.WriteLine("brendin adi deyisdirildi");
        }
    }
}
