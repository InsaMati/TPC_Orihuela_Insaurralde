﻿using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Web;
using System.Linq;
using System.Net.Cache;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class NegocioCategoria
    {
        public List<Categoria> ListarCategorias()
        {
            List<Categoria> Lista = new List<Categoria>();
            AccesoADatos Datos = new AccesoADatos();

            try
            {
                Datos.SetearQuery("select *from CATEGORIA");
                Datos.EjecutarLector();

                while (Datos.Leeme.Read())
                {
                    Categoria AuxCate = new Categoria();

                    AuxCate.Id = Datos.Leeme.GetInt16(0);
                    AuxCate.Nombre = Datos.Leeme.GetString(1);
                    AuxCate.Estado = Datos.Leeme.GetBoolean(2);

                    if (AuxCate.Estado == true) Lista.Add(AuxCate);

                }

                return Lista;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                Datos.CerrarConexion();
            }


        }

        public void EliminarCategoria (int id)
        {
            AccesoADatos Datos = new AccesoADatos();

            try
            {
                Datos.SetearQuery("update CATEGORIA set Estado = @Estado where ID = @Id");
                Datos.AgregarParametro("@Estado", Convert.ToString(0));
                Datos.AgregarParametro("@Id", Convert.ToString(id));
                Datos.EjecutarLector();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public void AgregarCategoria(string Categoria)
        {
            AccesoADatos Datos = new AccesoADatos();
            Datos.SetearQuery("INSERT INTO CATEGORIA ([Nombre],[Estado]) values (@Nombre,@Estado)");
            Datos.AgregarParametro("@Nombre", Categoria);
            Datos.AgregarParametro("@Estado", "1");
            Datos.EjecutarLector();
        }

        public void ModificarCategoria (string CategoriaVieja, string CategoriaNueva)
        {
            AccesoADatos Datos = new AccesoADatos();
            Datos.SetearQuery("UPDATE categoria SET nombre = @Nombre where nombre=@ViejoNombre");
            Datos.AgregarParametro("@Nombre", CategoriaNueva);
            Datos.AgregarParametro("@ViejoNombre", CategoriaVieja);
            Datos.EjecutarLector();
        }
    }
}
