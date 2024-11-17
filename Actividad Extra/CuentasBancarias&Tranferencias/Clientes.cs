using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuentasBancarias_Tranferencias
{
    internal class Clientes
    {
        public string id { get; set; }
        public string ClienteNombre { get; set; }
        public string ClienteApellido { get; set; }
        public string ClienteRFC { get; set; }
        public string ClienteTelefono { get; set; }
        public string ClienteDireccion { get; set; }

        //Métodos
        public void Menu()
        {
            int opc = 0;
            while (opc != 5)
            {
                Console.WriteLine("MENU DE CLIENTE");
                Console.WriteLine("¿Que desea hacer?");
                Console.WriteLine("1.- Mostrar Cliente");
                Console.WriteLine("2.- Buscar Cliente");
                Console.WriteLine("3.- Agregar Cliente");
                Console.WriteLine("4.- Eliminar Cliente");
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
                        MostrarClientes();
                        break;
                    case 2:
                        try
                        {
                            Console.WriteLine("Escriba el ID del cliente que desea buscar:");
                            this.id = Console.ReadLine().ToString();

                            BuscarClienteID(id);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("El ID no ha sido ingresado correctamente, Error: " + ex.ToString());
                        }
                        break;
                    case 3:
                        try
                        {
                            Console.WriteLine("Escriba el nombre del cliente:");
                            this.ClienteNombre = Convert.ToString(Console.ReadLine());

                            Console.WriteLine("Escriba el apellido del cliente:");
                            this.ClienteApellido = Convert.ToString(Console.ReadLine());

                            Console.WriteLine("Escriba el RFC del cliente:");
                            this.ClienteRFC = Convert.ToString(Console.ReadLine());

                            Console.WriteLine("Escriba el telefono del cliente:");
                            this.ClienteTelefono = Convert.ToString(Console.ReadLine());

                            Console.WriteLine("Escribe la direccion del cliente:");
                            this.ClienteDireccion = Convert.ToString(Console.ReadLine());

                            if(ClienteNombre.Equals("") || ClienteApellido.Equals("") || ClienteRFC.Equals("") || ClienteDireccion.Equals(""))
                            {
                                Console.WriteLine("Los datos no han sido ingresados correctamente!");
                            } else
                            {
                                AgregarCliente(ClienteNombre, ClienteApellido, ClienteRFC, ClienteTelefono, ClienteDireccion);
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
                            Console.WriteLine("Escriba el ID del cliente que desee eliminar:");
                            this.id = Console.ReadLine().ToString();

                            BorrarCliente(id);
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

        public static void MostrarClientes()
        {
            string connectionString = "Server=LENOVOLAP-RGS\\SQLEXPRESS;Database=BanksAccountsDB;Integrated Security=True;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM Clientes";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        Console.WriteLine("____________________________________________________________________________________________________________");
                        Console.WriteLine(" IDCliente | ClienteNombre | ClienteApellido | ClienteRFC             | ClienteTelefono | ClienteDireccion");
                        while (reader.Read())
                        {
                            Console.WriteLine(reader[0].ToString() + "          | " + reader[1].ToString() + "       | " + reader[2].ToString()
                                + "        | " + reader[3].ToString() + "          | " + reader[4].ToString() + "       | "+ reader[5].ToString());
                        }
                        Console.WriteLine("____________________________________________________________________________________________________________");
                        Console.WriteLine("Fin de la tabla");
                    }
                    connection.Close();
                }
                catch (Exception ex) { Console.WriteLine("Ha ocurrido un error -> " + ex.Message); }
            }
        }

        public static void BuscarClienteID(string id)
        {
            string connectionString = "Server=LENOVOLAP-RGS\\SQLEXPRESS;Database=BanksAccountsDB;Integrated Security=True;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM Clientes where IDCliente = "+id;
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        Console.WriteLine("____________________________________________________________________________________________________________");
                        Console.WriteLine(" IDCliente | ClienteNombre | ClienteApellido | ClienteRFC             | ClienteTelefono | ClienteDireccion");
                        while (reader.Read())
                        {
                            Console.WriteLine(reader[0].ToString() + "          | " + reader[1].ToString() + "       | " + reader[2].ToString()
                                + "        | " + reader[3].ToString() + "          | " + reader[4].ToString() + "       | " + reader[5].ToString());
                        }
                        Console.WriteLine("____________________________________________________________________________________________________________");
                        Console.WriteLine("Fin de la tabla");
                    }
                    connection.Close();
                }
                catch (Exception ex) { Console.WriteLine("Ha ocurrido un error -> " + ex.Message); }
            }
        }
        public static void BorrarCliente(string id)
        {
            string connectionString = "Server=LENOVOLAP-RGS\\SQLEXPRESS;Database=BanksAccountsDB;Integrated Security=True;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "DELETE FROM Clientes where IDCliente = " + id;
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
        public static void AgregarCliente(string ClienteNombre, string ClienteApellido, string ClienteRFC, string ClienteTelefono, string ClienteDireccion)
        {
            string connectionString = "Server=LENOVOLAP-RGS\\SQLEXPRESS;Database=BanksAccountsDB;Integrated Security=True;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "INSERT INTO Clientes (ClienteNombre, ClienteApellido, ClienteRFC, ClienteTelefono, ClienteDireccion) " +
                       "values ('" + ClienteNombre + "','" + ClienteApellido + "','" + ClienteRFC + "','"+ ClienteTelefono +"','"+ ClienteDireccion +"')";
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
