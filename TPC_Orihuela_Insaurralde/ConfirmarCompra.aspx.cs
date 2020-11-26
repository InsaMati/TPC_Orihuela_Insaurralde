﻿using System;
using Dominio;
using Negocio;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPC_Orihuela_Insaurralde
{
    public partial class ConfirmarCompra : System.Web.UI.Page
    {
        public Usuario Logueado = new Usuario();

        public List<ElementoCarrito> ElementosCarrito = new List<ElementoCarrito>();
        public DatosEnvio InfoEnvio = new DatosEnvio();
        public int MedioPago = new int();
        public int Cuotas = new int();
        public double Total = new double();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                Logueado = (Usuario)Session[Session.SessionID + "UsuarioLogueado"];
                if (Logueado == null) Response.Redirect("Login.aspx");

                VariablesSession();

            }
        }

        public void VariablesSession()
        {

            try
            {
                ElementosCarrito = (List<ElementoCarrito>)Session[Session.SessionID + "Lista"];
                MedioPago = (int)Session[Session.SessionID + "IdMetodoPago"];
                Cuotas = (int)Session[Session.SessionID + "Cuotas"];
                InfoEnvio = (DatosEnvio)Session[Session.SessionID + "DatosEnvio"];
                Total = (double)Session[Session.SessionID + "Total"];
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        protected void BtnConfirmarCompra_Click(object sender, EventArgs e)
        {

            try
            {
                AgregarCarrito();
                Response.Redirect("Inicio.aspx");
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void AgregarCarrito()
        {
          
            try
            {
                NegocioCarrito NegocioCarrito = new NegocioCarrito();
                CarritoCompra Aux = new CarritoCompra();
                VariablesSession();

                Logueado = (Usuario)Session[Session.SessionID + "UsuarioLogueado"];
                Aux.IdUsuario = Logueado.Id;
                Aux.CostoTotal = Total;

                NegocioCarrito.AltaCarrito(Aux);
                AltaElementoCarrito(NegocioCarrito.IdCarrito(Logueado));
                AltaPedido(NegocioCarrito.IdCarrito(Logueado));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void AltaElementoCarrito(int IdCarrito)
        {
            NegocioCarrito negocioCarrito = new NegocioCarrito();

            try
            {       
                foreach (var Elemento in ElementosCarrito)
                {
                    ElementoCarrito Auxiliar = new ElementoCarrito();

                    Auxiliar.IdCarrito = IdCarrito;
                    Auxiliar.articulo = new Articulo();
                    Auxiliar.articulo.Id = Elemento.articulo.Id;
                    Auxiliar.Cantidad = Elemento.Cantidad;
                    Auxiliar.SubTotal = Elemento.SubTotal;

                    negocioCarrito.AltaElemento(Auxiliar);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AltaPedido(int IdCarro)
        {
            NegocioCarrito NegocioCarrito = new NegocioCarrito();

            try
            {
                Pedido AuxPedido = new Pedido();
                AuxPedido.IDCarrito = IdCarro;

                AuxPedido.EstadoPedidos = new EstadoPedidos();
                AuxPedido.EstadoPedidos.Id = 1;
                AuxPedido.Fecha = DateTime.Now;

                NegocioCarrito.AltaPedido(AuxPedido);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}