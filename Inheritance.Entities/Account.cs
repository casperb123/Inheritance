using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Inheritance.Entities
{
    public class Account : Entity
    {
        protected string accountNumber;
        protected decimal balance;
        protected DateTime created;
        protected decimal creditLimit;
        protected List<Transaction> transactions;

        public List<Transaction> Transactions
        {
            get { return transactions; }
            set { transactions = value; }
        }

        public decimal CreditLimit
        {
            get { return creditLimit; }
            set
            {
                var validateCreditLimit = ValidateCreditLimit(value);

                if (!validateCreditLimit.isValid)
                {
                    throw new ArgumentException(validateCreditLimit.message);
                }

                creditLimit = value;
            }
        }

        /// <summary>
        /// The date the account were created
        /// </summary>
        public DateTime Created
        {
            get { return created; }
            set
            {
                var validateCreatedDate = ValidateCreatedDate(value);

                if (!validateCreatedDate.isValid)
                {
                    throw new ArgumentException(validateCreatedDate.message);
                }

                created = value;
            }
        }

        /// <summary>
        /// The balance of the account
        /// </summary>
        public decimal Balance
        {
            get { return balance; }
            set
            {
                if (value > 99999999999 || value < -99999999999)
                {
                    throw new ArgumentOutOfRangeException("The balance has to be between -999.999.999,99 and 999.999.999,99");
                }

                balance = value;
            }
        }

        public string AccountNumber
        {
            get { return accountNumber; }
            set
            {
                var validateAccountNumber = ValidateAccountNumber(value);

                if (!validateAccountNumber.isValid)
                {
                    throw new ArgumentException(validateAccountNumber.message);
                }

                accountNumber = value;
            }
        }

        public Account(string accountNumber, decimal balance, DateTime created, decimal creditLimit, List<Transaction> transactions)
            : base(default)
        {
            AccountNumber = accountNumber;
            Balance = balance;
            Created = created;
            CreditLimit = creditLimit;
            Transactions = transactions;
        }

        public Account(int id, string accountNumber, decimal balance, DateTime created, decimal creditLimit, List<Transaction> transactions)
            : base(id)
        {
            AccountNumber = accountNumber;
            Balance = balance;
            Created = created;
            CreditLimit = creditLimit;
            Transactions = transactions;
        }

        /// <summary>
        /// Withdraws the given amount from the balance
        /// </summary>
        /// <param name="amount">The amount to withdraw</param>
        public void Withdraw(decimal amount)
        {
            if (amount < 0 || amount > 25_000)
            {
                throw new ArgumentOutOfRangeException("The amount has to be between 0 and 25.000");
            }

            Balance -= amount;
        }

        /// <summary>
        /// Deposits the given amount to the balance
        /// </summary>
        /// <param name="amount">The amount to deposit</param>
        public void Deposit(decimal amount)
        {
            Balance += amount;
        }

        /// <summary>
        /// Returns the amount of days since the account were created
        /// </summary>
        /// <returns>Days since creation</returns>
        public int GetDaysSinceCreation()
        {
            return (DateTime.Now - Created).Days;
        }

        public static (bool isValid, string message) ValidateAccountNumber(string accountNumber)
        {
            if (!new Regex(@"^\d{4} \d{5}\d{0,10}$").IsMatch(accountNumber))
            {
                return (false, "The account number is invalid");
            }

            return (true, string.Empty);
        }

        public static (bool isValid, string message) ValidateCreatedDate(DateTime createdDate)
        {
            if (createdDate > DateTime.Now)
            {
                return (false, "The creation date can't be in the future");
            }

            return (true, string.Empty);
        }

        public static (bool isValid, string message) ValidateCreditLimit(decimal creditLimit)
        {
            if (creditLimit > -1)
            {
                return (false, "The credit limit can't be higher than -1");
            }

            return (true, string.Empty);
        }
    }
}
