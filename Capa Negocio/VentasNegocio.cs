using Capa_Datos;
using Capa_Entidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Negocio
{
    public class VentasNegocio
    {
        VentasDAO dao = new VentasDAO();

        public List<Cliente> ListarClientes()
        {
            return dao.ListarClientes();
        }

        public List<Productos> ListarProductos(string nombre)
        {
            return dao.ListarProductos(nombre);
        }

        public string GrabarVenta(string codigo, decimal total, List<Carrito> listaCarrito)
        {
            try
            {
                // El DAO retorna el número de la venta registrada
                return dao.GrabarVenta(codigo, total, listaCarrito);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }

        public VentaCabecera ObtenerVentaCabecera(string numVenta)
        {
            return dao.ObtenerVentaCabecera(numVenta);
        }

        public List<Carrito> ObtenerVentaDetalle(string numVenta)
        {
            return dao.ObtenerVentaDetalle(numVenta);
        }
    }
}
