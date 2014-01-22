using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Diagnostics;
using Microsoft.WindowsAzure.ServiceRuntime;
using Composite.WindowsAzure.WebRole;
using System.Diagnostics;

namespace Composite.WindowsAzure.Webrole.SimpleBoot
{
    public class WebRole : CompositeWebRole
    {

        /// <summary>
        /// Example of using a trace file for debugging purpose when remote accessing.
        /// </summary>
        static WebRole()
        {
            
            //var myTraceLog = new System.IO.FileStream("c:\\tracelog.txt",
            //System.IO.FileMode.Create);
            //var myListener = new TextWriterTraceListener(myTraceLog);
            //Trace.Listeners.Add(myListener);
            //Trace.TraceInformation("Hello");
            //Trace.Flush();
        }

        public override bool OnStart()
        {

            // For information on handling configuration changes
            // see the MSDN topic at http://go.microsoft.com/fwlink/?LinkId=166357.

            return base.OnStart();
        }
    }
}
