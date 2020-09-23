using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentGradeOrder
{
    class Student : IComparable
    {
        // Implement the Student class here
        // ...
        private string firstName;
        private string lastName;
        private string degree;
        private int grade;

        public Student(string firstName, string lastName, string degree, int grade)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.degree = degree;
            this.grade = grade;
        }

        public override string ToString()
        {
            return $"{lastName}, {firstName} ({degree}) Grade: {grade}";
        }

        public int CompareTo(object obj)
        {
            Student student = obj as Student;

            if(firstName.CompareTo(student.firstName) != 0)
            {
                return firstName.CompareTo(student.firstName);
            }else if(grade.CompareTo(student.grade) != 0)
            {
                return grade.CompareTo(student.grade);
            }else if(degree.CompareTo(student.degree) != 0)
            {
                return degree.CompareTo(student.degree);
            }else if (lastName.CompareTo(student.lastName)!=0)
            {
                return lastName.CompareTo(student.lastName);
            }
            else
            {
                return 0;
            }
            
        }
    }
    class Program
    {
        static void main(string[] args)
        {
            Student[] students = new Student[]
            {
                new Student("Jane", "Smith", "Bachelor of Engineering", 6),
                new Student("John", "Smith", "Bachelor of Engineering", 7),
                new Student("John", "Smith", "Bachelor of IT", 7),
                new Student("John", "Smith", "Bachelor of IT", 6),
                new Student("Jane", "Smith", "Bachelor of IT", 6),
                new Student("John", "Bloggs", "Bachelor of IT", 6),
                new Student("John", "Bloggs", "Bachelor of Engineering", 6),
                new Student("John", "Bloggs", "Bachelor of IT", 7),
                new Student("John", "Smith", "Bachelor of Engineering", 6),
                new Student("Jane", "Smith", "Bachelor of Engineering", 7),
                new Student("Jane", "Bloggs", "Bachelor of IT", 6),
                new Student("Jane", "Bloggs", "Bachelor of Engineering", 6),
                new Student("Jane", "Bloggs", "Bachelor of Engineering", 7),
                new Student("Jane", "Smith", "Bachelor of IT", 7),
                new Student("John", "Bloggs", "Bachelor of Engineering", 7),
                new Student("Jane", "Bloggs", "Bachelor of IT", 7),
            };

            Array.Sort(students);
            foreach (Student student in students)
            {
                Console.WriteLine("{0}", student);
            }

            Console.WriteLine("\nPress enter to exit.");
            Console.ReadLine();
        }
    }
}