using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alef_Vinal
{

    public class ApplicationSettings
    {
        public ConnectionStringsSettings ConnectionStrings { get; set; }
    }


    public class ConnectionStringsSettings
    {
        public string MainDb { get; set; }
    }
}
