using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuentasBancarias_Tranferencias
{
    internal class Bancos
    {
        public string id { get; set; }
        public string BancoNombre { get; set; }
        public string BancoCodigo { get; set; }
        public string bancoN { get; set; }
        //           Claves foráneas
        public string BancoMoneda { get; set; }

        //Métodos
        public void Menu()
        {
            int opc = 0;
            while (opc != 5)
            {
                Console.WriteLine("MENU DE BANCO");
                Console.WriteLine("¿Que desea hacer?");
                Console.WriteLine("1.- Mostrar Banco");
                Console.WriteLine("2.- Buscar Banco");
                Console.WriteLine("3.- Agregar Banco");
                Console.WriteLine("4.- Eliminar Banco");
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
                        MostrarBancos();
                        break;
                    case 2:
                        try
                        {
                            Console.WriteLine("Escriba el ID del banco que desea buscar:");
                            this.id = Console.ReadLine().ToString();

                            BuscarBancoID(id);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("El ID no ha sido ingresado correctamente, Error: " + ex.ToString());
                        }
                        break;
                    case 3:
                        try
                        {
                            Console.WriteLine("Escriba el nombre del banco:");
                            this.BancoNombre = Convert.ToString(Console.ReadLine());

                            Console.WriteLine("Escriba el codigo del banco:");
                            this.BancoCodigo = Convert.ToString(Console.ReadLine());

                            Console.WriteLine("¿Es nacional el banco? (Si/No):");
                            do
                            {
                                string BancoNacional = Convert.ToString(Console.ReadLine()).ToLower();
                                if (BancoNacional == "si")
                                {
                                    bancoN = "1";
                                    break;
                                }
                                else if (BancoNacional == "no")
                                {
                                    bancoN = "2";
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("Escriba 'si' o 'no'");
                                }
                            } while (true);
                            Console.WriteLine("Escriba la moneda principal del banco:");
                            this.BancoMoneda = Convert.ToString(Console.ReadLine());

                            if(BancoNombre.Equals("") || BancoCodigo.Equals("") || BancoMoneda.Equals(""))
                            {
                                Console.WriteLine("Los datos no han sido ingresados correctamente!");
                            } else
                            {
                                AgregarBanco(BancoNombre, BancoCodigo, bancoN, BancoMoneda);
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
                            Console.WriteLine("Escriba el ID del banco que desee eliminar:");
                            this.id = Console.ReadLine();

                            BorrarBanco(id);
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

        public static void MostrarBancos()
        {
            string connectionString = "Server=LENOVOLAP-RGS\\SQLEXPRESS;Database=BanksAccountsDB;Integrated Security=True;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM Bancos";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        Console.WriteLine("_____________________________________________________________________");
                        Console.WriteLine(" IDBanco | BancoNombre | BancoCodigo | BancoNacional | BancoMoneda");
                        while (reader.Read())
                        {
                            Console.WriteLine(reader[0].ToString() + "        | " + reader[1].ToString() + "    | " + reader[2].ToString()
                                + "     | " + reader[3].ToString() + "          | " + reader[4].ToString());
                        }
                        Console.WriteLine("_____________________________________________________________________");
                        Console.WriteLine("Fin de la tabla");
                    }
                    connection.Close();
                }
                catch (Exception ex) { Console.WriteLine("Ha ocurrido un error -> " + ex.Message); }
            }
        }

        public static void BuscarBancoID(string id)
        {
            string connectionString = "Server=LENOVOLAP-RGS\\SQLEXPRESS;Database=BanksAccountsDB;Integrated Security=True;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM Bancos where IDBanco = "+id;
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        Console.WriteLine("_____________________________________________________________________");
                        Console.WriteLine(" IDBanco | BancoNombre | BancoCodigo | BancoNacional | BancoMoneda");
                        while (reader.Read())
                        {
                            Console.WriteLine(reader[0].ToString() + "        | " + reader[1].ToString() + "    | " + reader[2].ToString()
                                + "     | " + reader[3].ToString() + "          | " + reader[4].ToString());
                        }
                        Console.WriteLine("_____________________________________________________________________");
                        Console.WriteLine("Fin de la tabla");
                    }
                    connection.Close();
                }
                catch (Exception ex) { Console.WriteLine("Ha ocurrido un error -> " + ex.Message); }
            }
        }
        public static void BorrarBanco(string id)
        {
            string connectionString = "Server=LENOVOLAP-RGS\\SQLEXPRESS;Database=BanksAccountsDB;Integrated Security=True;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "DELETE FROM Bancos where IDBanco = " + id;
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
        public static void AgregarBanco(string BancoNombre, string BancoCodigo, string BancoNacional, string BancoMoneda)
        {
            string connectionString = "Server=LENOVOLAP-RGS\\SQLEXPRESS;Database=BanksAccountsDB;Integrated Security=True;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "INSERT INTO Bancos (BancoNombre, BancoCodigo, BancoNacional, BancoMoneda) " +
                       "values ('" + BancoNombre + "','" + BancoCodigo + "','"+ BancoNacional +"','"+  BancoMoneda +"')";
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
