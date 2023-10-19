using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using doanhuuthanh_web.Data.Enums;

namespace doanhuuthanh_web.Data.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public Guid UserId { get; set; } // sử dụng để tạo Id duy nhất mỗi người dùng có 1 id,
        public string ShipName { get; set; }
        public string ShipAddress { get; set; }
        public string ShipEmail { get; set; }
        public string ShipPhoneNumber { get; set; }
        public OrderStatus Status{ get; set; }

        public List<OrderDetails> OrderDetail { get; set; }

        public AppUser AppUsers { get; set; }
    }
}
