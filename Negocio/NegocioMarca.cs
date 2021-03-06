﻿using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class NegocioMarca
    {
        public List<Marca> ListarMarcas()
        {
            List<Marca> Lista = new List<Marca>();
            AccesoADatos Datos = new AccesoADatos();

            try
            {
                Datos.SetearQuery("select *from MARCA");
                Datos.EjecutarLector();

                while (Datos.Leeme.Read())
                {
                    Marca AuxMarca = new Marca();

                    AuxMarca.Id = Datos.Leeme.GetInt16(0);
                    AuxMarca.Nombre = Datos.Leeme.GetString(1);
                    AuxMarca.Estado = Datos.Leeme.GetBoolean(2);

                    if (AuxMarca.Estado == true) Lista.Add(AuxMarca);
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
        public void EliminarMarca (int id)
        {
            AccesoADatos Datos = new AccesoADatos();

            Datos.SetearQuery("update MARCA set Estado = @Estado where ID = @Id");
            Datos.AgregarParametro("@Estado", Convert.ToString(0));
            Datos.AgregarParametro("@ID", Convert.ToString(id));
            Datos.EjecutarLector();

        }

        public void AgregarMarca (string Marca)
        {
            AccesoADatos Datos = new AccesoADatos();
            Datos.SetearQuery("INSERT INTO MARCA ([Nombre],[Estado]) values (@Nombre,@Estado)");
            Datos.AgregarParametro("@Nombre", Marca);
            Datos.AgregarParametro("@Estado", "1");
            Datos.EjecutarLector();
        }

        public void ModificarMarca(string MarcaVieja, string MarcaNueva)
        {
            AccesoADatos Datos = new AccesoADatos();
            Datos.SetearQuery("UPDATE marca SET nombre = @Nombre where nombre=@ViejoNombre");
            Datos.AgregarParametro("@Nombre", MarcaNueva);
            Datos.AgregarParametro("@ViejoNombre", MarcaVieja);
            Datos.EjecutarLector();
        }
    }
}
