﻿using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TPC_Orihuela_Insaurralde
{
    public partial class ABMLCategoria : System.Web.UI.Page
    {
        public List<Categoria> Lista = new List<Categoria>();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                NegocioCategoria Negocio = new NegocioCategoria();
                Lista = Negocio.ListarCategorias();

                var Eliminar = Request.QueryString["ID"];

                if(Eliminar != null)
                {
                    Negocio.EliminarCategoria(Convert.ToInt32(Eliminar));
                    Response.Redirect("AMBLCategoria.aspx");
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        
    }
}