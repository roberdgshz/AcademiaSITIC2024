using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuentasBancarias_Tranferencias
{
    internal class Ejercicios
    {
        public void Menu()
        {
            string connectionString = "Server=LENOVOLAP-RGS\\SQLEXPRESS;Database=BanksAccountsDB;Integrated Security=True;";
            int opc = 0;
            while (opc != 15)
            {
                Console.WriteLine("Elija el ejercicio a probar: ");
                Console.WriteLine("1.- Obtener todas las cuentas bancarias específicas por cliente.");
                Console.WriteLine("2.- Mostrar los clientes que tienen cuentas bancarias en diferentes bancos.");
                Console.WriteLine("3.- Obtener el saldo total de un cliente en sus diferentes bancos. (un registro por cada banco)");
                Console.WriteLine("4.- Filtrar tansferencias realizadas entre cuentas realizadas por distintos bancos.");
                Console.WriteLine("5.- Agrupar los saldos pormedio de distintos bancos");
                Console.WriteLine("6.- Mostrar todas las cuentas bancarias con saldo 0, agrupadas por bancos.");
                Console.WriteLine("7.- Listar transferencias realizadas entre un rango de fechas. (todos los bancos)");
                Console.WriteLine("8.- Listar transferencias realizadas entre un rango de fechas, agrupandolas por bancos.");
                Console.WriteLine("9.- Agrupa las cuentas con saldos 0 y las que tienen más de 0.");
                Console.WriteLine("10.- Agrupar los movimientos por fecha.");
                Console.WriteLine("11.- Mostrar todos los clientes y la sumatoria de sus saldos por banco.");
                Console.WriteLine("12.- Devolver los 3 clientes que tienen más saldos en sus cuentas.");
                Console.WriteLine("13.- Distingue y agrupa bancos nacionales e internacionales.");
                Console.WriteLine("14.- Distingue transferencias internacionales.");
                Console.WriteLine("15.- Volver.");
                try
                {
                    opc = Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception ex) { Console.WriteLine("Escriba el número que representa cada opción. " + ex.Message); }
                switch (opc)
                {
                    case 1: 
                        string query1 = "SELECT Clientes.IDCliente, Clientes.ClienteNombre, Cuentas.IDCuenta, Cuentas.CuentaClave, Cuentas.CuentaSaldo " +
                            "FROM Clientes " +
                            "JOIN Cuentas " +
                            "ON Clientes.IDCliente = Cuentas.CuentaIDCliente;";
                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            try
                            {
                                connection.Open();
                                using (SqlCommand command = new SqlCommand(query1, connection))
                                {
                                    SqlDataReader reader = command.ExecuteReader();
                                    Console.WriteLine("___________________________________________________________________");
                                    Console.WriteLine(" IDCliente | ClienteNombre | IDCuenta | CuentaClave | CuentaSaldo |");
                                    while (reader.Read())
                                    {
                                        Console.WriteLine(reader[0].ToString() + "          | " + reader[1].ToString() + "       | " + reader[2].ToString()
                                            + "        | " + reader[3].ToString() + "           | " + reader[4].ToString() + "       | ");
                                    }
                                    Console.WriteLine("___________________________________________________________________");
                                    Console.WriteLine("Fin de la tabla");
                                }
                                connection.Close();
                            }
                            catch (Exception ex) { Console.WriteLine("Ha ocurrido un error -> " + ex.Message); }
                        }
                        break;
                    case 2: 
                        string query2 = "SELECT Clientes.IDCliente, Clientes.ClienteNombre, COUNT(DISTINCT Cuentas.CuentaBancoClave) " +
                            "AS BancosDiferentes" +
                            " FROM Clientes " +
                            "JOIN Cuentas " +
                            "ON Clientes.IDCliente = Cuentas.CuentaIDCliente " +
                            "GROUP BY Clientes.IDCliente, Clientes.ClienteNombre " +
                            "HAVING COUNT(DISTINCT Cuentas.CuentaBancoClave) > 1;";
                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            try
                            {
                                connection.Open();
                                using (SqlCommand command = new SqlCommand(query2, connection))
                                {
                                    SqlDataReader reader = command.ExecuteReader();
                                    Console.WriteLine("_____________________________________________");
                                    Console.WriteLine(" IDCliente | ClienteNombre | Cantidad |");
                                    while (reader.Read())
                                    {
                                        Console.WriteLine(reader[0].ToString() + "          | " + reader[1].ToString() + "       | " + reader[2].ToString()
                                            + "        | ");
                                    }
                                    Console.WriteLine("_____________________________________________");
                                    Console.WriteLine("Fin de la tabla");
                                }
                                connection.Close();
                            }
                            catch (Exception ex) { Console.WriteLine("Ha ocurrido un error -> " + ex.Message); }
                        }
                        break;
                    case 3:
                        string query3 = "SELECT Clientes.IDCliente, Clientes.ClienteNombre, Cuentas.CuentaBancoClave, SUM(Cuentas.CuentaSaldo) " +
                            "AS SaldoTotal " +
                            "FROM Clientes " +
                            "JOIN Cuentas " +
                            "ON Clientes.IDCliente = Cuentas.CuentaIDCliente " +
                            "GROUP BY Clientes.IDCliente, Clientes.ClienteNombre, Cuentas.CuentaBancoClave;";
                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            try
                            {
                                connection.Open();
                                using (SqlCommand command = new SqlCommand(query3, connection))
                                {
                                    SqlDataReader reader = command.ExecuteReader();
                                    Console.WriteLine("_______________________________________________________");
                                    Console.WriteLine(" IDCliente | ClienteNombre | CuentaBancoClave | Suma  |");
                                    while (reader.Read())
                                    {
                                        Console.WriteLine(reader[0].ToString() + "          | " + reader[1].ToString() + "       | " + reader[2].ToString()
                                            + "          | " + reader[3].ToString() + " |");
                                    }
                                    Console.WriteLine("__________________________________________________________");
                                    Console.WriteLine("Fin de la tabla");
                                }
                                connection.Close();
                            }
                            catch (Exception ex) { Console.WriteLine("Ha ocurrido un error -> " + ex.Message); }
                        }
                        break;
                    case 4:
                        string query4 = "SELECT * " +
                            "FROM Movimientos " +
                            "JOIN Cuentas " +
                            "AS Emisor " +
                            "ON Movimientos.MovimientoCuentaEmisor = Emisor.CuentaClave " +
                            "JOIN Cuentas " +
                            "AS Receptor " +
                            "ON Movimientos.MovimientoCuentaReceptor = Receptor.CuentaClave " +
                            "WHERE Emisor.CuentaBancoClave <> Receptor.CuentaBancoClave;";
                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            try
                            {
                                connection.Open();
                                using (SqlCommand command = new SqlCommand(query4, connection))
                                {
                                    SqlDataReader reader = command.ExecuteReader();
                                    Console.WriteLine("___________________________________________________________________________________________________________________________");
                                    Console.WriteLine("IDMovimiento | MovimientoMonto | MovimientoFecha | MovimientoConcepto | MovimientoCuentaEmisor | MovimientoCuentaReceptor |");
                                    while (reader.Read())
                                    {
                                        Console.WriteLine(reader[0].ToString() + " | " + reader[1].ToString() + " | " + reader[2].ToString()
                                            + " | " + reader[3].ToString() + " | " + reader[4].ToString() + " | " + reader[5].ToString() + " | ");
                                    }
                                    Console.WriteLine("________________________________________________________________________________________________________________________");
                                    Console.WriteLine("Fin de la tabla");
                                }
                                connection.Close();
                            }
                            catch (Exception ex) { Console.WriteLine("Ha ocurrido un error -> " + ex.Message); }
                        }
                        break;
                    case 5:
                        string query5 = "SELECT Cuentas.CuentaBancoClave, AVG(Cuentas.CuentaSaldo) " +
                            "AS SaldoPromedio FROM Cuentas GROUP BY Cuentas.CuentaBancoClave;";
                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            try
                            {
                                connection.Open();
                                using (SqlCommand command = new SqlCommand(query5, connection))
                                {
                                    SqlDataReader reader = command.ExecuteReader();
                                    Console.WriteLine("_______________________________________________");
                                    Console.WriteLine("CuentaBancoClave | PromedioCuentaSaldo | ");
                                    while (reader.Read())
                                    {
                                        Console.WriteLine(reader[0].ToString() + "          | " + reader[1].ToString() + "  | ");
                                    }
                                    Console.WriteLine("_______________________________________________");
                                    Console.WriteLine("Fin de la tabla");
                                }
                                connection.Close();
                            }
                            catch (Exception ex) { Console.WriteLine("Ha ocurrido un error -> " + ex.Message); }
                        }
                        break;
                    case 6:
                        string query6 = "SELECT Cuentas.CuentaBancoClave, COUNT(Cuentas.IDCuenta) " +
                            "AS CuentasSaldoCero " +
                            "FROM Cuentas " +
                            "WHERE Cuentas.CuentaSaldo = 0 " +
                            "GROUP BY Cuentas.CuentaBancoClave;";
                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            try
                            {
                                connection.Open();
                                using (SqlCommand command = new SqlCommand(query6, connection))
                                {
                                    SqlDataReader reader = command.ExecuteReader();
                                    Console.WriteLine("_________________________________________");
                                    Console.WriteLine("CuentaBancoClave | Cantidad de Cuentas |");
                                    while (reader.Read())
                                    {
                                        Console.WriteLine(reader[0].ToString() + "         | " + reader[1].ToString() +" | ");
                                    }
                                    Console.WriteLine("_________________________________________");
                                    Console.WriteLine("Fin de la tabla");
                                }
                                connection.Close();
                            }
                            catch (Exception ex) { Console.WriteLine("Ha ocurrido un error -> " + ex.Message); }
                        }
                        break;
                    case 7:
                        string query7 = "SELECT * FROM Movimientos " +
                            "WHERE MovimientoFecha BETWEEN '2024-01-01' AND '2024-12-31';";
                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            try
                            {
                                connection.Open();
                                using (SqlCommand command = new SqlCommand(query7, connection))
                                {
                                    SqlDataReader reader = command.ExecuteReader();
                                    Console.WriteLine("____________________________________________________________________________________________________________________________");
                                    Console.WriteLine("IDMovimiento | MovimientoMonto | MovimientoFecha |  MovimientoConcepto | MovimientoCuentaEmisor | MovimientoCuentaReceptor |");
                                    while (reader.Read())
                                    {
                                        Console.WriteLine(reader[0].ToString() + " | " + reader[1].ToString() + " | " + reader[2].ToString() + " | " + reader[3].ToString() + " | " + 
                                            reader[4].ToString() + " | "+ reader[5].ToString());
                                    }
                                    Console.WriteLine("____________________________________________________________________________________________________________________________");
                                    Console.WriteLine("Fin de la tabla");
                                }
                                connection.Close();
                            }
                            catch (Exception ex) { Console.WriteLine("Ha ocurrido un error -> " + ex.Message); }
                        }
                        break;
                    case 8:
                        string query8 = "SELECT Movimientos.MovimientoFecha, Cuentas.CuentaBancoClave, COUNT(*) " +
                            "AS NumeroTransferencias " +
                            "FROM Movimientos " +
                            "JOIN Cuentas ON Movimientos.MovimientoCuentaEmisor = Cuentas.CuentaClave " +
                            "WHERE Movimientos.MovimientoFecha BETWEEN '2024-01-01' AND '2024-12-31' " +
                            "GROUP BY Movimientos.MovimientoFecha, Cuentas.CuentaBancoClave;";
                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            try
                            {
                                connection.Open();
                                using (SqlCommand command = new SqlCommand(query8, connection))
                                {
                                    SqlDataReader reader = command.ExecuteReader();
                                    Console.WriteLine("________________________________________________");
                                    Console.WriteLine("MovimientoFecha | CuentaBancoClave | Cantidad |");
                                    while (reader.Read())
                                    {
                                        Console.WriteLine(reader[0].ToString() + " | " + reader[1].ToString() + " | " + reader[2].ToString() +" |");
                                    }
                                    Console.WriteLine("________________________________________________");
                                    Console.WriteLine("Fin de la tabla");
                                }
                                connection.Close();
                            }
                            catch (Exception ex) { Console.WriteLine("Ha ocurrido un error -> " + ex.Message); }
                        }
                        break;
                    case 9:
                        string query9 = "SELECT " +
                            "CASE WHEN CuentaSaldo = 0 THEN 'Saldo Cero' " +
                            "ELSE 'Saldo Mayor a Cero' " +
                            "END AS TipoSaldo, COUNT(*) AS NumeroCuentas " +
                            "FROM Cuentas " +
                            "GROUP BY CASE WHEN CuentaSaldo = 0 THEN 'Saldo Cero' " +
                            "ELSE 'Saldo Mayor a Cero' END;";
                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            try
                            {
                                connection.Open();
                                using (SqlCommand command = new SqlCommand(query9, connection))
                                {
                                    SqlDataReader reader = command.ExecuteReader();
                                    Console.WriteLine("____________________________________");
                                    Console.WriteLine("TipoSaldo          | NumeroCuentas |");
                                    while (reader.Read())
                                    {
                                        Console.WriteLine(reader[0].ToString() + " | " + reader[1].ToString() + "            | ");
                                    }
                                    Console.WriteLine("____________________________________");
                                    Console.WriteLine("Fin de la tabla");
                                }
                                connection.Close();
                            }
                            catch (Exception ex) { Console.WriteLine("Ha ocurrido un error -> " + ex.Message); }
                        }
                        break;
                    case 10:
                        string query10 = "SELECT MovimientoFecha, COUNT(*) " +
                            "AS TotalMovimientos " +
                            "FROM Movimientos " +
                            "GROUP BY MovimientoFecha;";
                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            try
                            {
                                connection.Open();
                                using (SqlCommand command = new SqlCommand(query10, connection))
                                {
                                    SqlDataReader reader = command.ExecuteReader();
                                    Console.WriteLine("___________________________________________");
                                    Console.WriteLine("MovimientoFecha | Cantidad de Movimientos |");
                                    while (reader.Read())
                                    {
                                        Console.WriteLine(reader[0].ToString() + " | " + reader[1].ToString() + " | ");
                                    }
                                    Console.WriteLine("___________________________________________");
                                    Console.WriteLine("Fin de la tabla");
                                }
                                connection.Close();
                            }
                            catch (Exception ex) { Console.WriteLine("Ha ocurrido un error -> " + ex.Message); }
                        }
                        break;
                    case 11:
                        string query11 = "SELECT Clientes.IDCliente, Clientes.ClienteNombre, Cuentas.CuentaBancoClave, SUM(Cuentas.CuentaSaldo) " +
                            "AS TotalSaldo " +
                            "FROM Clientes " +
                            "JOIN Cuentas " +
                            "ON Clientes.IDCliente = Cuentas.CuentaIDCliente " +
                            "GROUP BY Clientes.IDCliente, Clientes.ClienteNombre, Cuentas.CuentaBancoClave;";
                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            try
                            {
                                connection.Open();
                                using (SqlCommand command = new SqlCommand(query11, connection))
                                {
                                    SqlDataReader reader = command.ExecuteReader();
                                    Console.WriteLine("__________________________________________________________");
                                    Console.WriteLine("IDCliente | ClienteNombre | CuentaBancoClave | Sumatoria |");
                                    while (reader.Read())
                                    {
                                        Console.WriteLine(reader[0].ToString() + " | " + reader[1].ToString() + " | " + reader[2].ToString() +" | "+ reader[3].ToString() + " | ");
                                    }
                                    Console.WriteLine("__________________________________________________________");
                                    Console.WriteLine("Fin de la tabla");
                                }
                                connection.Close();
                            }
                            catch (Exception ex) { Console.WriteLine("Ha ocurrido un error -> " + ex.Message); }
                        }
                        break;
                    case 12:
                        string query12 = "SELECT TOP 3 Clientes.IDCliente, Clientes.ClienteNombre, SUM(Cuentas.CuentaSaldo) " +
                            "AS TotalSaldo " +
                            "FROM Clientes " +
                            "JOIN Cuentas " +
                            "ON Clientes.IDCliente = Cuentas.CuentaIDCliente " +
                            "GROUP BY Clientes.IDCliente, Clientes.ClienteNombre " +
                            "ORDER BY TotalSaldo DESC;";
                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            try
                            {
                                connection.Open();
                                using (SqlCommand command = new SqlCommand(query12, connection))
                                {
                                    SqlDataReader reader = command.ExecuteReader();
                                    Console.WriteLine("_______________________________________");
                                    Console.WriteLine("IDCliente | ClienteNombre | Sumatoria |");
                                    while (reader.Read())
                                    {
                                        Console.WriteLine(reader[0].ToString() + " | " + reader[1].ToString() + " | " + reader[2].ToString() +" | ");
                                    }
                                    Console.WriteLine("_______________________________________");
                                    Console.WriteLine("Fin de la tabla");
                                }
                                connection.Close();
                            }
                            catch (Exception ex) { Console.WriteLine("Ha ocurrido un error -> " + ex.Message); }
                        }
                        break;
                    case 13:
                        string query13 = "SELECT BancoNacional, COUNT(*) " +
                            "AS NumeroBancos " +
                            "FROM Bancos " +
                            "GROUP BY BancoNacional;";
                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            try
                            {
                                connection.Open();
                                using (SqlCommand command = new SqlCommand(query13, connection))
                                {
                                    SqlDataReader reader = command.ExecuteReader();
                                    Console.WriteLine("__________________________");
                                    Console.WriteLine("BancoNacional | Cantidad |");
                                    while (reader.Read())
                                    {
                                        Console.WriteLine(reader[0].ToString() + " | " + reader[1].ToString() + " | ");
                                    }
                                    Console.WriteLine("__________________________");
                                    Console.WriteLine("Fin de la tabla");
                                }
                                connection.Close();
                            }
                            catch (Exception ex) { Console.WriteLine("Ha ocurrido un error -> " + ex.Message); }
                        }
                        break;
                    case 14:
                        string query14 = "SELECT * FROM Movimientos " +
                            "JOIN Cuentas AS Emisor ON Movimientos.MovimientoCuentaEmisor = Emisor.CuentaClave " +
                            "JOIN Cuentas AS Receptor ON Movimientos.MovimientoCuentaReceptor = Receptor.CuentaClave " +
                            "JOIN Bancos AS BancoEmisor ON Emisor.CuentaBancoClave = BancoEmisor.BancoCodigo " +
                            "JOIN Bancos AS BancoReceptor ON Receptor.CuentaBancoClave = BancoReceptor.BancoCodigo " +
                            "WHERE BancoEmisor.BancoNacional <> BancoReceptor.BancoNacional;";
                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            try
                            {
                                connection.Open();
                                using (SqlCommand command = new SqlCommand(query14, connection))
                                {
                                    SqlDataReader reader = command.ExecuteReader();
                                    Console.WriteLine("___________________________________________________________________________________________________________________________");
                                    Console.WriteLine("IDMovimiento | MovimientoMonto | MovimientoFecha | MovimientoConcepto | MovimientoCuentaEmisor | MovimientoCuentaReceptor |");
                                    while (reader.Read())
                                    {
                                        Console.WriteLine(reader[0].ToString() + " | " + reader[1].ToString() + " | " + reader[2].ToString() + " | " + reader[3].ToString() + " | " + reader[4].ToString() + " | " + reader[5].ToString() +" |");
                                    }
                                    Console.WriteLine("___________________________________________________________________________________________________________________________");
                                    Console.WriteLine("Fin de la tabla");
                                }
                                connection.Close();
                            }
                            catch (Exception ex) { Console.WriteLine("Ha ocurrido un error -> " + ex.Message); }
                        }
                        break;
                    case 15:
                        break;
                    default:
                        Console.WriteLine("Elija una opción disponible.");
                        break;
                }
            }
        }
    }
}
