using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Deposit
    {
        public int Id { get; set; }
        public int BankId { get; set; }
        public Bank Bank { get; set; }
        public decimal amount { get; set; }
        public DateTime DateOperation { get; set; }

    }
}
