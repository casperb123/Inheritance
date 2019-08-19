using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inheritance.Entities
{
    public class ChildSavingsAccount : Account
    {
        private string childSsn;
        private int yearsLocked;

        public int YearsLocked
        {
            get { return yearsLocked; }
            set { yearsLocked = value; }
        }

        public string ChildSsn
        {
            get { return childSsn; }
            set { childSsn = value; }
        }

        public ChildSavingsAccount(string accountNumber, decimal balance, DateTime created, decimal creditLimit, List<Transaction> transactions, string childSsn, int yearsLocked)
            :base(accountNumber, balance, created, creditLimit, transactions)
        {
            ChildSsn = childSsn;
            YearsLocked = yearsLocked;
        }

        public ChildSavingsAccount(int id, string accountNumber, decimal balance, DateTime created, decimal creditLimit, List<Transaction> transactions, string childSsn, int yearsLocked)
            :base(id, accountNumber, balance, created, creditLimit, transactions)
        {
            ChildSsn = childSsn;
            YearsLocked = yearsLocked;
        }

        public DateTime CanBeWithDrawedFrom()
        {
            DateTime dateTime = Created;
            dateTime = dateTime.AddYears(YearsLocked);

            return dateTime;
        }

        public void Withdraw(decimal amount)
        {
            if (CanBeWithDrawedFrom() > DateTime.Now)
            {
                throw new InvalidOperationException($"The account is locked until {CanBeWithDrawedFrom().ToLongDateString()}");
            }

            Balance -= amount;
        }

        public void Deposit(decimal amount)
        {
            List<Transaction> transactions = Transactions.Where(x => x.Timestamp.Year == DateTime.Now.Year).ToList();
            decimal deposited = 0;

            foreach (Transaction transaction in transactions)
            {
                deposited += transaction.Amount;
            }

            if (Balance + amount > 72_000)
            {
                throw new ArgumentOutOfRangeException("The balance can't be higher than 72.000");
            }

            if (deposited == 6_000)
            {
                throw new ArgumentOutOfRangeException("You can only deposit 6.000 per year");
            }

            Balance += amount;
        }
    }
}
