﻿using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class CarritoCompra
    {
        public int IdCarrito { get; set; }

        public int IdUsuario { get; set; }

        public double CostoTotal { get; set; }
    }
}
