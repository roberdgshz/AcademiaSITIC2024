using System;

namespace Exercises
{
    internal class Program
    {
        static void Main2(string[] args)
        {
            string courseName = "Academia SITIC";
            string courseName2 = "Academia SITIC 2";

            int studentCount = 28;
            bool isStartingNow = true;

            int? age = null;

            //Console.WriteLine(age.Value);
            Console.WriteLine(age.GetValueOrDefault(0));
            
            User user = new User();
            Employer emp = new Employer();
            //user.
            
            Console.ReadLine();
        }

        public class User
        {
            /*
            //forma corta
            public int IdUser { get; set; }

            //forma media
            private string _password;

            public string Password
            {
                get
                {
                    return _password;
                }
                set
                {
                    _password = value;
                }
            }


            //forma  larga
            private string _name;

            public string GetName()
            {
                return _name;
            }
            public void SetName(string nombre)
            {
                _name = nombre;
            }*/
            private int _idUser;
            private string _name;
            private string _password;
        }

        public class Person
        {
            public int IdPerson { get; set; }
            public string Name { get; set; }
        }

        public class Employer : Person
        {
            public int IdEmployer { get; set; }
        }
    }
}
