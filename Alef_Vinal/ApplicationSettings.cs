using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alef_Vinal
{

    public class ApplicationSettings
    {
        public ConnectionStrings EFCoreDb { get; set; }
    }


    public class ConnectionStrings
    {
        public string MainDb { get; set; }
    }
}
