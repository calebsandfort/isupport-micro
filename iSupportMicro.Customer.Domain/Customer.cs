using iSupportMicro.Domain;
using System;

namespace iSupportMicro.Customer.Domain
{
    [Serializable]
    public class Customer
    {
        public Customer(String firstName, String lastName, String email, ItemId id = null)
        {
            this.Id = id ?? new ItemId();
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Email = email;
        }

        public ItemId Id { get; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Email { get; set; }

        public override string ToString()
        {
            return $"{this.FirstName} {this.LastName} ({this.Email})";
        }
    }
}
