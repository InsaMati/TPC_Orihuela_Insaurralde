﻿using System;
using Dominio;
using Negocio;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class NegocioPedidos
    {
        public List<Pedido> ListarPedidos()
        {
            AccesoADatos Datos = new AccesoADatos();
            List<Pedido> Lista = new List<Pedido>();

            try
            {
                Datos.SetearQuery("select * from VW_ListarAdmPedidos");
                Datos.EjecutarLector();

                while (Datos.Leeme.Read())
                {
                    Pedido AuxPedido = new Pedido();

                    AuxPedido.id = Datos.Leeme.GetInt16(0);
                    AuxPedido.IDCarrito = Datos.Leeme.GetInt16(1);
                    AuxPedido.EstadoPedidos = new EstadoPedidos();

                    AuxPedido.EstadoPedidos.Id = Datos.Leeme.GetInt16(2);
                    AuxPedido.EstadoPedidos.Descripcion = Datos.Leeme.GetString(5);

                    AuxPedido.Fecha = Datos.Leeme.GetDateTime(3);
                    AuxPedido.Importe = (double)Datos.Leeme.GetDecimal(4);

                    Lista.Add(AuxPedido);
                }

                return Lista;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public void ModificarEstado(int IdPedido,int IdEstado)
        {
            AccesoADatos Datos = new AccesoADatos();

            try
            {
                Datos.SetearSp("SP_ModificarEstadoPedido");
                Datos.AgregarParametro("@IdPedido", Convert.ToString(IdPedido));
                Datos.AgregarParametro("@IdEstado", Convert.ToString(IdEstado));
                Datos.EjecutarLector();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public void AgregarMotivoRechazo(int IdPedido,string Motivo,string Mensaje)
        {
            AccesoADatos Datos = new AccesoADatos();
            Datos.SetearQuery("INSERT INTO MARCA ([Nombre],[Estado]) values (@Nombre,@Estado)");
            Datos.SetearQuery("insert into PedidosRechazados Values(@IDPedido,@Motivo,@Mensaje)");
            Datos.AgregarParametro("@IDPedido", Convert.ToString(IdPedido));
            Datos.AgregarParametro("@Motivo", Motivo);
            Datos.AgregarParametro("@Mensaje", Mensaje);
            Datos.EjecutarLector();
        }

        public MotivoRechazo RetornarMotivo(int IdPedido)
        {
            try
            {
                AccesoADatos Datos = new AccesoADatos();
                MotivoRechazo M = new MotivoRechazo();

                Datos.SetearQuery("select *from PedidosRechazados where IDPedido = @id");
                Datos.AgregarParametro("@id", Convert.ToString(IdPedido));
                Datos.EjecutarLector();

                if (Datos.Leeme.Read())
                {
                    M.IDPedido = Datos.Leeme.GetInt16(0);
                    M.Motivo = Datos.Leeme.GetString(1);
                    M.Mensaje = Datos.Leeme.GetString(2);

                }

                return M;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public List<PedidosRevision> Listar(int IdPedido)
        {
            AccesoADatos Datos = new AccesoADatos();
            List<PedidosRevision> Lista = new List<PedidosRevision>();

            try
            {
                Datos.SetearSp("SP_Ver_Pedido");
                Datos.AgregarParametro("@IdPedido", Convert.ToString(IdPedido));
                Datos.EjecutarLector();

                while (Datos.Leeme.Read())
                {
                    PedidosRevision Aux = new PedidosRevision();

                    Aux.Articulo = new Articulo();
                    Aux.Articulo.Nombre = Datos.Leeme.GetString(0);

                    Aux.ElementoCarrito = new ElementoCarrito();
                    Aux.ElementoCarrito.Cantidad = Datos.Leeme.GetInt16(1);
                    Aux.ElementoCarrito.SubTotal = (double)Datos.Leeme.GetDecimal(2);

                    Aux.DatosEnvio = new DatosEnvio();
                    Aux.DatosEnvio.Correo = Datos.Leeme.GetString(3);
                    Aux.DatosEnvio.Localidad = Datos.Leeme.GetString(4);
                    Aux.DatosEnvio.Calle = Datos.Leeme.GetString(5);
                    Aux.DatosEnvio.EntreCalles = Datos.Leeme.GetString(6);
                    Aux.DatosEnvio.CodigoPostal = Datos.Leeme.GetInt16(7);

                    Lista.Add(Aux);

                }

                return Lista;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
