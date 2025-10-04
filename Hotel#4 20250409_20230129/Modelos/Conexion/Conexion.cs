using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Modelos.Conexion
{
    public class Conexion
    {
        private static string servidor = "DESKTOP-URTJUTM\\SQLEXPRESS";
        private static string baseDeDatos = "Hotel";

        public static SqlConnection Conectar()
        {
            try
            {
                //creamos una cadena de conexion
                string cadena =
                    $"Data Source = {servidor},54321;Initial Catalog = {baseDeDatos};Integrated Security = true;";

                //Creamos un objeto de tipo SqlConnection
                SqlConnection con = new SqlConnection(cadena);

                //Abrimos la conexion entre SQL Server y nuestra aplicacion
                con.Open();
                return con;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en la conexión con la base de datos" + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
        }
    }
}
