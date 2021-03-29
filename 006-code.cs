using System;

namespace AbstractedTransaction
{
    public class Account
    {
        decimal _balance;
        string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public decimal Balance
        {
            get
            {
                return _balance;
            }
        }
        public Account(decimal balance, string name)
        {
            Name = name;
            _balance = balance;
        }
        public bool Deposit(decimal amount)
        {

            if (amount > 0)
            {
                _balance += amount;
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool Withdraw(decimal amount)
        {
            if (_balance >= amount && amount > 0)
            {
                _balance -= amount;
                return true;
            }
            else
            {
                return false;
            }
        }
        public void Print()
        {
            Console.WriteLine("Name of account in which transactions happened: {0}", _name);
            Console.WriteLine("After transaction: Updated Balance is: ${0}", _balance.ToString("C"));
        }
    }
}