﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    [Serializable]
   public class Payment
    {
        public decimal AmounToPay { get; set; }
        public string CardNumber { get; set; }
        public string Name { get; set; }
    }
}
