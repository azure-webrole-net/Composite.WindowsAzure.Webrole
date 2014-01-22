using Composite.WindowsAzure.WebRole.Plugins;
using Composite.WindowsAzure.WebRole.Websites;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using Microsoft.WindowsAzure;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Webrole
{
    public class MyWorkerPlugin : IWebRolePlugin
    {

        private const string TopicName = "SInnovations";
        private const string AllMessages = "AllMessages";

        private ManualResetEvent CompletedEvent = new ManualResetEvent(false);
        private SubscriptionClient Client;
        private Task Runner;

        public int Priority
        {
            get
            {
                return WebrolePluginPriority.LOW;
            }
        }

        public void WebsitesPeriodeCheck(object sender, WebsiteSettingsChangedEventArgs args)
        {
            if (Runner.Status == TaskStatus.Faulted)
            {
                Client.Close();
                CompletedEvent.Set();
                CompletedEvent = new ManualResetEvent(false);
                Runner = Task.Factory.StartNew(StartSubscriptionClient, TaskCreationOptions.LongRunning);
            }

        }

        public Task InitializePluginAsync()
        {
            Runner = Task.Factory.StartNew(StartSubscriptionClient, TaskCreationOptions.LongRunning);


            return Task.FromResult<object>(null);

        }

        private void StartSubscriptionClient()
        {
            string connectionString = CloudConfigurationManager.GetSetting("SInnovations.Servicebus.ConnectionString");

            var namespaceManager =
                NamespaceManager.CreateFromConnectionString(connectionString);

            TopicDescription td = new TopicDescription(TopicName);
            td.MaxSizeInMegabytes = 512;
            td.DefaultMessageTimeToLive = new TimeSpan(0, 5, 0);

            if (!namespaceManager.TopicExists(TopicName))
            {
                namespaceManager.CreateTopic(td);
            }
            if (!namespaceManager.SubscriptionExists(TopicName, AllMessages))
            {
                namespaceManager.CreateSubscription(TopicName, AllMessages);
            }


            Client = SubscriptionClient.CreateFromConnectionString
                (connectionString, TopicName, AllMessages);
            var options = new OnMessageOptions { AutoComplete = false, MaxConcurrentCalls = 10 };
            options.ExceptionReceived += options_ExceptionReceived;

            Client.OnMessageAsync(OnMessageAsync, options);
            CompletedEvent.WaitOne();
        }

        void options_ExceptionReceived(object sender, ExceptionReceivedEventArgs e)
        {
            Trace.TraceError(e.Exception.ToString());
        }


        private async Task OnMessageAsync(BrokeredMessage message)
        {

            Trace.TraceInformation(message.MessageId);
            Trace.TraceInformation(message.GetBody<string>());
            await message.CompleteAsync();
        }
        public Task WebsitesPeriodeCheckAsync(object sender, WebsiteSettingsChangedEventArgs args)
        {


            //We dont need this method.
            return Task.FromResult<object>(null);
        }

        public void Dispose()
        {
            Client.Close();
            CompletedEvent.Set();
        }
    }
}
