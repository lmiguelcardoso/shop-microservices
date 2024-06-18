﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Exceptions
{
    public class InternalServeException : Exception
    {

        public InternalServeException(string message) : base(message)
        {
            
        }
        public InternalServeException(string message, string details) : base(message)
        {
            Details = details;
        }

        public string? Details { get; set; }
    }
}
