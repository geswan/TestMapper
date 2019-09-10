using Mapper;
using System;
using System.Globalization;

namespace TestMapper
{

    class Program
    {
        public static void Main()
        {

            Student student = new Student
            {
                ForeName = "Anne",
                LastName = "Other",
                Id = 1,
                Dob = DateTime.Parse("05/10/1990", CultureInfo.CurrentCulture)
            };
            DemoMapStudentToDto(student);
            DemoMapStudentToDtoWithPairing(student);
            DemoMapStudentToNewInstanceOfDtoWithPairing(student);

            Console.ReadLine();
        }
        private static void DemoMapStudentToDto(Student student)
        {
            Dto dto = new Dto();
            Console.WriteLine($"Student before mapping: {student}");
            Console.WriteLine($"Dto before mapping: {dto}\r\n");
            Mapper<Student, Dto> mapper = new Mapper<Student, Dto>();
            mapper.Map(student, dto);
            Console.WriteLine($"Student after mapping: {student}");
            Console.WriteLine($"Dto after mapping: {dto}\r\n");
        }

        private static void DemoMapStudentToDtoWithPairing(Student student)
        {
            Dto dto = new Dto();
            Mapper<Student, Dto> mapper = new Mapper<Student, Dto>();
            Console.WriteLine("Pairing Student.ForeName With Dto.FirstName and Student.Id with Dto.RecordNumber");
            mapper.Pair("ForeName", "FirstName");
            mapper.Pair("Id", "RecordNumber");
            mapper.Map(student, dto);
            Console.WriteLine($"Student after mapping: {student}");
            Console.WriteLine($"Dto after mapping: {dto}\r\n");
        }

        private static void DemoMapStudentToNewInstanceOfDtoWithPairing(Student student)
        {
            Console.WriteLine($"Demo Mapping Student to new instance of Dto with pairing.");
            Mapper<Student, Dto> mapper = new Mapper<Student, Dto>();
            Console.WriteLine("Pairing Student.ForeName With Dto.FirstName and Student.Id with Dto.RecordNumber");
            mapper.Pair("ForeName", "FirstName");
            mapper.Pair("Id", "RecordNumber");
            Dto dto = mapper.Map(student);
            Console.WriteLine($"Student after mapping: {student}");
            Console.WriteLine($"New instance of Dto: {dto}\r\n");
        }
    }
}
