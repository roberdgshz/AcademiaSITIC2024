using System;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;

using System.Data.SqlClient;

namespace CuentasBancarias_Tranferencias
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "Server=LENOVOLAP-RGS\\SQLEXPRESS;Database=BanksAccountsDB;Integrated Security=True;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                Console.WriteLine("Conexión...");
                try
                {
                    connection.Open();
                    Console.WriteLine("Conexión exitosa!");
                    string query = "SELECT * FROM Cuentas";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            Console.WriteLine(reader[0].ToString()+' '+ reader[1].ToString()+ ' ' + reader[2].ToString()
                                + ' ' + reader[3].ToString()+ ' ' + reader[4].ToString()+ ' ' + reader[5].ToString()+ ' ' + reader[6].ToString());
                        }
                    }
                }
                catch (Exception ex) { Console.WriteLine("Error: " + ex.Message); }
            }
        }
    }
}
