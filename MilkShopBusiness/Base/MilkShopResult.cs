using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkShopBusiness.Base
{
    public class MilkShopResult : IMilkShopResult
    {
        public int Status { get; set; }
        public string? Message { get; set; }
        public object? Data { get; set; }

        public MilkShopResult()
        {
            Status = -1;
            Message = "Action fail";
        }

        public MilkShopResult(int status, string message)
        {
            Status = status;
            Message = message;
        }

        public MilkShopResult(int status, string message, object data)
        {
            Status = status;
            Message = message;
            Data = data;
        }
    }
}
