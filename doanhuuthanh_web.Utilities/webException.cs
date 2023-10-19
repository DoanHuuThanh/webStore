using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace doanhuuthanh_web.Utilities
{
    public class webException : Exception 
    {
        public webException() { 
        
        }

        public webException(string message) : base(message) { }

        public webException(string message, Exception inner) : base(message,inner) { }

    } 
}
