using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoothItems
{
    public class MissingItemException : Exception
    {
        public MissingItemException(string message)
        : base(message)
        {
        
        }
    }
}
