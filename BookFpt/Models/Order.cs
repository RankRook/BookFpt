using BookFpt.Models;
using BookFpt.Areas.Identity.Data;
using System;
using System.Collections.Generic;

namespace BookFpt.Models
{
    public class Order
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int Id { get; set; }
        public string UserId { get; set; }
        public DateTime Date { get; set; }
        public string Address { get; set; }
        public string FullName { get; set; }
        public string PaymentOption { get; set; }
        public string DeliveryOption { get; set; }
        public int Status { get; set; }
        public string Note { get; set; }

        public virtual SampleAppUser UserNavigation { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
