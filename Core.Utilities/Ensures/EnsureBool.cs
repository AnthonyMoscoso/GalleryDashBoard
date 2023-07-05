using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Ensures
{
    public class EnsureBool
    {
        private readonly bool _argument;
        private readonly string _argumentName;
        public EnsureBool(bool argument, string argumentName = "")
        {
            _argument = argument;
            _argumentName = string.IsNullOrWhiteSpace(argumentName) ? nameof(_argument) : argumentName;
        }
        public void IsTrue(Exception ex)
        {
            if (!_argument)
            {
                throw ex!;
            }
        }
        public void IsFalse( Exception ex)
        {
            if (_argument)
            {
                throw ex!;
            }
        }
    }
}
