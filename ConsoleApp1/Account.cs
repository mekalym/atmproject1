using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtmMachine
{
    public class Account
    {
        public int AccountNumber { get; private set; }
        public double Balance { get; private set; }
        public string Name { get; private set; }
        public int Pin { get; private set; }
        public AccountType AccountType { get; private set; }
        public int DepositAmt { get; private set; }
        public int WithdrawAmt { get; private set; }
        public int TransferAmt { get; private set; }


        public Account(int accountNumber, double balance, int pin, string name, AccountType accountType)
        {
            AccountNumber = accountNumber;
            Pin = pin;
            Balance = balance;
            Name = name;
            AccountType = accountType;
            DepositAmt = 0;
            WithdrawAmt = 0;
            TransferAmt = 0;


        }

        public void Deposit(double depositAmount)
        {
            if (depositAmount > 0)
            {
                Balance += depositAmount;
                DepositAmt++;

                Console.WriteLine($"Deposited #{depositAmount}. New balance: #{Balance}. Name: {Name}");
            }
            else
            {
                Console.WriteLine("Invalid deposit balance.");
            }
        }

        public bool Withdraw(double withdrawAmount)
        {
            if (withdrawAmount > 0 && withdrawAmount <= Balance)
            {
                Balance -= withdrawAmount;
                WithdrawAmt++;
                Console.WriteLine($"Withdrawn #{withdrawAmount}. New balance: #{Balance}. name: {Name}");
                return true;
            }
            else
            {
                Console.WriteLine("Insufficient funds or invalid withdrawal balance.");
                return false;
            }
        }
        //public void Transfer(double transferAmount)
        //{
        //    if (transferAmount > 0 && transferAmount <= Balance)
        //    {
        //        Balance -= transferAmount;
        //        TransferAmt++;
        //        Console.WriteLine($"Transferred ${transferAmount}. New balance: ${Balance}. name: ${Name}");

        //    }
        //}


        public void Create()
        {
            Console.WriteLine("Do you want to create an Account? \n1. Yes \n2. No");
            string Response = Console.ReadLine().ToUpper();
        }
    }

    public enum AccountType
    {
        Savings = 0,
        Current = 1
    }

}
