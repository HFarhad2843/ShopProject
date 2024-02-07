using Microsoft.EntityFrameworkCore;
using Shop.Business.Interfaces;
using Shop.Core.Entities;
using Shop.DataAccess.DataAccess;
using System.Collections.Generic;

namespace Shop.Business.Services;

public class ProductService:IProductService
{
    AppDbContext appDbContext = new AppDbContext();
    public void ShowAll(string Role)
    {
        List<Product> list = new List<Product>();
        if (Role=="admin")
        {
            list = appDbContext.products.Include(x => x.Category).Include(x => x.Brand).Include(x => x.Discount).ToList();
        }
        if (Role=="user")
        {
            list = appDbContext.products.Include(x => x.Category).Include(x => x.Brand).Include(x => x.Discount).Where(x=>x.IsDeleted==false).ToList();
        }
        Console.WriteLine("Mehsullar");

        foreach (var item in list)
        {
            Console.WriteLine("Id: " + item.Id + " Name: " + item.Name + " Category: "+item.Category.Name+ " Brand: " + item.Brand.Name +" Price: "+ item.Price +" Quantity: "+item.Quantity );
            if(item.Discount!=null)
            {
                Console.WriteLine("Endrim"  + item.Discount.Name + " Endrim faizi: " + item.Discount.DiscountPercent);
            }
        }
    }
    public void CreateProduct(Product product)
    {
        Product checkProduct = appDbContext.products.Where(x => x.Name == product.Name && x.IsDeleted == false).FirstOrDefault();
        if (checkProduct == null)
        {
            appDbContext.products.Add(product);
            appDbContext.SaveChanges();
            Console.WriteLine("product elave edildi");
        }
        else
        {
            Console.WriteLine("bu adda product movcuddur");
        }
    }

    public void UpdateProduct(Product product)
    {
        Product checkProduct = appDbContext.products.Where(x => x.Id == product.Id).FirstOrDefault();
        if (checkProduct == null)
        {
            Console.WriteLine("bele kategoriya yoxdur");
        }
        else
        {
            checkProduct.Name = product.Name;
            checkProduct.Quantity = product.Quantity;
            checkProduct.CategoryId = product.CategoryId;
            checkProduct.BrandId = product.BrandId;
            checkProduct.Price = product.Price;
            appDbContext.SaveChanges();
            Console.WriteLine("product melumatlari deyisdirildi");

        }
    }

    public void DeleteProduct(int id)
    {
        Product checkProduct = appDbContext.products.Where(x => x.Id == id).FirstOrDefault();
        if (checkProduct == null)
        {
            Console.WriteLine("bele product yoxdur");
        }
        else
        {
            checkProduct.IsDeleted = true;
            appDbContext.SaveChanges();
            Console.WriteLine("product silindi");
        }
    }

    public void Search(Product product)
    { 
        List<Product> products = appDbContext.products.Include(x => x.Category).Include(x => x.Brand).Include(x => x.Discount).ToList();

        if (product.CategoryId != null && product.CategoryId!=0)
        {
            products = products.Where(x => x.CategoryId == product.CategoryId).ToList();
        }

        if (product.BrandId != null && product.BrandId!=0)
        {
            products = products.Where(x => x.BrandId == product.BrandId).ToList();
        }

        if (product.Name != null)
        {
            products = products.Where(x => x.Name == product.Name).ToList();
        }
        Console.WriteLine("Mehsullar");

        foreach (var item in products)
        {
            Console.WriteLine("Id: " + item.Id + " Name: " + item.Name + " Category: " + item.Category.Name + " Brand: " + item.Brand.Name + " Price: " + item.Price + " Quantity: " + item.Quantity);
            if (item.Discount != null)
            {
                Console.WriteLine("Endrim " + item.Discount.Name + " Endrim faizi: " + item.Discount.DiscountPercent);
            }
        }

    }
}
