using System;

namespace AbstractedTransaction
{
    public abstract class Transaction
    {
        protected decimal _amount;
        protected bool _success;
        private bool _executed;
        private bool _reversed;
        DateTime _dateStamp;

        abstract public bool Success
        { get; }

        public bool Executed
        {
            get { return _executed; }
        }
        public bool Reversed
        {
            get { return _reversed; }
        }
        public DateTime DateStamp
        {
            get { return _dateStamp; }
        }
        public Transaction(decimal amount)
        {
            _amount = amount;
        }
        public abstract void Print();
        public virtual void Execute()
        {
            _dateStamp = DateTime.Now;
        }
        public virtual void Rollback()
        {
            _dateStamp = DateTime.Now;
        }
        public void setExecute()
        {
            _executed = true;
        }
        public void setReverese()
        {
            _reversed = true;
        }
    }

}
