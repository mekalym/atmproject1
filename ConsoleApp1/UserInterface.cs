using System;
using System.IO;
using System.Reflection.Metadata.Ecma335;
using System.Security.Principal;

namespace AtmMachine
{
    public class UserInterface
    {
        private string _transactionFilePath = @"C:\Users\USER\source\repos\ConsoleApp1\ConsoleApp1\Transactions.txt";
        private string _accountFilePath = @"C:\Users\USER\source\repos\ConsoleApp1\ConsoleApp1\AccountUsers.txt";
        public int GetUserInput()
        {
            int userInput = 0;

            while (userInput < 1 || userInput > 3)
            {

                Console.WriteLine("1. Login \n2. Create account \n3. Exit");

                try
                {
                    userInput = Convert.ToInt32(Console.ReadLine());

                    if (userInput < 1 || userInput > 3)
                    {
                        Console.WriteLine("Invalid choice. Please select a valid option.");
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid input format. Please enter a valid number.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
            }

            return userInput;
        }

        public int GetAccountNumber()
        {
            Console.WriteLine();
            Console.WriteLine("Input account number: ");
            int userInput = 0;
            while (true)
            {
                try
                {
                    userInput = Convert.ToInt32(Console.ReadLine());
                    break;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid input format. Please enter a valid number.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
         
            }
            return userInput;

        }

        public int GetPIN()
        {
            Console.WriteLine("Input pin: ");
            int userInput = 0;
            while (true)
            {
                try
                {
                    userInput = Convert.ToInt32(Console.ReadLine());
                    break;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid input format. Please enter a valid number.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }

            }
            return userInput;
        }

        public void DisplayAccountDetails(Account account)
        {
            Console.WriteLine();
            Console.WriteLine($"Account Number: {account.AccountNumber}");
            Console.WriteLine($"Name: {account.Name}");
            Console.WriteLine($"Account Type: {account.AccountType}");
            Console.WriteLine($"Balance: {account.Balance}");
        }

        public void PerformAccountOperations(Account account, AccountManager accountManager)
        {

            
            while(true)
            {
                Console.WriteLine();
                Console.WriteLine("1. Deposit");
                Console.WriteLine("2. Withdraw");
                Console.WriteLine("3. Check Balance");
                Console.WriteLine("4. Transfer");
                Console.WriteLine("5. Exit");

                Console.Write("Select an option: ");
                int choice = Convert.ToInt32(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        Console.Write("Enter deposit amount: ");
                        double depositAmount = 0;
                        while (true)
                        {
                            try
                            {
                                depositAmount = Convert.ToDouble(Console.ReadLine());
                                break;
                            }
                            catch (FormatException)
                            {
                                Console.WriteLine("Invalid input format. Please enter a valid number.");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"An error occurred: {ex.Message}");
                            }
                            Console.WriteLine("Deposit successful.");
                        }
                        account.Deposit(depositAmount);
                        accountManager.UpdateAccountBalanceInDataStore(account);


                        string depositLog = $"Account Number: {account.AccountNumber} | Transaction Type: Deposit | Amount: {depositAmount} | Date/Time: {DateTime.Now}";
                        File.AppendAllText(_transactionFilePath, depositLog + Environment.NewLine);
                        Console.WriteLine();
                        

                        break;
                    case 2:
                        Console.Write("Enter withdrawal amount: ");
                        double withdrawalAmount = 0;
                        try
                        {
                            withdrawalAmount = Convert.ToDouble(Console.ReadLine());

                            if (account.Withdraw(withdrawalAmount))
                            {
                                Console.WriteLine("Withdrawal successful.");
                                accountManager.UpdateAccountBalanceInDataStore(account);
                                string withdrawalLog = $"Account Number: {account.AccountNumber} | Transaction Type: Withdrawal | Amount: {withdrawalAmount} | Date/Time: {DateTime.Now}";
                                File.AppendAllText(_transactionFilePath, withdrawalLog + Environment.NewLine);
                                Console.WriteLine();
                            }
                            else
                            {
                                Console.WriteLine("Insufficient balance.");
                            }
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Invalid input format. Please enter a valid number.");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"An error occurred: {ex.Message}");
                        }
                        break;

                    case 3:
                        Console.WriteLine($"Account Balance: {account.Balance}");
                        Console.WriteLine();
                        break;
                    case 4:
                        Console.WriteLine("Enter Account number of the account you want to transfer to: ");
                        int recipientAccountNumber = Convert.ToInt32(Console.ReadLine());
                        Account recipientAccount = accountManager.FindAccount(recipientAccountNumber);

                        if (recipientAccount != null)
                        {
                            Console.WriteLine($"Account Number: {recipientAccount.AccountNumber}");
                            Console.WriteLine($"Name: {recipientAccount.Name}");
                            Console.WriteLine($"Account Type: {recipientAccount.AccountType}");

                            Console.WriteLine("Enter amount to Transfer: ");
                            double transferAmount = Convert.ToDouble(Console.ReadLine());

                            if (transferAmount > 0 && transferAmount <= account.Balance)
                            {
                                account.Withdraw(transferAmount);
                                recipientAccount.Deposit(transferAmount);
                                Console.WriteLine("Transfer successful.");
                                accountManager.UpdateAccountBalanceInDataStore(account);
                                accountManager.UpdateAccountBalanceInDataStore(recipientAccount);

                                string transferLog = $"Account Number: {account.AccountNumber} | Transaction Type: Transfer to Account {recipientAccount.AccountNumber} | Amount: {transferAmount} | Date/Time: {DateTime.Now}";
                                File.AppendAllText(_transactionFilePath, transferLog + Environment.NewLine);
                                Console.WriteLine();
                              
                            }
                            else
                            {
                                Console.WriteLine("Insufficient Balance.");
                            }

                        }
                        else
                        {
                            Console.WriteLine("Invalid Account Number for Transfer.");
                        }
                        break;
                    case 5:
                        Console.WriteLine("Exiting ATM.");
                        Console.WriteLine();
                        accountManager.UpdateAccountBalanceInDataStore(account);

                        break;
                        
                    default:
                        Console.WriteLine("Invalid choice.");
                        break;

                }
                //accountManager.UpdateAccountBalancesInDataStore(accountManager.LoadAccountsFromFile());
                accountManager.UpdateAccountBalanceInDataStore(account);
                Console.WriteLine("Do you want to perform another transaction?\n1. Yes\n2. No");
                string response = Console.ReadLine().ToLower(); 

                if (response == "1" || response == "yes")
                {
                    continue;
                }
                else if (response == "2" || response == "no")
                {
                    
                    Console.WriteLine("Thank you for using our ATM.");
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid response. Exiting ATM.");
                    break;
                }

            }
            
        }

        public void HandleAccountCreation(AccountManager accountManager)
        {
            Console.WriteLine("Enter your Fullname: ");
            string name = Console.ReadLine();

            Console.WriteLine("Do you want a Savings or Current account?\n1. Savings\n2. Current");
            int accountTypeChoice = Convert.ToInt32(Console.ReadLine());
            AccountType accountType = accountTypeChoice == 1 ? AccountType.Savings : AccountType.Current;

            int newAccountNumber = accountManager.GenerateUniqueAccountNumber();
            int newAccountPIN = accountManager.GenerateRandomPIN();
       
            Account newAccount = new Account(newAccountNumber, 0, newAccountPIN, name, accountType);
            accountManager.AddAccount(newAccount);

            Console.WriteLine("Do you want to deposit money? \nYes \nNo");
            string input = Console.ReadLine().ToUpper();
            double amount = 0; 

                if (input == "YES")
                {
                    try
                    {
                        Console.WriteLine("How much would you like to Deposit? : ");
                        amount = Convert.ToDouble(Console.ReadLine());
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Invalid input format. Please enter a valid ammount.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"An error occurred: {ex.Message}");
                    }
                    
                }
            Console.WriteLine();
            Console.WriteLine("Account created successfully.\nAccount Number: {0}\nPIN: {1}", newAccountNumber, newAccountPIN);
            string accountLog = $"{newAccountNumber} | {newAccountPIN} | {amount} | {name} | {accountType}";
            File.AppendAllText(_accountFilePath, accountLog + Environment.NewLine);
        }
    }

}
