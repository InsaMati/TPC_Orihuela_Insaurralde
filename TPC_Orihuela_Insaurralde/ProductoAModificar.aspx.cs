﻿using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPC_Orihuela_Insaurralde
{
    public partial class ProductoAModificar : System.Web.UI.Page
    {

        public List<Marca> ListaM = new List<Marca>();
        public List<Articulo> BuscarArticulo = new List<Articulo>();
        public List<Categoria> ListaC = new List<Categoria>();
        public Usuario Logueado = new Usuario();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                Logueado = (Usuario)Session[Session.SessionID + "UsuarioLogueado"];
                if (Logueado == null) Response.Redirect("Login.aspx");
                if (Logueado.TipoUsuario.Id >= 3) Response.Redirect("Inicio.aspx");

                if (!IsPostBack)
                {
                    var Modificar = Request.QueryString["Pro"];
                   
                    if (Modificar != null)
                    {
                        CargarListas();
                        Articulo Modificado = BuscarArticulo.Find(J => J.Id == int.Parse(Modificar));
                        CargarDropDown(Modificado.Marca.Id,Modificado.Categoria.Id);
                        CargarTextbox(Modificado);
                    }

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public void CargarListas()
        {
            NegocioMarca NegocioMarca = new NegocioMarca();
            NegocioCategoria NegocioCategoria = new NegocioCategoria();
            NegocioArticulo NegocioArticulo = new NegocioArticulo();

            ListaM = NegocioMarca.ListarMarcas();
            ListaC = NegocioCategoria.ListarCategorias();
            BuscarArticulo = NegocioArticulo.ListarArticulos();

        }

        public void CargarDropDown(int IdMarca, int IdCategoria)
        {
            int IndexCategoria = ListaC.FindIndex(J => J.Id == IdCategoria);
            int IndexMarca = ListaM.FindIndex(J => J.Id == IdMarca);

            DDCategoria.DataSource = ListaC;
            DDCategoria.SelectedIndex = IndexCategoria;
            DDCategoria.DataBind();

            DDMarca.DataSource = ListaM;
            DDMarca.SelectedIndex = IndexMarca;
            DDMarca.DataBind();
        }

        public void CargarTextbox(Articulo Precargar)
        {
            TxtCodigo.Text = Precargar.Codigo;
            TxtNombre.Text = Precargar.Nombre;
            TxtDescripcion.Text = Precargar.Descripcion;
            TxtImagen.Text = Precargar.UrlImagen;
            TxtPrecio.Text = Convert.ToString(Precargar.Precio);
            TxtStock.Text = Convert.ToString(Precargar.Stock);

        }

        public bool ValidarProducto()
        {
            Articulo Aux = new Articulo();

            Aux.Codigo = TxtCodigo.Text.Trim();
            Aux.Nombre = TxtNombre.Text.Trim();
            Aux.Descripcion = TxtDescripcion.Text.Trim();
            Aux.UrlImagen = TxtImagen.Text.Trim();
            string stock = TxtStock.Text.Trim();
            string precio = TxtPrecio.Text.Trim();

            if (Aux.Codigo.Length == 0) return false;
            if (Aux.Nombre.Length == 0) return false;
            if (Aux.Descripcion.Length == 0) return false;
            if (Aux.UrlImagen.Length == 0) return false;
            if (stock.Length == 0) return false;
            if (precio.Length == 0) return false;
            return true;
        }

        protected void BtnModificar_Click(object sender, EventArgs e)
        {
            NegocioArticulo NegocioArticulo = new NegocioArticulo();

            try
            {

                if (ValidarProducto() == true)
                {
                    CargarListas();

                    Articulo AuxModificar = new Articulo();

                    AuxModificar.Id = Convert.ToInt16(Request.QueryString["Pro"]);
                    AuxModificar.Codigo = TxtCodigo.Text;
                    AuxModificar.Nombre = TxtNombre.Text;
                    AuxModificar.Descripcion = TxtDescripcion.Text;
                    AuxModificar.Marca = ListaM.Find(BuscarMarca => BuscarMarca.Nombre == DDMarca.SelectedValue);
                    AuxModificar.Categoria = ListaC.Find(BuscarCategoria => BuscarCategoria.Nombre == DDCategoria.SelectedValue);
                    AuxModificar.UrlImagen = TxtImagen.Text;
                    AuxModificar.Precio = double.Parse(TxtPrecio.Text);
                    AuxModificar.Stock = Convert.ToInt16(TxtStock.Text);

                    NegocioArticulo.ModificarArticulo(AuxModificar);

                    Response.Redirect("ABMLProducto.aspx");
                }
                else
                {
                    string script = @"<script type='text/javascript'>
                            alert('Error campos vacios.');
                        </script>";
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", script, false);
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        protected void BtnVolver_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("ABMLProducto.aspx");
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}