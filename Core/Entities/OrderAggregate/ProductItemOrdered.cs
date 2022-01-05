using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities.OrderAggregate
{
    public class ProductItemOrdered // Value Entity
    {
        public ProductItemOrdered() // Required becuase we are dealing with Identity Framework.
        {
        }

        public ProductItemOrdered(int productItemId, string productName, string pictureUrl) // Contructor allows you to populate fields when you initialise a new instance of the ProductItemOrdered.
        {
            ProductItemId = productItemId;
            ProductName = productName;
            PictureUrl = pictureUrl;
        }

        // Value Entity - No Id Required - Because it will be owned by the order itself.
        public int ProductItemId { get; set; }
        public string ProductName { get; set; }
        public string PictureUrl { get; set; }
    }
}