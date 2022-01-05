using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Core.Entities.OrderAggregate
{
    public enum OrderStatus // An enum is a special "class" that represents a group of constants (unchangeable/read-only variables). 
    {
        // Flags to say what State the order is at.
        
        [EnumMember(Value = "Pending")]
        Pending, // 0
         [EnumMember(Value = "Payment Received")]
        PaymentReceived, // 1
         [EnumMember(Value = "Payment Failed")]
        PaymentFailed, // 2

        // Real Application will have more: Shipped / Completed.
    }
}