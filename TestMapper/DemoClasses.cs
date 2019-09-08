using System;

namespace TestMapper
{
    public class Student
    {
        public string ForeName { get; set; }
        public string LastName { get; set; }
        public DateTime Dob { get; set; }
        public int Id {get; set; }

        public override string ToString()
        {
            return "Student: " + ForeName + " " + LastName + " Dob " + Dob.ToShortDateString() + " Id " + Id ;
        }
    }
        public class Dto
        {
            public string ForeName { get; set; }
            public string LastName { get; set; }
            public DateTime Dob { get; set; }
        public override string ToString()
        {
            return "Dto: " + ForeName + " " + LastName + " Dob " + Dob.ToShortDateString();
        }
    }

        public class DatabaseRecord
        {
            public string ForeName { get; set; }
            public string LastName { get; set; }
            public DateTime Dob { get; set; }
            public int Id { get; set; }
            public string Address { get; set; }

        public override string ToString()
        {
            return "DatabaseRecord: " + ForeName + " " + LastName + " Dob "+Dob.ToShortDateString()+" Id  " + Id + " " + Address;
        }
    }

    }

