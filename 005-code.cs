using System;
using System.Collections.Generic;

namespace AbstractedTransaction
{
    public class Bank
    {
        List<Account> _accounts;
        List<Transaction> _transactions;

        public Bank()
        {
            _accounts = new List<Account>();
            _transactions = new List<Transaction>();
        }
        public void AddAccount(Account account)
        {
            _accounts.Add(account);
        }
        public Account GetAccount(String name)
        {
            for (int i = 0; i < _accounts.Count; i++)
            {
                if (name.Equals(_accounts[i].Name))
                    return _accounts[i];
            }
            return null;
        }
        public void ExecuteTransaction(Transaction transaction)
        {
            _transactions.Add(transaction);
            transaction.Execute();
            transaction.Print();

        }
        public void RollbackTransaction(Transaction transaction)
        {
            transaction.Rollback();
        }
        public void PrintTransactionHistory()
        {
            for (int i = 0; i < _transactions.Count; i++)
            {
                Console.WriteLine("Transaction number:" + i);
                Console.Write("status: ");
                if (_transactions[i].Reversed)
                   Console.WriteLine("Reversed");
                else if (_transactions[i].Success)
                    Console.WriteLine("Success");
                else if (_transactions[i].Executed)     
                    Console.WriteLine("executed");
                Console.WriteLine("\n timestamp: " + _transactions[i].DateStamp);
                _transactions[i].Print();
            }
        }

        public Transaction FindTransaction(int transnum)
        {
            if (transnum < 0 || transnum >= _transactions.Count)
            {
                throw new InvalidOperationException(" Transaction entered in Invalid");
            }
            return _transactions[transnum];
        }

    }
}
