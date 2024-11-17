using System;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;

namespace CuentasBancarias_Tranferencias
{
    internal class Program
    {
        enum opciones{
            Bancos,
            Clientes,
            Cuentas,
            Monedas,
            Cambios,
            Movimientos,
            Registros,
            Ejercicios,
            Info,
            Salir
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Bienvenido a la aplicación de Cuentas bancarias y transferencias");
            int option = 0;
            while (option != 10)
            {
                Console.WriteLine("Elija alguna de las siguientes opciones:");
                Console.WriteLine("1.- " + opciones.Bancos);
                Console.WriteLine("2.- " + opciones.Clientes);
                Console.WriteLine("3.- " + opciones.Cuentas);
                Console.WriteLine("4.- " + opciones.Monedas);
                Console.WriteLine("5.- " + opciones.Cambios);
                Console.WriteLine("6.- " + opciones.Movimientos);
                Console.WriteLine("7.- " + opciones.Registros);
                Console.WriteLine("8.- " + opciones.Ejercicios);
                Console.WriteLine("9.- " + opciones.Info);
                Console.WriteLine("10.- " + opciones.Salir);
                try
                {
                    option = Convert.ToInt32(Console.ReadLine());
                }
                catch(Exception ex) { Console.WriteLine("Escriba el número que representa cada opción. "+ex.Message); }
                switch (option) 
                {
                    case 1:
                        Bancos bancos = new Bancos();
                        bancos.Menu();
                        break;
                    case 2:
                        Clientes clientes = new Clientes();
                        clientes.Menu();
                        break;
                    case 3:
                        Cuentas cuentas = new Cuentas();
                        cuentas.Menu();
                        break;
                    case 4:
                        Monedas monedas = new Monedas();
                        monedas.Menu();
                        break;
                    case 5:
                        Cambios cambios = new Cambios();
                        cambios.Menu();
                        break;
                    case 6:
                        Movimientos movimientos = new Movimientos();
                        movimientos.Menu();
                        break;
                    case 7:
                        Registros registros = new Registros();
                        registros.Menu();
                        break;
                    case 8:
                        Ejercicios ejercicios = new Ejercicios();
                        ejercicios.Menu();
                        break;
                    case 9:
                        Console.WriteLine("Este proyecto pertenece al curso de SITIC 2024 como actividad complementaria adicional");
                        Console.WriteLine("con fines educativos y de práctica.");
                        Console.WriteLine("Realizado por Roberto Grijalva Sánchez.");
                        Console.WriteLine(".NET -> 5.0");
                        Console.WriteLine("DB   -> SQL Server");
                        break;
                    case 10:
                        Console.WriteLine("Hasta la próxima!");
                        Console.WriteLine("Fin del programa...");
                        break;
                    default:
                        Console.WriteLine("Escriba el número correspondiente por favor.");
                        break;
                }
                Console.WriteLine("------------------------------------------");
            }
        }
    }
}
