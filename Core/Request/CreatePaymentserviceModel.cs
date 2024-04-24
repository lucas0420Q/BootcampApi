using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Request;
public class CreatePaymentserviceModel
{
    public string DocumentNumber { get; set; }
    public decimal Amount { get; set; }
    public int ServiceId { get; set; }
    public int AccountId { get; set; }
    public DateTime PaymentServiceDateTime { get; set; }

}
