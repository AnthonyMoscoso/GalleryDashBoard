using Core.Utilities.Ensures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Core.Model.ValueObjects
{
    public class Email : BaseValueObject
    {
        public string Value { get; private set; }

        public Email(string value) 
        {
            Ensure.That(value, nameof(value)).NotNullOrEmpty();
            Ensure.That(value,nameof(Email)).IsEmail();
            Value = value;
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value; 
        }

        public static explicit operator Email(string value)
        {
            return new Email(value);
        }

        public static implicit operator string(Email email)
        {
            return email.Value;
        }

       
    }
}
