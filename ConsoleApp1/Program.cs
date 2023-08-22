using System;

namespace AtmMachine
{
    class Program
    {
        static void Main(string[] args)
        {
            AccountManager accountManager = new AccountManager();
            accountManager.LoadAccountsFromFile();
            UserInterface userInterface = new UserInterface();

            int input = userInterface.GetUserInput();

            while (input == 1)
            {
                int accountNumber = userInterface.GetAccountNumber();
                int pin = userInterface.GetPIN();

                Account foundAccount = accountManager.FindAccount(accountNumber);

                if (foundAccount != null && accountManager.Authenticate(foundAccount, pin))
                {
                    userInterface.DisplayAccountDetails(foundAccount);
                    userInterface.PerformAccountOperations(foundAccount, accountManager);
                }
                else
                {
                    Console.WriteLine("Invalid account number or PIN.");
                }

                input = userInterface.GetUserInput();
            }

            if (input == 2)
            {
                userInterface.HandleAccountCreation(accountManager);
            }
        }
    }
}
