using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuentasBancarias_Tranferencias
{
    internal class Monedas
    {
        public string id { get; set; }
        public string MonedaNombre { get; set; }
        public string MonedaNacionalidad { get; set; }
        public string MonedaClave { get; set; }

        //Métodos
        public void Menu()
        {
            int opc = 0;
            while (opc != 5)
            {
                Console.WriteLine("MENU DE MONEDA");
                Console.WriteLine("¿Que desea hacer?");
                Console.WriteLine("1.- Mostrar Moneda");
                Console.WriteLine("2.- Buscar Moneda");
                Console.WriteLine("3.- Agregar Moneda");
                Console.WriteLine("4.- Eliminar Moneda");
                Console.WriteLine("5.- Volver");
                Console.WriteLine("Escriba el número de la opción que desee:");
                try
                {
                    opc = Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception ex) { Console.WriteLine("Escriba el número que representa cada opción. " + ex.Message); }
                switch (opc)
                {
                    case 1:
                        MostrarMonedas();
                        break;
                    case 2:
                        try
                        {
                            Console.WriteLine("Escriba el ID del moneda que desea buscar:");
                            this.id = Console.ReadLine();

                            BuscarMonedaID(id);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("El ID no ha sido ingresado correctamente, Error: " + ex.ToString());
                        }
                        break;
                    case 3:
                        try
                        {
                            Console.WriteLine("Escriba el nombre de la moneda:");
                            this.MonedaNombre = Convert.ToString(Console.ReadLine());

                            Console.WriteLine("Escriba la nacionalidad de la moneda:");
                            this.MonedaNacionalidad = Convert.ToString(Console.ReadLine());

                            Console.WriteLine("Escriba la clave de la moneda:");
                            this.MonedaClave = Convert.ToString(Console.ReadLine());

                            if(MonedaClave.Equals("") || MonedaNacionalidad.Equals("") || MonedaClave.Equals(""))
                            {
                                Console.WriteLine("Los datos no han sido ingresados correctamente!");
                            } else
                            {
                                AgregarMoneda(MonedaNombre, MonedaNacionalidad, MonedaClave);
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Los datos no fueron ingresados correctamente!, Error: " + ex.ToString());
                        }
                        break;
                    case 4:
                        try
                        {
                            Console.WriteLine("Escriba el ID del moneda que desee eliminar:");
                            this.id = Console.ReadLine().ToString();

                            BorrarMoneda(id);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("El ID no ha sido ingresado correctamente, Error: " + ex.ToString());
                        }
                        break;
                    case 5:
                        break;
                    default:
                        Console.WriteLine("Elija una opción disponible.");
                        break;
                }
                Console.WriteLine("-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_");
            }
        }

        public static void MostrarMonedas()
        {
            string connectionString = "Server=LENOVOLAP-RGS\\SQLEXPRESS;Database=BanksAccountsDB;Integrated Security=True;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM Monedas";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        Console.WriteLine("_____________________________________________________________________");
                        Console.WriteLine(" IDMoneda | MonedaNombre        | MonedaNacionalidad | MonedaClave");
                        while (reader.Read())
                        {
                            Console.WriteLine(reader[0].ToString() + "         | " + reader[1].ToString() + "| " + reader[2].ToString()
                                + "     | " + reader[3].ToString());
                        }
                        Console.WriteLine("_______________________________________________________________________");
                        Console.WriteLine("Fin de la tabla");
                    }
                    connection.Close();
                }
                catch (Exception ex) { Console.WriteLine("Ha ocurrido un error -> " + ex.Message); }
            }
        }

        public static void BuscarMonedaID(string id)
        {
            string connectionString = "Server=LENOVOLAP-RGS\\SQLEXPRESS;Database=BanksAccountsDB;Integrated Security=True;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM Monedas where IDMoneda = "+id;
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        Console.WriteLine("_____________________________________________________________________");
                        Console.WriteLine(" IDMoneda | MonedaNombre        | MonedaNacionalidad | MonedaClave");
                        while (reader.Read())
                        {
                            Console.WriteLine(reader[0].ToString() + "         | " + reader[1].ToString() + "| " + reader[2].ToString()
                                + "     | " + reader[3].ToString());
                        }
                        Console.WriteLine("_____________________________________________________________________");
                        Console.WriteLine("Fin de la tabla");
                    }
                    connection.Close();
                }
                catch (Exception ex) { Console.WriteLine("Ha ocurrido un error -> " + ex.Message); }
            }
        }
        public static void BorrarMoneda(string id)
        {
            string connectionString = "Server=LENOVOLAP-RGS\\SQLEXPRESS;Database=BanksAccountsDB;Integrated Security=True;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "DELETE FROM Monedas where IDMoneda = " + id;
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
        public static void AgregarMoneda(string MonedaNombre, string MonedaNacionalidad, string MonedaClave)
        {
            string connectionString = "Server=LENOVOLAP-RGS\\SQLEXPRESS;Database=BanksAccountsDB;Integrated Security=True;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "INSERT INTO Monedas (MonedaNombre, MonedaNacionalidad, MonedaClave) " +
                        "values ('" + MonedaNombre + "','" + MonedaNacionalidad + "','" + MonedaClave + "')";
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
