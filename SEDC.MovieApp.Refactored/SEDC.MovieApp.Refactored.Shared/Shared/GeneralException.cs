using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEDC.MovieApp.Refactored.Shared.Shared
{
    public class GeneralException : Exception
    {
        public GeneralException(string message) : base(message) { }
    }
}
