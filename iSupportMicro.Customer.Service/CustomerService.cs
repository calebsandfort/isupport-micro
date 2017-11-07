using System;
using System.Collections.Generic;
using System.Fabric;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Data.Collections;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;
using iSupportMicro.Domain;
using Microsoft.ServiceFabric.Data;

namespace iSupportMicro.Customer.Service
{
    using iSupportMicro.Customer.Domain;
    using Microsoft.ServiceFabric.Services.Remoting.Runtime;

    /// <summary>
    /// An instance of this class is created for each service replica by the Service Fabric runtime.
    /// </summary>
    internal sealed class CustomerService : StatefulService, ICustomerService
    {
        internal const string CustomerServiceType = "iSupportMicro.Customer.Service";
        private const string CustomerItemDictionaryName = "customers";

        public CustomerService(StatefulServiceContext context)
            : base(context)
        { }

        /// <summary>
        /// Optional override to create listeners (e.g., HTTP, Service Remoting, WCF, etc.) for this service replica to handle client or user requests.
        /// </summary>
        /// <remarks>
        /// For more information on service communication, see https://aka.ms/servicefabricservicecommunication
        /// </remarks>
        /// <returns>A collection of listeners.</returns>
        protected override IEnumerable<ServiceReplicaListener> CreateServiceReplicaListeners()
        {
            return new[] { new ServiceReplicaListener(context => this.CreateServiceRemotingListener(context)) };
        }


        /// <summary>
        /// Used internally to generate inventory items and adds them to the ReliableDict we have.
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        public async Task<bool> CreateCustomerAsync(Customer customer)
        {
            IReliableDictionary<ItemId, Customer> customers =
                await this.StateManager.GetOrAddAsync<IReliableDictionary<ItemId, Customer>>(CustomerItemDictionaryName);

            using (ITransaction tx = this.StateManager.CreateTransaction())
            {
                await customers.AddAsync(tx, customer.Id, customer);
                await tx.CommitAsync();
                //ServiceEventSource.Current.ServiceMessage(this, "Created customer: {0}", customer);
            }

            return true;
        }

        /// <summary>
        /// Retrieves a customer-specific view (defined in the InventoryItemView class in the Fabrikam Common namespace)
        /// of all items in the IReliableDictionary in InventoryService. Only items with a CustomerAvailableStock greater than
        /// zero are returned as a business logic constraint to reduce overordering. 
        /// </summary>
        /// <returns>IEnumerable of InventoryItemView</returns>
        public async Task<IEnumerable<Customer>> GetCustomersAsync(CancellationToken ct)
        {
            IReliableDictionary<ItemId, Customer> customers =
                await this.StateManager.GetOrAddAsync<IReliableDictionary<ItemId, Customer>>(CustomerItemDictionaryName);

            ServiceEventSource.Current.Message("Called GetCustomers to return CustomerEntity");

            //await this.PrintInventoryItemsAsync(inventoryItems, ct);

            IList<Customer> results = new List<Customer>();

            using (ITransaction tx = this.StateManager.CreateTransaction())
            {
                //erviceEventSource.Current.Message("Generating item views for {0} items", await inventoryItems.GetCountAsync(tx));

                IAsyncEnumerator<KeyValuePair<ItemId, Customer>> enumerator =
                    (await customers.CreateEnumerableAsync(tx)).GetAsyncEnumerator();

                while (await enumerator.MoveNextAsync(ct))
                {
                    results.Add(enumerator.Current.Value);
                }
            }

            //if (results.Count == 0)
            //{
            //    results = await Populate();
            //}

            return results;
        }

        private async Task<IList<Customer>> Populate()
        {
            IList<Customer> customers = new List<Customer>();
            customers.Add(new Customer("Adam", "Nelson", "anelson@isupport.com"));
            customers.Add(new Customer("Caleb", "Sandfort", "csandfort@isupport.com"));
            customers.Add(new Customer("Dan", "Green", "dgreen@isupport.com"));

            await Task.WhenAll(customers.Select(x => CreateCustomerAsync(x)));

            return customers;
        }
    }
}
