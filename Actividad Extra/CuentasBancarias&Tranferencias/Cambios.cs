using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuentasBancarias_Tranferencias
{
    internal class Cambios
    {
        public string id { get; set; }
        public int CambioTipo { get; set; }
        public string CambioFecha { get; set; }
        //           Claves foráneas
        public long CambioIDMovimiento { get; set; }

        //Métodos
        public void Menu()
        {
            int opc = 0;
            while (opc != 5)
            {
                Console.WriteLine("MENU DE CAMBIO");
                Console.WriteLine("¿Que desea hacer?");
                Console.WriteLine("1.- Mostrar Cambio");
                Console.WriteLine("2.- Buscar Cambio");
                Console.WriteLine("3.- Agregar Cambio");
                Console.WriteLine("4.- Eliminar Cambio");
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
                        MostrarCambios();
                        break;
                    case 2:
                        try
                        {
                            Console.WriteLine("Escriba el ID del cambio que desea buscar:");
                            this.id = Console.ReadLine().ToString();

                            BuscarCambioID(id);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("El ID no ha sido ingresado correctamente, Error: " + ex.ToString());
                        }
                        break;
                    case 3:
                        try
                        {
                            Console.WriteLine("Escriba el tipo de cambio:");
                            this.CambioTipo = Convert.ToInt32(Console.ReadLine());

                            Console.WriteLine("Escriba el id del movimiento:");
                            this.CambioIDMovimiento = Convert.ToInt64(Console.ReadLine());

                            if( CambioTipo.Equals("") || CambioIDMovimiento.Equals(""))
                            {
                                Console.WriteLine("Los datos no han sido ingresados correctamente!");
                            } else
                            {
                                AgregarCambio(CambioTipo, CambioIDMovimiento);
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Los datos no han sido ingresados correctamente!, Error: " + ex.ToString());
                        }
                        break;
                    case 4:
                        try
                        {
                            Console.WriteLine("Escriba el ID del cambio que desee eliminar:");
                            this.id = Console.ReadLine().ToString();

                            BorrarCambio(id);
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

        public static void MostrarCambios()
        {
            string connectionString = "Server=LENOVOLAP-RGS\\SQLEXPRESS;Database=BanksAccountsDB;Integrated Security=True;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM Cambios";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        Console.WriteLine("____________________________________________________________");
                        Console.WriteLine(" IDCambio | CambioTipo | CambioFecha | CambioIDMovimiento");
                        while (reader.Read())
                        {
                            Console.WriteLine(reader[0].ToString() + " | " + reader[1].ToString() + " | " + reader[2].ToString()
                                + " | " + reader[3].ToString());
                        }
                        Console.WriteLine("______________________________________________________________");
                        Console.WriteLine("Fin de la tabla");
                    }
                    connection.Close();
                }
                catch (Exception ex) { Console.WriteLine("Ha ocurrido un error -> " + ex.Message); }
            }
        }

        public static void BuscarCambioID(string id)
        {
            string connectionString = "Server=LENOVOLAP-RGS\\SQLEXPRESS;Database=BanksAccountsDB;Integrated Security=True;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM Cambios where IDCambio = "+id;
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        Console.WriteLine("____________________________________________________________");
                        Console.WriteLine(" IDCambio | CambioTipo | CambioFecha | CambioIDMovimiento");
                        while (reader.Read())
                        {
                            Console.WriteLine(reader[0].ToString() + " | " + reader[1].ToString() + " | " + reader[2].ToString()
                                + " | " + reader[3].ToString());
                        }
                        Console.WriteLine("______________________________________________________________");
                        Console.WriteLine("Fin de la tabla");
                        connection.Close();
                    }
                }
                catch (Exception ex) { Console.WriteLine("Ha ocurrido un error -> " + ex.Message); }
            }
        }
        public static void BorrarCambio(string id)
        {
            string connectionString = "Server=LENOVOLAP-RGS\\SQLEXPRESS;Database=BanksAccountsDB;Integrated Security=True;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string query = "DELETE FROM Cambios where IDCambio = " + id;
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
        public static void AgregarCambio(int CambioTipo, long CambioIDMovimiento)
        {
            string connectionString = "Server=LENOVOLAP-RGS\\SQLEXPRESS;Database=BanksAccountsDB;Integrated Security=True;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    DateTime CambioFecha = DateTime.Today;
                    string query = "INSERT INTO Cambios (CambioTipo, CambioFecha, CambioIDMovimiento) " +
                       "values (" + CambioTipo + ",'" + CambioFecha + "'," + CambioIDMovimiento + ")";
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
