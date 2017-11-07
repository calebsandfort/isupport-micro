using Microsoft.ServiceFabric.Services.Remoting;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace iSupportMicro.Customer.Domain
{
    public interface ICustomerService : IService
    {
        Task<IEnumerable<Customer>> GetCustomersAsync(CancellationToken cancellationToken);
        Task<bool> CreateCustomerAsync(Customer customer);
    }
}
