using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercises
{
    class ErrorHandler
    {
        #region Properties
        private static List<User> UserList { get; set; } = GetUsers();
        #endregion

        #region Variables
        private const int MIN_AGE = 10;
        private const int MAX_AGE = 100;
        #endregion

        #region Classes
        public class User
        {
            #region Properties
            public int UserId { get; set; }
            public string Username { get; set; }
            public string Password { get; set; }
            #endregion

            #region Constructors
            public User() { }

            public User(int userId, string userName, string password)
            {
                UserId = userId;
                Username = userName;
                Password = password;
            }

            public User(string userName, string password)
            {
                Username = userName;
                Password = password;
            }
            #endregion
        }

        public class CustomAppException : Exception 
        {
            private eErrorType ErrorResponseEx { get; set; } = eErrorType.Ninguno;
            public CustomAppException(): base() { }

            public CustomAppException(string message, eErrorType type) : base(message)
            {
                ErrorResponseEx = type;
            }
        }

        public enum eErrorType
        {
            Ninguno = 0,
            Validacion,
            Conexion,
            InformacionDuplicada,
            Autenticacion,
            Desconocido = 99,
        }
        #endregion

        private static List<User> GetUsers()
        {
            return new()
            {
                new(1, "admin", "admin"),
                new(2, "sa", "12345"),
                new(3, "johndoe", "contrasena"),
                new(4, "miguel94", "19940707")
            };
        }

        public static void Main(string[] args)
        {
            const string username = "Tamaris";
            const string password = "admin";



            Console.ReadKey();
        }

        public static int RegisterUserWithoutValidations(string username, string password, string ageInput)
        {
            int userId,
                age;
            Console.WriteLine("Conexión a la base de datos");
            Console.WriteLine("Abrimos transacción");

            age = Convert.ToInt32(ageInput);

            Console.WriteLine("Ejecutamos acciones en la base de datos");

            if (!IsExistingUser(username))
                InsertUser(new(username, password));

            return 0;
        }

        public static void RegisterUserWithValidations(string username, string password, string ageInput)
        {
            Console.WriteLine("Iniciamos el proceso de registro de clientes");
            try
            {
                Console.WriteLine("Abrimos transacción");
                int age = Convert.ToInt32(ageInput);
                Console.WriteLine("Ejecutamos acciones en la base de datos");
                if (!IsExistingUser(username))
                    InsertUser(new(username, password));
                Console.WriteLine("Confirmo los camcios");

            }
            catch (CustomAppException ex)
            {
                Console.Write("Rollback: "+ex.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("Rollback: "+e);
            }
        }

        public static bool IsExistingUser(string username)
        {
            return UserList != null && UserList.Any(user => user.Username == username);
            //return (UserList?.Any(user => user.Username == username)).GetValueOrDefault();
        }

        public static bool InsertUser(User user)
        {
            user.UserId = UserList != null ? (UserList.Max(user => user.UserId) + 1) : 1;

            if (UserList == null)
                UserList = new();

            UserList.Add(user);

            #region Comment
            //if (UserList != null)
            //    UserList.Add(user);
            //else
            //{
            //    UserList = new();
            //    UserList.Add(user);
            //} 
            #endregion

            Console.WriteLine("Acción ejecutada en base de datos => Usuario insertado correctamente");
            return true;
        }

        public static int ValidateAge(string ageInput)
        {
            if (!int.TryParse(ageInput, out int age))
                throw new CustomAppException("La edad viene en un formato incorrecto!",eErrorType.Validacion);
            if (age < MIN_AGE || age > MAX_AGE)
                throw new CustomAppException($"La edad debe estar entre {MIN_AGE} y {MAX_AGE} años",eErrorType.Validacion);
            return age;
        }
    }
}
