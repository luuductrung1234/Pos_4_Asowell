using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Entities.CustomEntities
{
    public enum PaymentMethod
    {
        All = -1,
        Cash,               // trả tiền mặt
        Cheque,             // trả sec
        Deferred,           // trả sau
        International,      // thanh toán quốc tế (VISA, MasterCard)
        Credit,             // tín dụng nói chung
        OnAcount
    }
}
