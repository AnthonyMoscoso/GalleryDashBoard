using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Core.Utilities.Ensures
{
    public class EnsureString
    {
        private readonly string? _value;
        private readonly string _argumentName;
        public EnsureString(string? value,string argumentName ="") 
        { 
            _value= value;
            _argumentName= string.IsNullOrWhiteSpace( argumentName)? nameof(value) : argumentName;
        }

        /// <summary>
        /// Ensure that argument is equals another value ignoring upper case and lower case 
        /// </summary>
        /// <param name="equalsValue">Argument to compare</param>
        /// <param name="mesage">error message</param>
        /// <exception cref="ArgumentException">Thrower if values not are equals </exception>
        public void EqualsTo(string equalsValue,string mesage = "")
        {

            if (_value!= null && !_value.Trim().ToLower().Equals(equalsValue.Trim().ToLower()))
            {
                mesage = string.IsNullOrWhiteSpace(_value) ? _argumentName : mesage;
                throw new ArgumentException(mesage);
            }
        }
        /// <summary>
        /// Esure thjat string not null or empty
        /// </summary>
        /// <param name="mesage">error message</param>
        /// <exception cref="ArgumentException">thrower is string value is null or empty</exception>
        public void NotNullOrEmpty(string mesage= "")
        {
            if (string.IsNullOrWhiteSpace(_value))
            {
                mesage = string.IsNullOrWhiteSpace(_value) ? $"Argument not valid {_argumentName} value is null or empty" : mesage ;
                throw new ArgumentNullException(mesage);
            }
        }
        /// <summary>
        /// Ensure that argument is on enum
        /// </summary>
        /// <param name="enumType">Type of enum</param>
        /// <param name="mesage">error message</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrower is argument value not is on enum</exception>
        public  void IsEnum(Type enumType, string mesage = "")
        {
            
            if (!Enum.IsDefined(enumType, _value!))
            {
                mesage = string.IsNullOrWhiteSpace(mesage) ? _argumentName : mesage;
                throw new ArgumentOutOfRangeException(nameof(mesage));
            }
        }
        /// <summary>
        /// Ensure that argument is a email
        /// </summary>
        /// <exception cref="FormatException">Thrower is email not valid</exception>
        public void IsEmail()
        {
            try
            {
                MailAddress emailAddress = new(_value!);
            }
            catch
            {
                throw new FormatException("Email format not valid");
            }
        }

        public void IsHexadecimalColor()
        {
            if (string.IsNullOrWhiteSpace(_value))
            {
                throw new FormatException("hexadecimal format not valid");
            }
            // Expresión regular para verificar el formato de código hexadecimal válido
            var regex = new Regex(@"^#([A-Fa-f0-9]{6}|[A-Fa-f0-9]{3})$");

            // Verificar si el color cumple con el formato de código hexadecimal
            if (!regex.IsMatch(_value))
            {
                throw new FormatException("hexadecimal format not valid");
            }

            return ; // No es un código hexadecimal de color válido
        }
    }
}
