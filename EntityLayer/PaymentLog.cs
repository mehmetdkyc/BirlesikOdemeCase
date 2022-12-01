using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer
{
    public class PaymentLog
    {
        [Key]
        public string orderId { get; set; }
        public string rnd { get; set; }
        public string hostReference { get; set; }
        public string authCode { get; set; }
        public string totalAmount { get; set; }
        public string responseHash { get; set; }
        public string responseCode { get; set; }
        public string responseMessage { get; set; }
        public string customerId { get; set; }
        public string extraData { get; set; }
        public string installmentCount { get; set; }
        public string cardNumber { get; set; }
        public string saleDate { get; set; }
        public string vPosName { get; set; }
        public string paymentSystem { get; set; }
    }
}
