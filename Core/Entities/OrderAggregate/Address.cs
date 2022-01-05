using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities.OrderAggregate
{
    public class Address // Value Entity - Doesnt need an Id as it will be owned by order.
    {
        public Address()
        {
        }

        public Address(string firstName, string lastName, string street, string city, string state, string zipCode) // Contructor allows you to populate fields when you initialise a new instance of the Address.
        {
            FirstName = firstName;
            LastName = lastName;
            Street = street;
            City = city;
            State = state;
            ZipCode = zipCode;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }

    }
}