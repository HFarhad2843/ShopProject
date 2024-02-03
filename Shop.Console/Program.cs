using Shop.Business.Services;
using Shop.Business.Utilities.Helpers;
using Shop.Core.Entities;

Console.WriteLine("Shop Project");
Console.WriteLine();
Console.WriteLine("Shop App Start:");
Console.WriteLine();
int SessionId = 0;
UserService userService = new();
ProductService productService = new();
CategoryService categoryService = new();
BrandService brandService = new();
WalletService walletService = new();
BasketProductService basketProductService = new();
bool isContinue = true;
while (isContinue)
{
    Console.WriteLine("Choose the option:");
    Console.WriteLine("-- Users--\n" +
    "1 - Login User\n"+
    "2 - Register User");

    Console.WriteLine("-- Shop area--\n" +
    "3 - Show all Categories\n" +
    "4 - Show all Brands\n"+
    "5 - Show all Products\n");

    Console.WriteLine("-- Account Information--\n" +
"6 - Show all Wallets\n"+
"7 - Add Product To Basket");

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
            }
        }
    }
}
// Finish