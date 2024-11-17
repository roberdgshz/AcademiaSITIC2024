using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace CuentasBancarias_Tranferencias
{
    internal class Movimientos
    {
        public string id { get; set; }
        public double MovimientoMonto { get; set; }
        public string RegistroFecha { get; set; }
        public string MovimientoConcepto { get; set; }
        //           Claves foráneas
        public long MovimientoCuentaEmisor { get; set; }
        public long MovimientoCuentaReceptor { get; set; }

        //Métodos
        public void Menu()
        {
            int opc = 0;
            while (opc != 4)
            {
                Console.WriteLine("MENU DE MOVIMIENTO");
                Console.WriteLine("¿Que desea hacer?");
                Console.WriteLine("1.- Mostrar Movimiento");
                Console.WriteLine("2.- Buscar Movimiento");
                Console.WriteLine("3.- Realizar Movimiento");
                //Console.WriteLine("4.- Eliminar movimiento");
                Console.WriteLine("4.- Volver");
                Console.WriteLine("Escriba el número de la opción que desee:");
                try
                {
                    opc = Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception ex) { Console.WriteLine("Escriba el número que representa cada opción. " + ex.Message); }
                switch (opc)
                {
                    case 1:
                        MostrarMovimientos();
                        break;
                    case 2:
                        try
                        {
                            Console.WriteLine("Escriba el ID del movimiento que desea buscar:");
                            this.id = Console.ReadLine();

                            BuscarMovimientoID(id);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("El ID no ha sido ingresado correctamente, Error: " + ex.ToString());
                        }
                        break;
                    case 3:
                        try
                        {
                            Console.WriteLine("Escriba el monto del movimiento:");
                            this.MovimientoMonto = Convert.ToInt64(Console.ReadLine());

                            Console.WriteLine("Escriba el concepto del movimiento:");
                            this.MovimientoConcepto = Console.ReadLine().ToString();

                            Console.WriteLine("Escriba la clave de la cuenta emisor:");
                            this.MovimientoCuentaEmisor = Convert.ToInt64(Console.ReadLine());

                            Console.WriteLine("Escriba la clave de la cuenta receptor:");
                            this.MovimientoCuentaReceptor = Convert.ToInt64(Console.ReadLine());

                            if(MovimientoMonto.Equals("") || MovimientoConcepto.Equals("") || MovimientoCuentaEmisor.Equals("") || MovimientoCuentaReceptor.Equals(""))
                            {
                                Console.WriteLine("Los datos no han sido ingresados correctamente!");
                            } else
                            {
                                if (ValidarSaldo(MovimientoMonto,MovimientoCuentaEmisor))
                                {
                                    if (ValidarMoneda(MovimientoCuentaEmisor, MovimientoCuentaReceptor))
                                    {
                                        AgregarMovimiento(MovimientoMonto, MovimientoConcepto, MovimientoCuentaEmisor, MovimientoCuentaReceptor);
                                        ActualizarCuentaSaldoEmisor(MovimientoMonto, MovimientoCuentaEmisor);
                                        ActualizarCuentaSaldoReceptor(MovimientoMonto, MovimientoCuentaReceptor);
                                        AgregarRegistro(MovimientoCuentaEmisor);

                                    } else
                                    {
                                        double comision = CalcularCambio(MovimientoMonto);
                                        AgregarCambio(comision);
                                        comision += MovimientoMonto;
                                        AgregarMovimiento(comision, MovimientoConcepto, MovimientoCuentaEmisor, MovimientoCuentaReceptor);
                                        ActualizarCuentaSaldoEmisor(MovimientoMonto, MovimientoCuentaEmisor);
                                        ActualizarCuentaSaldoReceptor(MovimientoMonto, MovimientoCuentaReceptor);
                                        AgregarRegistro(MovimientoCuentaEmisor);
                                    }
                                } else
                                {
                                    Console.WriteLine("No hay suficiente saldo para hacer la transferencia!");
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Los datos no han sido ingresados correctamente!, Error: " + ex.ToString());
                        }
                        break;
                    case 4:
                        break;
                    /*case 4444:
                        Console.WriteLine("Escriba el ID del movimiento que desee eliminar:");
                        this.id = Console.ReadLine().ToString();

                        BorrarMovimiento(id);
                        break;*/ //Esto es para pruebas!
                    default:
                        Console.WriteLine("Elija una opción disponible.");
                        break;
                }
                Console.WriteLine("-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_");
            }
        }

        public static void MostrarMovimientos()
        {
            string connectionString = "Server=LENOVOLAP-RGS\\SQLEXPRESS;Database=BanksAccountsDB;Integrated Security=True;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM Movimientos";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        Console.WriteLine("_________________________________________________________________________________________________________________________");
                        Console.WriteLine("IDMovimiento | MovimientoMonto | MovimientoFecha | MovimientoConcepto| MovimientoCuentaEmisor | MovimientoCuentaReceptor");
                        while (reader.Read())
                        {
                            Console.WriteLine(reader[0].ToString() + " | " + reader[1].ToString() + " | " + reader[2].ToString()
                                + " | " + reader[3].ToString()+" | " + reader[4].ToString()+" | " + reader[5].ToString());
                        }
                        Console.WriteLine("_________________________________________________________________________________________________________________________");
                        Console.WriteLine("Fin de la tabla");
                    }
                    connection.Close();
                }
                catch (Exception ex) { Console.WriteLine("Ha ocurrido un error -> " + ex.Message); }
            }
        }
        public static void BuscarMovimientoID(string id)
        {
            string connectionString = "Server=LENOVOLAP-RGS\\SQLEXPRESS;Database=BanksAccountsDB;Integrated Security=True;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM Movimientos where IDMovimiento = "+id;
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        Console.WriteLine("_________________________________________________________________________________________________________________________");
                        Console.WriteLine("IDMovimiento | MovimientoMonto | MovimientoFecha | MovimientoConcepto| MovimientoCuentaEmisor | MovimientoCuentaReceptor");
                        while (reader.Read())
                        {
                            Console.WriteLine(reader[0].ToString() + " | " + reader[1].ToString() + " | " + reader[2].ToString()
                                + " | " + reader[3].ToString() + " | " + reader[4].ToString() + " | " + reader[5].ToString());
                        }
                        Console.WriteLine("_________________________________________________________________________________________________________________________");
                        Console.WriteLine("Fin de la tabla");
                    }
                    connection.Close();
                }
                catch (Exception ex) { Console.WriteLine("Ha ocurrido un error -> " + ex.Message); }
            }
        }
        public static void BorrarMovimiento(string id)
        {
            string connectionString = "Server=LENOVOLAP-RGS\\SQLEXPRESS;Database=BanksAccountsDB;Integrated Security=True;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "DELETE FROM Movimientos where IDMovimiento = " + id;
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
        public static void AgregarMovimiento(double MovimientoMonto, string MovimientoConcepto, long MovimientoCuentaEmisor, long MovimientoCuentaReceptor)
        {
            string connectionString = "Server=LENOVOLAP-RGS\\SQLEXPRESS;Database=BanksAccountsDB;Integrated Security=True;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    DateTime MovimientoFecha = DateTime.Now;
                    string query = "INSERT INTO Movimientos (MovimientoMonto, MovimientoFecha, MovimientoConcepto, MovimientoCuentaEmisor, MovimientoCuentaReceptor) " +
                       "values (" + MovimientoMonto + ",'" + MovimientoFecha + "','" + MovimientoConcepto + "',"+ MovimientoCuentaEmisor +","+MovimientoCuentaReceptor+")";
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

        public static void ActualizarCuentaSaldoReceptor(double saldoAbono, long cuentaClave)
        {
            string connectionString = "Server=LENOVOLAP-RGS\\SQLEXPRESS;Database=BanksAccountsDB;Integrated Security=True;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT CuentaSaldo FROM Cuentas WHERE CuentaClave = " + cuentaClave;
                    double saldo = 0;
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            saldo = Convert.ToInt64(reader[0].ToString());
                        }
                    }
                    connection.Close();
                    saldo += saldoAbono;
                    connection.Open();
                    string query2 = "UPDATE Cuentas SET CuentaSaldo = "+saldo+" WHERE CuentaClave = "+cuentaClave;
                    using (SqlCommand command = new SqlCommand(query2, connection))
                    {
                        int rowsAffected = command.ExecuteNonQuery();
                        Console.WriteLine($"Filas afectadas: {rowsAffected}");
                    }
                    connection.Close();
                }
                catch (Exception ex) { Console.WriteLine(ex.Message); }
            }
        }

        public static void ActualizarCuentaSaldoEmisor(double saldoCargo, long cuentaClave)
        {
            string connectionString = "Server=LENOVOLAP-RGS\\SQLEXPRESS;Database=BanksAccountsDB;Integrated Security=True;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT CuentaSaldo FROM Cuentas WHERE CuentaClave = " + cuentaClave;
                    double saldo = 0;
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            saldo = Convert.ToInt64(reader[0].ToString());
                        }
                    }
                    connection.Close();
                    saldo -= saldoCargo;
                    connection.Open();
                    string query2 = "UPDATE Cuentas SET CuentaSaldo = " + saldo + " WHERE CuentaClave = " + cuentaClave;
                    using (SqlCommand command = new SqlCommand(query2, connection))
                    {
                        int rowsAffected = command.ExecuteNonQuery();
                        Console.WriteLine($"Filas afectadas: {rowsAffected}");
                    }
                    connection.Close();
                }
                catch (Exception ex) { Console.WriteLine(ex.Message); }
            }
        }

        public static bool ValidarSaldo(double MovimientoMonto,long MovimientoCuentaEmisor)
        {
            string connectionString = "Server=LENOVOLAP-RGS\\SQLEXPRESS;Database=BanksAccountsDB;Integrated Security=True;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT CuentaSaldo FROM Cuentas WHERE CuentaClave = " + MovimientoCuentaEmisor;
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        long saldo = 0;
                        while (reader.Read())
                        {
                            saldo = Convert.ToInt64(reader[0].ToString());
                        }
                        if (saldo >= MovimientoMonto)
                        {
                            return true;
                        }
                    }
                    connection.Close();
                } catch (Exception ex) { Console.WriteLine("Error ->" + ex.ToString()); }
            }
            return false;
        }

        public static void AgregarRegistro(long cuentaClave)
        {
            string connectionString = "Server=LENOVOLAP-RGS\\SQLEXPRESS;Database=BanksAccountsDB;Integrated Security=True;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    double RegistroSaldo = 0;
                    DateTime RegistroFecha = DateTime.Now;

                    string queryRegistroSaldo = "SELECT CuentaSaldo FROM Cuentas WHERE CuentaClave = " + cuentaClave;
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(queryRegistroSaldo, connection))
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            RegistroSaldo = Convert.ToDouble(reader[0].ToString());
                        }
                    }
                    connection.Close();

                    string query = "INSERT INTO Registros(RegistroSaldo,RegistroFecha,RegistroCuentaClave) " +
                        "VALUES ("+RegistroSaldo+",'"+RegistroFecha+"',"+cuentaClave+")";
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            int rowsAffected = command.ExecuteNonQuery();
                            Console.WriteLine($"Filas afectadas: {rowsAffected}");
                        }
                    }
                    connection.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error -> " + ex.ToString());
                }
            }
        }
        public static void AgregarCambio(double cambioTipo)
        {
            string connectionString = "Server=LENOVOLAP-RGS\\SQLEXPRESS;Database=BanksAccountsDB;Integrated Security=True;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    DateTime CambioFecha = DateTime.Now;
                    long IDMovimiento = 0;
                    connection.Open();
                    string queryIDMovimiento = "SELECT IDMovimiento FROM Movimientos WHERE MovimientoFecha LIKE '"+CambioFecha+"'";
                    using (SqlCommand command = new SqlCommand(queryIDMovimiento, connection))
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            IDMovimiento = Convert.ToInt64(reader[0].ToString());
                        }
                    }
                    connection.Close();

                    connection.Open();
                    string query = "INSERT INTO Cambios(CambioTipo, CambioFecha, CambioIDMovimiento) " +
                        "VALUES ("+cambioTipo+",'" + CambioFecha + "',"+IDMovimiento+")";
                    using (SqlCommand command = new SqlCommand(queryIDMovimiento, connection))
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            int rowsAffected = command.ExecuteNonQuery();
                            Console.WriteLine($"Filas afectadas: {rowsAffected}");
                        }
                    }
                    connection.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error -> " + ex.ToString());
                }
            }
        }
        public static double CalcularCambio(double saldoTransferido)
        {
            double cambio = (saldoTransferido * .035);
            return (cambio + saldoTransferido);
        }

        public static bool ValidarMoneda(long MovimientoCuentaEmisor, long MovimientoCuentaReceptor)
        {
            string connectionString = "Server=LENOVOLAP-RGS\\SQLEXPRESS;Database=BanksAccountsDB;Integrated Security=True;";
            
            string queryMonedaEmisor = "";
            string MonedaEmisor = "";

            string queryMonedaReceptor = "";
            string MonedaReceptor = "";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(queryMonedaEmisor, connection))
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            MonedaEmisor = reader[0].ToString();
                        }
                    }
                    connection.Close();

                    connection.Open();
                    using (SqlCommand command = new SqlCommand(queryMonedaReceptor, connection))
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            MonedaReceptor = reader[0].ToString();
                        }
                    }
                    connection.Close();

                    if (MonedaEmisor.Equals(MonedaEmisor))
                    {
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.ToString());
                }
            }
            return false;
        }
    }
}
