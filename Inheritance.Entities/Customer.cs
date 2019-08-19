using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Inheritance.Entities
{
    public class Customer : Person
    {
        private List<Account> accounts;

        public List<Account> Accounts
        {
            get { return accounts; }
            set { accounts = value; }
        }

        public decimal AccountFee
        {
            get
            {
                decimal totalFees = 0;
                int totalAccounts = Accounts.Count;

                if (Enumerable.Range(1, 3).Contains(Accounts.Count))
                {
                    if (Rating == 1 || Rating == 2)
                    {
                        totalFees = 23;
                    }
                    else if (Rating == 3 || Rating == 4)
                    {
                        totalFees = 29;
                    }
                    else if (Rating >= 5)
                    {
                        totalFees = totalAccounts * 6;
                    }
                }
                else if (Enumerable.Range(3, 9).Contains(Accounts.Count))
                {
                    if (Rating == 1 || Rating == 2)
                    {
                        totalFees = 60;
                    }
                    else if (Rating == 3 || Rating == 4)
                    {
                        totalFees = 87;
                    }
                    else if (Rating >= 5)
                    {
                        totalFees = totalAccounts * 13;
                    }
                }
                else if (Accounts.Count >= 9)
                {
                    if (Rating == 1 || Rating == 2)
                    {
                        totalFees = 6;
                    }
                    else if (Rating == 3 || Rating == 4)
                    {
                        totalFees = 13;
                    }
                    else if (Rating >= 5)
                    {
                        totalFees = totalAccounts * 19.75m;
                    }
                }

                return totalFees;
            }
        }

        public int MonthlyFreeTransactions
        {
            get
            {
                int freeTransctions = 0;

                if (Rating == 1 || Rating == 2)
                {
                    freeTransctions = 40;
                }
                else if (Rating == 3 || Rating == 4)
                {
                    freeTransctions = 20;
                }
                else if (Rating >= 5)
                {
                    freeTransctions = 10;
                }

                return freeTransctions;
            }
        }

        public decimal TransactionFee
        {
            get
            {
                decimal transactionFee = 0;

                if (Rating == 1 || Rating == 2)
                {
                    transactionFee = 0.78m;
                }
                else if (Rating == 3 || Rating == 4)
                {
                    transactionFee = 0.99m;
                }
                else if (Rating >= 5)
                {
                    transactionFee = 1.99m;
                }

                return transactionFee;
            }
        }

        /// <summary>
        /// Returns the rating for the customer
        /// </summary>
        public int Rating
        {
            get
            {
                decimal debt = GetDebts();
                decimal assets = GetAssets();

                if (debt <= -2_250_000 && assets > 1_250_000)
                {
                    return 1;
                }
                else if (debt <= -2_250_000 && assets >= 50_000 && assets <= 1_250_000)
                {
                    return 2;
                }
                else if (debt <= -250_000 && debt >= -2_250_000 && assets >= 50_000 && assets <= 1_250_000)
                {
                    return 3;
                }
                else if (debt < 0 && debt > -250_000 && assets > 0 && assets < 50_000)
                {
                    if (debt + assets < 0)
                    {
                        return 5;
                    }

                    return 4;
                }
                else
                {
                    throw new InvalidOperationException("Couldn't get any rating");
                }
            }
        }

        public Customer(string firstName, string lastName, string ssn, List<Account> accounts)
            : base(firstName, lastName, ssn)
        {
            Accounts = accounts;
        }

        public Customer(int id, string firstName, string lastName, string ssn, List<Account> accounts)
            : base(id, firstName, lastName, ssn)
        {
            Accounts = accounts;
        }

        /// <summary>
        /// Returns the customers debts
        /// </summary>
        /// <returns>The customers debts</returns>
        public decimal GetDebts()
        {
            decimal debt = 0;

            foreach (Account account in Accounts)
            {
                if (account.Balance < 0)
                {
                    debt += account.Balance;
                }
            }

            return debt;
        }

        /// <summary>
        /// Returns the customers assets
        /// </summary>
        /// <returns>The customers assets</returns>
        public decimal GetAssets()
        {
            decimal asset = 0;

            foreach (Account account in Accounts)
            {
                if (account.Balance > 0)
                {
                    asset += account.Balance;
                }
            }

            return asset;
        }

        /// <summary>
        /// Returns the customers total balance
        /// </summary>
        /// <returns>The customers total balance</returns>
        public decimal GetTotalBalance()
        {
            return GetAssets() + GetDebts();
        }

        public decimal GetTotalFeesFor(DateTime year)
        {
            List<Transaction> transactions = Accounts.SelectMany(a => a.Transactions).ToList();
            transactions = transactions.Where(t => t.Timestamp.Year == year.Year).ToList();
            List<int> monthsToCheck = transactions.Select(t => t.Timestamp.Month).Distinct().ToList();
            decimal totalFees = 0;

            foreach (int month in monthsToCheck)
            {
                if (Rating == 1 || Rating == 2)
                {
                    if (transactions.Count > 40)
                    {
                        int totalTransactions = transactions.Count - 40;

                        totalFees = 0.78m * totalTransactions;
                    }
                }
                else if (Rating == 3 || Rating == 4)
                {
                    if (transactions.Count > 20)
                    {
                        int totalTransactions = transactions.Count - 20;

                        totalFees = 0.99m * totalTransactions;
                    }
                }
                else if (Rating >= 5)
                {
                    if (transactions.Count > 10)
                    {
                        int totalTransactions = transactions.Count - 10;

                        totalFees = 1.99m * totalTransactions;
                    }
                }
            }

            return totalFees;
        }

        public decimal GetTotalFeesFor(DateTime from, DateTime to)
        {
            throw new NotImplementedException();
        }
    }
}
