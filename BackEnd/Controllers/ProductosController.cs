using System.Collections.Generic;
using System;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using DA = DataAccess;
using EN = Entities;
using RES = Response;
using BackEnd.Models;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Http;
using DataAccess;

namespace BackEnd.Controllers
{
    [Route("api/productos")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly string sqlConn;
        public ProductosController(IOptions<ConnectionString> optionsConnectionStrings)
        {
            sqlConn = optionsConnectionStrings.Value.conexion;
        }

        [Produces("application/json")]
        [ProducesResponseType(200, Type = typeof(Respuesta))]
        [ProducesResponseType(400)]
        [Route("listar")]
        [HttpGet]
        public IActionResult listar()
        {
            try
            {
                EN.ClassOut.ListarProducto classOut = DA.Productos.Listar(sqlConn);

                List<RES.ListarProducto> list = new List<RES.ListarProducto>();
                foreach (EN.ClassOut.ProductosListar item in classOut.Listar)
                {
                    list.Add(new RES.ListarProducto(
                        item.id,
                        item.companiaVenta,
                        item.almacenVenta,
                        item.tipoMovimiento,
                        item.tipoDocumento,
                        item.nuroDocumento,
                        item.idItem,
                        item.proveedor,
                        item.almacenDestino,
                        item.cantidad
                        ));
                }
                return Ok(new Respuesta("Éxito", "Se obtuvieron los siguientes procudctos.", list));
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [Produces("application/json")]
        [ProducesResponseType(200, Type = typeof(Respuesta))]
        [ProducesResponseType(400)]
        [Route("filtro")]
        [HttpPost]
        public IActionResult filtro(EN.ClassIn.ProductoFiltro req)
        {
            try
            {
                EN.ClassIn.ProductoFiltro classIn = new EN.ClassIn.ProductoFiltro()
                {
                    FechaInicio = req.FechaInicio,
                    FechaFin = req.FechaFin,
                    TipoMovimiento = req.TipoMovimiento,
                    NuroDocumento = req.NuroDocumento
                };
                
               EN.ClassOut.ListarProducto classOut = DA.Productos.Filtro(sqlConn, classIn);
                List<RES.ListarProducto> list = new List<RES.ListarProducto>();
                foreach (EN.ClassOut.ProductosListar item in classOut.Listar)
                {
                    list.Add(new RES.ListarProducto(
                        item.id,
                        item.companiaVenta,
                        item.almacenVenta,
                        item.tipoMovimiento,
                        item.tipoDocumento,
                        item.nuroDocumento,
                        item.idItem,
                        item.proveedor,
                        item.almacenDestino,
                        item.cantidad
                        ));
                }

                return Ok(new Respuesta("Éxito", "Se obtuvieron los siguientes procudctos por filtro.", list));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Produces("application/json")]
        [ProducesResponseType(200, Type = typeof(Respuesta))]
        [ProducesResponseType(400)]
        [Route("insertar")]
        [HttpPost]
        public IActionResult Insertar(EN.ClassIn.ProductoNuevo req)
        {
            try
            {
                EN.ClassIn.ProductoNuevo classIn = new EN.ClassIn.ProductoNuevo()
                {
                    Id = req.Id,
                    CompaVenta = req.CompaVenta,
                    AlmacenVenta = req.AlmacenVenta,
                    TipoMovi = req.TipoMovi,
                    TipoDocu = req.TipoDocu,
                    IdItem = req.IdItem
                };

                int res = DA.Productos.Insertar(sqlConn, classIn);
                if (res != 1)
                {
                    return Ok(new Respuesta("Error", "complete todo los campos", res));
                }

                return Ok(new Respuesta("Éxito", "Se agrego correctamente el producto.", res));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


    }
}
