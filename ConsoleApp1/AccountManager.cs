using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
namespace AtmMachine
{
    public class AccountManager
    {
        private string _accountFilePath = @"C:\Users\USER\source\repos\ConsoleApp1\ConsoleApp1\AccountUsers.txt";

        public List<Account> LoadAccountsFromFile()
        {
            List<Account> accounts = new List<Account>();

            string[] accountLines = File.ReadAllLines(_accountFilePath);
            //persist transaction

            foreach (string line in accountLines)
            {
                string[] accountData = line.Split('|');
                int accountNumber = Convert.ToInt32(accountData[0]);
                int pin = Convert.ToInt32(accountData[1]);
                double balance = Convert.ToDouble(accountData[2]);
                string name = accountData[3];
                AccountType accountType = (AccountType)Enum.Parse(typeof(AccountType), accountData[4]);

                Account account = new Account(accountNumber, balance, pin, name, accountType);
                accounts.Add(account);
            }

            return accounts;
        }

        public void UpdateAccountBalanceInDataStore(Account account)
        {
            List<string> lines = File.ReadAllLines(_accountFilePath).ToList();
            for (int i = 0; i < lines.Count; i++)
            {
                string[] parts = lines[i].Split('|');
                if (parts.Length >= 3 && parts[0].Trim() == account.AccountNumber.ToString())
                {
                    parts[2] = account.Balance.ToString();
                    lines[i] = string.Join(" | ", parts);
                    break;
                }
            }
            File.WriteAllLines(_accountFilePath, lines);
        }


        public Account FindAccount(int accountNumber)
        {
            List<Account> accounts = LoadAccountsFromFile();
            return accounts.Find(account => account.AccountNumber == accountNumber);
        }

        public bool Authenticate(Account account, int pin)
        {
            return account.Pin == pin;
        }

        public void AddAccount(Account newAccount)
        {
            List<Account> accounts = LoadAccountsFromFile();
            accounts.Add(newAccount);
        }

        public int GenerateUniqueAccountNumber()
        {
            Random random = new Random();

            while (true)
            {
                int potentialAccountNumber = random.Next(1000000000, 2000000000);
                List<Account> accounts = LoadAccountsFromFile();
                if (!accounts.Any(account => account.AccountNumber == potentialAccountNumber))
                {
                    return potentialAccountNumber;
                }
            }
        }

        public int GenerateRandomPIN()
        {
            Random random = new Random();
            return random.Next(1000, 9999); // Generates a random 4-digit PIN   
        }
    }

}

//public class AccountManager
//{
//    private List<Account> _accounts;

//    public AccountManager()
//    {
//        _accounts = new List<Account>
//        {
//            new Account(accountNumber: 1326535459, pin: 1234, balance: 203.33, name: "moses", accountType: AccountType.Current),
//            new Account(accountNumber: 1342905725, pin: 1233, balance: 174.34, name: "sara", accountType: AccountType.Savings),
//            new Account(accountNumber: 1212164411, pin: 1235, balance: 456.55, name: "idris", accountType: AccountType.Savings),
//            new Account(accountNumber: 1300310820, pin: 1236, balance: 816.43, name: "gabriel", accountType: AccountType.Current),
//            new Account(accountNumber: 1963559478, pin: 1237, balance: 32.46, name: "chris", accountType: AccountType.Current)
//        };
//    }

//    public Account FindAccount(int accountNumber)
//    {
//        return _accounts.Find(account => account.AccountNumber == accountNumber);
//    }

//    public bool Authenticate(Account account, int pin)
//    {
//        return account.Pin == pin;
//    }

//    public void AddAccount(Account newAccount)
//    {
//        _accounts.Add(newAccount);
//    }

//    public int GenerateUniqueAccountNumber()
//    {
//        Random random = new Random();

//        while (true)
//        {
//            int potentialAccountNumber = random.Next(1000000000, 2000000000);

//            if (!_accounts.Any(account => account.AccountNumber == potentialAccountNumber))
//            {
//                return potentialAccountNumber;
//            }
//        }
//    }

//    public int GenerateRandomPIN()
//    {
//        Random random = new Random();
//        return random.Next(1000, 9999); // Generates a random 4-digit PIN   
//    }
//}
