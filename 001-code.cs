using System;

namespace AbstractedTransaction
{
    public class WithdrawTransaction : Transaction, TransactonInterface
    {
        Account _account;

        public override bool Success
        {
            get { return _success; }
            //set { _success = value; }
        }

        public WithdrawTransaction(Account account, decimal amount) : base(amount)
        {
            _account = account;
            _amount = amount;
        }
        public override void Print()
        {
            if (Executed)
            {
                Console.WriteLine("transaction executed..");
                if (Success)
                {
                    Console.WriteLine("sucess! an amount of " + _amount + "is withdrawn from your account");
                }
                else if (Reversed)
                {
                    Console.WriteLine("transaction unsuccessfull! rolling back success");
                }
                else
                {
                    Console.WriteLine("transaction unsuccessful");
                }
            }
            else
            {
                Console.WriteLine("transaction couldn't take place");
            }
        }


        public override void Execute()
        {
            base.Execute();
            if (Executed)
                throw new InvalidOperationException("the transaction has been already executed!");
            else
            {


                if (_account.Withdraw(_amount))
                {
                    // Console.WriteLine("fgjgk");
                    _success = true;
                    base.setExecute();
                }
                else
                    throw new InvalidOperationException(" insufficient funds!!");
            }
        }
        public override void Rollback()
        {
            base.Rollback();


            if (!_success)
            {
                throw new InvalidOperationException("Transaction didn't take place");
            }
            else if (Reversed)
            {
                throw new InvalidOperationException("Transaction roll back already done!");
            }

            else if (_success && !Reversed)
            {
                _account.Deposit(_amount);
                base.setReverese();

            }
        }
    }
}
