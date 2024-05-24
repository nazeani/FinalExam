using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Exceptions
{
    public class ImageLengthExceptions : Exception
    {
        public ImageLengthExceptions(string? message) : base(message)
        {
        }
    }
}
