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
bool isContinue = true;
while (isContinue)
{
    if (SessionId == 0 && Role==string.Empty)
    {
        Console.WriteLine("1 - Login User\n"+ "2 - Register User");
    }
    if (SessionId>0 && Role=="User")
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
    if(SessionId>0  && Role=="Admin")
    {
        Console.WriteLine("-- Admin area--\n");
        Console.WriteLine("-- Categories--\n"+
            "3-Show all Categories\n"+ "14-Create Category\n"+"15-Update Category\n"+"16-Delete Category\n");
        Console.WriteLine("-- Brands--\n" +
                "4-Show all Brands\n" + "17-Create Brand\n" + "18-Update Brand\n" + "19-Delete Brand\n");

    }


    //Console.WriteLine("Choose the option:");
    //Console.WriteLine("-- Users--\n" +
    //"1 - Login User\n"+
    //"2 - Register User");

    //Console.WriteLine("-- Shop area--\n" +
    //"3 - Show all Categories\n" +
    //"4 - Show all Brands\n"+
    //"5 - Show all Products\n");

//    Console.WriteLine("-- Account Information--\n" +
//"6 - Show all Wallets\n"+
//"7 - Add Product To Basket\n"+
//"8 - Show Basket\n"+
//"9 - Create Invoice\n"+
//"10 - Show Invoices\n"+
//"11 - Pay Invoice\n"+
//"12 - Create Wallet");

    string? option = Console.ReadLine();
    const int MaxMenu = 21;
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

                    // Now you have all the inputs, you can create the User object
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
                    categoryService.ShowAll();

                    break;
                case (int)Menu.ShowAllBrands:
                    brandService.ShowAll();
                    break;

                case (int)Menu.ShowAllProducts:
                    productService.ShowAll();
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
                        Console.WriteLine("zenbile atmaq istediyiniz mehsulun id-ni secin");
                        int ProductId = Convert.ToInt32(Console.ReadLine());

                        Console.WriteLine("zenbile atmaq istediyiniz mehsulun sayini secin");
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
                        Console.WriteLine("Hal hazirda zenbilde olan mallarin siyahisi:");
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
                        Console.WriteLine("Brand adini daxil edin");
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
            }
        }
    }
}
// Finish