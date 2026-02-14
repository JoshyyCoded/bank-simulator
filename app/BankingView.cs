using ATMApp.Services;

namespace ATMApp.View
{
    public static class BankingView
{
    public static void Run()
    {
        Console.WriteLine("Jos Hua Necole Largado");
        Console.WriteLine("=== Simple ATM Machine ===");

        double balance = 1000.00;
        double lastTransAmt = 0.0;
        bool isSuccessful = false;

        while (true)
        {
            Console.WriteLine("1: Check Balance");
            Console.WriteLine("2: Deposit Money");
            Console.WriteLine("3: Withdraw Money");
            Console.WriteLine("4: Print Mini Statement");
            Console.WriteLine("5: Exit");
            Console.Write("Enter your option: ");

            string userInput = Console.ReadLine();
            if (!int.TryParse(userInput, out int choice))
            {
                Console.WriteLine("Invalid option selected. Please try again.");
                Console.WriteLine();
                continue;
            }

            switch (choice)
            {
                case 1:
                    double currentBalance = BankingServices.GetBalance(balance);
                    Console.WriteLine($"Current Balance: {currentBalance}");
                    Console.WriteLine();
                    break;

                case 2:
                    Console.Write("Enter amount to deposit: ");
                    if (double.TryParse(Console.ReadLine(), out double depositAmt) && depositAmt > 0)
                    {
                        bool depositSuccess = BankingServices.Deposit(ref balance, depositAmt);
                        if (depositSuccess)
                        {
                            lastTransAmt = depositAmt;
                            Console.WriteLine($"Deposit Successful.");
                            Console.WriteLine($"Updated balance: {balance}");
                            Console.WriteLine();
                        }
                        else
                        {
                            Console.WriteLine("Deposit failed. Try again.");
                            Console.WriteLine();
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid deposit amount. Please enter a positive value.");
                        Console.WriteLine();
                    }
                    break;

                case 3:
                    Console.Write("Enter amount to withdraw: ");
                    if (double.TryParse(Console.ReadLine(), out double withdrawAmt) && withdrawAmt > 0)
                    {
                        BankingServices.Withdraw(ref balance, withdrawAmt, out isSuccessful);
                        if (isSuccessful)
                        {
                            lastTransAmt = -withdrawAmt;
                            Console.WriteLine("Withdrawal successful.");
                            Console.WriteLine($"Updated balance: {balance}");
                            Console.WriteLine();
                        }
                        else
                        {
                            Console.WriteLine("Withdrawal failed. Insufficient balance or invalid amount.");
                            Console.WriteLine();
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid amount. Please enter a positive value.");
                        Console.WriteLine();
                    }
                    break;

                case 4:
                    Console.WriteLine("--- Mini Statement ---");
                    Console.WriteLine($"Current Balance: {balance}");
                    Console.WriteLine($"Last Transaction Amount: {lastTransAmt}");
                    Console.WriteLine();
                    break;

                case 5:
                    Console.WriteLine("Thank you for using the ATM. Goodbye!");
                    return;

                default:
                    Console.WriteLine("Invalid Input. Please enter an integer value.");
                    Console.WriteLine();
                    break;
            }
        }
    }
}
}
