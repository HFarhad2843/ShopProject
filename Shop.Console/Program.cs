using Shop.Business.Services;
using Shop.Business.Utilities.Helpers;
using Shop.Core.Entities;
using System.Security.Principal;

Console.WriteLine("Shop Project");
Console.WriteLine();
Console.WriteLine("Shop App Start:");
Console.WriteLine();
int SessionId = 0;
string Role = string.Empty;
UserService userService = new();
ProductService productService = new();
CategoryService categoryService = new();
BrandService brandService = new();
WalletService walletService = new();
BasketProductService basketProductService = new();
InvoiceService invoiceService = new();
DiscountService discountService = new();
bool isContinue = true;
const string guestMessage = "Siz login olmamisiniz.Yalniz mehsullara baxis kecire bilersiniz.Mehsullarin endrimleri mehsullar haqqindaki melumatin asagisinda yerlesir.Mehsullar uzerinde alish verish etmek ucun login olmalisiz.Sade user kimi login olmaq ucun 1 admin useri kimi login olmaq ucun 2 duymesini secin";
Console.WriteLine();
Console.WriteLine("Mehsullar");
Console.WriteLine(guestMessage);
productService.ShowAll("user");

while (isContinue)
{
    if (SessionId == 0 && Role==string.Empty)
    {
        Console.WriteLine("1 - Login User\n"+ "2 - Register User");
    }
    if (SessionId>0 && Role=="user")
    {
        Console.WriteLine("-- Shop area--\n" +
   "3 - Show all Categories\n" +
   "4 - Show all Brands\n" +
   "5 - Show all Products\n");

        Console.WriteLine("-- Account Information--\n" +
   "6 - Show all Wallets\n" +
   "7 - Add Product To Basket\n" +
   "8 - Show Basket\n" +
   "9 - Create Invoice\n" +
   "10 - Show Invoices\n" +
   "11 - Pay Invoice\n" +
   "12 - Create Wallet");
    }
    if(SessionId>0  && Role=="admin")
    {
        Console.WriteLine("-- Admin area--\n");
        Console.WriteLine("-- Categories--\n"+
        "3-Show all Categories\n"+ "14-Create Category\n"+"15-Update Category\n"+"16-Delete Category\n");
        Console.WriteLine("-- Brands--\n" +
        "4-Show all Brands\n" + "17-Create Brand\n" + "18-Update Brand\n" + "19-Delete Brand\n");
        Console.WriteLine("-- Products--\n" +
        "5-Show all Product\n" + "20-Create Product\n" + "21-Update Product\n" + "22-Delete Product\n");
        Console.WriteLine("-- Discounts--\n" +
        "23-Show all Discounts\n" + "24-Create Discount\n");
    }

    string? option = Console.ReadLine();
    const int MaxMenu = 30;
    int optionNumber;
    bool isInt = int.TryParse(option, out optionNumber);
    if (isInt)
    {
        if (optionNumber >= 0 && optionNumber <= MaxMenu)
        {
            switch (optionNumber)
            {
                    case (int)Menu.LoginUser:
                    Console.WriteLine("Enter User Name:");
                     string userName = Console.ReadLine();
                    Console.WriteLine("Enter Password:");
                    string password = Console.ReadLine();
                    if (userService.UserLogin(userName, password))
                    {
                        Console.WriteLine("Giriş uğurludur:");
                        SessionId = userService.GetUserId(userName, password);
                        Role = userService.GetUserRole(SessionId);
                    }
                    else 
                    { 
                        Console.WriteLine("Belə istifadəçi yoxdur");
                    }
                   
                    break;
                case (int)Menu.RegisterUser:
                    Console.WriteLine("Enter User Name:");
                    string userRegName = Console.ReadLine();

                    Console.WriteLine("Enter User Surname:");
                    string userSurname = Console.ReadLine();

                    Console.WriteLine("Enter Email:");
                    string email = Console.ReadLine();

                    Console.WriteLine("Enter Password:");
                    string userPassword = Console.ReadLine();

                    Console.WriteLine("Enter Phone:");
                    string phone = Console.ReadLine();

                    
                    var user = new User
                    {
                        UserName = userRegName,
                        UserSurname = userSurname,
                        Email = email,
                        Password = userPassword,
                        Phone = phone
                    };
                    userService.Register(user);

                    break;
                case (int)Menu.ShowAllCategories:
                    categoryService.ShowAll(Role);

                    break;
                case (int)Menu.ShowAllBrands:
                    brandService.ShowAll(Role);
                    break;

                case (int)Menu.ShowAllProducts:
                    productService.ShowAll(Role);
                    break;

                case (int)Menu.ShowAllWallets:
                    if(SessionId==0)
                    {
                        Console.WriteLine("Ilk once login olun");
                    }
                    else
                    {
                        walletService.GetWalletByUserId(SessionId);
                    }
                    break;
                case (int)Menu.AddBasketProduct:
                    if (SessionId == 0)
                    {
                        Console.WriteLine("Ilk once login olun");
                    }
                    else
                    {
                        productService.ShowAll(Role);
                        Console.WriteLine("Zenbile atmaq istediyiniz mehsulun id-ni secin");
                        int ProductId = Convert.ToInt32(Console.ReadLine());

                        Console.WriteLine("Zenbile atmaq istediyiniz mehsulun sayini secin");
                        int Quantity = Convert.ToInt32(Console.ReadLine());
                        basketProductService.AddBasketProduct(ProductId,Quantity,SessionId);
                    }
                    break;
                case (int)Menu.ShowBasket:
                    if (SessionId == 0)
                    {
                        Console.WriteLine("Ilk once login olun");
                    }
                    else
                    {
                        basketProductService.GetUserBasketProducts(SessionId);
                    }
                    break;
                case (int)Menu.CreateInvoice:
                    if (SessionId == 0)
                    {
                        Console.WriteLine("Ilk once login olun");
                    }
                    else
                    {
                        Console.WriteLine("Hal-hazirda zenbilde olan mallarin siyahisi:");
                        basketProductService.GetUserBasketProducts(SessionId);

                        Console.WriteLine("Odeme kartini secin:");
                        walletService.GetWalletByUserId(SessionId);
                        int walletId = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Kart novunu secin:");
                        string PaymentMethod = Console.ReadLine();
                        var invoice = new Invoice
                        {
                            UserId = SessionId,
                            WalletId = walletId,
                            PaymentMethod= PaymentMethod
                        };
                        invoiceService.CreateInvoice(invoice);
                    }

                    break;
                case (int)Menu.ShowInvoices:
                    if (SessionId == 0)
                    {
                        Console.WriteLine("Ilk once login olun");
                    }
                    else
                    {
                        invoiceService.GetInovicesByUserId(SessionId);
                    }
                    break;
                case (int)Menu.PayInvoice:
                    if (SessionId == 0)
                    {
                        Console.WriteLine("Ilk once login olun");
                    }
                    else
                    {
                        invoiceService.GetInovicesByUserId(SessionId);
                        Console.WriteLine("odemek istediyiniz invoice id-ni daxil edin");
                        int InvoiceId=Convert.ToInt32(Console.ReadLine());
                        invoiceService.PayInvoice(InvoiceId);
                    }
                    break;
                case (int)Menu.CreateWallet:
                    if (SessionId == 0)
                    {
                        Console.WriteLine("Ilk once login olun");
                    }
                    else
                    {
                        Wallet wallet = new Wallet();
                        wallet.UserId = SessionId;
                        Console.WriteLine("Card name daxil edin");
                        wallet.CardName = Console.ReadLine();
                        Console.WriteLine("Card number daxil edin");
                        wallet.CardNumber = Convert.ToInt64(Console.ReadLine());
                        Console.WriteLine("Balansı daxil edin");
                        wallet.Balance=Convert.ToDecimal(Console.ReadLine());
                        walletService.CreateWallet(wallet);
                    }
                    break;

                case (int)Menu.CreateCategory:
                    if (SessionId == 0)
                    {
                        Console.WriteLine("Ilk once login olun");
                    }
                    else
                    {
                        Console.WriteLine("Kateqoriya adini daxil edin");
                        Category category = new ();
                        category.Name = Console.ReadLine();
                        
                        categoryService.CreateCategory(category);
                    }
                    break;
                case (int)Menu.UpdateCategory:
                    if (SessionId == 0)
                    {
                        Console.WriteLine("Ilk once login olun");
                    }
                    else
                    {
                        Category category = new ();
                        Console.WriteLine("Id ni daxil edin");
                        category.Id=Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Kateqoriya adini daxil edin");
                        category.Name = Console.ReadLine();
                        categoryService.UpdateCategory(category);
                    }
                    break;
                case (int)Menu.DeleteCategory:
                    if (SessionId == 0)
                    {
                        Console.WriteLine("Ilk once login olun");
                    }
                    else
                    {
                        Console.WriteLine("Id ni daxil edin");
                        int CategoryId = Convert.ToInt32(Console.ReadLine()); 
                        categoryService.DeleteCategory(CategoryId);
                    }
                    break;
                case (int)Menu.CreateBrand:
                    if (SessionId == 0)
                    {
                        Console.WriteLine("Ilk once login olun");
                    }
                    else
                    {
                        Console.WriteLine("Brend adini daxil edin");
                        Brand brand = new();
                        brand.Name = Console.ReadLine();

                        brandService.CreateBrand(brand);
                    }
                    break;
                case (int)Menu.UpdateBrand:
                    if (SessionId == 0)
                    {
                        Console.WriteLine("Ilk once login olun");
                    }
                    else
                    {
                        Brand brand = new();
                        Console.WriteLine("Id ni daxil edin");
                        brand.Id = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Brend adini daxil edin");
                        brand.Name = Console.ReadLine();
                        brandService.UpdateBrand(brand);
                    }
                    break;
                case (int)Menu.DeleteBrand:
                    if (SessionId == 0)
                    {
                        Console.WriteLine("Ilk once login olun");
                    }
                    else
                    {
                        Console.WriteLine("Id ni daxil edin");
                        int BrandId = Convert.ToInt32(Console.ReadLine());
                        brandService.DeleteBrand(BrandId);
                    }
                    break;

                case (int)Menu.CreateProduct:
                    if (SessionId == 0)
                    {
                        Console.WriteLine("Ilk once login olun");
                    }
                    else
                    {
                        Product product = new();
                        Console.WriteLine("Product adini daxil edin");
                        product.Name = Console.ReadLine();
                        Console.WriteLine("Product qiymetini daxil edin");
                        product.Price = Convert.ToDecimal(Console.ReadLine());
                        Console.WriteLine("Stok sayini daxil edin");
                        product.Quantity = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Kateqoriyani daxil edin");
                        categoryService.ShowAll(Role);
                        product.CategoryId = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Brendi daxil edin");
                        brandService.ShowAll(Role);
                        product.BrandId = Convert.ToInt32(Console.ReadLine());
                        int? isDiscount = 0;
                        Console.WriteLine("Endirim daxil edirsinizmi? Beli-1, Xeyr-2 ");
                        isDiscount = Convert.ToInt32(Console.ReadLine());
                        if (isDiscount == 1)
                        {
                            discountService.ShowAll();
                            Console.WriteLine("Endirim secin");
                            product.DiscountId = Convert.ToInt32(Console.ReadLine());
                        }

                        productService.CreateProduct(product);
                    }
                    break;
                case (int)Menu.UpdateProduct:
                    if (SessionId == 0)
                    {
                        Console.WriteLine("Ilk once login olun");
                    }
                    else
                    {
                        Product product = new();
                        Console.WriteLine("Product adini daxil edin");
                        product.Name = Console.ReadLine();
                        Console.WriteLine("Product qiymetini daxil edin");
                        product.Price = Convert.ToDecimal(Console.ReadLine());
                        Console.WriteLine("Stok sayini daxil edin");
                        product.Quantity = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Kateqoriyani daxil edin");
                        categoryService.ShowAll(Role);
                        product.CategoryId = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Brendi daxil edin");
                        brandService.ShowAll(Role);
                        product.BrandId = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Endrimi daxil edin");
                        discountService.ShowAll();
                        product.DiscountId = Convert.ToInt32(Console.ReadLine());
                        productService.UpdateProduct(product);

                    }
                    break;
                case (int)Menu.DeleteProduct:
                    if (SessionId == 0)
                    {
                        Console.WriteLine("Ilk once login olun");
                    }
                    else
                    {
                        Console.WriteLine("Id ni daxil edin");
                        int ProductId = Convert.ToInt32(Console.ReadLine());
                        productService.DeleteProduct(ProductId);
                    }
                    break;

                case (int)Menu.ShowDiscounts:
                    if (SessionId == 0)
                    {
                        Console.WriteLine("Ilk once login olun");
                    }
                    else
                    {
                        discountService.ShowAll();
                    }
                    break;
                case (int)Menu.CreateDiscount:
                    if (SessionId == 0)
                    {
                        Console.WriteLine("Ilk once login olun");
                    }
                    else
                    {
                        Discount discount = new();
                        Console.WriteLine("Discount adini daxil edin");
                        discount.Name = Console.ReadLine();
                        Console.WriteLine("Endrim faizini daxil edin");
                        discount.DiscountPercent = Convert.ToDecimal(Console.ReadLine());
                        Console.WriteLine("Description daxil edin");
                        discount.Description = Console.ReadLine();
                        Console.WriteLine("Baslangic  tarixi daxil edin");
                        discount.StartTime = Convert.ToDateTime(Console.ReadLine());
                        Console.WriteLine("Son tarixi daxil edin");
                        discount.EndTime = Convert.ToDateTime(Console.ReadLine());
                        discountService.CreateDiscount(discount);
                    }
                    break;
       
            }
        }
    }
}
// Finish