using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity.Common
{
    public class PayBand
    {
        public PayBand()
        {
        }

        public int PayBandId { get; set; }
        public string PayBandName { get; set; }
        public decimal ScaleFrom { get; set; }
        public decimal ScaleTo { get; set; }
        public decimal GradePay { get; set; }

    }
}
