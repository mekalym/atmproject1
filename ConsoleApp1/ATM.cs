using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtmMachine
{
    public class ATM
    {
        private Account _account;

        public ATM(Account account)
        {
            _account = account;
        }

        public void Deposit(double amount)
        {
            _account.Deposit(amount);
        }

        public bool Withdraw(double amount)
        {
            return _account.Withdraw(amount);
        }

        public void DisplayBalance()
        {
            Console.WriteLine($"Account Number: {_account.AccountNumber}");
            Console.WriteLine($"Current Balance: ${_account.Balance}");
        }

        public void Create()
        {
            _account.Create();
        }
    }

}
