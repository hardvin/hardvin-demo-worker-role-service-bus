using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using System;

namespace hardvin_demo_web_worker_role_service_bus.Servicios
{
    public class QueueConnector
    {
        // Thread-safe. Recommended that you cache rather than recreating it
        // on every request.
        public QueueClient ClienteCola;

        // Obtain these values from the portal.
        public const string Namespace = "demohardvin";

        // The name of your queue.
        public const string QueueName = "ProcessingQueue";

        public NamespaceManager CreateNamespaceManager()
        {
            // Create the namespace manager which gives you access to
            // management operations.
            var uri = ServiceBusEnvironment.CreateServiceUri(
                "sb", Namespace, String.Empty);
            var tP = TokenProvider.CreateSharedAccessSignatureTokenProvider(
                "RootManageSharedAccessKey", "2W832UjJPNvxBaYHc6aEx3ebVJt9lFAU32zRZjECNQ0=");
            return new NamespaceManager(uri, tP);
        }

        public void Initialize()
        {
            // Using Http to be friendly with outbound firewalls.
            ServiceBusEnvironment.SystemConnectivity.Mode =
                ConnectivityMode.Http;

            // Create the namespace manager which gives you access to
            // management operations.
            var namespaceManager = CreateNamespaceManager();

            // Create the queue if it does not exist already.
            if (!namespaceManager.QueueExists(QueueName))
            {
                namespaceManager.CreateQueue(QueueName);
            }

            // Get a client to the queue.
            var messagingFactory = MessagingFactory.Create(
                namespaceManager.Address,
                namespaceManager.Settings.TokenProvider);
            ClienteCola = messagingFactory.CreateQueueClient(
                "ProcessingQueue");
        }
    }
}