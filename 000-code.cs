using System;

namespace AbstractedTransaction
{
    public class DepositTransaction : Transaction, TransactonInterface
    {
        Account _account;

        public override bool Success
        {
            get { return _success; }
            // set { _success = value; }
        }

        public DepositTransaction(Account account, decimal amount) : base(amount)
        {
            _account = account;
        }
        public override void Print()
        {
            if (Executed)
            {
                Console.WriteLine("transaction was executed..");
                if (Success)
                {
                    Console.WriteLine("transaction success..amount of" + _amount + "was deposited");
                }
                else if (Reversed)
                {
                    Console.WriteLine("transaction unsuccessful.. rolling back..");
                }
                else
                {
                    Console.WriteLine("transaction unsuccessful..");
                }
            }
            else
                Console.WriteLine("transaction wasn't executed..");
        }
        public override void Execute()
        {
            base.Execute();
            if (Executed)
                throw new InvalidOperationException("transaction already executed..");
            else
            {
                if (_account.Deposit(_amount))
                {
                    _success = true;
                    base.setExecute();
                }
                else
                    throw new InvalidOperationException("amount entered is invalid..");
            }

        }
        public override void Rollback()
        {
            base.Rollback();

            if (!Success)
            {
                throw new InvalidOperationException("Transaction didn't take place");
            }
            else if (Reversed)
            {
                throw new InvalidOperationException("Transaction roll back already done!");
            }

            else if (Success && !Reversed)
            {
                _account.Withdraw(_amount);
                base.setReverese();
            }


        }
    }
}
