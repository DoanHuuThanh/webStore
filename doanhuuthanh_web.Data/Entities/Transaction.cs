using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using doanhuuthanh_web.Data.Enums;

namespace doanhuuthanh_web.Data.Entities
{
    public class Transaction
    {
        public int Id { get; set; }
        public DateTime TransacionDate { get; set; } // ngày giao dịch
        public string ExternalTransactionId { get; set; }   //id giao dịch bên ngoài
        public decimal Amount { get; set; }
        public decimal Fee { get; set; }
        public string Result { get; set;  }
        public string Message { get; set; } 
        public TransactionStatus Status { get; set; }
        public string Provider { get; set; } // nhà cung cấp

        public Guid UserId { get; set; }    
        public AppUser AppUsers { get; set; }    

    }
}
