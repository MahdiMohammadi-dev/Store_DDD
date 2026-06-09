using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.Entity.Products
{
    public sealed class ProductException : Exception
    {
        public ProductException(string message) : base(message)
        {
        }
    }
}
