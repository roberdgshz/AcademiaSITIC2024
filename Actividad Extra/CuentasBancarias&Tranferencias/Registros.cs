using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuentasBancarias_Tranferencias
{
    internal class Registros
    {
        public string id {get; set;}
        public double RegistroSaldo { get; }
        public string RegistroFecha { get; }

        //           Claves foráneas
        public long RegistroIDCliente { get; }
        public long RegistroIDMovimiento { get; }
        public string RegistroBancoClave { get; }
        public long RegistroCuentaClave { get; }

        //Métodos
        public void Menu()
        {
            int opc = 0;
            while(opc != 3)
            {
                Console.WriteLine("MENU DE REGISTRO");
                Console.WriteLine("¿Que desea hacer?");
                Console.WriteLine("1.- Mostrar Registros");
                Console.WriteLine("2.- Buscar registro");
                Console.WriteLine("3.- Volver");
                Console.WriteLine("Escriba el número de la opción que desee:");
                try
                {
                    opc = Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception ex) { Console.WriteLine("Escriba el número que representa cada opción. " + ex.Message); }
                switch (opc)
                {
                    case 1: MostrarRegistros();
                        break;
                    case 2:
                        try
                        {
                            Console.WriteLine("Escriba el ID del registro que desea buscar:");
                            this.id = Console.ReadLine().ToString();

                            BuscarRegistroID(id);
                        }
                        catch(Exception ex)
                        {
                            Console.WriteLine("El ID no ha sido ingresado correctamente, Error: "+ex.ToString());
                        }
                        break;
                    case 3:
                        break;
                    default: Console.WriteLine("Elija una opción disponible.");
                        break;
                }
                Console.WriteLine("-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_");
            }
        }


        public static void MostrarRegistros()
        {
            string connectionString = "Server=LENOVOLAP-RGS\\SQLEXPRESS;Database=BanksAccountsDB;Integrated Security=True;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM Registros";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        Console.WriteLine("__________________________________________________________________________________________________________________________________");
                        Console.WriteLine("IDRegistro | RegistroSaldo | RegistroFecha | RegistroIDCliente | RegistroIDMovimiento | RegistroBancoClave | RegistroCuentaClave");
                        while (reader.Read())
                        {
                            Console.WriteLine(reader[0].ToString() + " | " + reader[1].ToString() + " | " + reader[2].ToString()
                                + " | " + reader[3].ToString() + " | " + reader[4].ToString() + " | " + reader[5].ToString()+" | " + reader[6].ToString());
                        }
                        Console.WriteLine("__________________________________________________________________________________________________________________________________");
                        Console.WriteLine("Fin de la tabla");
                    }
                    connection.Close();
                }
                catch (Exception ex) { Console.WriteLine("Ha ocurrido un error -> "+ex.Message); }
            } 
        }

        public static void BuscarRegistroID(string id)
        {
            string connectionString = "Server=LENOVOLAP-RGS\\SQLEXPRESS;Database=BanksAccountsDB;Integrated Security=True;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM Registros where IDRegistro = "+id;
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        Console.WriteLine("__________________________________________________________________________________________________________________________________");
                        Console.WriteLine("IDRegistro | RegistroSaldo | RegistroFecha | RegistroIDCliente | RegistroIDMovimiento | RegistroBancoClave | RegistroCuentaClave");
                        while (reader.Read())
                        {
                            Console.WriteLine(reader[0].ToString() + " | " + reader[1].ToString() + " | " + reader[2].ToString()
                                + " | " + reader[3].ToString() + " | " + reader[4].ToString() + " | " + reader[5].ToString() + " | " + reader[6].ToString());
                        }
                        Console.WriteLine("__________________________________________________________________________________________________________________________________");
                    }
                        connection.Close();
                }
                catch (Exception ex) { Console.WriteLine("Ha ocurrido un error -> " + ex.Message); }
            }
        }
        public static void BorrarRegistro(string id)
        {
            string connectionString = "Server=LENOVOLAP-RGS\\SQLEXPRESS;Database=BanksAccountsDB;Integrated Security=True;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "DELETE FROM Registros where IDRegistro = " + id;
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        int rowsAffected = command.ExecuteNonQuery();
                        Console.WriteLine($"Filas afectadas: {rowsAffected}");
                    }
                    connection.Close();
                }
                catch (Exception ex) { Console.WriteLine("Ha ocurrido un error -> " + ex.Message); }
            }
        }
        public static void AgregarRegistro(float RegistroSaldo, long RegistroIDCliente, long RegistroIDMovimiento, string RegistroBancoClave, long RegistroCuentaClave)
        {
            string connectionString = "Server=LENOVOLAP-RGS\\SQLEXPRESS;Database=BanksAccountsDB;Integrated Security=True;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    DateTime RegistroFecha = DateTime.Today;
                    string query = "INSERT INTO Registros ( RegistroSaldo, RegistroFecha, RegistroIDCliente, RegistroIDMovimiento, RegistroBancoClave, RegistroCuentaClave) " +
                       "values (" +RegistroSaldo+ ",'" + RegistroFecha + "'," + RegistroIDCliente + ","+ RegistroIDMovimiento +",'"+ RegistroBancoClave +"',"+ RegistroCuentaClave +")";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        int rowsAffected = command.ExecuteNonQuery();
                        Console.WriteLine($"Filas afectadas: {rowsAffected}");
                    }
                    connection.Close();
                }
                catch (Exception ex) { Console.WriteLine("Ha ocurrido un error -> " + ex.Message); }
            }
        }
    }
}
