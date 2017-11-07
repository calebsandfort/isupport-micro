using iSupportMicro.Common;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Fabric;
using System.Fabric.Query;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Services.Remoting.Client;
using Microsoft.ServiceFabric.Services.Client;
using System.Threading;

namespace iSupportMicroWeb.Controllers
{
    using global::iSupportMicroWeb.Models;
    using iSupportMicro.Customer.Domain;

    public class CustomerController : Controller
    {
        public const string CustomerServiceName = "iSupportMicro.Customer.Service";
        private static FabricClient fc = new FabricClient();

        [HttpPost]
        [Route("api/customer/add")]
        public Task<bool> CreateCustomer([FromBody] Customer customer)
        {
            Customer c = new Customer(customer.FirstName, customer.LastName, customer.Email);

            ServiceUriBuilder builder = new ServiceUriBuilder(CustomerServiceName);
            ICustomerService customerServiceClient = ServiceProxy.Create<ICustomerService>(builder.ToUri(), c.Id.GetPartitionKey());

            try
            {
                return customerServiceClient.CreateCustomerAsync(c);
            }
            catch (Exception ex)
            {
                ServiceEventSource.Current.Message("Web Service: Exception creating {0}: {1}", c, ex);
                throw;
            }
        }

        /// <summary>
        /// Right now, this method makes an API call via a ServiceProxy to retrieve Inventory Data directly
        /// from InventoryService. In the future, this call will be made with a specified category parameter, 
        /// and based on this could call a specific materialized view to return. There would be no option 
        /// to return the entire inventory service in one call, as this would be slow and expensive at scale.  
        /// </summary>
        /// <returns>Task of type IEnumerable of InventoryItemView objects</returns>
        [HttpGet]
        [Route("api/customer")]
        public async Task<object> GetCustomers([FromQuery]GridOptions options)
        {
            try
            {
                ServiceUriBuilder builder = new ServiceUriBuilder(CustomerServiceName);
                Uri serviceName = builder.ToUri();

                List<Customer> itemList = new List<Customer>();

                ServicePartitionList partitions = await fc.QueryManager.GetPartitionListAsync(serviceName);

                foreach (Partition p in partitions)
                {
                    long minKey = (p.PartitionInformation as Int64RangePartitionInformation).LowKey;
                    ICustomerService customerServiceClient = ServiceProxy.Create<ICustomerService>(serviceName, new ServicePartitionKey(minKey));

                    IEnumerable<Customer> result = await customerServiceClient.GetCustomersAsync(CancellationToken.None);
                    if (result != null)
                    {
                        itemList.AddRange(result);
                    }
                }

                //itemList.Sort();

                return new { data = itemList, total = itemList.Count } ;
            }
            catch(Exception ex)
            {
                ServiceEventSource.Current.Message($"Exception: {ex.Message}");
                return null;
            }
        }
    }
}
