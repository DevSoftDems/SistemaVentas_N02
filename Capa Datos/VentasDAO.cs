﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa_Entidad;
using System.Data;
using Newtonsoft.Json;

namespace Capa_Datos
{
    public class VentasDAO
    {
        public List<Cliente> ListarClientes()
        {
            var lista = new List<Cliente>();
            DataTable dt = DBHelper.RetornaDataTable("PA_CLIENTES");
            string cad_json = JsonConvert.SerializeObject(dt);
            lista = JsonConvert.DeserializeObject<List<Cliente>>(cad_json);
            return lista;
        }

        public List<Productos> ListarProductos(string nombre)
        {
            var lista = new List<Productos>();
            DataTable dt = DBHelper.RetornaDataTable("PA_PRODUCTOS", nombre);
            string cad_json = JsonConvert.SerializeObject(dt);
            lista = JsonConvert.DeserializeObject<List<Productos>>(cad_json);
            return lista;
        }

        public string GrabarVenta(string codigo, decimal total, List<Carrito> listaCarrito)
        {
            try
            {
                // Grabar cabecera de la venta PA_GRABAR_VENTAS_CAB
                string num_vta = DBHelper.RetornaValor("PA_GRABAR_VENTAS_CAB", codigo, total).ToString();

                // Grabar detalle de la venta PA_GRABAR_DETALLE_VENTA
                foreach (Carrito item in listaCarrito)
                {
                    // ------- Modificacion para que se grabe el stock en vez de la Talla
                    DBHelper.EjecutarSP("PA_GRABAR_DETALLE_VENTA", num_vta, item.idProd, item.stock, item.precio, item.nomProd);
                }
                // Retornar el número de venta generado para usarlo posteriormente
                return num_vta;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }


        }

        // Obtener información de la cabecera de una venta
        public VentaCabecera ObtenerVentaCabecera(string numVenta)
        {
            DataTable dt = DBHelper.RetornaDataTable("PA_OBTENER_VENTA_CAB", numVenta);
            string cad_json = JsonConvert.SerializeObject(dt);
            var lista = JsonConvert.DeserializeObject<List<VentaCabecera>>(cad_json);
            return lista.FirstOrDefault();
        }

        // Obtener el detalle de productos de una venta
        public List<Carrito> ObtenerVentaDetalle(string numVenta)
        {
            DataTable dt = DBHelper.RetornaDataTable("PA_OBTENER_DETALLE_VENTA", numVenta);
            string cad_json = JsonConvert.SerializeObject(dt);
            return JsonConvert.DeserializeObject<List<Carrito>>(cad_json);
        }

    }
}
