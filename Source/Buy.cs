﻿using System.Runtime.InteropServices;

namespace SkyEdge
{
    [ComVisible(true)]
    public class Buy
    {
        public int id { get; set; }

        public string name { get; set; }

        public decimal price { get; set; }
    }
}