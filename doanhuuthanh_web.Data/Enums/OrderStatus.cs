using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace doanhuuthanh_web.Data.Enums
{
    public enum OrderStatus
    {
        InProgress, //trong tiến trình
        Confirmed, //Đã xác nhận
        Shipping, // đang vận chuyển
        Success, //Thành công
        Canceled, // đã hủy
    }
}
