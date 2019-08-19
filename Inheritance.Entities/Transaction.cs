using System;
using System.Collections.Generic;
using System.Text;

namespace Inheritance.Entities
{
    public class Transaction : Entity
    {
        protected string sender;
        protected string receiver;
        protected decimal amount;
        protected DateTime timestamp;

        public DateTime Timestamp
        {
            get { return timestamp; }
            set
            {
                var validateTimestamp = ValidateTimestamp(value);

                if (!validateTimestamp.isValid)
                {
                    throw new ArgumentException(validateTimestamp.message);
                }

                timestamp = value;
            }
        }

        public decimal Amount
        {
            get { return amount; }
            set
            {
                var validateAmount = ValidateAmount(value);

                if (!validateAmount.isValid)
                {
                    throw new ArgumentException(validateAmount.message);
                }

                amount = value;
            }
        }

        public string Receiver
        {
            get { return receiver; }
            set { receiver = value; }
        }

        public string Sender
        {
            get { return sender; }
            set { sender = value; }
        }


        public Transaction(string sender, string receiver, decimal amount, DateTime timestamp)
            : this(default, sender, receiver, amount, timestamp) { }

        public Transaction(int id, string sender, string receiver, decimal amount, DateTime timestamp)
            : base(id)
        {
            Sender = sender;
            Receiver = receiver;
            Amount = amount;
            Timestamp = timestamp;
        }

        public static (bool isValid, string message) ValidateTimestamp(DateTime timestamp)
        {
            if (timestamp > DateTime.Now)
            {
                return (false, "The timestamp can't be in the future");
            }

            return (true, string.Empty);
        }

        public static (bool isValid, string message) ValidateAmount(decimal amount)
        {
            if (amount < 1)
            {
                return (false, "The amount can't be less than 1");
            }

            return (true, string.Empty);
        }
    }
}
