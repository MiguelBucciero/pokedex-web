using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Negocio
{
    public class TraineeNegocio
    {
		AccesoDatos datos = new AccesoDatos( );

        public void Actualizar(Trainee user)
        {
            try
			{
				datos.setearConsulta("UPDATE USERS SET nombre = @nombre, apellido = @apellido, fechaNacimiento = @fechaNacimiento, imagenPerfil = @imagenPerfil WHERE id = @id");
				datos.setearParametro("@nombre", user.Nombre);
				datos.setearParametro("@apellido", user.Apellido);
				datos.setearParametro("@fechaNacimiento", user.FechaNacimiento);
				datos.setearParametro("@imagenPerfil", user.ImagenPerfil);
				datos.setearParametro("@id", user.Id);
				datos.ejecutarAccion();

            }
			catch (Exception ex)
			{

				throw ex;
			}
			finally
			{
				datos.cerrarConexion();
            }
        }

        public int insertarNuevo(Trainee nuevo)
		{
			try
			{
				datos.setearProcedimiento("insertarNuevo");
				datos.setearParametro("@email", nuevo.Email);
				datos.setearParametro("@pass", nuevo.Pass);
				return datos.ejecutarAccionScalar();
			}
			catch (Exception ex)
			{

				throw ex;
			}
			finally
			{
				datos.cerrarConexion();
			}
		}
		public bool Login(Trainee trainee)
		{
			AccesoDatos datos = new AccesoDatos();
            try
			{
				datos.setearConsulta("SELECT id, email, pass, admin, imagenPerfil, nombre, apellido, fechaNacimiento FROM USERS WHERE email = @email AND pass = @pass");
				datos.setearParametro("@email", trainee.Email);
				datos.setearParametro("@pass", trainee.Pass);
				datos.ejecutarLectura();
				if (datos.Lector.Read())
				{
					trainee.Id = (int)datos.Lector["id"];
					trainee.Admin = (bool)datos.Lector["admin"];
					if(!(datos.Lector["imagenPerfil"] is DBNull))
						trainee.ImagenPerfil = (string)datos.Lector["imagenPerfil"];
					if(!(datos.Lector["nombre"] is DBNull))
						trainee.Nombre = (string)datos.Lector["nombre"];
					if(!(datos.Lector["apellido"] is DBNull))
                        trainee.Apellido = (string)datos.Lector["apellido"];
					if(!(datos.Lector["fechaNacimiento"] is DBNull))
                        trainee.FechaNacimiento = (DateTime)datos.Lector["fechaNacimiento"];

					return true;
                }
				return false;

            }
			catch (Exception ex)
			{

				throw ex;
			}
			finally
			{
				datos.cerrarConexion();
            }
        }
    }
}
