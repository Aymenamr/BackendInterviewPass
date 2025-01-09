using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewPass.WebApi.Exceptions
{
    public class Duplicate : Exception
    {
        public Duplicate():base("Already Exists!")
        {
        }
    }
}
