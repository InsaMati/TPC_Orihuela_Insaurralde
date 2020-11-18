﻿using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class NegocioUsuario
    {
        public List<Usuario> ListarUsuarios()
        {
            List<Usuario> ListaUsuarios = new List<Usuario>();
            AccesoADatos Datos = new AccesoADatos();

            try
            {
                Datos.SetearQuery("SELECT * FROM USUARIO");
                Datos.EjecutarLector();

                while (Datos.Leeme.Read())
                {
                    var aux = new Usuario();

                    aux.Id = Datos.Leeme.GetInt32(0);
                    aux.Email = Datos.Leeme.GetString(1);
                    aux.Contraseña = Datos.Leeme.GetString(2);
                    aux.TipoUsuario.Id = Datos.Leeme.GetInt32(3);
                    aux.Estado = Datos.Leeme.GetBoolean(4);
                }
                return ListaUsuarios;
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

        public List<Usuario> ListarUsuariosXTipo(int IDTipo)
        {
            List<Usuario> Lista = new List<Usuario>();
            AccesoADatos Datos = new AccesoADatos();
            try
            {
                Datos.SetearQuery("SP_ListarUsuarios");
                Datos.AgregarParametro("@Tipo", Convert.ToString(IDTipo));
                Datos.EjecutarLector();

                while (Datos.Leeme.Read())
                {
                    var aux = new Usuario();

                    aux.Id = Datos.Leeme.GetInt32(0);
                    aux.Email = Datos.Leeme.GetString(1);
                    aux.Contraseña = Datos.Leeme.GetString(2);
                    aux.TipoUsuario.Id = Datos.Leeme.GetInt32(3);
                    aux.Estado = Datos.Leeme.GetBoolean(4);
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

        public void EliminarUsuario(int id)
        {
            AccesoADatos Datos = new AccesoADatos();
            try
            {
                Datos.SetearQuery("update USUARIO set Estado = 0 where ID = @ID");
                Datos.AgregarParametro("@ID", Convert.ToString(id));
                Datos.EjecutarLector();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public void RegistrarUsuario(Usuario user)
        {
            AccesoADatos Datos = new AccesoADatos();

            try
            {
                Datos.SetearSp("SP_AgregarUsuario");
                Datos.AgregarParametro("@Email", user.Email);
                Datos.AgregarParametro("@Contraseña", user.Contraseña);
                Datos.AgregarParametroSmallInt("@IdTipoUsuario", user.TipoUsuario.Id);

                Datos.EjecutarLector();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public Usuario Login(Usuario Verificar)
        {
            AccesoADatos Datos = new AccesoADatos();

            try
            {
                Datos.SetearSp("SP_VERIFICAR_USUARIO");
                Datos.AgregarParametro("@Email", Verificar.Email);
                Datos.AgregarParametro("@Contraseña", Verificar.Contraseña);
                Datos.EjecutarLector();

                if (Datos.Leeme.Read())
                {
                    Verificar.Id = Datos.Leeme.GetInt16(0);
                    Verificar.Email = Datos.Leeme.GetString(1);
                    Verificar.Contraseña = Datos.Leeme.GetString(2);

                    Verificar.TipoUsuario = new TipoUsuario();

                    Verificar.TipoUsuario.Id = Datos.Leeme.GetInt16(3);
                    Verificar.Estado = Datos.Leeme.GetBoolean(4);

                }

                return Verificar;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int RetornarId(string Email)
        {
            AccesoADatos Datos = new AccesoADatos();

            try
            {
                int Aux = new int();

                Datos.SetearQuery("select ID from USUARIO where Email = @Email");
                Datos.AgregarParametro("@Email", Email);
                Datos.EjecutarLector();

                if (Datos.Leeme.Read()) Aux = Datos.Leeme.GetInt16(0);

                return Aux;

            }
            catch (Exception ex)
            {

                throw ex;
            }


        }

    }
}
