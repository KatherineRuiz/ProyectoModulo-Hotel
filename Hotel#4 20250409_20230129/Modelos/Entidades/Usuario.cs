using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Modelos.Entidades
{
    public class Usuario
    {
        //Declaramos los atributos de la clase
        private int idUsuario;
        private string nombreUsuario;
        private string clave;
        private int id_Rol;

        public int IdUsuario { get => idUsuario; set => idUsuario = value; }
        public string NombreUsuario { get => nombreUsuario; set => nombreUsuario = value; }
        public string Clave { get => clave; set => clave = value; }
        public int Id_Rol { get => id_Rol; set => id_Rol = value; }

        //Metodo para verificar el login de los usuarios
        public bool VerificarLogin(string nombreusuario, string clave)
        {
            string hashEnBaseDeDatos = "";
            SqlConnection con = Conexion.Conexion.Conectar();
            string query = "Select clave, id_rol from Usuario Where nombreUsuario = @Usuario";
            SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@Usuario", nombreusuario);

            if (cmd.ExecuteScalar() == null)
            {
                return false;
            }
            else
            {
                hashEnBaseDeDatos = cmd.ExecuteScalar().ToString();

                return BCrypt.Net.BCrypt.Verify(clave, hashEnBaseDeDatos);
            }
        }

       //Metodo para traer los usuarios de la base de datos
        public static DataTable CargarUsuario()
        {
            try
            {
                //Creamos un objeto conexion
                SqlConnection conexion = Conexion.Conexion.Conectar();
                //Creamos la consulta y la enviamos a la base de datos 
                string consultaQuery = "select Usuario.idUsuario, Usuario.nombreUsuario As [Usuario], Rol.nombreRol As [Rol]," +
                    "from Usuario" +
                    "\r\ninner join\r\nRol On Usuario.id_Rol = Rol.idRol";
                SqlDataAdapter ad = new SqlDataAdapter(consultaQuery, conexion);
                //Creamos la tabla virtual, la rellenamos con la información obtenida y la retornamos
                DataTable dt = new DataTable();
                ad.Fill(dt);

                return dt;
            }
            catch (Exception)
            {
                MessageBox.Show("Este correo electrónico ya existe, ingrese uno distinto", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
            
        }

        //Método para insertar usuarios 
        public bool InsertarUsuario()
        {
            try
            {
                //Creamos un objeto conexion
                SqlConnection con = Conexion.Conexion.Conectar();
                //Creamos el comando para insertar
                string comando = "insert into Usuario(nombreUsuario, clave, id_Rol)" + "values (@nombreUsuario, @clave, @estadoUsuario, @id_Rol);";
                SqlCommand cmd = new SqlCommand(comando, con);
                cmd.Parameters.AddWithValue("@nombreUsuario", nombreUsuario);
                cmd.Parameters.AddWithValue("@clave", clave);
                cmd.Parameters.AddWithValue("@id_Rol", id_Rol);

                if (cmd.ExecuteNonQuery() > 0)
                {
                    return true;
                }
                else
                {
                    MessageBox.Show("Este correo electrónico ya existe, ingrese uno distinto", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("La insersión del usuario falló: " + ex);
                return false;
            }
            
        }

        public bool EliminarUsuario(int id)
        {
            SqlConnection conexion = Conexion.Conexion.Conectar();
            string colsultaDelete = "Delete from Usuario where idUsuario = @id";
            SqlCommand delete = new SqlCommand(colsultaDelete, conexion);
            delete.Parameters.AddWithValue("@id", id);
            if (delete.ExecuteNonQuery() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool ActualizarUsuario()
        {
            try
            {
                SqlConnection conexion = Conexion.Conexion.Conectar();
                string consultaUpdate = "Update Usuario set nombreUsuario = @nombre, clave = @clave, estadoUsuario = @estadoUsuario, id_Rol = @id_Rol where idUsuario = @idUsuario";
                SqlCommand actualizar = new SqlCommand(consultaUpdate, conexion);
                actualizar.Parameters.AddWithValue("@nombre", nombreUsuario);
                actualizar.Parameters.AddWithValue("@clave", clave);
                actualizar.Parameters.AddWithValue("@id_Rol", id_Rol);
                actualizar.Parameters.AddWithValue("@idUsuario", IdUsuario);
                actualizar.ExecuteNonQuery();
                MessageBox.Show("Datos Actualizados", "Actualizar");
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar los datos" + ex);
                return false;
            }
        }


    }
}
