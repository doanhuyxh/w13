using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace PTEcommerce.Services.AutoCalculateResult
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            //ServiceAutoCalculateResult a = new ServiceAutoCalculateResult();
            //a.WorkProcess(null, null);
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new ServiceAutoCalculateResult()
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
