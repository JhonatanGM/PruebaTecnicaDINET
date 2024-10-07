using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.ClassOut
{
    public class ListarProducto
    {
        public List<ProductosListar> Listar { get; set; }
    }
    public class ProductosListar
    {
            public string id { get; set; }
            public string companiaVenta { get; set; }
            public string almacenVenta { get; set; }
            public string tipoMovimiento { get; set; }
            public string tipoDocumento { get; set; }
            public string nuroDocumento { get; set; }
            public string idItem { get; set; }
            public string proveedor { get; set; }
            public string almacenDestino { get; set; }
            public string cantidad { get; set; }
        
    }
}
