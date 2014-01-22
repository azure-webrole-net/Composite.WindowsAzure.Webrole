using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Diagnostics;
using Microsoft.WindowsAzure.ServiceRuntime;
using Microsoft.WindowsAzure.Storage;
using Composite.WindowsAzure.WebRole;
using Composite.WindowsAzure.WebRole.Plugins;
using Composite.WindowsAzure.WebRole.Websites;
using System.Threading.Tasks;

namespace CWAW.Nightly.Role
{

    public class MyWorkerPlugin : IWebrolePlugin
    {


        //The priority tells in what order this plugin should be loaded and order of triggering the periodecheck.
        //All plugins are run asynchronously in groups, and each group is run in order. Groups are split at priority 100,200,300 ... 
        public int Priority
        {
            get { 
                return 1000;// WebrolePluginPriority.low; 
            }
        }

        public void WebsitesPeriodeCheck(object sender, WebsiteSettingsChangedEventArgs args)
        {
            //In this method you can do some basic checks that your plugin is running and if faulted, restart it.
 
        }

        public Task InitializePluginAsync()
        {

            //start up your background task here but do not return the running task. If the returned Task do not stop, the webrole will not start.

            // You could start up an monitor for a Azure Service Bus Queue to do things in the background.
            return Task.FromResult<object>(null);
        }

        public System.Threading.Tasks.Task WebsitesPeriodeCheckAsync(object sender, WebsiteSettingsChangedEventArgs args)
        {
            //We dont need this method.
            return Task.FromResult<object>(null);
        }

        public void Dispose()
        {
           
        }
    }
    public class Role : CompositeWebRole
    {
        public override void Run()
        {
            // This is a sample worker implementation. Replace with your logic.
            Trace.TraceInformation("CWAW.Nightly.Role entry point called", "Information");

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
