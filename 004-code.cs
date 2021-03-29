using System;

namespace AbstractedTransaction
{
    public interface TransactonInterface
    {
        void Print();
        void Execute();
        void Rollback();
        bool Executed { get; }
        bool Success { get; }
        bool Reversed { get; }
    }
    enum MenuOption
    {
            AddAcc = 1,
            Withdraw = 2,
            Deposit = 3,
            Transfer = 4,
            Print = 5,
            PrintHistory = 6,
            Quit = 7

    }
    public class BankSystem
    {
        static Account FindAccount(Bank bank)
        {
            Console.WriteLine("Name of account which you want to find: ");
            string n = Console.ReadLine();
            Account resacc = bank.GetAccount(n);
            if (resacc == null)
                Console.WriteLine("Account does'nt exist!");
            return resacc;

        }
        static MenuOption ReadUserInput()
        {
            int input;
            do
            {
                Console.WriteLine("\nWelcome user!\n");
                Console.WriteLine("To:\na)Add account enter'1'\nb)Withdraw enter'2'\nc)Deposit enter'3'\nd)tranfer enter'4'\ne)print enter'5'\nf)Print history enter 6\ng)Quit enter '7'\n enter choice: ");
                input = Convert.ToInt32(Console.ReadLine());
            } while (input < 1 || input > 7);
            MenuOption option = (MenuOption)input;
            return option;
        }
        static void DoprintingHistory(Bank bank)
        {
            bank.PrintTransactionHistory();
            Console.WriteLine("Enter the number of transaction you want to rollback else enter '-1' to exit:");
            int num = Convert.ToInt32(Console.ReadLine());
            if (num != -1)
                DoRollback(num, bank);

        }
        static void DoRollback(int transnum, Bank bank)
        {
            bank.RollbackTransaction(bank.FindTransaction(transnum));
        }
        static void DoWithdraw(Bank bank)
        {
            Account account = FindAccount(bank);
            if (account != null)
            {
                Console.WriteLine("enter the amount you want to withdraw:");
                int amount = Convert.ToInt32(Console.ReadLine());
                WithdrawTransaction withdrawl = new WithdrawTransaction(account, amount);
                bank.ExecuteTransaction(withdrawl);
            }

        }
        static void DoDeposit(Bank bank)
        {
            Account account = FindAccount(bank);
            if (account != null)
            {
                Console.WriteLine("enter the amount you want to deposit:");
                int amount = Convert.ToInt32(Console.ReadLine());
                DepositTransaction deposit = new DepositTransaction(account, amount);
                bank.ExecuteTransaction(deposit);
            }


        }
        static void DoTransfer(Bank bank)
        {
            Console.WriteLine("Withdrawl account:");
            Account fromaccount = FindAccount(bank);
            Console.WriteLine("Deposit account:");
            Account toaccount = FindAccount(bank);
            if (fromaccount != null && toaccount != null)
            {
                Console.WriteLine("enter the amount you want to transfer:");
                int amount = Convert.ToInt32(Console.ReadLine());
                TransferTransaction transfer = new TransferTransaction(fromaccount, toaccount, amount);
                bank.ExecuteTransaction(transfer);
            }


        }
        static void Print(Bank bank)
        {
            Account acc = FindAccount(bank);
            if (acc != null)
                acc.Print();
        }
        static void DoAddAccount(Bank bank)
        {
            Console.WriteLine("Name of account for which you want to add: ");
            string n = Console.ReadLine();
            Console.WriteLine("Enter balance in the account: ");
            decimal b = Convert.ToDecimal(Console.ReadLine());
            Account acc = new Account(b, n);
            bank.AddAccount(acc);
        }
        public static void Main(string[] args)
        {
            int break_ = 0;
            Bank bank = new Bank();
            while (break_ != 1)
            {
                MenuOption option = ReadUserInput();
                Console.WriteLine("YOU SELECTED TO" + option + "\n" + "To Change enter '1' else '0':");
                int cnfrm = Convert.ToInt32(Console.ReadLine());
                if (cnfrm == 1)
                {
                    option = ReadUserInput();
                    Console.WriteLine("YOU SELECTED TO" + option + "\n");
                }
                try
                {
                    switch (option)
                    {
                        case MenuOption.AddAcc:
                            {
                                DoAddAccount(bank);
                                break;
                            }
                        case MenuOption.Withdraw:
                            {
                                DoWithdraw(bank);
                                break;
                            }
                        case MenuOption.Deposit:
                            {
                                DoDeposit(bank);
                                break;
                            }
                        case MenuOption.Transfer:
                            {
                                DoTransfer(bank);
                                break;
                            }
                        case MenuOption.Print:
                            {
                                Print(bank);
                                break;
                            }
                        case MenuOption.PrintHistory:
                            {
                                DoprintingHistory(bank);
                                break;
                            }
                        case MenuOption.Quit:
                            {
                                break_ = 1;
                                break;
                            }

                    }
                }
                catch (InvalidOperationException err)
                {
                    Console.WriteLine(err.Message);
                }

            }
        }
    }
}
