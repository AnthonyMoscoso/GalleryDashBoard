﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Core.Utilities.Validations
{
    public class ValidateString
    {
        private string _value;  
        public ValidateString(string value) 
        {
            _value = value;
        }

        public bool IsPhoneNumber()
        {
            string patron = @"^(\+\d{1,3}\s?)?\d{9,}$";
            return Regex.IsMatch(_value, patron);
        }

        public bool IsEmail()
        {
            try
            {
                MailAddress emailAddress = new(_value!);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public  bool IsHexadecimalColor()
        {

            // Expresión regular para verificar el formato de código hexadecimal válido
            var regex = new Regex(@"^#([A-Fa-f0-9]{6}|[A-Fa-f0-9]{3})$");

            return string.IsNullOrWhiteSpace(_value) && regex.IsMatch(_value); 
        }
    }
}
