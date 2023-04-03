﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.OrderModels.Dtos
{
    public class OrderCreateDto
    {
        public int Qty { get; set; }

        public string OrderedBy { get; set; }

        public int ProductCode { get; set; }
    }
}
