using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Exercises
{
    class Programa
    {
        static void Main(string[] args) {
            List<People> employeers = new() 
            { 
                new People
                {
                    Name = "María",
                    Age = 18,
                    Gender = eGender.Female
                },
                new People
                {
                    Name = "Andrea",
                    Age = 25,
                    Gender = eGender.Female
                },
            };

            employeers.Add(new("Ricardo", 33, eGender.Male));
            employeers.Add(new("Elias", 21, eGender.Male));
            employeers.Add(new("Marcos", 21, eGender.Male));
            employeers.Add(new("Cristian", 23, eGender.Male));
            employeers.Add(new("Layla", 22, eGender.Female));
            employeers.Add(new("Oswaldo", 31, eGender.Male));
            employeers.Add(new("Ulloa", 32, eGender.Male));
            employeers.Add(new("Gil", 33, eGender.Male));


            foreach (var item in employeers)
            {
                Console.WriteLine($"Nombre : {item.Name} ");
            }

            Console.WriteLine("-------------------------------------------------------");

            List<People> students = new()
            {
                new("Iris",23,eGender.Female),
                new("Jesus",26,eGender.Male),
                new("Andres",33,eGender.Male),
                new("America",22,eGender.Female),
                new("Paolas",23,eGender.Female),
                new("Christian",22,eGender.Male),
            };

            #region Where
            Console.WriteLine("\nWHERE - Filtrar nombres que comiencen con la letra 'A'");
            List<People> filteredEmployeers = employeers.Where(employeer => employeer.Name.StartsWith('A')).ToList();
            foreach(People filteredEmployer in filteredEmployeers)
            {
                Console.WriteLine(filteredEmployer.Name);
            }

            Console.WriteLine("\nWHERE - Filtrar empleados que tengan más de 30 años");
            filteredEmployeers = employeers.Where(employeer => employeer.Age > 30).ToList();
            foreach (People filteredEmployer in filteredEmployeers)
            {
                Console.WriteLine(filteredEmployer);
            }
            #endregion
            #region Select
            Console.WriteLine("\nSELECT - Filtrar el nombre de los empleados");
            //List<People> filteredEmployeers = employeers.Select(employeer => employeer.Name).ToList();
            foreach (People filteredEmployer in filteredEmployeers)
            {
                Console.WriteLine(filteredEmployer);
            }
            #endregion
            #region OrderBy & OrderByDescending
            Console.WriteLine("\nORDERBY - Ordenar los empleados vs la lista original");
            List<People> filteredStudents = students.OrderBy(student =>student.Name).ToList();
            People originalStudent = null;
            int i = 0;
            foreach (var filteredStudent in filteredStudents)
            {
                originalStudent = students[i];
                Console.WriteLine($"{filteredStudent.Name} - {originalStudent.Name}");
                i++;
            }

            Console.Write("\n OrderByDescending - Ordenar por edad y vamos a diferenciarlo con la lista original");
            filteredStudents = students.OrderByDescending(student => student.Age).ToList();
            originalStudent = null;
            i = 0;
            foreach (var filteredStudent in filteredStudents)
            {
                originalStudent = students[i];
                Console.WriteLine($"{filteredStudent.Name} - {originalStudent.Name}");
                i++;
            }
            #endregion
            #region GroupBy
            Console.WriteLine("\nGROUPBY - Agrupamiento por género");
            var groupedByGender = students.GroupBy(student => student.Gender);
            foreach(var group in groupedByGender)
            {
                Console.WriteLine($"Género(grupo): {group.Key}");
                foreach(var person in group)
                {
                    Console.WriteLine($"Nombre: {person.Name}");
                }
            }

            Console.WriteLine("\nGROUPBY - Agrupamiento por Edad");
            var groupedByAge = students.GroupBy(student => student.Age);
            //int cantidad = 0;
            foreach (var group in groupedByAge)
            {
                Console.WriteLine($"Edad(grupo): {group.Key}, Cantidad: {group.Count()}");
                foreach (var person in group)
                {
                    Console.WriteLine($"Nombre: {person.Name}");
                    //cantidad++;
                }
                //Console.WriteLine($"Total: {cantidad}");
                //cantidad = 0;
            }

            #region ANY
            Console.WriteLine("\nANY - Verifica si hay valores o no dentro de la lista");
            Console.WriteLine($"Existen valores en 'employeers': {employeers.Any()}");
            Console.WriteLine($"Existen empleados mayores de 30 en 'employeers': {employeers.Any(i => i.Age > 0)}");
            #endregion
            #endregion
            Console.ReadKey();

        }
    }

    #region People
    public class People
    {
        #region Classes
        public string Name { get; set; }
        public int Age { get; set; }
        public eGender Gender { get; set; }
        #endregion

        #region Constructors

        #region Methods
        public override string ToString()
        {
            return $"Nombre: {Name}, Edad: {Age}, Genero: {this.GetStringGender(Gender)}";
        }

        private string GetStringGender(eGender gender)
        {
            string genderString;
            switch (gender)
            {
                case eGender.Female:    genderString = "Femenino";   break;
                case eGender.Male:      genderString = "Masculino";  break;
                case eGender.Undefined: genderString = "Indefinido"; break;
                default:                genderString = "No definido";break;
            }
            return genderString;
        }
        #endregion
        public People() { }

        public People(string name, int age, eGender gender)
        {
            Name = name;
            Age = age;
            Gender = gender;
        }
        #endregion
    }
    #endregion
    #region Enums
    public enum eGender
    {
        Undefined = 0,
        Female = 1,
        Male = 2,
    }
    #endregion
}
