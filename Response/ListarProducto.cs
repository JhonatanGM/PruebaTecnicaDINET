using System;

namespace Response
{
    public class ListarProducto
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

        public ListarProducto(string id, string companiaVenta, string almacenVenta, string tipoMovimiento, string tipoDocumento, string nuroDocumento, string idItem, string proveedor, string almacenDestino, string cantidad)
        {
            this.id = id;
            this.companiaVenta = companiaVenta;
            this.almacenVenta = almacenVenta;
            this.tipoMovimiento = tipoMovimiento;
            this.tipoDocumento = tipoDocumento;
            this.nuroDocumento = nuroDocumento;
            this.idItem = idItem;
            this.proveedor = proveedor;
            this.almacenDestino = almacenDestino;
            this.cantidad = cantidad;
        }
    }
}
