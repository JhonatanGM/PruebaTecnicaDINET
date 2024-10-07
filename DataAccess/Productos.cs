using System.Data;
using Dapper;
using System.Transactions;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using EN = Entities;
using System;

namespace DataAccess
{
    public class Productos
    {
        public static EN.ClassOut.ListarProducto Listar(string sqlConn)
        {

            using (var con = new SqlConnection(Config.DecryptConnection(sqlConn)))
            {
                try
                {
                    var param = new DynamicParameters();
                    var result = con.QueryMultiple("vw_consultaProductos", param, commandType: CommandType.StoredProcedure);

                    EN.ClassOut.ListarProducto classOut = new EN.ClassOut.ListarProducto();
                    classOut.Listar = result.Read<EN.ClassOut.ProductosListar>().ToList();
                    return classOut;

                }
                catch
                {
                    throw;
                }
            }
        }

        public static EN.ClassOut.ProductosFiltro Filtro(string sqlConn, EN.ClassIn.ProductoFiltro classIn)
        {

            using (var con = new SqlConnection(Config.DecryptConnection(sqlConn)))
            {
                try
                {
                    var param = new DynamicParameters();
                    param.Add("@IniFecha", classIn.FechaInicio);
                    param.Add("@FinFecha", classIn.FechaFin);
                    param.Add("@TipoMovimiento", classIn.TipoMovimiento);
                    param.Add("@NuroDocumento", classIn.NuroDocumento);
                    var result = con.QueryMultiple("consultaProductosFiltro", param, commandType: CommandType.StoredProcedure);

                    EN.ClassOut.ProductosFiltro classOut = new EN.ClassOut.ProductosFiltro();
                    classOut.Listar = result.Read<EN.ClassOut.ProductosListar>().ToList();
                    return classOut;

                }
                catch
                {
                    throw;
                }
            }
        }

        public static int Insertar(string sqlConn, EN.ClassIn.ProductoNuevo classIn)
        {
            using (TransactionScope scope = new TransactionScope())
            using (var con = new SqlConnection(Config.DecryptConnection(sqlConn)))
            {
                try
                {
                    int idRetorno = 0;

                    var param = new DynamicParameters();
                    param.Add("@Id", classIn.Id);
                    param.Add("@CompaVenta", classIn.CompaVenta);
                    param.Add("@AlmacenVenta", classIn.AlmacenVenta);
                    param.Add("@TipoMovi", classIn.TipoMovi);
                    param.Add("@TipoDocu", classIn.TipoDocu);
                    param.Add("@IdItem", classIn.IdItem);
                    param.Add("@ReturnVal", dbType: DbType.Int32, direction: ParameterDirection.Output);

                    con.Execute("nuevosPoductos", param, commandType: CommandType.StoredProcedure);

                    idRetorno = param.Get<Int32>("@ReturnVal");
                    scope.Complete();

                    return idRetorno;

                }
                catch
                {
                    throw;
                }
            }
        }

    }
}
