using System;

namespace AbstractedTransaction
{
    public class TransferTransaction : Transaction, TransactonInterface
    {
        Account _fromAccount;
        Account _toAccount;
        DepositTransaction _deposit;
        WithdrawTransaction _withdraw;

        public override bool Success
        {
            get
            {
                if (_deposit.Success && _withdraw.Success)
                {
                    return true;
                }

                return false;
            }


        }

        public TransferTransaction(Account fromAccount, Account toAccount, decimal amount) : base(amount)
        {
            _fromAccount = fromAccount;
            _toAccount = toAccount;

            DepositTransaction deposit = new DepositTransaction(toAccount, amount);
            _deposit = deposit;
            WithdrawTransaction withdraw = new WithdrawTransaction(fromAccount, amount);
            _withdraw = withdraw;

        }
        public override void Print()
        {
            if (Success)
            {
                Console.WriteLine("Transferred $" + _amount + " from " + _fromAccount.Name + " to " + _toAccount.Name);
                _withdraw.Print();
                _deposit.Print();
            }
            else if (Reversed)
                Console.WriteLine("transaction rolled back..");
            else
                Console.WriteLine("Transaction unsuccessfull!");
        }
        public override void Execute()
        {
            base.Execute();
            if (Executed)
            {
                throw new InvalidOperationException("The transfer transaction has been already attempted");
            }

            if (_amount > _fromAccount.Balance)
            {
                throw new InvalidOperationException("Insuffiecient funds!");
            }

            _withdraw.Execute();

            if (_withdraw.Success)
            {
                _deposit.Execute();

                if (!_deposit.Success)
                {
                    _withdraw.Rollback();
                }
            }

            base.setExecute();
        }
        public override void Rollback()
        {
            base.Rollback();
            if (!Success)
            {
                throw new InvalidOperationException("Transaction wasn't success..");
            }
            else if (Reversed)
            {
                throw new InvalidOperationException("Transaction already rolled back. .");
            }

            else
            {
                _deposit.Rollback();
                _withdraw.Rollback();
                base.setReverese();

            }
        }
    }
}
    

