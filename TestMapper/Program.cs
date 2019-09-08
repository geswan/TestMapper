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
            Dto dto = new Dto();
            Mapper<Student, Dto> mapper = new Mapper<Student, Dto>();

            Console.WriteLine($"Student before mapping: {student}");
            Console.WriteLine($"Dto before mapping: {dto}\r\n");
            mapper.Map(student, dto);
            Console.WriteLine($"Student after mapping: {student}");
            Console.WriteLine($"Dto after mapping: {dto}\r\n");
            DatabaseRecord record = new DatabaseRecord { Id = 2 };
            Console.WriteLine($"DatabaseRecord before mapping: {record}");
            Mapper<Dto, DatabaseRecord> dbMapper = new Mapper<Dto, DatabaseRecord>();
            dbMapper.Map(dto, record);
            Console.WriteLine($"DatabaseRecord after mapping: {record}\r\n");
            DatabaseRecord newRecord = dbMapper.Map(dto);
            Console.WriteLine($"DatabaseRecord after mapping to new instance: {newRecord}");
            Console.ReadLine();
        }
    }
}
