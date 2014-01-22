using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Diagnostics;
using Microsoft.WindowsAzure.ServiceRuntime;
using Composite.WindowsAzure.WebRole;
using System.Diagnostics;
using Webrole;

namespace WebRole
{
    public class WebRole : CompositeWebRole
    {
        public override void Run()
        {
            // This is a sample worker implementation. Replace with your logic.
            Trace.TraceInformation("Webrole entry point called", "Information");

            base.Run();
        }

        public override bool OnStart()
        {
            CompositeWebRole.DependencyResolver.RegisterPlugin<MyWorkerPlugin>();

            // For information on handling configuration changes
            // see the MSDN topic at http://go.microsoft.com/fwlink/?LinkId=166357.

            return base.OnStart();
        }
    }
}
