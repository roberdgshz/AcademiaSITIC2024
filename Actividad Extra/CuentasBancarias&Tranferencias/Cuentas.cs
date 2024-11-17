using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuentasBancarias_Tranferencias
{
    internal class Cuentas
    {
        public string id {  get; set; }
        public long CuentaClave { get; set; }
        public double CuentaSaldo { get; set; }
        public string CuentaCorreo { get; set; }
        //           Claves foráneas
        public long CuentaIDCliente { get; set; }
        public string CuentaBancoClave { get; set; }
        public string CuentaMonedaClave { get; set; }

        //Métodos
        public void Menu()
        {
            int opc = 0;
            while (opc != 5)
            {
                Console.WriteLine("MENU DE CUENTA");
                Console.WriteLine("¿Que desea hacer?");
                Console.WriteLine("1.- Mostrar Cuenta");
                Console.WriteLine("2.- Buscar Cuenta");
                Console.WriteLine("3.- Agregar Cuenta");
                Console.WriteLine("4.- Eliminar Cuenta");
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
                        MostrarCuentas();
                        break;
                    case 2:
                        try
                        {
                            Console.WriteLine("Escriba el ID de la cuenta que desea buscar:");
                            this.id = Console.ReadLine().ToString();

                            BuscarCuentaID(id);
                        }
                        catch(Exception ex)
                        {
                            Console.WriteLine("El ID no ha sido ingresado correctamente, Error: " + ex.ToString());
                        }
                        break;
                    case 3:
                        try
                        {
                            Console.WriteLine("Escriba la clave de la cuenta:");
                            this.CuentaClave = Convert.ToInt64(Console.ReadLine());

                            Console.WriteLine("Escriba el saldo de la cuenta:");
                            this.CuentaSaldo = Convert.ToSingle(Console.ReadLine());

                            Console.WriteLine("Escriba el correo de la cuenta:");
                            this.CuentaCorreo = Convert.ToString(Console.ReadLine());

                            Console.WriteLine("Escriba el id del cliente:");
                            this.CuentaIDCliente = Convert.ToInt64(Console.ReadLine());

                            Console.WriteLine("Escriba la clave del banco:");
                            this.CuentaBancoClave = Convert.ToString(Console.ReadLine());

                            Console.WriteLine("Escriba la moneda de la cuenta:");
                            this.CuentaMonedaClave = Convert.ToString(Console.ReadLine());

                            if(CuentaClave.Equals("") || CuentaSaldo.Equals("") || CuentaCorreo.Equals("") || CuentaIDCliente.Equals("") || 
                                CuentaBancoClave.Equals("") || CuentaMonedaClave.Equals(""))
                            {
                                Console.WriteLine("Los datos no han sido ingresados correctamente!");
                            } else
                            {
                                AgregarCuenta(CuentaClave, CuentaSaldo, CuentaCorreo, CuentaIDCliente, CuentaBancoClave, CuentaMonedaClave);
                            }
                        } catch(Exception ex)
                        {
                            Console.WriteLine("Los datos no han sido ingresados correctamente!, Error: " + ex.ToString());
                        }
                        break;
                    case 4:
                        try
                        {
                            Console.WriteLine("Escriba el ID de la cuentao que desee eliminar:");
                            this.id = Console.ReadLine().ToString();

                            BorrarCuenta(id);
                        }
                        catch(Exception ex)
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

        public static void MostrarCuentas()
        {
            string connectionString = "Server=LENOVOLAP-RGS\\SQLEXPRESS;Database=BanksAccountsDB;Integrated Security=True;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM Cuentas";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        Console.WriteLine("________________________________________________________________________________________________________________________");
                        Console.WriteLine(" IDCuenta | CuentaClave | CuentaSaldo | CuentaCorreo           | CuentaIDCliente | CuentaBancoClave | CuentaMonedaClave");
                        while (reader.Read())
                        {
                            Console.WriteLine(reader[0].ToString() + "         | " + reader[1].ToString() + "           | " + reader[2].ToString()
                                + "       | " + reader[3].ToString() + " | " + reader[4].ToString() + "               | " + reader[5].ToString() + "          | " + reader[6].ToString());
                        }
                        Console.WriteLine("________________________________________________________________________________________________________________________");
                        Console.WriteLine("Fin de la tabla");
                    }
                    connection.Close();
                }
                catch (Exception ex) { Console.WriteLine("Ha ocurrido un error -> " + ex.Message); }
            }
        }

        public static void BuscarCuentaID(string id)
        {
            string connectionString = "Server=LENOVOLAP-RGS\\SQLEXPRESS;Database=BanksAccountsDB;Integrated Security=True;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM Cuentas where IDCuenta = "+id;
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        Console.WriteLine("________________________________________________________________________________________________________________________");
                        Console.WriteLine(" IDCuenta | CuentaClave | CuentaSaldo | CuentaCorreo           | CuentaIDCliente | CuentaBancoClave | CuentaMonedaClave");
                        while (reader.Read())
                        {
                            Console.WriteLine(reader[0].ToString() + "         | " + reader[1].ToString() + "           | " + reader[2].ToString()
                                + "       | " + reader[3].ToString() + " | " + reader[4].ToString() + "               | " + reader[5].ToString() + "          | " + reader[6].ToString());
                        }
                        Console.WriteLine("________________________________________________________________________________________________________________________");
                        Console.WriteLine("Fin de la tabla");
                    }
                    connection.Close();
                }
                catch (Exception ex) { Console.WriteLine("Ha ocurrido un error -> " + ex.Message); }
            }
        }
        public static void BorrarCuenta(string id)
        {
            string connectionString = "Server=LENOVOLAP-RGS\\SQLEXPRESS;Database=BanksAccountsDB;Integrated Security=True;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "DELETE FROM Cuentas where IDCuenta = " + id;
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
        public static void AgregarCuenta(long CuentaClave, double CuentaSaldo, string CuentaCorreo, long CuentaIDCliente, string CuentaBancoClave, string CuentaMonedaClave)
        {
            string connectionString = "Server=LENOVOLAP-RGS\\SQLEXPRESS;Database=BanksAccountsDB;Integrated Security=True;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "INSERT INTO Cuentas (CuentaClave, CuentaSaldo, CuentaCorreo, CuentaIDCliente, CuentaBancoClave, CuentaMonedaClave) " +
                       "values ('" + CuentaClave + "','" + CuentaSaldo + "','" + CuentaCorreo + "','"+CuentaIDCliente+"','"+CuentaBancoClave+"','"+CuentaMonedaClave+"')";
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
