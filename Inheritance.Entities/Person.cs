using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Inheritance.Entities
{
    public abstract class Person : Entity
    {
        protected string firstName;
        protected string lastName;
        protected string ssn;

        public string Ssn
        {
            get { return ssn; }
            set
            {
                var validateSsn = ValidateSsn(value);

                if (!validateSsn.isValid)
                {
                    throw new ArgumentException(validateSsn.message);
                }

                ssn = value;
            }
        }

        public string LastName
        {
            get { return lastName; }
            set
            {
                var validateName = ValidateName(value);

                if (!validateName.isValid)
                {
                    throw new ArgumentException(validateName.message);
                }

                lastName = value;
            }
        }

        public string FirstName
        {
            get { return firstName; }
            set
            {
                var validateName = ValidateName(value);

                if (!validateName.isValid)
                {
                    throw new ArgumentException(validateName.message);
                }

                firstName = value;
            }
        }

        public Person(string firstName, string lastName, string ssn)
            : this(default, firstName, lastName, ssn) { }

        public Person(int id, string firstName, string lastName, string ssn)
            : base(id)
        {
            FirstName = firstName;
            LastName = lastName;
            Ssn = ssn;
        }

        public static (bool isValid, string message) ValidateName(string name)
        {
            if (name is null)
            {
                return (false, "The name can't be null");
            }

            if (name == string.Empty)
            {
                return (false, "The name can't be empty");
            }

            if (name.Any(char.IsDigit))
            {
                return (false, "The name can't contain any numbers");
            }

            return (true, string.Empty);
        }

        public static (bool isValid, string message) ValidateSsn(string ssn)
        {
            if (!new Regex(@"^\d{3}-\d{2}-\d{4}$").IsMatch(ssn))
            {
                return (false, "The SSN doesn't math with the syntax");
            }

            return (true, string.Empty);
        }
    }
}
