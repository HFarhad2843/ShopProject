using Shop.Business.Services;
using Shop.Business.Utilities.Helpers;

Console.WriteLine("Shop Project");
Console.WriteLine();
Console.WriteLine("Shop App Start:");
Console.WriteLine();

UserService userService = new();

bool isContinue = true;
while (isContinue)
{

    Console.WriteLine("Choose the option:");
    Console.WriteLine("-- Users--\n" +
    "1 - Login Users\n");

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
                    if (userService.UserLogin(userName, password)) Console.WriteLine("Giriş uğurludur:");
                    else { Console.WriteLine("Belə istifadəçi yoxdur"); }
                   
                    break;
            }
        }
    }
}
// Finish